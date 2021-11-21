module Fable.Python.Tests.AsyncIO

open Util.Testing
open Fable.Python.AsyncIO

[<Fact>]
let ``test builder run zero works`` () =
    let tsk = task {
        ()
    }
    let result = asyncio.run(tsk)
    result |> equal ()

[<Fact>]
let ``test builder run int works`` () =
    let tsk = task {
        return 42
    }
    let result = asyncio.run(tsk)
    result |> equal 42

