module Fable.Python.Tests.AsyncIO

open Fable.Python.Testing
open Fable.Python.AsyncIO

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
