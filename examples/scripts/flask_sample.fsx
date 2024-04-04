#r "nuget: Fable.Core"
#r "nuget: Fable.Python"
#r "nuget: Feliz.ViewEngine"
#r "nuget: Zanaptak.TypedCssClasses"
open Fable.Python.Builtins
open Fable.Python.Flask
open Feliz.ViewEngine
open Zanaptak.TypedCssClasses

type Bulma = CssClasses<"https://cdnjs.cloudflare.com/ajax/libs/bulma/0.9.3/css/bulma.min.css", Naming.PascalCase>

module View =
    let title (str: string) = Html.p [ prop.classes [ Bulma.Title ]; prop.text str ]
    let subTitle (str: string) = Html.p [ prop.classes [ Bulma.Subtitle ]; prop.text str ]

    let model = {|
            Title="Fable Python |> F# ♥️ Python"
            Description="Demo Website, Fable Python running on Flask!"
            Banner="https://unsplash.it/1200/900?random"
            PermaLink="https://fable.io"
            Author="dag@brattli.net"
            Brand="public/favicon.png"
        |}

    let head =
        Html.head [
            Html.title [ prop.text model.Title ]

            Html.meta [ prop.charset.utf8 ]
            Html.meta [ prop.name "author"; prop.content model.Author ]
            Html.meta [ prop.name "description"; prop.content model.Description ]

            Html.meta [ prop.httpEquiv.contentType; prop.content "text/html"; prop.charset.utf8 ]
            Html.meta [ prop.name "viewport"; prop.content "width=device-width, initial-scale=1" ]

            Html.meta [
                prop.custom ("http-equiv", "Cache-Control")
                prop.content "no-cache, no-store, must-revalidate"
            ]
            Html.meta [ prop.custom ("http-equiv", "Pragma"); prop.content "no-cache" ]
            Html.meta [ prop.custom ("http-equiv", "Expires"); prop.content "0" ]

            Html.link [ prop.rel "icon"; prop.href "public/favicon.ico" ]
            Html.link [ prop.rel "stylesheet"; prop.href "https://cdnjs.cloudflare.com/ajax/libs/bulma/0.9.3/css/bulma.min.css"; prop.crossOrigin.anonymous ]
        ]

    let body = Html.div [
        title model.Title
        subTitle model.Description
    ]

    let hero =
        Html.section [
            prop.classes [ Bulma.Hero; Bulma.IsFullheightWithNavbar ]
            prop.style [
                style.backgroundImageUrl (model.Banner)
                style.backgroundPosition "center"
                style.backgroundSize.cover
            ]
            prop.children [
                Html.div [
                    prop.classes [ Bulma.HeroBody; Bulma.IsDark ]
                    prop.children [ Html.div [ prop.classes [ Bulma.Container ]; prop.children body ] ]
                ]
            ]
        ]

    let html =
        Html.html [
            head
            Html.body hero
        ]

let renderView () =
    View.html |> Render.htmlDocument


module App =

    let run () = 
        let app = Flask.Create(__name__, "/public")

        // Setup the routes. See if we can use attributes instead
        app.route("/")(renderView) |> ignore

App.run()