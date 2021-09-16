module Fable.Python.Tests.Builtins

open Util.Testing
open Fable.Python.Builtins

[<Fact>]
let ``test print works`` () =
    let result = builtins.print "Hello, world!"
    result |> equal ()

let ``test __name__ works`` () =
    __name__ |> equal "tests.test_builtins"
