# Fable.Python

F# to Python compiler extension for Fable.

## Project Structure

- `src/stdlib/` - Python standard library bindings (Builtins, Json, Os, etc.)
- `src/flask/` - Flask web framework bindings
- `src/fastapi/` - FastAPI web framework bindings
- `src/pydantic/` - Pydantic model bindings
- `test/` - Test files
- `examples/` - Example applications (flask, fastapi, django, timeflies)
- `build/` - Generated Python output (gitignored)

## Build Commands

```bash
just clean           # Clean all build artifacts (build/, obj/, bin/, .fable/)
just build           # Build the project
just test-python     # Run Python tests
just restore         # Restore .NET and paket dependencies
just example-flask   # Build and run Flask example
just example-fastapi # Build and run FastAPI example
just dev-fastapi     # Run FastAPI with hot-reload
```

## Build Output

Generated Python code goes to `build/` directories (gitignored):
- `build/` - Main library output
- `build/tests/` - Test output
- `examples/*/build/` - Example outputs

## Key Concepts

### Fable Type Serialization

F# types compile to non-native Python types:

- `int` → `Int32` (not Python's `int`)
- `int64` → `Int64`
- F# array → `FSharpArray` (not Python's `list`)
- `ResizeArray<T>` → Python `list`
- `nativeint` → Python `int`

Use `Fable.Python.Json.dumps` with `fableDefault` for JSON serialization of Fable types.
Use `ResizeArray<T>` for collections in web API responses.
Use Pydantic `BaseModel` for FastAPI request/response types (handles `Int32` correctly).

See `JSON.md` for detailed serialization documentation.

### Decorator Attributes

Route decorators use `Py.DecorateTemplate`:

```fsharp
[<Erase; Py.DecorateTemplate("""app.get("{0}")""")>]
type GetAttribute(path: string) = inherit Attribute()
```

Class attributes use `Py.ClassAttributesTemplate` for Pydantic-style classes.

## Releasing

When asked to create a release, read `RELEASING.md` for the release process.
Use `Release-As: X.Y.Z-alpha.N.P` in commit messages or PR descriptions to set the version.
