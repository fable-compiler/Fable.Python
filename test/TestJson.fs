module Fable.Python.Tests.Json

open Util.Testing
open Fable.Python.Json

[<Fact>]
let ``test works`` () =
    let result = true
    result |> equal true

[<Fact>]
let ``test json dumps works`` () =
    let object = {| A = 10; B = 20 |}
    let result = dumps object
    result |> equal """{"A": 10, "B": 20}"""

[<Fact>]
let ``test json loads works`` () =
    let input = """{"Foo": 10, "Bar": "test"}"""
    let object = {| Foo = 10; Bar = "test" |}
    let result: {| Foo: int; Bar: string |} = unbox (json.loads input)
    result |> equal object

[<Fact>]
let ``test json.dumps with nativeint works`` () =
    let value: nativeint = 42n
    let result = json.dumps value
    result |> equal "42"

[<Fact>]
let ``test json.dumps with ResizeArray of nativeint works`` () =
    let values = ResizeArray([ 1n; 2n; 3n ])
    let result = json.dumps values
    result |> equal "[1, 2, 3]"

[<Fact>]
let ``test json.dumps with nested object works`` () =
    let obj =
        {| Name = "test"
           Values = ResizeArray([ 1n; 2n; 3n ]) |}

    let result = dumps obj
    result |> equal """{"Name": "test", "Values": [1, 2, 3]}"""

[<Fact>]
let ``test json.loads with array works`` () =
    let input = "[1, 2, 3]"
    let result: int array = unbox (json.loads input)
    result.Length |> equal 3

[<Fact>]
let ``test json.dumps with indent works`` () =
    let obj = {| A = 1n |}
    let result = dumpsIndented obj 2
    result.Contains("\n") |> equal true
