module Fable.Python.Tests.Functools

open Fable.Python.Testing
open Fable.Python.Functools

[<Fact>]
let ``test reduce sum works`` () =
    functools.reduce ((fun a b -> a + b), [ 1; 2; 3; 4; 5 ])
    |> equal 15

[<Fact>]
let ``test reduce product works`` () =
    functools.reduce ((fun a b -> a * b), [ 1; 2; 3; 4; 5 ])
    |> equal 120

[<Fact>]
let ``test reduce with initializer works`` () =
    functools.reduce ((fun acc x -> acc + x), [ 1; 2; 3 ], 10)
    |> equal 16

[<Fact>]
let ``test reduce string fold with initializer works`` () =
    functools.reduce ((fun acc s -> acc + s), [ "b"; "c"; "d" ], "a")
    |> equal "abcd"

[<Fact>]
let ``test lruCache memoises results`` () =
    let callCount = ResizeArray<int>()
    let expensive (x: int) =
        callCount.Add x
        x * x
    let cached = functools.lruCache (128, expensive)
    cached 5 |> equal 25
    cached 5 |> equal 25
    cached 3 |> equal 9
    callCount.Count |> equal 2

[<Fact>]
let ``test cache memoises results`` () =
    let callCount = ResizeArray<int>()
    let expensive (x: int) =
        callCount.Add x
        x * 2
    let cached = functools.cache expensive
    cached 7 |> equal 14
    cached 7 |> equal 14
    cached 4 |> equal 8
    callCount.Count |> equal 2
