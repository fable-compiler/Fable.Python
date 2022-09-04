module Program

open Giraffe

let webApp =
    GET
    |> HttpHandler.choose [
        route "/ping"
        |> HttpHandler.text "Hello World!"
    ]

let app = Middleware.useGiraffe webApp