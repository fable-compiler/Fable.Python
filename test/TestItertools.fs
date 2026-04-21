module Fable.Python.Tests.Itertools

open Fable.Python.Testing
open Fable.Python.Itertools

[<Fact>]
let ``test count from start works`` () =
    itertools.count 1 |> Seq.take 4 |> Seq.toList |> equal [ 1; 2; 3; 4 ]

[<Fact>]
let ``test count with step works`` () =
    itertools.count (0, 2) |> Seq.take 4 |> Seq.toList |> equal [ 0; 2; 4; 6 ]

[<Fact>]
let ``test cycle works`` () =
    itertools.cycle [ 1; 2; 3 ]
    |> Seq.take 7
    |> Seq.toList
    |> equal [ 1; 2; 3; 1; 2; 3; 1 ]

[<Fact>]
let ``test repeat with times works`` () =
    itertools.repeat ("x", 3) |> Seq.toList |> equal [ "x"; "x"; "x" ]

[<Fact>]
let ``test accumulate works`` () =
    itertools.accumulate [ 1; 2; 3; 4; 5 ]
    |> Seq.toList
    |> equal [ 1; 3; 6; 10; 15 ]

[<Fact>]
let ``test accumulate with func works`` () =
    itertools.accumulate ([ 1; 2; 3; 4 ], fun a b -> a * b)
    |> Seq.toList
    |> equal [ 1; 2; 6; 24 ]

[<Fact>]
let ``test chain two sequences works`` () =
    itertools.chain ([ 1; 2 ], [ 3; 4 ]) |> Seq.toList |> equal [ 1; 2; 3; 4 ]

[<Fact>]
let ``test chain three sequences works`` () =
    itertools.chain ([ 1 ], [ 2; 3 ], [ 4; 5 ])
    |> Seq.toList
    |> equal [ 1; 2; 3; 4; 5 ]

[<Fact>]
let ``test chainFromIterable works`` () =
    itertools.chainFromIterable [ [ 1; 2 ]; [ 3; 4 ]; [ 5 ] ]
    |> Seq.toList
    |> equal [ 1; 2; 3; 4; 5 ]

[<Fact>]
let ``test compress works`` () =
    itertools.compress ([ 1; 2; 3; 4; 5 ], [ true; false; true; false; true ])
    |> Seq.toList
    |> equal [ 1; 3; 5 ]

[<Fact>]
let ``test dropwhile works`` () =
    itertools.dropwhile ((fun x -> x < 3), [ 1; 2; 3; 4; 5 ])
    |> Seq.toList
    |> equal [ 3; 4; 5 ]

[<Fact>]
let ``test filterfalse works`` () =
    itertools.filterfalse ((fun x -> x % 2 = 0), [ 1; 2; 3; 4; 5 ])
    |> Seq.toList
    |> equal [ 1; 3; 5 ]

[<Fact>]
let ``test islice with stop works`` () =
    itertools.islice ([ 1; 2; 3; 4; 5 ], 3) |> Seq.toList |> equal [ 1; 2; 3 ]

[<Fact>]
let ``test islice with start and stop works`` () =
    itertools.islice ([ 1; 2; 3; 4; 5 ], 1, 4) |> Seq.toList |> equal [ 2; 3; 4 ]

[<Fact>]
let ``test pairwise works`` () =
    itertools.pairwise [ 1; 2; 3; 4 ]
    |> Seq.toList
    |> equal [ (1, 2); (2, 3); (3, 4) ]

[<Fact>]
let ``test takewhile works`` () =
    itertools.takewhile ((fun x -> x < 4), [ 1; 2; 3; 4; 5 ])
    |> Seq.toList
    |> equal [ 1; 2; 3 ]

[<Fact>]
let ``test combinations works`` () =
    itertools.combinations ([ 1; 2; 3 ], 2)
    |> Seq.map Seq.toList
    |> Seq.toList
    |> equal [ [ 1; 2 ]; [ 1; 3 ]; [ 2; 3 ] ]

[<Fact>]
let ``test permutations with r works`` () =
    itertools.permutations ([ 1; 2; 3 ], 2) |> Seq.length |> equal 6

[<Fact>]
let ``test product two sequences works`` () =
    itertools.product ([ 1; 2 ], [ "a"; "b" ])
    |> Seq.toList
    |> equal [ (1, "a"); (1, "b"); (2, "a"); (2, "b") ]
