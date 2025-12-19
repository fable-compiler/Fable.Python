module Fable.Python.Tests.Types

open Util.Testing
open Fable.Python.Fable.Types

// Test typeName function

[<Fact>]
let ``test typeName returns Int32 for int`` () =
    let value = 42
    typeName value |> equal "Int32"

[<Fact>]
let ``test typeName returns Int64 for int64`` () =
    let value = 42L
    typeName value |> equal "Int64"

[<Fact>]
let ``test typeName returns Float64 for float`` () =
    let value = 3.14
    typeName value |> equal "Float64"

[<Fact>]
let ``test typeName returns str for string`` () =
    let value = "hello"
    typeName value |> equal "str"

// Test isIntegralType function

[<Fact>]
let ``test isIntegralType returns true for int`` () =
    let value = 42
    isIntegralType value |> equal true

[<Fact>]
let ``test isIntegralType returns true for int64`` () =
    let value = 42L
    isIntegralType value |> equal true

[<Fact>]
let ``test isIntegralType returns true for int16`` () =
    let value = 42s
    isIntegralType value |> equal true

[<Fact>]
let ``test isIntegralType returns true for int8`` () =
    let value = 42y
    isIntegralType value |> equal true

[<Fact>]
let ``test isIntegralType returns true for uint32`` () =
    let value = 42u
    isIntegralType value |> equal true

[<Fact>]
let ``test isIntegralType returns true for uint64`` () =
    let value = 42UL
    isIntegralType value |> equal true

[<Fact>]
let ``test isIntegralType returns false for float`` () =
    let value = 3.14
    isIntegralType value |> equal false

[<Fact>]
let ``test isIntegralType returns false for string`` () =
    let value = "hello"
    isIntegralType value |> equal false

// Test isNumericType function

[<Fact>]
let ``test isNumericType returns true for int`` () =
    let value = 42
    isNumericType value |> equal true

[<Fact>]
let ``test isNumericType returns true for int64`` () =
    let value = 42L
    isNumericType value |> equal true

[<Fact>]
let ``test isNumericType returns true for float`` () =
    let value = 3.14
    isNumericType value |> equal true

[<Fact>]
let ``test isNumericType returns true for float32`` () =
    let value = 3.14f
    isNumericType value |> equal true

[<Fact>]
let ``test isNumericType returns false for string`` () =
    let value = "hello"
    isNumericType value |> equal false

[<Fact>]
let ``test isNumericType returns false for bool`` () =
    let value = true
    isNumericType value |> equal false

// Test isArrayType function

[<Fact>]
let ``test isArrayType returns true for int array`` () =
    let value = [| 1; 2; 3 |]
    isArrayType value |> equal true

[<Fact>]
let ``test isArrayType returns true for string array`` () =
    let value = [| "a"; "b"; "c" |]
    isArrayType value |> equal true

[<Fact>]
let ``test isArrayType returns true for float array`` () =
    let value = [| 1.0; 2.0; 3.0 |]
    isArrayType value |> equal true

[<Fact>]
let ``test isArrayType returns false for list`` () =
    let value = [ 1; 2; 3 ]
    isArrayType value |> equal false

[<Fact>]
let ``test isArrayType returns false for string`` () =
    let value = "hello"
    isArrayType value |> equal false

[<Fact>]
let ``test isArrayType returns false for int`` () =
    let value = 42
    isArrayType value |> equal false
