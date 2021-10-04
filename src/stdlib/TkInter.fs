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
    member _.mainloop() = nativeOnly
    member _.after(second: float, callback: unit -> unit) = nativeOnly

[<Import("Frame", "tkinter")>]
type Frame(master: Misc) =
    [<Import("Frame", "tkinter")>]
    [<Emit("$0($1, width=$2, height=$3, bg=$4)")>]
    new (master: Tk, width: int, height: int, bg: string) = Frame(master :> Misc)

    member _.bind(sequence : string, func: (Event -> unit)) : string option = nativeOnly
    member _.pack() : unit = nativeOnly

[<Import("Label", "tkinter")>]
type Label (master: Frame) =
    [<Import("Label", "tkinter")>]
    [<Emit("$0($1, text=$2, fg=$3, bg=$4)")>]
    new (master: Frame, text: string, fg: string, bg: string) = Label(master)

    [<Emit("$0.place(x=$1, y=$2)")>]
    member _.place(x: int, y: int) = nativeOnly
