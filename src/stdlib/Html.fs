module Fable.Python.Html

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    abstract escape: string -> string
    abstract escape: string * bool -> string
    abstract unescape: string -> string

/// HTML interfaces
[<ImportAll("html")>]
let html: IExports = nativeOnly
