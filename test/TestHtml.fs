module Fable.Python.Tests.Html

open Fable.Python.Testing
open Fable.Python.Html

[<Fact>]
let ``test html.escape escapes ampersand`` () =
    html.escape "a & b" |> equal "a &amp; b"

[<Fact>]
let ``test html.escape escapes less-than`` () =
    html.escape "<tag>" |> equal "&lt;tag&gt;"

[<Fact>]
let ``test html.escape escapes double quotes by default`` () =
    html.escape "\"hello\"" |> equal "&quot;hello&quot;"

[<Fact>]
let ``test html.escape with quote=false leaves quotes unescaped`` () =
    html.escape ("\"hello\"", false) |> equal "\"hello\""

[<Fact>]
let ``test html.escape with quote=true escapes quotes`` () =
    html.escape ("\"hello\"", true) |> equal "&quot;hello&quot;"

[<Fact>]
let ``test html.unescape reverses escape`` () =
    html.unescape "&lt;tag&gt;" |> equal "<tag>"

[<Fact>]
let ``test html.unescape reverses ampersand`` () =
    html.unescape "a &amp; b" |> equal "a & b"

[<Fact>]
let ``test html.unescape reverses quot`` () =
    html.unescape "&quot;hello&quot;" |> equal "\"hello\""

[<Fact>]
let ``test html roundtrip`` () =
    let original = "<div class=\"main\">Hello & World</div>"
    html.unescape (html.escape original) |> equal original
