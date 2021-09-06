module Fable.Python.Tests.Builtins

open Util.Testing
open Fable.Python.Builtins

// type MyObject = {
//     Foo: int
//     Bar: string
// }

[<Fact>]
let ``test print works`` () =
    let result = builtins.print "Hello, world!"
    result |> equal ()
