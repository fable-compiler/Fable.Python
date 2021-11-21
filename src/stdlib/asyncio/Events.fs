namespace Fable.Python.AsyncIO

open Fable.Core

type [<AllowNullLiteral>] AbstractEventLoop =
    abstract member run_forever: unit -> unit
    abstract member create_future<'T> :  unit -> Future<'T>
