module Fable.Python.Os

open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames

type IExports =
    abstract environ : Dictionary<string, string>
    abstract getenv : key: string -> string option
    abstract getenv : key: string * ``default``: string -> string
    abstract putenv : key: string * value: string -> unit


/// Miscellaneous operating system interfaces
[<ImportAll("os")>]
let os: IExports = nativeOnly
