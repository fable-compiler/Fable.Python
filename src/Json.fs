module Fable.Python.Json

open Fable.Core


type Json =
    abstract dumps : obj: obj -> string

[<ImportDefault("os")>]
let json: Json = nativeOnly
