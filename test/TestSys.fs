module Fable.Python.Tests.Sys

open Fable.Python.Testing
open Fable.Python.Sys

[<Fact>]
let ``test sys.platform is non-empty string`` () = sys.platform.Length > 0 |> equal true

[<Fact>]
let ``test sys.version is non-empty string`` () = sys.version.Length > 0 |> equal true

[<Fact>]
let ``test sys.maxsize is positive`` () = sys.maxsize > 0n |> equal true

[<Fact>]
let ``test sys.maxunicode is 1114111`` () = sys.maxunicode |> equal 1114111

[<Fact>]
let ``test sys.path has at least one element`` () = sys.path.Count > 0 |> equal true

[<Fact>]
let ``test sys.argv has at least one element`` () = sys.argv.Count > 0 |> equal true

[<Fact>]
let ``test sys.byteorder is little or big`` () =
    let order = sys.byteorder
    (order = ByteOrder.Little || order = ByteOrder.Big) |> equal true
