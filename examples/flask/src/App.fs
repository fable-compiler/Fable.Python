module Flask.App

open Fable.Python.Builtins
open Fable.Python.Flask
open Feliz.ViewEngine

let app = Flask.Create(__name__, "/public")

let htmlPage page =
    page |> Render.htmlDocument


let model : Model = {
    Title="Fable Python |> F# ♥️ Python"
    Description="Demo Website, Fable Python running on Flask!"
    Banner="https://unsplash.it/1200/900?random"
    PermaLink="https://fable.io"
    Author="dag@brattli.net"
    Brand="public/favicon.png"
}

let title (str: string) = Html.p [ prop.classes [ Bulma.Title ]; prop.text str ]
let subTitle (str: string) = Html.p [ prop.classes [ Bulma.Subtitle ]; prop.text str ]

let helloWorld () =
    let body = Html.div [
        title model.Title
        subTitle model.Description
    ]

    Html.html [
        Head.head model

        Navbar.navbar model
        Hero.hero model body
    ]
    |> htmlPage

// Setup the routes. See if we can use attributes instead
app.route("/")(helloWorld) |> ignore

[<EntryPoint>]
let main args = 1
