module Fable.Python.Json

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type Json =
    abstract dumps : obj: obj -> string
    abstract loads : string -> obj

[<ImportDefault("json")>]
let json: Json = nativeOnly
