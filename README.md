# Fable Python

![Build and Test](https://github.com/dbrattli/Fable.Python/workflows/Build%20and%20Test/badge.svg)
[![Nuget](https://img.shields.io/nuget/vpre/Fable.Python)](https://www.nuget.org/packages/Fable.Python/)

Python bindings for Fable. This library will eventually contain Python (stdlib)
bindings for Fable based on Python
[typeshed](https://github.com/python/typeshed).

It will also contain type binding for many other libraries as well such
as Flask, MicroBit and many more

## Installation

Prerequisite to compile F# to Python using Fable:

```sh
> dotnet tool install --global fable-py --version 4.0.0-alpha-004
> dotnet add package Fable.Core.Experimental --version 4.0.0-alpha-005
```

To use the `Fable.Python` library:

```sh
> dotnet package add Fable.Python
```

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


