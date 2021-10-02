module Fable.Python.TkInter

open Fable.Core

type Misc =
    abstract member title : string with get, set

[<Import("Frame", "tkinter")>]
type Frame(master: Misc) =
    do ()

[<Import("Tk", "tkinter")>]
type Tk (name: string) =
    interface Misc with
        member _.title: string = nativeOnly
        member _.title with set (value: string) = nativeOnly