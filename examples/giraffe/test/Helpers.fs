[<AutoOpen>]
module Giraffe.Tests.Helpers

open System
open System.Collections.Generic
open System.Threading.Tasks

open Giraffe.Python
open System.Text

// ---------------------------------
// Common functions
// ---------------------------------


let waitForDebuggerToAttach() =
    printfn "Waiting for debugger to attach."
    printfn "Press enter when debugger is attached in order to continue test execution..."
    Console.ReadLine() |> ignore

let removeNewLines (html : string) : string =
    html.Replace(Environment.NewLine, String.Empty)


// ---------------------------------
// Test server/client setup
// ---------------------------------

let next : HttpFunc = Some >> Task.FromResult

let printBytes (bytes : byte[]) =
    bytes |> Array.fold (
        fun (s : string) (b : byte) ->
            match s.Length with
            | 0 -> $"%i{b}"
            | _ -> $"%s{s},%i{b}") ""


type HttpTester (?method: string, ?path: string, ?status: int) =
    let _method = defaultArg method "GET"
    let _path = defaultArg path "/"
    let _status = defaultArg status 200
    let _scope = Dictionary<string, obj> (dict ["method", _method :> obj; "path", _path; "status", _status])
    let _response = Dictionary<string, obj> ()

    let send (response: Response) =
        let inline toMap kvps =
            kvps
            |> Seq.map (|KeyValue|)
            |> Map.ofSeq

        task {
            let xs = toMap response
            for KeyValue(key, value) in xs do
                if key <> "type" then
                    _response.Add(key, value)
        }

    let receive () =
        task {
            return Dictionary<string, obj> ()
        }

    let ctx = HttpContext(_scope, receive, send)

    member this.Context = ctx
    member this.Body = _response["body"] :?> byte []
    member this.Text =
        let body = _response["body"] :?> byte array
        Encoding.UTF8.GetString(body)
