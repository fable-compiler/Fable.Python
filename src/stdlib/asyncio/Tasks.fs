module Fable.Python.AsyncIO.Tasks

open Fable.Core

type Task<'T> =
    inherit Future<'T>

type Coroutine = interface end


type IExports =
    abstract create_task<'T> : fn: Coroutine -> Task<'T>
    abstract sleep : seconds: float -> result: 'T -> Future<'T>
    abstract run : main: Awaitable<'T> -> 'T

[<ImportAll("asyncio")>]
let asyncio: IExports = nativeOnly
