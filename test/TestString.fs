module Fable.Python.Tests.String

open Util.Testing
open Fable.Python.String

[<Fact>]
let ``test string format works`` () =
    let result = "The sum of 1 + 2 is {0}".format (1 + 2)
    result |> equal "The sum of 1 + 2 is 3"

[<Fact>]
let ``test string format 2 works`` () =
    let result = "The sum of {0} + 2 is {1}".format (1, 1 + 2)
    result |> equal "The sum of 1 + 2 is 3"
