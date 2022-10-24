module Fable.Python.Tests.Json

open Util.Testing
open Fable.Python.Json

[<Fact>]
let ``works`` () =
    let result = true
    result |> equal true

[<Fact>]
let ``json dumps works`` () =
    let object = {| A = 10; B = 20 |}
    let result = json.dumps object
    result |> equal """{"A": 10, "B": 20}"""

[<Fact>]
let ``json loads works`` () =
    let input = """{"Foo": 10, "Bar": "test"}"""
    let object = {| Foo = 10; Bar = "test" |}
    let result: {| Foo: int; Bar: string |} = unbox (json.loads input)
    result |> equal object
