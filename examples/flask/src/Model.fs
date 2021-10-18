namespace Flask

open Zanaptak.TypedCssClasses

type Bulma = CssClasses<"https://cdnjs.cloudflare.com/ajax/libs/bulma/0.9.3/css/bulma.min.css", Naming.PascalCase>

type Site = {
    Author: string
    Banner: string
    Title: string
    Description: string
    PermaLink: string
    Brand: string
}