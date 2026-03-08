module Fable.Python.Tests.Traceback

open Fable.Python.Testing
open Fable.Python.Traceback

[<Fact>]
let ``test format_exc returns string`` () =
    // When no exception is active, format_exc returns "NoneType: None\n"
    let result = traceback.format_exc ()
    (result.Length > 0) |> equal true

[<Fact>]
let ``test format_stack returns list`` () =
    let result = traceback.format_stack ()
    (result.Count > 0) |> equal true
