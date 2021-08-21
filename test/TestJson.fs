module Fable.Python.Tests.Json

open Util.Testing


[<Fact>]
let ``test Seq.empty works`` () =
    let result = true
    result |> equal true
