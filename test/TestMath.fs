module Fable.Python.Tests.Math

open Fable.Python.Testing
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
    math.comb (5, 2) |> equal 10
    math.comb (10, 3) |> equal 120

[<Fact>]
let ``test copysign works`` () =
    math.copysign (1.0, -1.0) |> equal -1.0
    math.copysign (-1.0, 1.0) |> equal 1.0

[<Fact>]
let ``test fabs works`` () =
    math.fabs -5.0 |> equal 5.0
    math.fabs 5.0 |> equal 5.0

[<Fact>]
let ``test factorial works`` () =
    math.factorial 5 |> equal 120
    math.factorial 0 |> equal 1

[<Fact>]
let ``test fmod works`` () =
    math.fmod (10.0, 3.0) |> equal 1.0
    math.fmod (7.0, 2.0) |> equal 1.0

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
    math.pow (2.0, 3.0) |> equal 8.0
    math.pow (10.0, 2.0) |> equal 100.0

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
    math.atan2 (0.0, 1.0) |> equal 0.0

[<Fact>]
let ``test pi constant works`` () =
    math.pi |> fun x -> (x > 3.14159 && x < 3.14160) |> equal true

[<Fact>]
let ``test e constant works`` () =
    math.e |> fun x -> (x > 2.71828 && x < 2.71829) |> equal true

[<Fact>]
let ``test tau constant works`` () =
    math.tau |> fun x -> (x > 6.28318 && x < 6.28319) |> equal true

[<Fact>]
let ``test inf constant works`` () =
    math.isinf math.inf |> equal true

[<Fact>]
let ``test sqrt works`` () =
    math.sqrt 4.0 |> equal 2.0
    math.sqrt 9.0 |> equal 3.0

[<Fact>]
let ``test degrees works`` () =
    math.degrees math.pi |> fun x -> (x > 179.999 && x < 180.001) |> equal true

[<Fact>]
let ``test radians works`` () =
    math.radians 180.0 |> fun x -> (x > 3.14158 && x < 3.14160) |> equal true

[<Fact>]
let ``test trunc works`` () =
    math.trunc 2.7 |> equal 2
    math.trunc -2.7 |> equal -2

[<Fact>]
let ``test hypot works`` () =
    math.hypot (3.0, 4.0) |> equal 5.0

[<Fact>]
let ``test isqrt works`` () =
    math.isqrt 16 |> equal 4
    math.isqrt 17 |> equal 4

[<Fact>]
let ``test fsum works`` () =
    math.fsum [ 1.0; 2.0; 3.0 ] |> equal 6.0

[<Fact>]
let ``test nan constant works`` () =
    math.isnan math.nan |> equal true

[<Fact>]
let ``test prod works`` () =
    math.prod [ 1; 2; 3; 4 ] |> equal 24

[<Fact>]
let ``test perm works`` () =
    math.perm 5 |> equal 120
    math.perm (5, 2) |> equal 20

[<Fact>]
let ``test dist works`` () =
    math.dist ([| 0.0; 0.0 |], [| 3.0; 4.0 |]) |> equal 5.0

[<Fact>]
let ``test cosh works`` () =
    math.cosh 0.0 |> equal 1.0

[<Fact>]
let ``test sinh works`` () =
    math.sinh 0.0 |> equal 0.0

[<Fact>]
let ``test tanh works`` () =
    math.tanh 0.0 |> equal 0.0

[<Fact>]
let ``test acosh works`` () =
    math.acosh 1.0 |> equal 0.0

[<Fact>]
let ``test asinh works`` () =
    math.asinh 0.0 |> equal 0.0

[<Fact>]
let ``test atanh works`` () =
    math.atanh 0.0 |> equal 0.0

[<Fact>]
let ``test erf works`` () =
    math.erf 0.0 |> equal 0.0

[<Fact>]
let ``test erfc works`` () =
    math.erfc 0.0 |> equal 1.0

[<Fact>]
let ``test gamma works`` () =
    math.gamma 1.0 |> equal 1.0
    math.gamma 5.0 |> equal 24.0

[<Fact>]
let ``test lgamma works`` () =
    math.lgamma 1.0 |> equal 0.0

[<Fact>]
let ``test log with base works`` () =
    math.log (8.0, 2.0) |> equal 3.0
    math.log (100.0, 10.0) |> equal 2.0

[<Fact>]
let ``test ldexp works`` () =
    math.ldexp (1.0, 3n) |> equal 8.0
    math.ldexp (0.5, 2n) |> equal 2.0

[<Fact>]
let ``test frexp works`` () =
    let m, e = math.frexp 8.0
    m |> equal 0.5
    e |> equal 4

[<Fact>]
let ``test modf works`` () =
    let frac, intPart = math.modf 3.7
    (frac > 0.699 && frac < 0.701) |> equal true
    intPart |> equal 3.0

[<Fact>]
let ``test remainder works`` () =
    math.remainder (10.0, 3.0) |> equal 1.0
    math.remainder (5.0, 2.0) |> equal 1.0

[<Fact>]
let ``test isclose works`` () =
    math.isclose (1.0, 1.0) |> equal true
    math.isclose (1.0, 2.0) |> equal false

[<Fact>]
let ``test isclose with tolerances works`` () =
    math.isclose (1.0, 1.001, rel_tol=0.01) |> equal true
    math.isclose (1.0, 2.0, abs_tol=0.1) |> equal false

[<Fact>]
let ``test nextafter works`` () =
    let x = math.nextafter (1.0, 2.0)
    (x > 1.0) |> equal true

[<Fact>]
let ``test ulp works`` () =
    let x = math.ulp 1.0
    (x > 0.0) |> equal true

[<Fact>]
let ``test exp2 works`` () =
    math.exp2 3.0 |> equal 8.0
    math.exp2 0.0 |> equal 1.0

[<Fact>]
let ``test cbrt works`` () =
    math.isclose (math.cbrt 27.0, 3.0) |> equal true
    math.isclose (math.cbrt 8.0, 2.0) |> equal true
