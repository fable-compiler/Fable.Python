namespace Flask

open System

open Feliz.ViewEngine

module Navbar =
    let navbar (site: Site) =
        Html.nav [
            prop.classes [ Bulma.Navbar; Bulma.IsPrimary ]
            prop.children [
                Html.div [
                    prop.classes [ Bulma.NavbarBrand ]
                    prop.children [
                        Html.a [
                            prop.classes [ Bulma.NavbarItem ]
                            prop.href site.PermaLink
                            prop.children [
                                Html.img [ prop.alt "Brand"; prop.src (site.Brand) ]
                                Html.p [
                                    prop.classes [ Bulma.Title; Bulma.Is4 ]
                                    prop.text site.Title
                                ]
                            ]
                        ]
                    ]
                ]

                Html.div [
                    prop.classes [ Bulma.NavbarEnd ]
                    prop.children [
                        Html.div [
                            prop.className Bulma.NavbarItem
                            prop.children [
                                Html.p [ prop.classes [ Bulma.Title; Bulma.Is6 ]; prop.text "Welcome to F# eXchange 2021" ]
                            ]
                        ]
                        Html.div [
                            prop.className Bulma.NavbarItem
                            prop.children [
                                Html.form [
                                    prop.action "/login"
                                    prop.method "get"
                                    prop.children [
                                        Html.input [
                                            prop.className Bulma.Button
                                            prop.type' "submit"
                                            prop.value "Login"
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
