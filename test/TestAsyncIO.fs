module Fable.Python.Tests.AsyncIO

open Fable.Core
open Fable.Python.Testing
open Fable.Python.AsyncIO

/// asyncio.Lock is a stdlib async context manager: `async with lock` acquires it
/// via __aenter__ and releases it via __aexit__.
[<Import("Lock", "asyncio")>]
type private Lock() =
    [<Emit("$0.locked()")>]
    member _.locked() : bool = nativeOnly

/// A minimal hand-written async context manager used to exercise the branches
/// that stdlib's asyncio.Lock never hits: __aexit__ returning a truthy value
/// (suppress the exception) and __aexit__ raising on the success path.
/// `exits` counts how many times __aexit__ was awaited.
[<AttachMembers>]
type private TrackingCm(suppress: bool, raiseOnSuccessExit: bool) =
    member val exits = 0 with get, set

    member this.``__aenter__``() : System.Threading.Tasks.Task<obj> = task { return box this }

    member this.``__aexit__``(excType: obj, exc: obj, tb: obj) : System.Threading.Tasks.Task<bool> =
        task {
            this.exits <- this.exits + 1

            if raiseOnSuccessExit && isNull (box exc) then
                failwith "exit boom"

            return suppress
        }

[<Fact>]
let ``test builder run zero works`` () =
    let tsk = task { () }
    let result = asyncio.run (tsk)
    result |> equal ()

[<Fact>]
let ``test builder run int works`` () =
    let tsk = task { return 42 }
    let result = asyncio.run (tsk)
    result |> equal 42

[<Fact>]
let ``test sleep works`` () =
    let tsk =
        task {
            do! asyncio.create_task (asyncio.sleep (0.1))
            return 42
        }

    let result = asyncio.run (tsk)
    result |> equal 42

[<Fact>]
let ``test sleep with value works`` () =
    let tsk = task { return! asyncio.create_task (asyncio.sleep (0.1, 42)) }
    let result = asyncio.run tsk
    result |> equal 42

[<Fact>]
let ``test multiple awaits work`` () =
    let tsk =
        task {
            let! a = asyncio.create_task (asyncio.sleep (0.01, 10))
            let! b = asyncio.create_task (asyncio.sleep (0.01, 20))
            let! c = asyncio.create_task (asyncio.sleep (0.01, 12))
            return a + b + c
        }

    let result = asyncio.run tsk
    result |> equal 42

[<Fact>]
let ``test nested tasks work`` () =
    let inner () =
        task {
            do! asyncio.create_task (asyncio.sleep 0.01)
            return 21
        }

    let outer =
        task {
            let! x = inner ()
            let! y = inner ()
            return x + y
        }

    let result = asyncio.run outer
    result |> equal 42

[<Fact>]
let ``test get_running_loop works`` () =
    let tsk =
        task {
            let loop = asyncio.get_running_loop ()
            return loop <> null
        }

    let result = asyncio.run tsk
    result |> equal true

[<Fact>]
let ``test task with string result works`` () =
    let tsk =
        task {
            let! result = asyncio.create_task (asyncio.sleep (0.01, "hello"))
            return result + " world"
        }

    let result = asyncio.run tsk
    result |> equal "hello world"

[<Fact>]
let ``test task with list result works`` () =
    let tsk =
        task {
            let! a = asyncio.create_task (asyncio.sleep (0.01, [ 1; 2 ]))
            let! b = asyncio.create_task (asyncio.sleep (0.01, [ 3; 4 ]))
            return a @ b
        }

    let result = asyncio.run tsk
    result |> equal [ 1; 2; 3; 4 ]

[<Fact>]
let ``test task with option result works`` () =
    let tsk =
        task {
            let! a = asyncio.create_task (asyncio.sleep (0.01, Some 42))
            return a
        }

    let result = asyncio.run tsk
    result |> equal (Some 42)

[<Fact>]
let ``test async context manager enters and exits`` () =
    let lock = Lock()
    let cm = unbox<IAsyncContextManager<obj>> lock

    let tsk =
        task {
            // Inside the body the lock is held (acquired by __aenter__)...
            let! insideLocked = AsyncContextManager.using cm (fun _ -> task { return lock.locked () })
            // ...and released again afterwards (by __aexit__).
            return insideLocked, lock.locked ()
        }

    let inside, after = asyncio.run tsk
    inside |> equal true
    after |> equal false

[<Fact>]
let ``test async context manager exits on error`` () =
    let lock = Lock()
    let cm = unbox<IAsyncContextManager<obj>> lock

    let tsk =
        task {
            try
                let! _ =
                    AsyncContextManager.using cm (fun _ ->
                        task {
                            do failwith "boom"
                            return 0
                        })

                return false
            with _ ->
                // __aexit__ must have released the lock even though the body threw.
                return not (lock.locked ())
        }

    asyncio.run tsk |> equal true

[<Fact>]
let ``test tryUsing suppresses error when exit returns true`` () =
    let manager = TrackingCm(suppress = true, raiseOnSuccessExit = false)
    let cm = unbox<IAsyncContextManager<obj>> manager

    let tsk =
        task {
            // Body raises, but __aexit__ returns true: tryUsing honors the
            // suppression and yields None instead of re-raising.
            let! result =
                AsyncContextManager.tryUsing cm (fun _ ->
                    task {
                        do failwith "boom"
                        return 0
                    })

            return result, manager.exits
        }

    // No exception escapes, result is None, and __aexit__ ran exactly once.
    asyncio.run tsk |> equal (None, 1)

[<Fact>]
let ``test using re-raises even when exit returns true`` () =
    let manager = TrackingCm(suppress = true, raiseOnSuccessExit = false)
    let cm = unbox<IAsyncContextManager<obj>> manager

    let tsk =
        task {
            try
                // `using` always re-raises, ignoring the truthy __aexit__ return.
                let! _ =
                    AsyncContextManager.using cm (fun _ ->
                        task {
                            do failwith "boom"
                            return 0
                        })

                return -1
            with _ ->
                return manager.exits
        }

    asyncio.run tsk |> equal 1

[<Fact>]
let ``test async context manager does not exit twice when success exit raises`` () =
    let manager = TrackingCm(suppress = false, raiseOnSuccessExit = true)
    let cm = unbox<IAsyncContextManager<obj>> manager

    let tsk =
        task {
            try
                // Body succeeds; the success-path __aexit__ raises. That error must
                // propagate without __aexit__ being invoked a second time.
                let! _ = AsyncContextManager.using cm (fun _ -> task { return 0 })
                return -1
            with _ ->
                return manager.exits
        }

    asyncio.run tsk |> equal 1
