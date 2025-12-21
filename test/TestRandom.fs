module Fable.Python.Tests.Random

open Fable.Python.Testing
open Fable.Python.Random

[<Fact>]
let ``test seed with int works`` () =
    random.seed 42
    let r1 = random.random ()
    random.seed 42
    let r2 = random.random ()
    r1 |> equal r2

[<Fact>]
let ``test random returns value in range`` () =
    random.seed 42
    let r = random.random ()
    (r >= 0.0 && r < 1.0) |> equal true

[<Fact>]
let ``test uniform works`` () =
    random.seed 42
    let r = random.uniform (10.0, 20.0)
    (r >= 10.0 && r <= 20.0) |> equal true

[<Fact>]
let ``test randint works`` () =
    random.seed 42
    let r = random.randint (1, 10)
    (r >= 1 && r <= 10) |> equal true

[<Fact>]
let ``test randrange with stop works`` () =
    random.seed 42
    let r = random.randrange 10
    (r >= 0 && r < 10) |> equal true

[<Fact>]
let ``test randrange with start and stop works`` () =
    random.seed 42
    let r = random.randrange (5, 10)
    (r >= 5 && r < 10) |> equal true

[<Fact>]
let ``test randrange with step works`` () =
    random.seed 42
    let r = random.randrange (0, 10, 2)
    (r >= 0 && r < 10 && r % 2 = 0) |> equal true

[<Fact>]
let ``test choice with array works`` () =
    random.seed 42
    let arr = [| 1; 2; 3; 4; 5 |]
    let r = random.choice arr
    (Array.contains r arr) |> equal true

[<Fact>]
let ``test choice with list works`` () =
    random.seed 42
    let lst = [ "a"; "b"; "c" ]
    let r = random.choice lst
    (List.contains r lst) |> equal true

[<Fact>]
let ``test sample works`` () =
    random.seed 42
    let arr = [| 1; 2; 3; 4; 5 |]
    let r = random.sample (arr, 3)
    Seq.length r |> equal 3

[<Fact>]
let ``test choices works`` () =
    random.seed 42
    let arr = [| 1; 2; 3 |]
    let r = random.choices (arr, 5)
    Seq.length r |> equal 5

[<Fact>]
let ``test shuffle works`` () =
    random.seed 42
    let arr = ResizeArray [ 1; 2; 3; 4; 5 ]
    random.shuffle arr
    // After shuffle, the array should still contain all original elements
    (arr |> Seq.sort |> Seq.toList) |> equal [ 1; 2; 3; 4; 5 ]

[<Fact>]
let ``test getrandbits works`` () =
    random.seed 42
    let r = random.getrandbits 8
    (r >= 0 && r < 256) |> equal true

[<Fact>]
let ``test gauss works`` () =
    random.seed 42
    // Just verify it returns a float without error
    let r = random.gauss (0.0, 1.0)
    (r = r) |> equal true // NaN check

[<Fact>]
let ``test expovariate works`` () =
    random.seed 42
    let r = random.expovariate 1.0
    (r >= 0.0) |> equal true

[<Fact>]
let ``test betavariate works`` () =
    random.seed 42
    let r = random.betavariate (2.0, 5.0)
    (r >= 0.0 && r <= 1.0) |> equal true

[<Fact>]
let ``test triangular works`` () =
    random.seed 42
    let r = random.triangular (0.0, 10.0, 5.0)
    (r >= 0.0 && r <= 10.0) |> equal true

[<Fact>]
let ``test normalvariate works`` () =
    random.seed 42
    let r = random.normalvariate (0.0, 1.0)
    (r = r) |> equal true // NaN check
