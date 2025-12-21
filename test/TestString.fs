module Fable.Python.Tests.String

open Fable.Python.Testing
open Fable.Python.String

[<Fact>]
let ``test string format works`` () =
    let result = "The sum of 1 + 2 is {0}".format (1 + 2)
    result |> equal "The sum of 1 + 2 is 3"

[<Fact>]
let ``test string format 2 works`` () =
    let result = "The sum of {0} + 2 is {1}".format (1, 1 + 2)
    result |> equal "The sum of 1 + 2 is 3"

// String module constants

[<Fact>]
let ``test ascii_letters constant`` () =
    pyString.ascii_letters |> equal "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

[<Fact>]
let ``test ascii_lowercase constant`` () =
    pyString.ascii_lowercase |> equal "abcdefghijklmnopqrstuvwxyz"

[<Fact>]
let ``test ascii_uppercase constant`` () =
    pyString.ascii_uppercase |> equal "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

[<Fact>]
let ``test digits constant`` () =
    pyString.digits |> equal "0123456789"

[<Fact>]
let ``test hexdigits constant`` () =
    pyString.hexdigits |> equal "0123456789abcdefABCDEF"

[<Fact>]
let ``test octdigits constant`` () =
    pyString.octdigits |> equal "01234567"

[<Fact>]
let ``test punctuation constant`` () =
    // Just verify it contains some expected punctuation
    pyString.punctuation.Contains "!" |> equal true
    pyString.punctuation.Contains "." |> equal true
    pyString.punctuation.Contains "@" |> equal true

[<Fact>]
let ``test printable constant`` () =
    // Printable includes digits, letters, punctuation, whitespace
    pyString.printable.Contains "a" |> equal true
    pyString.printable.Contains "0" |> equal true
    pyString.printable.Contains " " |> equal true

[<Fact>]
let ``test whitespace constant`` () =
    // Whitespace includes space, tab, newline, etc.
    pyString.whitespace.Contains " " |> equal true
    pyString.whitespace.Contains "\t" |> equal true
    pyString.whitespace.Contains "\n" |> equal true

// capwords function

[<Fact>]
let ``test capwords works`` () =
    pyString.capwords "hello world" |> equal "Hello World"

[<Fact>]
let ``test capwords with multiple spaces`` () =
    pyString.capwords "hello   world" |> equal "Hello World"

[<Fact>]
let ``test capwords with custom separator`` () =
    pyString.capwords ("hello-world", "-") |> equal "Hello-World"

// Template class

[<Fact>]
let ``test Template substitute with dict works`` () =
    let t = Template "$who likes $what"
    let result = t.substitute {| who = "tim"; what = "kung pao" |}
    result |> equal "tim likes kung pao"

[<Fact>]
let ``test Template safe_substitute works`` () =
    let t = Template "$who likes $what"
    let result = t.safe_substitute {| who = "tim" |}
    result |> equal "tim likes $what"

[<Fact>]
let ``test Template template property`` () =
    let t = Template "$name"
    t.template |> equal "$name"

[<Fact>]
let ``test Template is_valid works`` () =
    let t = Template "$valid"
    t.is_valid () |> equal true

[<Fact>]
let ``test Template get_identifiers works`` () =
    let t = Template "$who likes $what"
    let ids = t.get_identifiers ()
    Seq.length ids |> equal 2
    Seq.contains "who" ids |> equal true
    Seq.contains "what" ids |> equal true
