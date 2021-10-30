# Fable Python

![Build and Test](https://github.com/dbrattli/Fable.Python/workflows/Build%20and%20Test/badge.svg)
[![Nuget](https://img.shields.io/nuget/vpre/Fable.Python)](https://www.nuget.org/packages/Fable.Python/)

[Fable](https://github.com/fable-compiler/Fable/tree/beyond) is a compiler that
translates F# source files to JavaScript and Python.

This Fable Python repository contains the Python bindings for Fable. This
library will eventually contain Python (stdlib) bindings for Fable based on
Python [typeshed](https://github.com/python/typeshed). It will also contain
type binding for many other libraries as well such as Flask, MicroBit and many
more. Some bindings have already been added:

- Python Standard Libray
- Jupyter
- Flask
- MicroBit
- CogniteSdk

## Installation

Prerequisite for compiling F# to Python using Fable:

```sh
> dotnet tool install --global fable-py --version 4.0.0-alpha-005
> dotnet add package Fable.Core.Experimental --version 4.0.0-alpha-006
```

To use the `Fable.Python` library in your Fable project:

```sh
> dotnet package add Fable.Python
```

## Usage

```fs
open Fable.Python.Json

let object = {| A=10; B=20 |}
let result = json.dumps object
```

To compile an F# Fable project to Python run e.g:

```sh
> dotnet fable-py MyProject.fsproj
```

For more examples see the
[examples](https://github.com/dbrattli/Fable.Python/tree/main/examples) folder.
It contains example code for using Fable Python with:

- [Flask](https://github.com/dbrattli/Fable.Python/tree/main/examples/flask).
  References [Feliz.ViewEngine](https://github.com/dbrattli/Feliz.ViewEngine)
  as a nuget package.
- [MicroBit](https://github.com/dbrattli/Fable.Python/tree/main/examples/microbit)
- [Timeflies](https://github.com/dbrattli/Fable.Python/tree/main/examples/timeflies),
  Cool demo using Tkinter and references
  [FSharp.Control.AsyncRx](https://github.com/dbrattli/AsyncRx) as a nuget
  package.


## Contributing

If the type binding you are looking for is currently missing (it probably is),
then add it to the relavant files (or add new ones). Open a
[PR](https://github.com/dbrattli/Fable.Python/pull/3/files) to get them
included. There's not much Python specific documentation yet, but the process
of adding type bindings for Python is similar to JS:

- https://fable.io/docs/communicate/js-from-fable.html
- https://medium.com/@zaid.naom/f-interop-with-javascript-in-fable-the-complete-guide-ccc5b896a59f


## Differences from JS

Note that import all is different from JS. E.g:

```fs
[<ImportAll("flask")>]
let flask: IExports = nativeOnly
```

This will generate `import flask` and not a wildcard import `from flask import
*`. The latter version is discoraged anyways.

## Auto-generation

Parts of this library could benefit from code-generation based on the type
annotations in Python [typeshed](https://github.com/python/typeshed) similar to
[ts2fable](https://github.com/fable-compiler/ts2fable). Even so we should keep
this library manually updated based on PRs to ensure the quality of the code.

Current plan:

1. Add bindings for Python `ast` module (in progress)
2. Use `ast` module to parse Python typeshed annotations
3. Generate F# bindings


