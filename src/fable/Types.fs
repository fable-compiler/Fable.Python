/// Utilities for detecting and working with Fable types at runtime in Python.
/// These helpers are useful when you need to distinguish between native Python types
/// and Fable-compiled F# types (e.g., int, float, Int64, FSharpArray).
module Fable.Python.Fable.Types

open Fable.Core

/// Get the Python type name of an object
[<Emit("type($0).__name__")>]
let typeName (o: obj) : string = nativeOnly

/// Check if an object is an F# integral type.
/// The default F# int compiles to Python's native int; other widths use Fable runtime types.
let isIntegralType (o: obj) : bool =
    match typeName o with
    | "int"
    | "Int8"
    | "Int16"
    | "Int32"
    | "Int64"
    | "UInt8"
    | "UInt16"
    | "UInt32"
    | "UInt64" -> true
    | _ -> false

/// Check if an object is an F# numeric type.
/// The default F# int and float compile to Python's native int and float.
let isNumericType (o: obj) : bool =
    match typeName o with
    | "int"
    | "float"
    | "Int8"
    | "Int16"
    | "Int32"
    | "Int64"
    | "UInt8"
    | "UInt16"
    | "UInt32"
    | "UInt64"
    | "Float32"
    | "Float64" -> true
    | _ -> false

/// Check if an object is a Fable array type (FSharpArray, GenericArray, or typed arrays)
let isArrayType (o: obj) : bool =
    match typeName o with
    | "FSharpArray"
    | "GenericArray"
    | "Int8Array"
    | "Int16Array"
    | "Int32Array"
    | "Int64Array"
    | "UInt8Array"
    | "UInt16Array"
    | "UInt32Array"
    | "UInt64Array"
    | "Float32Array"
    | "Float64Array" -> true
    | _ -> false
