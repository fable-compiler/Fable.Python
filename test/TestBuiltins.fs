module Fable.Python.Tests.Builtins

open Util.Testing
open Fable.Python.Builtins

[<Fact>]
let ``test print works`` () =
    let result = builtins.print "Hello, world!"
    result |> equal ()

[<Fact>]
let ``test write works`` () =
    let result = builtins.``open``(StringPath "test.txt", OpenTextMode.OpenTextModeWriting.Read)
    result.write "ABC" |> equal ()

let ``test __name__ works`` () = __name__ |> equal "test_builtins"
