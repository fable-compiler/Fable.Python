# Fable Python

Python bindings for Fable. This library will eventually contain Python (stdlib)
bindings for Fable based on Python
[typeshed](https://github.com/python/typeshed).

## Installation

TBD

## Usage

```fs
open Fable.Python.Json

let object = {| A=10; B=20 |}
let result = json.dumps object
```

## Auto-generation

Parts of this library could benefit from code-generation based on the type
annotations in Python [typeshed](https://github.com/python/typeshed) similar to
[ts2fable](https://github.com/fable-compiler/ts2fable). Even so we should keep
this library manually updated based on PRs to ensure the quality of the code.

Current plan:

1. Add bindings for Python `ast` module (in progress)
2. Use `ast` module to parse Python typeshed annotations
3. Generate F# bindings


