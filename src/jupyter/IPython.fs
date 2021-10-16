module Fable.Python.IPython

open Fable.Core

[<Import("display", "IPython.display")>]
let display<'T>(obj: 'T) : unit = nativeOnly