# Fable Python

![Build and Test](https://github.com/dbrattli/Fable.Python/workflows/Build%20and%20Test/badge.svg)
[![Nuget](https://img.shields.io/nuget/vpre/Fable.Python)](https://www.nuget.org/packages/Fable.Python/)

[Fable](https://github.com/fable-compiler/Fable) is a compiler that translates F# source files to JavaScript and Python.

**Fable.Python** provides Python type bindings for Fable, enabling you to write type-safe F# code that compiles to Python. This community-driven library includes bindings for the Python standard library and popular frameworks like Flask, FastAPI, and Pydantic.

## Requirements

- Python 3.12 or greater
- .NET 8.0 or greater
- [Fable](https://fable.io/) compiler

## Installation

Install the Fable compiler:

```sh
dotnet tool install --global fable --prerelease
dotnet add package Fable.Core --prerelease
```

Add Fable.Python to your project:

```sh
dotnet add package Fable.Python
```

## Quick Start

```fsharp
open Fable.Python.Json

let data = {| name = "Alice"; age = 30 |}
let jsonStr = dumps data
```

Compile to Python:

```sh
fable --lang Python MyProject.fsproj
```

## Available Bindings

### Python Standard Library

|         Module          |                 Description                 |
| ----------------------- | ------------------------------------------- |
| `Fable.Python.Builtins` | Built-in functions (open, print, len, etc.) |
| `Fable.Python.Json`     | JSON serialization with Fable type support  |
| `Fable.Python.Os`       | Operating system interfaces                 |
| `Fable.Python.Sys`      | System-specific parameters                  |
| `Fable.Python.Math`     | Mathematical functions                      |
| `Fable.Python.Random`   | Random number generation                    |
| `Fable.Python.Logging`  | Logging facilities                          |
| `Fable.Python.Time`     | Time-related functions                      |
| `Fable.Python.String`   | String operations                           |
| `Fable.Python.Base64`   | Base64 encoding/decoding                    |
| `Fable.Python.Queue`    | Queue data structures                       |
| `Fable.Python.Ast`      | Abstract Syntax Tree                        |
| `Fable.Python.AsyncIO`  | Async programming (Events, Futures, Tasks)  |
| `Fable.Python.TkInter`  | GUI toolkit                                 |

### Web Frameworks

|         Package         |             Description             |
| ----------------------- | ----------------------------------- |
| `Fable.Python.Flask`    | Flask web framework                 |
| `Fable.Python.FastAPI`  | FastAPI with automatic OpenAPI docs |
| `Fable.Python.Pydantic` | Data validation and settings        |

## JSON Serialization

Fable types (like `Int32`, F# records, unions) need special handling for JSON serialization. Use `Fable.Python.Json.dumps`:

```fsharp
open Fable.Python.Json

type User = { Id: int; Name: string }
let user = { Id = 1; Name = "Bob" }
let json = dumps user  // {"Id": 1, "Name": "Bob"}
```

See [JSON.md](JSON.md) for detailed documentation on serialization patterns.

## Web Framework Examples

### FastAPI

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
        UserResponse(Id = user_id, Name = "Alice")
```

### Flask

```fsharp
open Fable.Python.Flask
open Fable.Python.Json

[<APIClass>]
type Routes() =
    [<Get("/api/hello")>]
    static member hello() : string =
        dumps {| message = "Hello, World!" |}
```

## Examples

The [examples](examples/) directory contains working applications:

|                  Example                   |                  Description                   |
| ------------------------------------------ | ---------------------------------------------- |
| [fastapi](examples/fastapi/)               | REST API with Pydantic models and Swagger docs |
| [flask](examples/flask/)                   | Web app with Feliz.ViewEngine HTML rendering   |
| [django](examples/django/)                 | Full Django project                            |
| [django-minimal](examples/django-minimal/) | Single-file Django app                         |
| [pydantic](examples/pydantic/)             | Pydantic model examples                        |
| [timeflies](examples/timeflies/)           | Tkinter GUI with AsyncRx                       |

Run an example:

```sh
just example-fastapi   # FastAPI with auto-reload
just example-flask     # Flask web app
just example-timeflies # Tkinter desktop app
```

## Development

This project uses [just](https://github.com/casey/just) as a command runner and [uv](https://docs.astral.sh/uv/) for Python package management.

### Setup

```sh
# Install just (macOS)
brew install just

# Full setup (restore .NET and Python dependencies)
just setup
```

### Commands

```sh
just              # Show all available commands
just build        # Build F# to Python
just test         # Run all tests (native .NET and Python)
just test-python  # Run only Python tests
just format       # Format code with Fantomas
just pack         # Create NuGet package
just clean        # Clean build artifacts
```

### Project Structure

```txt
src/
├── stdlib/       # Python standard library bindings
├── flask/        # Flask bindings
├── fastapi/      # FastAPI bindings
├── pydantic/     # Pydantic bindings
└── jupyter/      # Jupyter bindings
test/             # Test suite
examples/         # Example applications
build/            # Generated Python output (gitignored)
```

## Compatible Libraries

These libraries work with Fable.Python:

- [AsyncRx](https://github.com/dbrattli/AsyncRx) - Reactive programming
- [Fable.Giraffe](https://github.com/dbrattli/Fable.Giraffe) - Giraffe port
- [Fable.Logging](https://github.com/dbrattli/Fable.logging) - Logging
- [Fable.Requests](https://github.com/Zaid-Ajaj/Fable.Requests) - HTTP requests
- [Fable.Jupyter](https://github.com/fable-compiler/Fable.Jupyter) - Jupyter notebooks
- [Fable.Pyexpecto](https://github.com/Freymaurer/Fable.Pyxpecto) - Testing
- [Fable.SimpleJson.Python](https://github.com/Zaid-Ajaj/Fable.SimpleJson.Python) - JSON parsing
- [Fable.Sedlex](https://github.com/thautwarm/Fable.Sedlex) - Lexer generator
- [Feliz.ViewEngine](https://github.com/dbrattli/Feliz.ViewEngine) - HTML rendering
- [Femto](https://github.com/Zaid-Ajaj/Femto) - Package management
- [FsToolkit.ErrorHandling](https://demystifyfp.gitbook.io/fstoolkit-errorhandling/) - Error handling
- [TypedCssClasses](https://github.com/zanaptak/TypedCssClasses) - Type-safe CSS

## Contributing

Contributions are welcome! If a type binding you need is missing, open a [PR](https://github.com/dbrattli/Fable.Python/pulls) to add it.

### Commit Convention

This project uses [Conventional Commits](https://www.conventionalcommits.org/) for automated releases:

|    Type    |            Description             |
| ---------- | ---------------------------------- |
| `feat`     | New features (bumps minor version) |
| `fix`      | Bug fixes (bumps patch version)    |
| `docs`     | Documentation changes              |
| `chore`    | Maintenance tasks                  |
| `refactor` | Code refactoring                   |
| `test`     | Tests                              |

Breaking changes use `!` (e.g., `feat!: breaking change`).

### Adding Bindings

The `src/stdlib/` directory contains Python standard library bindings. Third-party library bindings are accepted if:

- The package is publicly available on [PyPI](https://pypi.org/)
- The package supports Python 3.12+
- The package doesn't ship with its own type stubs

For guidance on creating bindings, see:

- [Fable JS Interop](https://fable.io/docs/communicate/js-from-fable.html) (patterns apply to Python)
- [F# Interop Guide](https://medium.com/@zaid.naom/f-interop-with-javascript-in-fable-the-complete-guide-ccc5b896a59f)

### Import Pattern

Note that `ImportAll` generates a module import:

```fsharp
[<ImportAll("flask")>]
let flask: IExports = nativeOnly
```

This generates `import flask`, not `from flask import *`.

## License

MIT
