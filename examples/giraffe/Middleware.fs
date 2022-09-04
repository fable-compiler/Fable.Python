namespace Giraffe

open System
open System.Threading.Tasks

type App = Func<Scope, unit -> Task<Response>, Request -> Task<unit>, Task<unit>>

module Middleware =
    let useGiraffe (handler: HttpHandler) : App =
        let func: HttpFunc = handler earlyReturn

        let app (scope: Scope) (receive: unit -> Task<Response>) (send: Request -> Task<unit>) =
            task {
                printfn "Scope %A" scope
                let ctx = HttpContext(scope, receive, send)
                let! result = func ctx
                ()
            }

        Func<_,_,_,_>(app)
