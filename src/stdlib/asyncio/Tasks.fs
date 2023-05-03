namespace Fable.Python.AsyncIO

open Fable.Core

type Task<'T> =
    inherit Future<'T>

type Coroutine<'T> =
    inherit Awaitable<'T>

    abstract member send: 'T -> unit
    abstract member close: unit -> unit
    abstract member throw: exn -> unit


type IExports =
    abstract create_task<'T> : fn: Coroutine<'T> -> Task<'T>
    abstract sleep: seconds: float -> result: 'T -> Future<'T>
    abstract run<'T> : main: Awaitable<'T> -> 'T
    abstract run<'T> : main: System.Threading.Tasks.Task<'T> -> 'T

    [<Emit("$0.get_running_loop()")>]
    abstract get_running_loop: unit -> AbstractEventLoop

    [<Emit("$0.get_event_loop()")>]
    abstract get_event_loop: unit -> AbstractEventLoop

[<AutoOpen>]
module AsyncIOImpl =
    [<ImportAll("asyncio")>]
    let asyncio: IExports = nativeOnly
