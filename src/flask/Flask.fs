module Fable.Python.Flask

open Fable.Core

// fsharplint:disable MemberNames
type RequestBase =
    abstract url : string

type Request =
    inherit RequestBase

[<Import("Flask", "flask")>]
type Flask (name: string) =
    member _.route(rule: string) : ((unit -> string) -> Flask) = nativeOnly
    member _.route(rule: string, methods: string array) : ((unit -> string) -> Flask) = nativeOnly


type IExports =
    abstract render_template : template_name_or_list: string -> string
    abstract render_template : template_name_or_list: string seq -> string
    abstract request : Request

[<ImportAll("flask")>]
let flask: IExports = nativeOnly
