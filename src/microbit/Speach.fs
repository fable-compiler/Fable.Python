module Fable.Python.Speach

open Fable.Core

type ISpeach =
    abstract say : text: string -> unit

[<ImportAll("speach")>]
let speach: ISpeach = nativeOnly
