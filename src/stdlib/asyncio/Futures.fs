namespace Fable.Python.AsyncIO

open Fable.Core

type Awaitable<'T> = interface end

type Future<'T> =
    inherit Awaitable<'T>

    abstract member cancel : unit -> unit
    abstract member cancelled : unit -> bool
    abstract member ``done`` : unit -> bool
    abstract member result : unit -> 'T
    abstract member ``exception`` : unit -> exn
    abstract member add_done_callback : callback: (unit -> unit) -> unit
    abstract set_name : string -> unit
    abstract get_name : unit -> string
