# Writing Fable.Python Bindings: A Comprehensive Guide

This guide documents best practices for writing F# type bindings for Python libraries in Fable.Python, based on the Glutinum project's insights and the existing patterns in this repository.

## Table of Contents

1. [Core Principles](#core-principles)
2. [Import Patterns](#import-patterns)
3. [Type System Mappings](#type-system-mappings)
4. [Fable.Core Attributes Reference](#fablecore-attributes-reference)
5. [Common Patterns](#common-patterns)
6. [Best Practices](#best-practices)
7. [Testing Your Bindings](#testing-your-bindings)
8. [Examples](#examples)

## Core Principles

When writing Fable.Python bindings, follow these guiding principles:

1. **Provide near-native F# experience** while staying close to the original Python API
2. **Minimize friction** by avoiding erased union types (U2, U3, etc.) - use function overloads instead
3. **Maintain documentation compatibility** - users should be able to reference Python documentation
4. **Type safety first** - leverage F#'s type system to catch errors at compile time
5. **Follow F# conventions** - use camelCase for F# identifiers (Fable auto-converts to snake_case)

## Import Patterns

### Standard Module Import Pattern

The recommended pattern for importing Python modules:

```fsharp
[<Erase>]
type IExports =
    abstract function_name: param: type -> returnType
    abstract another_function: param1: type1 * param2: type2 -> returnType

[<ImportAll("module_name")>]
let moduleName: IExports = nativeOnly
```

**What this generates:**

```python
import module_name
```

**Important:** `[<ImportAll("module")>]` generates `import module`, NOT `from module import *`

### Importing Specific Classes

For Python classes that need to be instantiated or inherited:

```fsharp
[<Import("ClassName", "module_name")>]
type ClassName =
    abstract member property: type
    abstract member method: param: type -> returnType
```

**What this generates:**

```python
from module_name import ClassName
```

### Global Variables

For Python global constants or variables:

```fsharp
[<Global>]
let __name__: string = nativeOnly
```

Or use `[<Emit>]` for special cases:

```fsharp
[<Emit("__name__")>]
let __name__: string = nativeOnly
```

## Type System Mappings

Understanding how .NET/F# types map to Python:

### Direct Mappings

| F# Type | Python Type | Notes |
|---------|-------------|-------|
| `string` | `str` | Direct mapping |
| `bool` | `bool` | Direct mapping |
| `int` | `int` | Custom PyO3 wrapper maintains F# semantics |
| `float` | `float` | Custom PyO3 wrapper maintains F# semantics |
| `char` | `str` | Single-character string |
| `unit` | `None` | Void/None value |
| `'T option` | `T \| None` | **Erased** - Some 5 becomes just 5 |
| `'T array` | `list[T]` | Mutable array |
| `'T list` | Various | Immutable linked list (custom) |
| `ResizeArray<'T>` | `list[T]` | Python's native list |
| `seq<'T>` / `IEnumerable<'T>` | iterable | Any iterable |

### Complex Type Mappings

| F# Type | Python Type | Notes |
|---------|-------------|-------|
| Records | `dataclass` | Compiled to Python dataclasses |
| Anonymous records | `dict` | Dictionary with string keys |
| Discriminated unions | Various | Depends on attributes (see StringEnum) |
| Interfaces | Protocols | Maps to Python protocols/ABCs |
| Tuples | `tuple` | Native Python tuples |

### Special Interfaces

| .NET Interface | Python Protocol | Generated Methods |
|----------------|-----------------|-------------------|
| `IDisposable` | Context manager | `__enter__`, `__exit__` |
| `IEnumerable<'T>` | Iterator | `__iter__` |
| `IEquatable<'T>` | Equality | `__eq__` |
| `IComparable<'T>` | Comparison | `__lt__`, `__eq__` |

## Fable.Core Attributes Reference

### Import Attributes

#### `[<ImportAll("module")>]`

Imports the entire module and binds it to an F# value.

```fsharp
[<ImportAll("json")>]
let json: IExports = nativeOnly
```

Generates: `import json`

#### `[<Import("selector", "module")>]`

Imports a specific member from a module.

```fsharp
[<Import("Flask", "flask")>]
let Flask: FlaskStatic = nativeOnly
```

Generates: `from flask import Flask`

#### `[<ImportMember("module")>]`

Imports a member whose name matches the F# value name.

```fsharp
[<ImportMember("datetime")>]
let datetime: DateTimeStatic = nativeOnly
```

Generates: `from datetime import datetime`

#### `[<Global>]`

References a global Python variable without import.

```fsharp
[<Global>]
let __name__: string = nativeOnly
```

### Type Attributes

#### `[<Erase>]`

Prevents code generation for the type - it's only used at compile time.

**Use for:**

- Module export interfaces (IExports pattern)
- DSL types that compile away
- Virtual types for API organization

```fsharp
[<Erase>]
type IExports =
    abstract dumps: obj: obj -> string
```

**Important:** The Glutinum blog post advises minimizing erased unions (U2, U3), but using `[<Erase>]` for module export interfaces is acceptable and idiomatic for Fable.Python.

#### `[<StringEnum>]`

Compiles discriminated unions to string literals.

```fsharp
[<StringEnum>]
[<RequireQualifiedAccess>]
type HttpMethod =
    | [<CompiledName("GET")>] Get
    | [<CompiledName("POST")>] Post
    | Put  // Compiles to "put" with default CaseRules
```

**Case Rules:**

- `CaseRules.None` - Use exact case
- `CaseRules.LowerFirst` - Default: lowerFirst
- `CaseRules.SnakeCase` - snake_case
- `CaseRules.KebabCase` - kebab-case

**Use for:**

- String-based enumerations
- API parameters that accept specific string values
- Mode flags and options

#### `[<AllowNullLiteral>]`

Allows the type to be null/None.

```fsharp
[<AllowNullLiteral>]
type OptionalObject =
    abstract property: string
```

#### `[<RequireQualifiedAccess>]`

Requires qualified access to union cases or module members.

```fsharp
[<StringEnum>]
[<RequireQualifiedAccess>]
type FileMode =
    | Read
    | Write
    | Append

// Usage: FileMode.Read instead of just Read
```

**Best practice:** Always use with `[<StringEnum>]` to avoid polluting the namespace.

### Code Generation Attributes

#### `[<Emit("expression")>]`

Directly emits Python code. Placeholders `$0`, `$1`, `$2` represent arguments.

```fsharp
// For special syntax cases
[<Emit("$0.get_running_loop()")>]
abstract get_running_loop: unit -> AbstractEventLoop

// For custom operators
[<Emit("$0 if $1 else $2")>]
let inline ternary condition whenTrue whenFalse = nativeOnly
```

**Use sparingly for:**

- Python-specific syntax not expressible in F#
- Special operators or constructs
- Named arguments with special positioning

#### `[<NamedParams(fromIndex = n)>]`

Converts parameters starting from index `n` to Python keyword arguments.

```fsharp
type IExports =
    [<NamedParams(fromIndex = 1)>]
    abstract open:
        file: string *
        ?mode: string *
        ?encoding: string ->
            TextIOWrapper
```

Generates: `open(file, mode=..., encoding=...)`

**Use for:**

- Python functions with many optional parameters
- Functions where parameter order is important but some are optional

### Python-Specific Attributes (Fable 5.0.0+)

#### `[<Py.Decorator("decorator.name")>]`

Applies Python decorators to classes or functions.

```fsharp
[<Py.Decorator("dataclass")>]
type MyClass = ...
```

Generates:

```python
@dataclass
class MyClass:
    ...
```

#### `[<Py.ClassAttributes(style)>]`

Controls how class members are generated.

### Member Attributes

#### `[<AttachMembers>]`

Attaches all members directly to the class prototype. Disables overload support.

#### `[<Mangle>]`

Forces name mangling on interfaces for overload safety.

## Common Patterns

### Pattern 1: Simple Module with Functions

For modules that export only functions (e.g., `json`, `time`, `os`):

```fsharp
module Fable.Python.ModuleName

open Fable.Core

[<Erase>]
type IExports =
    /// Function documentation from Python docs
    abstract function_name: param: type -> returnType

    /// Function with multiple parameters
    abstract another_function: param1: type1 * param2: type2 -> returnType

/// Module description from Python docs
[<ImportAll("module_name")>]
let moduleName: IExports = nativeOnly
```

### Pattern 2: Module with Classes

For modules that export classes (e.g., `ast`, `datetime`):

```fsharp
module Fable.Python.ModuleName

open Fable.Core

// Import the class
[<Import("ClassName", "module_name")>]
type ClassName =
    abstract property: type
    abstract method: param: type -> returnType

// Import the module for other functions
[<Erase>]
type IExports =
    abstract module_function: param: type -> returnType

[<ImportAll("module_name")>]
let moduleName: IExports = nativeOnly
```

### Pattern 3: Function Overloads

**DO THIS** (multiple overloads):

```fsharp
[<Erase>]
type IExports =
    /// Parse a string
    abstract parse: source: string -> AST
    /// Parse a string with filename
    abstract parse: source: string * filename: string -> AST
    /// Parse a string with filename and mode
    abstract parse: source: string * filename: string * mode: Mode -> AST
```

**NOT THIS** (erased unions):

```fsharp
// ❌ Avoid this pattern - creates friction
abstract parse: source: string * options: U2<string, string * Mode> -> AST
```

### Pattern 4: String Enumerations

For Python parameters that accept specific string values:

```fsharp
[<StringEnum>]
[<RequireQualifiedAccess>]
type FileMode =
    | [<CompiledName("r")>] Read
    | [<CompiledName("w")>] Write
    | [<CompiledName("a")>] Append
    | [<CompiledName("r+")>] ReadUpdate

[<Erase>]
type IExports =
    abstract open: path: string * mode: FileMode -> File
```

Usage in F#:

```fsharp
file.open("data.txt", FileMode.Read)
```

Generates in Python:

```python
file.open("data.txt", "r")
```

### Pattern 5: Optional Parameters

F# optional parameters work naturally:

```fsharp
[<Erase>]
type IExports =
    abstract open: path: string * ?mode: string * ?encoding: string -> File
```

### Pattern 6: Convenience Wrappers

Provide F#-friendly wrappers for common operations:

```fsharp
[<ImportAll("builtins")>]
let builtins: IExports = nativeOnly

// Convenience wrapper for common use
let print obj = builtins.print obj
```

### Pattern 7: Type Aliases

For complex or commonly-used types:

```fsharp
type _identifier = string
type _Opener = Tuple<string, int> -> int

[<Erase>]
type IExports =
    abstract get_identifier: unit -> _identifier
    abstract open: path: string * opener: _Opener -> File
```

## Best Practices

### 1. Documentation

Always include XML documentation comments from the Python documentation:

```fsharp
[<Erase>]
type IExports =
    /// Return the absolute value of the argument.
    abstract abs: int -> int
```

### 2. Namespace Organization

Follow the established pattern:

- Stdlib modules: `Fable.Python.ModuleName`
- Third-party libraries: `Fable.Python.LibraryName`

### 3. File Organization

```fsharp
// 1. Module declaration
module Fable.Python.ModuleName

// 2. Open statements
open System
open Fable.Core

// 3. Disable linting for Python naming conventions
// fsharplint:disable MemberNames,InterfaceNames

// 4. Type aliases
type _identifier = string

// 5. Class/type imports
[<Import("ClassName", "module")>]
type ClassName = ...

// 6. Module exports interface
[<Erase>]
type IExports = ...

// 7. Module import
[<ImportAll("module")>]
let moduleName: IExports = nativeOnly

// 8. Convenience wrappers (if any)
let wrapper x = moduleName.function x
```

### 4. Naming Conventions

- **F# identifiers**: Use `camelCase` - Fable automatically converts to `snake_case`
- **Special Python names**: Use backticks for F# keywords: `` ``open`` ``, `` ``module`` ``
- **Preserve Python semantics**: Keep parameter names close to Python documentation

### 5. Overloading Strategy

When Python accepts different argument types:

1. **Same type, different arities**: Use multiple overloads

   ```fsharp
   abstract parse: string -> AST
   abstract parse: string * filename: string -> AST
   ```

2. **Different types, same arity**: Use multiple overloads

   ```fsharp
   abstract abs: int -> int
   abstract abs: float -> float
   ```

3. **Complex variations**: Consider separate function names or StringEnum for mode flags

### 6. Handling Python's Dynamic Nature

For truly dynamic APIs, use these escape hatches:

```fsharp
// Dynamic property access
let value = pythonObject?propertyName

// Dynamic method calls
pythonObject?method(arg1, arg2)

// Dynamic setting
pythonObject?property <- value

// Type casting (unsafe)
let typed: SomeType = unbox pythonObject
```

**Use sparingly** - prefer typed bindings when possible.

### 7. Inheritance and Interfaces

Python classes often inherit from others:

```fsharp
// Base type
[<Import("TextIOBase", "io")>]
type TextIOBase =
    abstract read: unit -> string
    abstract write: s: string -> int

// Derived type
[<Import("TextIOWrapper", "io")>]
type TextIOWrapper =
    inherit TextIOBase
    inherit System.IDisposable  // Adds context manager support
```

### 8. Avoiding Common Pitfalls

**DON'T:**

- ❌ Use erased union types (U2, U3) unless absolutely necessary
- ❌ Forget `nativeOnly` after import declarations
- ❌ Mix different import patterns in the same binding
- ❌ Ignore Python's naming conventions
- ❌ Over-use `[<Emit>]` when proper attributes exist

**DO:**

- ✅ Use multiple overloads instead of union parameters
- ✅ Use `[<StringEnum>]` for string-based enumerations
- ✅ Keep bindings focused and coherent
- ✅ Document all public APIs
- ✅ Test your bindings with actual Python code

## Testing Your Bindings

### 1. Write F# Tests

Create test files in the `test/` directory:

```fsharp
module Tests.ModuleName

open Xunit
open Fable.Python.ModuleName

[<Fact>]
let ``module should parse simple expression`` () =
    let result = moduleName.parse "1 + 1"
    // Assertions...
```

### 2. Compile and Run with Pytest

```bash
# Compile F# tests to Python
dotnet fable --lang Python --outDir build/tests test

# Run with pytest
uv run pytest build/tests
```

### 3. Verify Generated Python

Check the compiled Python in `build/tests/` to ensure:

- Imports are correct
- Function calls use proper Python syntax
- Snake_case conversion is applied correctly

## Examples

### Example 1: Simple Utility Module (json)

```fsharp
module Fable.Python.Json

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Serialize obj to a JSON formatted string
    abstract dumps: obj: obj -> string
    /// Deserialize s (a string instance containing a JSON document) to a Python object
    abstract loads: s: string -> obj

/// JSON encoder and decoder
[<ImportAll("json")>]
let json: IExports = nativeOnly
```

### Example 2: Module with Classes (ast)

```fsharp
module rec Fable.Python.Ast

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type _identifier = string

// Base class for all AST nodes
[<Import("AST", "ast")>]
type AST =
    abstract foo: int

// Specific node types
[<Import("Module", "ast")>]
type Module =
    inherit AST
    abstract body: stmt array

[<Import("stmt", "ast")>]
type stmt =
    inherit AST

// String enumeration for modes
[<StringEnum>]
[<RequireQualifiedAccess>]
type Mode =
    | [<CompiledName("exec")>] Exec
    | [<CompiledName("eval")>] Eval

// Module functions
[<Erase>]
type IExports =
    /// Parse the source into an AST node
    abstract parse: source: string -> AST
    abstract parse: source: string * filename: string -> AST
    abstract parse: source: string * filename: string * mode: Mode -> AST

    /// Convert an AST back to Python code
    abstract unparse: astObj: AST -> string

/// Abstract Syntax Trees
[<ImportAll("ast")>]
let ast: IExports = nativeOnly
```

### Example 3: Complex Module with Named Parameters (builtins)

```fsharp
module Fable.Python.Builtins

open System
open Fable.Core

// fsharplint:disable MemberNames

type TextIOBase =
    abstract read: unit -> string
    abstract read: size: int -> string
    abstract write: s: string -> int

type TextIOWrapper =
    inherit IDisposable
    inherit TextIOBase

type _Opener = Tuple<string, int> -> int

[<Erase>]
type IExports =
    /// Return the absolute value of the argument
    abstract abs: int -> int
    /// Return the absolute value of the argument
    abstract abs: float -> float

    /// Return the length of an object
    abstract len: obj -> int

    /// Open file and return a stream
    [<NamedParams(fromIndex = 1)>]
    abstract open:
        file: string *
        ?mode: string *
        ?buffering: int *
        ?encoding: string *
        ?errors: string *
        ?newline: string *
        ?closefd: bool *
        ?opener: _Opener ->
            TextIOWrapper

[<ImportAll("builtins")>]
let builtins: IExports = nativeOnly

// Convenience wrapper
let print obj = builtins.print obj
```

### Example 4: Third-Party Library (Flask)

```fsharp
module Fable.Python.Flask

open Fable.Core

// fsharplint:disable MemberNames

type Request =
    abstract url: string
    abstract method: string
    abstract headers: obj

type Flask =
    /// Register a route handler
    abstract route: rule: string -> ((unit -> string) -> Flask)
    /// Register a route handler with HTTP methods
    abstract route: rule: string * methods: string array -> ((unit -> string) -> Flask)

type FlaskStatic =
    /// Create a Flask application
    [<Emit("$0($1, static_url_path=$2)")>]
    abstract Create: name: string * static_url_path: string -> Flask

[<Import("Flask", "flask")>]
let Flask: FlaskStatic = nativeOnly

[<Erase>]
type IExports =
    /// Render a template
    abstract render_template: template_name: string -> string

    /// The request object
    abstract request: Request

    /// Generate a URL for the given endpoint
    [<Emit("flask.url_for($0, filename=$1)")>]
    abstract url_for: endpoint: string * filename: string -> string

[<ImportAll("flask")>]
let flask: IExports = nativeOnly
```

## Contributing Guidelines Checklist

Before submitting new bindings:

- [ ] Package is publicly available on PyPI
- [ ] Package supports Python 3.12+ (Fable 5 requirement)
- [ ] Package doesn't ship with its own type stubs
- [ ] Bindings follow the standard patterns documented here
- [ ] All public APIs have XML documentation
- [ ] Tests are written and passing
- [ ] File is added to `src/Fable.Python.fsproj` in correct order
- [ ] Bindings are in the correct directory (`src/stdlib/` or `src/<library>/`)
- [ ] Code follows F# naming conventions (camelCase)
- [ ] No erased union types (U2, U3) unless absolutely necessary
- [ ] FSharpLint issues are addressed or suppressed with good reason

## Further Resources

- [Fable Python Documentation](https://fable.io/docs/python/)
- [Glutinum: A New Era for Fable Bindings](https://fable.io/blog/2024/2024-01-01-Glutinum_a_new_era.html)
- [Glutinum CLI](https://github.com/glutinum-org/cli)
- [Python Documentation](https://docs.python.org/3/)
- [Fable.Python Repository](https://github.com/fable-compiler/Fable.Python)

## Summary

Writing effective Fable.Python bindings requires:

1. **Understanding the Python API** - Read the official docs thoroughly
2. **Following established patterns** - Use the IExports + ImportAll pattern
3. **Leveraging F#'s type system** - Make invalid states unrepresentable
4. **Prioritizing developer experience** - Avoid erased unions, use overloads
5. **Testing thoroughly** - Both native F# tests and compiled Python tests

The goal is to provide a **near-native F# experience** that allows developers to use Python libraries with all the benefits of F#'s type safety, while maintaining compatibility with Python's documentation and idioms.
