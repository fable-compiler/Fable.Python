namespace Flask

open Feliz.ViewEngine

module Hero =
    let title (str: string) = Html.p [ prop.classes [ Bulma.Title ]; prop.text str ]
    let subTitle (str: string) = Html.p [ prop.classes [ Bulma.Subtitle ]; prop.text str ]

    let hero (model: Model) body =
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
