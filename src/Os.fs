module Fable.Python.Os

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type IExports =
    abstract getenv: key:string -> string option
    abstract getenv: key:string * ``default``: string -> string
    abstract putenv: key:string * value: string -> unit


[<Import("os")>]
let os : IExports = nativeOnly
