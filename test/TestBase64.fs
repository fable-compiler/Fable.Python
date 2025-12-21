module Fable.Python.Tests.Base64

open Fable.Python.Testing
open Fable.Python.Base64
open Fable.Python.Builtins

[<Fact>]
let ``test b64encode works`` () =
    let input = builtins.bytes "Hello, World!"B
    let result = base64.b64encode input
    result |> equal (builtins.bytes "SGVsbG8sIFdvcmxkIQ=="B)

[<Fact>]
let ``test b64decode from bytes works`` () =
    let encoded = builtins.bytes "SGVsbG8sIFdvcmxkIQ=="B
    let result = base64.b64decode encoded
    result |> equal (builtins.bytes "Hello, World!"B)

[<Fact>]
let ``test b64decode from string works`` () =
    let encoded = "SGVsbG8sIFdvcmxkIQ=="
    let result = base64.b64decode encoded
    result |> equal (builtins.bytes "Hello, World!"B)

[<Fact>]
let ``test roundtrip b64encode and b64decode works`` () =
    let original = builtins.bytes "Test data 123!@#"B
    let encoded = base64.b64encode original
    let decoded = base64.b64decode encoded
    decoded |> equal original

[<Fact>]
let ``test standard_b64encode works`` () =
    let input = builtins.bytes "Hello"B
    let result = base64.standard_b64encode input
    result |> equal (builtins.bytes "SGVsbG8="B)

[<Fact>]
let ``test standard_b64decode from string works`` () =
    let encoded = "SGVsbG8="
    let result = base64.standard_b64decode encoded
    result |> equal (builtins.bytes "Hello"B)

[<Fact>]
let ``test standard_b64decode from bytes works`` () =
    let encoded = builtins.bytes "SGVsbG8="B
    let result = base64.standard_b64decode encoded
    result |> equal (builtins.bytes "Hello"B)

[<Fact>]
let ``test urlsafe_b64encode works`` () =
    // Using bytes that produce + and / in standard base64
    let input = builtins.bytes [| 0xfbuy; 0xffuy; 0xfeuy |]
    let result = base64.urlsafe_b64encode input
    // URL-safe uses - and _ instead of + and /
    result |> equal (builtins.bytes "-__-"B)

[<Fact>]
let ``test urlsafe_b64decode from string works`` () =
    let encoded = "-__-"
    let result = base64.urlsafe_b64decode encoded
    result |> equal (builtins.bytes [| 0xfbuy; 0xffuy; 0xfeuy |])

[<Fact>]
let ``test urlsafe_b64decode from bytes works`` () =
    let encoded = builtins.bytes "-__-"B
    let result = base64.urlsafe_b64decode encoded
    result |> equal (builtins.bytes [| 0xfbuy; 0xffuy; 0xfeuy |])

[<Fact>]
let ``test b32encode works`` () =
    let input = builtins.bytes "Hello"B
    let result = base64.b32encode input
    result |> equal (builtins.bytes "JBSWY3DP"B)

[<Fact>]
let ``test b16encode works`` () =
    let input = builtins.bytes "Hello"B
    let result = base64.b16encode input
    result |> equal (builtins.bytes "48656C6C6F"B)

[<Fact>]
let ``test empty input works`` () =
    let input = builtins.bytes [||]
    let encoded = base64.b64encode input
    let decoded = base64.b64decode encoded
    decoded |> equal (builtins.bytes [||])
