module Program

open System.Threading.Tasks
open Giraffe

let handler: HttpHandler =
    route "/ping"
    >=> text "Hello World!"

let func : HttpFunc = handler earlyReturn

let app
    (
        scope: Scope,
        receive: unit -> Task<Response>,
        send: Request -> Task<unit>
    ) =
    task {
        printfn "Scope %A" scope
        let ctx = HttpContext(scope, receive, send)
        let! result = func ctx
        ()
    }