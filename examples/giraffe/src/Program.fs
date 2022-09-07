module Program

open Giraffe.Python
open Giraffe.Python.Pipelines

let webApp = choose [
    route "/ping"
    |> HttpHandler.text "pong"
]

let app = Middleware.useGiraffe webApp
