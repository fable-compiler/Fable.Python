module Fable.Python.Tests.Json

open Util.Testing
open Fable.Python.Json

// Test types for union and record serialization
type SimpleUnion =
    | CaseA
    | CaseB of int
    | CaseC of string * int

type SimpleRecord = { Name: string; Age: int }

[<Fact>]
let ``test Json.dumps with anonymous record works`` () =
    let object = {| A = 10; B = 20 |}
    let result = Json.dumps object
    result |> equal """{"A": 10, "B": 20}"""

[<Fact>]
let ``test Json.loads works`` () =
    let input = """{"Foo": 10, "Bar": "test"}"""
    let object = {| Foo = 10; Bar = "test" |}
    let result: {| Foo: int; Bar: string |} = unbox (Json.loads input)
    result |> equal object

[<Fact>]
let ``test Json.dumps with int works`` () =
    let value: int = 42
    let result = Json.dumps value
    result |> equal "42"

[<Fact>]
let ``test Json.dumps with nativeint works`` () =
    let value: nativeint = 42n
    let result = Json.dumps value
    result |> equal "42"

[<Fact>]
let ``test Json.dumps with ResizeArray works`` () =
    let values = ResizeArray([ 1n; 2n; 3n ])
    let result = Json.dumps values
    result |> equal "[1, 2, 3]"

[<Fact>]
let ``test Json.dumps with ResizeArray of int works`` () =
    let values = ResizeArray([ 1; 2; 3 ])
    let result = Json.dumps values
    result |> equal "[1, 2, 3]"

[<Fact>]
let ``test Json.dumps with nested object works`` () =
    let obj =
        {| Name = "test"
           Values = ResizeArray([ 1n; 2n; 3n ]) |}

    let result = Json.dumps obj
    result |> equal """{"Name": "test", "Values": [1, 2, 3]}"""

[<Fact>]
let ``test Json.loads with array works`` () =
    let input = "[1, 2, 3]"
    let result: int array = unbox (Json.loads input)
    result.Length |> equal 3

[<Fact>]
let ``test Json.dumps with indent works`` () =
    let obj = {| A = 1n |}
    let result = Json.dumps(obj, indent = 2)
    result.Contains("\n") |> equal true

[<Fact>]
let ``test Json.dumps with record works`` () =
    let record = { Name = "Alice"; Age = 30 }
    let result = Json.dumps record
    // Note: Fable records use lowercase slot names
    result |> equal """{"name": "Alice", "age": 30}"""

[<Fact>]
let ``test Json.dumps with simple union case works`` () =
    let union = CaseA
    let result = Json.dumps union
    // Note: Union cases without fields serialize as just the case name string
    result |> equal "\"CaseA\""

[<Fact>]
let ``test Json.dumps with union case with single field works`` () =
    let union = CaseB 42
    let result = Json.dumps union
    result |> equal """["CaseB", 42]"""

[<Fact>]
let ``test Json.dumps with union case with multiple fields works`` () =
    let union = CaseC("hello", 123)
    let result = Json.dumps union
    result |> equal """["CaseC", "hello", 123]"""
