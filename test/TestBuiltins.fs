module Fable.Python.Tests.Builtins

open Util.Testing
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
