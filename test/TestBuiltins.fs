module Fable.Python.Tests.Builtins

open Util.Testing
open Fable.Python.Builtins

[<Fact>]
let ``test print works`` () =
    let result = builtins.print "Hello, world!"
    result |> equal ()

let ``test __name__ works`` () = __name__ |> equal "test_builtins"

[<Fact>]
let ``test write works`` () =
    let result = builtins.``open``("test.txt", OpenTextMode.Write)
    result.write "ABC" |> equal 3
    
let ``test read works`` () =
