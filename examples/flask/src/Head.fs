namespace Flask

open Feliz.ViewEngine

module Head =
    let head (model: Model) =
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
