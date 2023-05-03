namespace Fable.Python.AsyncIO

open Fable.Core

[<AllowNullLiteral>]
type AbstractEventLoop =
    abstract member run_forever: unit -> unit
    abstract member create_future<'T> : unit -> Future<'T>
