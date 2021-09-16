module Fable.Python.Json

open Fable.Core

// fsharplint:disable MemberNames

type IExports =
    abstract dumps : obj: obj -> string
    abstract loads : string -> obj

[<ImportAll("json")>]
let json: IExports = nativeOnly
