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
