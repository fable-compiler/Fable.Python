module Fable.Python.Json

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type IExports =
    abstract dumps : obj: obj -> string
    abstract loads : string -> obj

[<ImportDefault("json")>]
let json: IExports = nativeOnly
