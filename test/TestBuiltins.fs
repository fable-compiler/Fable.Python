module Fable.Python.Tests.Builtins

open Fable.Core.PyInterop
open Fable.Python.Testing
open Fable.Python.Builtins
open Fable.Python.Os

[<Fact>]
let ``test print works`` () =
    let result = builtins.print "Hello, world!"
    result |> equal ()

let ``test __name__ works`` () = __name__ |> equal "test_builtins"

[<Fact>]
let ``test write works`` () =
    let tempFile = os.path.join (os.path.expanduser "~", ".fable_test_temp.txt")
    let result = builtins.``open`` (tempFile, OpenTextMode.Write)
    result.write "ABC" |> equal 3
    result.Dispose()
    os.remove tempFile

[<Fact>]
let ``test max with two arguments works`` () =
    builtins.max (3, 5) |> equal 5
    builtins.max (10, 2) |> equal 10

[<Fact>]
let ``test max with three arguments works`` () =
    builtins.max (3, 5, 1) |> equal 5
    builtins.max (1, 2, 10) |> equal 10

[<Fact>]
let ``test max with iterable works`` () =
    builtins.max [ 1; 2; 3; 4; 5 ] |> equal 5
    builtins.max [ 10; -5; 3 ] |> equal 10

[<Fact>]
let ``test min with two arguments works`` () =
    builtins.min (3, 5) |> equal 3
    builtins.min (10, 2) |> equal 2

[<Fact>]
let ``test min with three arguments works`` () =
    builtins.min (3, 5, 1) |> equal 1
    builtins.min (1, 2, 10) |> equal 1

[<Fact>]
let ``test min with iterable works`` () =
    builtins.min [ 1; 2; 3; 4; 5 ] |> equal 1
    builtins.min [ 10; -5; 3 ] |> equal -5

[<Fact>]
let ``test sum works`` () =
    builtins.sum [ 1; 2; 3; 4; 5 ] |> equal 15
    builtins.sum [ 10; -5; 3 ] |> equal 8

[<Fact>]
let ``test all works`` () =
    builtins.all [ true; true; true ] |> equal true
    builtins.all [ true; false; true ] |> equal false
    builtins.all [] |> equal true

[<Fact>]
let ``test any works`` () =
    builtins.any [ false; false; true ] |> equal true
    builtins.any [ false; false; false ] |> equal false
    builtins.any [] |> equal false

[<Fact>]
let ``test bool works`` () =
    builtins.bool 1 |> equal true
    builtins.bool 0 |> equal false
    builtins.bool "" |> equal false
    builtins.bool "x" |> equal true

[<Fact>]
let ``test dict empty works`` () =
    let d = builtins.dict ()
    builtins.len d |> equal 0

[<Fact>]
let ``test dict from pairs works`` () =
    let d = builtins.dict [ "a", 1; "b", 2; "c", 3 ]
    builtins.len d |> equal 3
    d.["a"] |> equal 1
    d.["b"] |> equal 2
    d.["c"] |> equal 3

[<Fact>]
let ``test list works`` () =
    let xs = builtins.list (seq { 1..3 })
    builtins.len xs |> equal 3
    xs.[0] |> equal 1
    xs.[2] |> equal 3

[<Fact>]
let ``test pyInstanceof with type references`` () =
    // Use emitPyExpr to construct genuinely Python-native values, since F#'s
    // `int`/`float`/`bool` compile to Fable wrapper classes, not Python primitives.
    let pyIntVal: obj = emitPyExpr () "42"
    let pyFloatVal: obj = emitPyExpr () "3.14"
    let pyBoolVal: obj = emitPyExpr () "True"
    let pyStrVal: obj = emitPyExpr () "'hello'"

    pyInstanceof pyIntVal pyInt |> equal true
    pyInstanceof pyFloatVal pyFloat |> equal true
    pyInstanceof pyBoolVal pyBool |> equal true
    pyInstanceof pyStrVal pyStr |> equal true
    pyInstanceof (builtins.dict ()) pyDict |> equal true
    pyInstanceof (builtins.list (seq { 1..3 })) pyList |> equal true

    // Cross-checks
    pyInstanceof pyStrVal pyInt |> equal false
    pyInstanceof (builtins.dict ()) pyList |> equal false

[<Fact>]
let ``test pyNone is None`` () =
    // bool(None) is False
    builtins.bool pyNone |> equal false
    // None has type NoneType, so isinstance(None, type(None)) holds
    builtins.isinstance (pyNone, builtins.``type`` pyNone) |> equal true

[<Fact>]
let ``test abs with int works`` () =
    builtins.abs -5 |> equal 5
    builtins.abs 0 |> equal 0
    builtins.abs 42 |> equal 42

[<Fact>]
let ``test abs with float works`` () =
    builtins.abs -3.14 |> equal 3.14
    builtins.abs 0.0 |> equal 0.0
    builtins.abs 2.72 |> equal 2.72

[<Fact>]
let ``test chr and ord round-trip works`` () =
    builtins.chr 65 |> equal 'A'
    builtins.chr 97 |> equal 'a'
    builtins.ord 'A' |> equal 65
    builtins.ord 'a' |> equal 97

[<Fact>]
let ``test chr ord round-trip preserves value`` () =
    let code = 9731 // snowman ☃
    builtins.ord (builtins.chr code) |> equal code

[<Fact>]
let ``test len with list works`` () =
    builtins.len ([ 1; 2; 3 ] |> box) |> equal 3
    builtins.len ([] |> box) |> equal 0

[<Fact>]
let ``test len with string works`` () =
    builtins.len ("hello" |> box) |> equal 5
    builtins.len ("" |> box) |> equal 0

[<Fact>]
let ``test map with single iterable works`` () =
    builtins.map ((fun x -> x * 2), [ 1; 2; 3 ])
    |> Seq.toList
    |> equal [ 2; 4; 6 ]

[<Fact>]
let ``test map with two iterables works`` () =
    builtins.map ((fun (a, b) -> a + b), [ 1; 2; 3 ], [ 10; 20; 30 ])
    |> Seq.toList
    |> equal [ 11; 22; 33 ]

[<Fact>]
let ``test str conversion works`` () =
    builtins.str 42 |> equal "42"
    builtins.str true |> equal "True"

[<Fact>]
let ``test int conversion works`` () =
    builtins.int "42" |> equal 42
    builtins.int 3.9 |> equal 3
    // Python's int() truncates toward zero, not floor
    builtins.int -3.9 |> equal -3

[<Fact>]
let ``test float conversion works`` () =
    builtins.float "3.14" |> equal 3.14
    builtins.float 42 |> equal 42.0

[<Fact>]
let ``test isinstance works`` () =
    let pyIntVal: obj = emitPyExpr () "42"
    let pyStrVal: obj = emitPyExpr () "'hello'"
    builtins.isinstance (pyStrVal, pyStr) |> equal true
    builtins.isinstance (pyIntVal, pyInt) |> equal true

[<Fact>]
let ``test isinstance returns false for wrong type`` () =
    let pyIntVal: obj = emitPyExpr () "42"
    builtins.isinstance (pyIntVal, pyStr) |> equal false

[<Fact>]
let ``test type returns type object`` () =
    let pyStrVal: obj = emitPyExpr () "'hello'"
    let pyIntVal: obj = emitPyExpr () "42"
    let t = builtins.``type`` pyStrVal
    builtins.isinstance (pyStrVal, t) |> equal true
    builtins.isinstance (pyIntVal, t) |> equal false

[<Fact>]
let ``test bytes from byte array works`` () =
    let b = builtins.bytes [| 72uy; 101uy; 108uy; 108uy; 111uy |]
    builtins.len b |> equal 5
    b.[0] |> equal 72uy
    b.[4] |> equal 111uy

[<Fact>]
let ``test bytes from string with encoding works`` () =
    let b = builtins.bytes ("ABC", "utf-8")
    builtins.len b |> equal 3
    b.[0] |> equal 65uy
    b.[2] |> equal 67uy
