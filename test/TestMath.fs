module Fable.Python.Tests.Math

open Util.Testing
open Fable.Python.Math

[<Fact>]
let ``test ceil works`` () =
    math.ceil 2.3 |> equal 3
    math.ceil 2.9 |> equal 3
    math.ceil -1.5 |> equal -1

[<Fact>]
let ``test floor works`` () =
    math.floor 2.3 |> equal 2
    math.floor 2.9 |> equal 2
    math.floor -1.5 |> equal -2

[<Fact>]
let ``test comb works`` () =
    math.comb 5 2 |> equal 10
    math.comb 10 3 |> equal 120

[<Fact>]
let ``test copysign works`` () =
    math.copysign 1.0 -1 |> equal -1.0
    math.copysign -1.0 1 |> equal 1.0

[<Fact>]
let ``test fabs works`` () =
    math.fabs -5.0 |> equal 5.0
    math.fabs 5.0 |> equal 5.0

[<Fact>]
let ``test factorial works`` () =
    math.factorial 5.0 |> equal 120.0
    math.factorial 0.0 |> equal 1.0

[<Fact>]
let ``test fmod works`` () =
    math.fmod 10 3 |> equal 1
    math.fmod 7 2 |> equal 1

[<Fact>]
let ``test gcd works`` () =
    math.gcd (12, 8) |> equal 4
    math.gcd (15, 25) |> equal 5

[<Fact>]
let ``test lcm works`` () =
    math.lcm (4, 6) |> equal 12
    math.lcm (3, 5) |> equal 15

[<Fact>]
let ``test isfinite works`` () =
    math.isfinite 1.0 |> equal true
    math.isfinite infinity |> equal false
    math.isfinite nan |> equal false

[<Fact>]
let ``test isinf works`` () =
    math.isinf infinity |> equal true
    math.isinf (-infinity) |> equal true
    math.isinf 1.0 |> equal false

[<Fact>]
let ``test isnan works`` () =
    math.isnan nan |> equal true
    math.isnan 1.0 |> equal false

[<Fact>]
let ``test exp works`` () =
    math.exp 0.0 |> equal 1.0
    math.exp 1.0 |> fun x -> (x > 2.718 && x < 2.719) |> equal true

[<Fact>]
let ``test log works`` () =
    math.log 1.0 |> equal 0.0
    math.log (math.exp 1.0) |> fun x -> (x > 0.999 && x < 1.001) |> equal true

[<Fact>]
let ``test log2 works`` () =
    math.log2 8.0 |> equal 3.0
    math.log2 1.0 |> equal 0.0

[<Fact>]
let ``test log10 works`` () =
    math.log10 100.0 |> equal 2.0
    math.log10 1.0 |> equal 0.0

[<Fact>]
let ``test pow works`` () =
    math.pow 2.0 3.0 |> equal 8.0
    math.pow 10.0 2.0 |> equal 100.0

[<Fact>]
let ``test sin works`` () =
    math.sin 0.0 |> equal 0.0

[<Fact>]
let ``test cos works`` () =
    math.cos 0.0 |> equal 1.0

[<Fact>]
let ``test tan works`` () =
    math.tan 0.0 |> equal 0.0

[<Fact>]
let ``test asin works`` () =
    math.asin 0.0 |> equal 0.0

[<Fact>]
let ``test acos works`` () =
    math.acos 1.0 |> equal 0.0

[<Fact>]
let ``test atan works`` () =
    math.atan 0.0 |> equal 0.0

[<Fact>]
let ``test atan2 works`` () =
    math.atan2 0.0 1.0 |> equal 0.0
