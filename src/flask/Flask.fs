module Fable.Python.Flask

open Fable.Core

// fsharplint:disable MemberNames
type RequestBase =
    abstract url: string

type Request =
    inherit RequestBase

type Flask =
    abstract route: rule: string -> ((unit -> string) -> Flask)
    abstract route: rule: string * methods: string array -> ((unit -> string) -> Flask)

type FlaskStatic =
    [<Emit("$0($1, static_url_path=$2)")>]
    abstract Create: name: string * static_url_path: string -> Flask

[<Import("Flask", "flask")>]
let Flask: FlaskStatic = nativeOnly

type IExports =
    abstract render_template: template_name_or_list: string -> string
    abstract render_template: template_name_or_list: string seq -> string
    abstract request: Request

    [<Emit("flask.url_for($0, filename=$1)")>]
    abstract url_for: route: string * filename: string -> string

[<ImportAll("flask")>]
let flask: IExports = nativeOnly
