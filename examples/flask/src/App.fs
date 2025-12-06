module Flask.App

open Fable.Python.Builtins
open Fable.Python.Flask
open Feliz.ViewEngine

// Create Flask app
let app = Flask(__name__, static_url_path = "/public")

// Helper to render HTML
let htmlPage page =
    page |> Render.htmlDocument

// Page model
let model : Model = {
    Title = "Fable Python |> F# -> Python"
    Description = "Demo Website, Fable Python running on Flask!"
    Banner = "https://unsplash.it/1200/900?random"
    PermaLink = "https://fable.io"
    Author = "dag@brattli.net"
    Brand = "public/favicon.png"
}

// Helper components
let title (str: string) = Html.p [ prop.classes [ Bulma.Title ]; prop.text str ]
let subTitle (str: string) = Html.p [ prop.classes [ Bulma.Subtitle ]; prop.text str ]

// Routes using class-based decorator pattern
[<APIClass>]
type Routes() =
    /// Home page
    [<Get("/")>]
    static member index() : string =
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

    /// About page
    [<Get("/about")>]
    static member about() : string =
        let body = Html.div [
            title "About"
            subTitle "This is a Flask app built with F# and Fable!"
            Html.p [
                prop.text "Fable.Python compiles F# code to Python, allowing you to use F#'s type safety and functional programming features while targeting Python frameworks like Flask."
            ]
        ]

        Html.html [
            Head.head model
            Navbar.navbar model
            Hero.hero { model with Title = "About"; Description = "Learn more about Fable.Python" } body
        ]
        |> htmlPage

    /// JSON API endpoint
    [<Get("/api/info")>]
    static member api_info() : obj =
        jsonify {|
            title = model.Title
            description = model.Description
            framework = "Flask"
            language = "F# (via Fable)"
        |}

    /// Health check
    [<Get("/health")>]
    static member health() : obj =
        jsonify {| status = "healthy" |}
