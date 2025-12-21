/// Cross-platform test utilities for writing tests that run on both .NET (with XUnit) and Python (with pytest).
///
/// Example usage:
/// ```fsharp
/// open Fable.Python.Testing
///
/// [<Fact>]
/// let ``test addition works`` () =
///     let result = 2 + 2
///     result |> equal 4
///
/// [<Fact>]
/// let ``test throws on invalid input`` () =
///     throwsAnyError (fun () -> failwith "boom")
/// ```
module Fable.Python.Testing

open System

#if FABLE_COMPILER
open Fable.Core.Testing

/// Assert equality (expected first, then actual - F# style)
let equal expected actual : unit = Assert.AreEqual(actual, expected)

/// Assert inequality
let notEqual expected actual : unit = Assert.NotEqual(actual, expected)

/// Attribute to mark test functions (compiles to test_ prefix for pytest)
type FactAttribute() =
    inherit System.Attribute()

#else
open Xunit

/// Assert equality (expected first, then actual - F# style)
let equal<'T> (expected: 'T) (actual: 'T) : unit = Assert.Equal(expected, actual)

/// Assert inequality
let notEqual<'T> (expected: 'T) (actual: 'T) : unit = Assert.NotEqual(expected, actual)

/// FactAttribute is already provided by XUnit
type FactAttribute = Xunit.FactAttribute
#endif

// Exception testing helpers (work on both platforms)

let private run (f: unit -> 'a) =
    try
        f () |> Ok
    with e ->
        Error e.Message

/// Assert that a function throws an error with the exact message
let throwsError (expected: string) (f: unit -> 'a) : unit =
    match run f with
    | Error actual when actual = expected -> ()
    | Error actual -> equal expected actual
    | Ok _ -> equal expected "No error was thrown"

/// Assert that a function throws an error containing the expected substring
let throwsErrorContaining (expected: string) (f: unit -> 'a) : unit =
    match run f with
    | Error _ when String.IsNullOrEmpty expected -> ()
    | Error (actual: string) when actual.Contains expected -> ()
    | Error actual -> equal (sprintf "Error containing '%s'" expected) actual
    | Ok _ -> equal (sprintf "Error containing '%s'" expected) "No error was thrown"

/// Assert that a function throws any error
let throwsAnyError (f: unit -> 'a) : unit =
    match run f with
    | Error _ -> ()
    | Ok _ -> equal "An error" "No error was thrown"

/// Assert that a function does not throw
let doesntThrow (f: unit -> 'a) : unit =
    match run f with
    | Ok _ -> ()
    | Error msg -> equal "No error" (sprintf "Error: %s" msg)
