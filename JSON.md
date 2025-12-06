# JSON Serialization with Fable.Python

This document explains how to properly serialize F# types to JSON when using Fable.Python.

## The Problem

When Fable compiles F# to Python, certain types are not native Python types:

|     F# Type      |     Fable Python Type      | Native Python? |
| ---------------- | -------------------------- | -------------- |
| `int`            | `Int32`                    | No             |
| `int64`          | `Int64`                    | No             |
| `float32`        | `Float32`                  | No             |
| F# record        | Class with `__slots__`     | No             |
| F# union         | Class with `tag`, `fields` | No             |
| F# array         | `FSharpArray`              | No             |
| `ResizeArray<T>` | `list`                     | Yes            |
| `nativeint`      | `int`                      | Yes            |
| `string`         | `str`                      | Yes            |

Python's standard `json.dumps()` and web framework serializers (Flask's `jsonify`, FastAPI's `jsonable_encoder`) don't know how to serialize these Fable-specific types.

### Why Can't Fable's Int32 Just Inherit from Python's int?

Fable.Python uses [PyO3](https://pyo3.rs/) for its runtime. Due to [PyO3 limitations](https://github.com/PyO3/pyo3/issues/991), it's not possible to create a Rust type that subclasses Python's immutable `int` type. This means `Int32` is a separate type that needs special handling during serialization.

## The Solution: fableDefault

The `Fable.Python.Json` module provides a `fableDefault` function that handles Fable types:

```fsharp
open Fable.Python.Json

// Use the convenience function (recommended)
let jsonStr = dumps myObject

// Or use json.dumps with fableDefault explicitly
let jsonStr = json.dumps(myObject, ``default`` = fableDefault)

// With indentation
let prettyJson = dumpsIndented myObject 2
```

### What fableDefault Handles

|                 Type                  |                Serialization                |
| ------------------------------------- | ------------------------------------------- |
| `Int8`, `Int16`, `Int32`, `Int64`     | → Python `int`                              |
| `UInt8`, `UInt16`, `UInt32`, `UInt64` | → Python `int`                              |
| `Float32`, `Float64`                  | → Python `float`                            |
| F# Records (with `__slots__`)         | → Python `dict`                             |
| F# Unions (with `tag`, `fields`)      | → `["CaseName", ...fields]` or `"CaseName"` |

## Usage Examples

### Basic Serialization

```fsharp
open Fable.Python.Json

// Anonymous record with F# int (compiles to Int32)
let data = {| id = 42; name = "Alice" |}
let json = dumps data
// Output: {"id": 42, "name": "Alice"}

// F# record
type User = { Id: int; Name: string }
let user = { Id = 1; Name = "Bob" }
let json = dumps user
// Output: {"Id": 1, "Name": "Bob"}

// F# discriminated union
type Status = Active | Inactive | Pending of string
let status = Pending "review"
let json = dumps status
// Output: ["Pending", "review"]
```

### With Web Frameworks

#### Flask

Flask's `jsonify` does **not** handle Fable types. Use `dumps` from `Fable.Python.Json`:

```fsharp
open Fable.Python.Flask
open Fable.Python.Json

[<APIClass>]
type Routes() =
    [<Get("/users/<int:user_id>")>]
    static member get_user(user_id: int) : string =
        // Use dumps for Fable type support
        dumps {| id = user_id; name = "Alice" |}

    [<Get("/simple")>]
    static member simple() : obj =
        // jsonify works ONLY with native Python types
        jsonify {| message = "Hello"; count = 42n |}  // 'n' suffix = native int
```

#### FastAPI

FastAPI's `jsonable_encoder` does **not** handle Fable types in anonymous records. You have two options:

**Option 1: Use Pydantic models** (recommended for FastAPI)

```fsharp
open Fable.Python.FastAPI
open Fable.Python.Pydantic

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type UserResponse(Id: int, Name: string) =
    inherit BaseModel()
    member val Id: int = Id with get, set
    member val Name: string = Name with get, set

[<APIClass>]
type API() =
    [<Get("/users/{user_id}")>]
    static member get_user(user_id: int) : UserResponse =
        UserResponse(Id = user_id, Name = "Alice")  // Works! Pydantic handles Int32
```

**Option 2: Use nativeint for anonymous records**

```fsharp
[<Delete("/items/{item_id}")>]
static member delete_item(item_id: int) : obj =
    {| status = "deleted"; id = nativeint item_id |}  // Convert to native int
```

### Collections

Use `ResizeArray<T>` instead of F# arrays for web API responses:

```fsharp
// Good - ResizeArray compiles to Python list
let users = ResizeArray<User>()
users.Add(User(Id = 1, Name = "Alice"))
let json = dumps users

// Avoid - F# array compiles to FSharpArray
let users = [| User(Id = 1, Name = "Alice") |]  // May not serialize correctly
```

## Quick Reference

| Scenario | Solution |
|----------|----------|
| JSON API with Fable types | Use `Fable.Python.Json.dumps` |
| Flask endpoint | Use `dumps` instead of `jsonify` |
| FastAPI endpoint | Use Pydantic models or `nativeint` |
| Int literals in anonymous records | Use `42n` suffix for native int |
| Collections in API responses | Use `ResizeArray<T>` |
| F# array needed | Convert with `ResizeArray(myArray)` |

## API Reference

```fsharp
module Fable.Python.Json

/// Default serializer for Fable types
val fableDefault: obj -> obj

/// Serialize to JSON with Fable type support
val dumps: obj -> string

/// Serialize to JSON with indentation
val dumpsIndented: obj -> int -> string

/// Serialize to file with Fable type support
val dump: obj -> TextIOWrapper -> unit

/// Serialize to file with indentation
val dumpIndented: obj -> TextIOWrapper -> int -> unit

/// Raw Python json module (use with fableDefault for Fable types)
val json: IExports
```

## Further Reading

- [PyO3 Issue #991](https://github.com/PyO3/pyo3/issues/991) - Why Int32 can't subclass Python's int
- [Python json module](https://docs.python.org/3/library/json.html) - Standard library documentation
- [Pydantic](https://docs.pydantic.dev/) - Data validation for Python (works with Fable's Int32)
