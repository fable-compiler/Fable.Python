# Fable Python

![Build and Test](https://github.com/dbrattli/Fable.Python/workflows/Build%20and%20Test/badge.svg)
[![Nuget](https://img.shields.io/nuget/vpre/Fable.Python)](https://www.nuget.org/packages/Fable.Python/)

[Fable](https://github.com/fable-compiler/Fable/tree/beyond) is a compiler that
translates F# source files to JavaScript and Python.

This Fable Python repository is a community driven project that contains the Python type bindings for Fable. The
library will eventually contain Python (stdlib) bindings for Fable based on Python
[typeshed](https://github.com/python/typeshed). It will also contain type binding for many other 3rd party libraries
such as Flask, MicroBit and many more. Some bindings have already been added:

- Python Standard Libray
- Jupyter
- Flask
- MicroBit
- CogniteSdk

## Version

This library currently targets Python 3.9. Types bindings for other versions of Python should not be added to this
library until we decide how to deal with Python version handling.

## Installation

Prerequisite for compiling F# to Python using Fable:

```sh
> dotnet tool install --global fable-py --version 4.0.0-alpha-032
> dotnet add package Fable.Core.Experimental --version 4.0.0-alpha-032
```

To use the `Fable.Python` library in your Fable project:

```sh
> dotnet add package Fable.Python
```

## Usage

```fs
open Fable.Python.Json

let object = {| A=10; B=20 |}
let result = json.dumps object
```

To compile an F# Fable project to Python run e.g:

```sh
> fable-py MyProject.fsproj
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

## Libraries that uses or works with Fable Python

- [AsyncRx](https://github.com/dbrattli/AsyncRx)
- [Fable.Sedlex](https://github.com/thautwarm/Fable.Sedlex)
- [Fable.SimpleJson.Python](https://github.com/Zaid-Ajaj/Fable.SimpleJson.Python)
- [Feliz.ViewEngine](https://github.com/dbrattli/Feliz.ViewEngine)
- [TypedCssClasses](https://github.com/zanaptak/TypedCssClasses)

## Contributing

This project is community driven. If the type binding you are looking for is currently missing, then
you need to add them to the relavant files (or add new ones). Open a [PR](https://github.com/dbrattli/Fable.Python/pull/3/files) to
get them included.

The `src/stdlib` directory contains type bindings for modules in the Python 3 standard library. We also accept type
bindings for 3rd party libraries as long as:

- the package is publicly available on the [Python Package Index](https://pypi.org/);
- the package supports any Python version supported by Fable Python; and
- the package does not ship with its own stubs or type annotations

There's not much Python specific documentation yet, but the process of adding type bindings for Python is similar to JS:

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
