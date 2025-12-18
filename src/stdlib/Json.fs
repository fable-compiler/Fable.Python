/// Type bindings for Python json module: https://docs.python.org/3/library/json.html
module Fable.Python.Json

open Fable.Core
open Fable.Python.Builtins

// fsharplint:disable MemberNames


[<Erase>]
type IExports =
    /// Serialize obj to a JSON formatted string
    /// See https://docs.python.org/3/library/json.html#json.dumps
    abstract dumps: obj: obj -> string
    /// Serialize obj to a JSON formatted string with indentation
    /// See https://docs.python.org/3/library/json.html#json.dumps
    abstract dumps: obj: obj * indent: int -> string
    /// Serialize obj to a JSON formatted string with a custom default function
    /// See https://docs.python.org/3/library/json.html#json.dumps
    [<NamedParams(fromIndex = 1)>]
    abstract dumps: obj: obj * ``default``: (obj -> obj) -> string
    /// Serialize obj to a JSON formatted string with indentation and custom default
    /// See https://docs.python.org/3/library/json.html#json.dumps
    [<NamedParams(fromIndex = 1)>]
    abstract dumps: obj: obj * indent: int * ``default``: (obj -> obj) -> string
    /// Serialize obj to a JSON formatted string with separators, ensure_ascii, and custom default
    /// See https://docs.python.org/3/library/json.html#json.dumps
    [<NamedParams(fromIndex = 1)>]
    abstract dumps:
        obj: obj * separators: string array * ensure_ascii: bool * ``default``: (obj -> obj) -> string
    /// Serialize obj to a JSON formatted string with indent, separators, ensure_ascii, and custom default
    /// See https://docs.python.org/3/library/json.html#json.dumps
    [<NamedParams(fromIndex = 1)>]
    abstract dumps:
        obj: obj * indent: int * separators: string array * ensure_ascii: bool * ``default``: (obj -> obj) -> string
    /// Deserialize a JSON document from a string to a Python object
    /// See https://docs.python.org/3/library/json.html#json.loads
    abstract loads: s: string -> obj
    /// Serialize obj as a JSON formatted stream to a file-like object
    /// See https://docs.python.org/3/library/json.html#json.dump
    abstract dump: obj: obj * fp: TextIOWrapper -> unit
    /// Serialize obj as a JSON formatted stream with indentation
    /// See https://docs.python.org/3/library/json.html#json.dump
    abstract dump: obj: obj * fp: TextIOWrapper * indent: int -> unit
    /// Serialize obj as a JSON formatted stream with a custom default function
    /// See https://docs.python.org/3/library/json.html#json.dump
    [<NamedParams(fromIndex = 2)>]
    abstract dump: obj: obj * fp: TextIOWrapper * ``default``: (obj -> obj) -> unit
    /// Serialize obj as a JSON formatted stream with indentation and custom default
    /// See https://docs.python.org/3/library/json.html#json.dump
    [<NamedParams(fromIndex = 2)>]
    abstract dump: obj: obj * fp: TextIOWrapper * indent: int * ``default``: (obj -> obj) -> unit
    /// Deserialize a JSON document from a file-like object to a Python object
    /// See https://docs.python.org/3/library/json.html#json.load
    abstract load: fp: TextIOWrapper -> obj

/// JSON encoder and decoder
[<ImportAll("json")>]
let json: IExports = nativeOnly

// Helper functions for runtime type inspection
[<Emit("type($0).__name__")>]
let private typeName (o: obj) : string = nativeOnly

[<Emit("hasattr($0, $1)")>]
let private hasattr (o: obj) (name: string) : bool = nativeOnly

[<Emit("getattr($0, $1)")>]
let private getattr (o: obj) (name: string) : obj = nativeOnly

[<Emit("int($0)")>]
let private toInt (o: obj) : obj = nativeOnly

[<Emit("float($0)")>]
let private toFloat (o: obj) : obj = nativeOnly

[<Emit("{slot: getattr($0, slot) for slot in $0.__slots__}")>]
let private slotsToDict (o: obj) : obj = nativeOnly

[<Emit("[$1] + list($0.fields) if $0.fields else $1")>]
let private unionToList (o: obj) (caseName: string) : obj = nativeOnly

[<Emit("getattr(type($0), 'cases', lambda: [])()")>]
let private getCases (o: obj) : string array = nativeOnly

[<Emit("(_ for _ in ()).throw(TypeError(f'Object of type {type($0).__name__} is not JSON serializable'))")>]
let private raiseTypeError (o: obj) : obj = nativeOnly

/// Default function for JSON serialization of Fable types.
/// Handles Int8, Int16, Int32, Int64, UInt8, UInt16, UInt32, UInt64 → int
/// Handles Float32, Float64 → float
/// Handles Union types (tag, fields, cases) → [caseName, ...fields] or just caseName
/// Handles Record types (__slots__) → dict of slot names to values
let fableDefault (o: obj) : obj =
    let name = typeName o

    match name with
    | "Int8"
    | "Int16"
    | "Int32"
    | "Int64"
    | "UInt8"
    | "UInt16"
    | "UInt32"
    | "UInt64" -> toInt o
    | "Float32"
    | "Float64" -> toFloat o
    | _ ->
        if hasattr o "tag" && hasattr o "fields" then
            let cases = getCases o
            let tag: int = getattr o "tag" :?> int
            let caseName = if tag < cases.Length then cases.[tag] else "Case" + string tag
            unionToList o caseName
        elif hasattr o "__slots__" then
            slotsToDict o
        else
            raiseTypeError o

/// Fable-aware JSON serialization with proper overloads.
/// Automatically handles Fable types (Int8, Int16, Int32, Int64, UInt8, UInt16, UInt32, UInt64, Float32, Float64, unions, records).
[<Erase>]
type Json =
    /// Serialize obj to JSON, automatically handling Fable types
    static member inline dumps(obj: obj) : string =
        json.dumps (obj, ``default`` = fableDefault)

    /// Serialize obj to JSON with indentation, automatically handling Fable types
    static member inline dumps(obj: obj, indent: int) : string =
        json.dumps (obj, indent, ``default`` = fableDefault)

    /// Serialize obj to JSON with custom separators and ensure_ascii, automatically handling Fable types
    static member inline dumps(obj: obj, separators: string array, ensureAscii: bool) : string =
        json.dumps (obj, separators = separators, ensure_ascii = ensureAscii, ``default`` = fableDefault)

    /// Serialize obj to JSON with indentation, custom separators, and ensure_ascii, automatically handling Fable types
    static member inline dumps(obj: obj, indent: int, separators: string array, ensureAscii: bool) : string =
        json.dumps (obj, indent = indent, separators = separators, ensure_ascii = ensureAscii, ``default`` = fableDefault)

    /// Serialize obj as JSON stream to file, automatically handling Fable types
    static member inline dump(obj: obj, fp: TextIOWrapper) : unit =
        json.dump (obj, fp, ``default`` = fableDefault)

    /// Serialize obj as JSON stream to file with indentation, automatically handling Fable types
    static member inline dump(obj: obj, fp: TextIOWrapper, indent: int) : unit =
        json.dump (obj, fp, indent, ``default`` = fableDefault)

    /// Deserialize a JSON document from a string to a Python object
    static member inline loads(s: string) : obj =
        json.loads s

    /// Deserialize a JSON document from a file-like object to a Python object
    static member inline load(fp: TextIOWrapper) : obj =
        json.load fp
