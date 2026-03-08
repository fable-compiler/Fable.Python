module Fable.Python.Tests.Queue

open Fable.Python.Testing
open Fable.Python.Queue

[<Fact>]
let ``test Queue put and get work`` () =
    let q = Queue<int>()
    q.put 42
    q.get () |> equal 42

[<Fact>]
let ``test Queue empty works`` () =
    let q = Queue<int>()
    q.empty () |> equal true
    q.put 1
    q.empty () |> equal false

[<Fact>]
let ``test Queue qsize works`` () =
    let q = Queue<int>()
    q.qsize () |> equal 0
    q.put 1
    q.put 2
    q.qsize () |> equal 2

[<Fact>]
let ``test Queue get_nowait works`` () =
    let q = Queue<string>()
    q.put "hello"
    q.get_nowait () |> equal "hello"

[<Fact>]
let ``test Queue get_nowait raises Empty`` () =
    let q = Queue<int>()
    let mutable caught = false

    try
        q.get_nowait () |> ignore
    with :? Empty ->
        caught <- true

    caught |> equal true

[<Fact>]
let ``test SimpleQueue put and get work`` () =
    let q = SimpleQueue<int>()
    q.put 10
    q.put 20
    q.get () |> equal 10
    q.get () |> equal 20

[<Fact>]
let ``test SimpleQueue get_nowait works`` () =
    let q = SimpleQueue<int>()
    q.put 99
    q.get_nowait () |> equal 99
