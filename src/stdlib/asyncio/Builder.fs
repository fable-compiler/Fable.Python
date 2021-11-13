namespace Fable.Python.AsyncIO

open Fable.Core


type FutureBuilder () =
    [<Emit("Promise.resolve($1)")>]
    member _.Return(a: 'T): Future<'T> = nativeOnly

    [<Emit("$1")>]
    member _.ReturnFrom(p: Future<'T>): Future<'T> = nativeOnly


[<AutoOpen>]
module FutureImpl =

    let future = FutureBuilder()
