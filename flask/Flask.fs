module Flask

open Fable.Core

// fsharplint:disable MemberNames
type RequestBase =
    abstract url : string

type Request =
    inherit RequestBase

type Flask =
    abstract route : rule: string -> ((unit -> string) -> Flask)
    abstract route : rule: string * methods: string array -> ((unit -> string) -> Flask)


type FlaskStatic =
    [<Emit("$0(import_name=$1)")>]
    abstract Create : string -> Flask

[<Import("Flask", "flask")>]
let Flask: FlaskStatic = nativeOnly

type IExports =
    abstract render_template : template_name_or_list: string -> string
    abstract render_template : template_name_or_list: string seq -> string
    abstract request : Request

[<ImportAll("flask")>]
let flask : IExports = nativeOnly
