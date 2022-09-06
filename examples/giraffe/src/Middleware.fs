namespace Giraffe.Python

open System
open System.Threading.Tasks

type App = Func<Scope, unit -> Task<Response>, Request -> Task<unit>, Task<unit>>

module Middleware =
    let useGiraffe (handler: HttpHandler) : App =
        let func: HttpFunc = handler earlyReturn
        let defaultHandler = setStatusCode 404 >=> text ""
        let defaultFunc: HttpFunc = defaultHandler earlyReturn

        let app (scope: Scope) (receive: unit -> Task<Response>) (send: Request -> Task<unit>) =
            task {
                //printfn "Scope %A" scope
                let ctx = HttpContext(scope, receive, send)
                let! result = func ctx

                match result with
                | None ->
                    let! _ = defaultFunc ctx
                    ()
                | _ -> ()
            }

        Func<_, _, _, _>(app)
