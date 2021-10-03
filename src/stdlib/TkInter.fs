module Fable.Python.TkInter

open Fable.Core

type Event =
    abstract member x: int
    abstract member y: int

type Misc =
    abstract member bind : sequence : string * func: (Event -> unit) -> string option

type Wm =
    abstract member title : unit -> string
    abstract member title : ``string``:  string -> unit

[<Import("Tk", "tkinter")>]
type Tk (screenName: string option) =
    interface Wm with
        member _.title(``string``: string) = nativeOnly
        member _.title() = nativeOnly

    interface Misc with
        member _.bind(sequence : string, func: (Event -> unit)) = nativeOnly

    new () = Tk (None)

    member _.title(``string``: string) = nativeOnly
    member _.title() = nativeOnly

    member _.update() = nativeOnly

[<Import("Frame", "tkinter")>]
type Frame(master: Misc) =
    [<Emit("Frame($0, width=$1, height=$2, bg=$3)")>]
    new (root: Tk, width: int, height: int, bg: string) = Frame(root :> Misc)

    member x.bind(sequence : string, func: (Event -> unit)) : string option = nativeOnly
