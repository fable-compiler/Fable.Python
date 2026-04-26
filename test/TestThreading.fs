module Fable.Python.Tests.Threading

open Fable.Python.Testing
open Fable.Python.Threading
open Fable.Python.Builtins

[<Fact>]
let ``test get_ident returns nonzero`` () =
    let ident = threading.get_ident ()
    (ident <> 0) |> equal true

[<Fact>]
let ``test active_count is at least 1`` () =
    (threading.active_count () >= 1) |> equal true

[<Fact>]
let ``test local creates thread-local storage`` () =
    let local = threading.local ()
    setattr local "value" 42
    getattr local "value" 0 |> equal 42

// ============================================================================
// Lock tests
// ============================================================================

[<Fact>]
let ``test Lock acquire and release work`` () =
    let lock = Lock()
    let acquired = lock.acquire ()
    acquired |> equal true
    lock.release ()

[<Fact>]
let ``test Lock locked reflects state`` () =
    let lock = Lock()
    lock.locked () |> equal false
    lock.acquire () |> ignore
    lock.locked () |> equal true
    lock.release ()
    lock.locked () |> equal false

[<Fact>]
let ``test Lock acquire non-blocking fails when locked`` () =
    let lock = Lock()
    lock.acquire () |> ignore
    // Non-blocking acquire should fail since lock is already held
    let second = lock.acquire (blocking = false)
    second |> equal false
    lock.release ()

// ============================================================================
// RLock tests
// ============================================================================

[<Fact>]
let ``test RLock acquire and release work`` () =
    let rlock = RLock()
    let acquired = rlock.acquire ()
    acquired |> equal true
    rlock.release ()

[<Fact>]
let ``test RLock is reentrant`` () =
    let rlock = RLock()
    // Same thread can acquire multiple times
    rlock.acquire () |> ignore
    let second = rlock.acquire ()
    second |> equal true
    rlock.release ()
    rlock.release ()

// ============================================================================
// Event tests
// ============================================================================

[<Fact>]
let ``test Event is_set starts false`` () =
    let ev = Event()
    ev.is_set () |> equal false

[<Fact>]
let ``test Event set and clear work`` () =
    let ev = Event()
    ev.set ()
    ev.is_set () |> equal true
    ev.clear ()
    ev.is_set () |> equal false

[<Fact>]
let ``test Event wait returns true when already set`` () =
    let ev = Event()
    ev.set ()
    let result = ev.wait ()
    result |> equal true

[<Fact>]
let ``test Event wait with timeout returns false when not set`` () =
    let ev = Event()
    // Timeout of 0 seconds — event not set, should return false immediately
    let result = ev.wait (timeout = 0.0)
    result |> equal false

// ============================================================================
// Thread tests
// ============================================================================

[<Fact>]
let ``test Thread runs target function`` () =
    let results = ResizeArray<int>()
    let t = Thread(target = fun () -> results.Add 42)
    t.start ()
    t.join ()
    results.Count |> equal 1
    results.[0] |> equal 42

[<Fact>]
let ``test Thread is_alive reflects state`` () =
    let ev = Event()
    let t = Thread(target = fun () -> ev.wait () |> ignore)
    t.is_alive () |> equal false
    t.start ()
    t.is_alive () |> equal true
    ev.set ()
    t.join ()
    t.is_alive () |> equal false

[<Fact>]
let ``test Thread name can be set`` () =
    let t = Thread(target = (fun () -> ()), name = "worker")
    t.name |> equal "worker"

[<Fact>]
let ``test Thread daemon defaults to false`` () =
    let t = Thread(target = fun () -> ())
    t.daemon |> equal false

[<Fact>]
let ``test main_thread returns a Thread`` () =
    let mt = threading.main_thread ()
    mt.is_alive () |> equal true

[<Fact>]
let ``test current_thread returns a Thread`` () =
    let ct = threading.current_thread ()
    ct.is_alive () |> equal true

[<Fact>]
let ``test enumerate returns list with at least one thread`` () =
    let threads = threading.enumerate ()
    (threads.Count >= 1) |> equal true
