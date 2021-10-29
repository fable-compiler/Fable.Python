module Fable.Python.String

open Fable.Core

// fsharplint:disable MemberNames

type IExports =
    abstract ascii_letters: string
    abstract ascii_lowercase: string
    abstract ascii_uppercase: string
    abstract digits: string
    abstract hexdigits: string
    abstract octdigits: string
    abstract punctuation: string
    abstract printable: string
    abstract whitespace: string
    abstract capwords : string -> string
    abstract capwords : string * string -> string

 /// A collection of string constants
[<ImportAll("string")>]
let string: IExports = nativeOnly
