// Type bindings for Python asyncio futures: https://docs.python.org/3/library/asyncio-future.html
namespace Fable.Python.AsyncIO

open Fable.Core

type Awaitable<'T> = interface end

type Future<'T> =
    inherit Awaitable<'T>

    abstract member cancel: unit -> unit
    abstract member cancelled: unit -> bool
    abstract member ``done``: unit -> bool

    [<Emit("$0.set_result($1)")>]
    abstract set_result: __result: 'T -> unit

    abstract member result: unit -> 'T
    abstract member ``exception``: unit -> exn

    [<Emit("$0.set_exception($1)")>]
    abstract set_exception: __exception: exn -> unit

    abstract member add_done_callback: callback: (unit -> unit) -> unit
    abstract set_name: string -> unit
    abstract get_name: unit -> string
