module Fable.Python.Os

open Fable.Core

// fsharplint:disable MemberNames

type IExports =
    abstract getenv : key: string -> string option
    abstract getenv : key: string * ``default``: string -> string
    abstract putenv : key: string * value: string -> unit


[<ImportAll("os")>]
let os: IExports = nativeOnly