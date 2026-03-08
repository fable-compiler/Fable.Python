module Fable.Python.Tests.BuiltinsAttr

open Fable.Python.Testing
open Fable.Python.Builtins
open Fable.Python.Threading

[<Fact>]
let ``test hasattr works`` () =
    let local = threading.local ()
    setattr local "name" "test"
    builtins.hasattr (local, "name") |> equal true

[<Fact>]
let ``test getattr with default works`` () =
    let local = threading.local ()
    builtins.getattr (local, "missing", "default") |> equal "default"

[<Fact>]
let ``test setattr and getattr work`` () =
    let local = threading.local ()
    builtins.setattr (local, "x", 42)
    builtins.getattr (local, "x", 0) |> equal 42

[<Fact>]
let ``test hasattr returns false for missing attribute`` () =
    let local = threading.local ()
    builtins.hasattr (local, "nonexistent") |> equal false
