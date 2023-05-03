module Fable.Python.IPython

open Fable.Core

type IDisplay =
    abstract display: value: obj -> unit
    abstract Code: data: string -> unit

    [<Emit("display.Code($1, language=$2)")>]
    abstract Code: data: string * language: string -> unit

    [<Emit("display.Markdown($1)")>]
    abstract Markdown: data: string -> unit

[<Import("display", "IPython")>]
let display: IDisplay = nativeOnly
