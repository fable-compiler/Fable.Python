module Fable.Python.Tests.Sys

open Fable.Python.Testing
open Fable.Python.Sys

[<Fact>]
let ``test sys.platform is non-empty string`` () =
    sys.platform.Length > 0 |> equal true

[<Fact>]
let ``test sys.version is non-empty string`` () =
    sys.version.Length > 0 |> equal true

[<Fact>]
let ``test sys.maxsize is large positive int`` () =
    sys.maxsize > 0 |> equal true

[<Fact>]
let ``test sys.maxunicode is 1114111`` () =
    sys.maxunicode |> equal 1114111

[<Fact>]
let ``test sys.path is list`` () =
    sys.path |> ignore
    sys.path.Count >= 0 |> equal true

[<Fact>]
let ``test sys.argv is list`` () =
    sys.argv |> ignore
    sys.argv.Count >= 0 |> equal true

[<Fact>]
let ``test sys.byteorder is little or big`` () =
    let order = sys.byteorder
    (order = ByteOrder.Little || order = ByteOrder.Big) |> equal true
