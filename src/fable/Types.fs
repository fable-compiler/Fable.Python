/// Utilities for detecting and working with Fable types at runtime in Python.
/// These helpers are useful when you need to distinguish between native Python types
/// and Fable-compiled F# types (e.g., Int32, Float64, FSharpArray).
module Fable.Python.Fable.Types

open Fable.Core

/// Get the Python type name of an object
[<Emit("type($0).__name__")>]
let typeName (o: obj) : string = nativeOnly

/// Check if an object is a Fable integral type (Int8, Int16, Int32, Int64, UInt8, UInt16, UInt32, UInt64)
let isIntegralType (o: obj) : bool =
    match typeName o with
    | "Int8"
    | "Int16"
    | "Int32"
    | "Int64"
    | "UInt8"
    | "UInt16"
    | "UInt32"
    | "UInt64" -> true
    | _ -> false

/// Check if an object is a Fable numeric type (integral types + Float32, Float64)
let isNumericType (o: obj) : bool =
    match typeName o with
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
