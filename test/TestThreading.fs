module Fable.Python.Tests.Threading

open Fable.Python.Testing
open Fable.Python.Threading
open Fable.Python.Builtins

[<Fact>]
let ``test get_ident returns nonzero`` () =
    let ident = threading.get_ident ()
    (ident <> 0) |> equal true

[<Fact>]
let ``test active_count is at least 1`` () =
    (threading.active_count () >= 1) |> equal true

[<Fact>]
let ``test local creates thread-local storage`` () =
    let local = threading.local ()
    setattr local "value" 42
    getattr local "value" 0 |> equal 42
