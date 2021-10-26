module Fable.Python.Html

open Fable.Core

// fsharplint:disable MemberNames

type IExports =
    abstract escape : string -> string
    abstract escape : string * bool -> string
    abstract unescape : string -> string

 /// Miscellaneous operating system interfaces
[<ImportAll("html")>]
let html: IExports = nativeOnly
