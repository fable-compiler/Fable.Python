/// Type bindings for Python string module: https://docs.python.org/3/library/string.html
module Fable.Python.String

open System
open Fable.Core

// fsharplint:disable MemberNames

type System.String with

    [<Emit("$0.format($1...)")>]
    member _.format([<ParamArray>] args: Object[]) = nativeOnly

/// Template class for $-based string substitution
[<Import("Template", "string")>]
type Template(template: string) =
    /// The template string passed to the constructor
    member _.template: string = nativeOnly

    /// Perform substitution, returning a new string
    /// Raises KeyError if placeholders are missing from mapping
    [<Emit("$0.substitute($1)")>]
    member _.substitute(mapping: obj) : string = nativeOnly

    /// Perform substitution using keyword arguments
    [<Emit("$0.substitute(**$1)")>]
    member _.substituteKw(kwargs: obj) : string = nativeOnly

    /// Like substitute(), but returns original placeholder if missing
    [<Emit("$0.safe_substitute($1)")>]
    member _.safe_substitute(mapping: obj) : string = nativeOnly

    /// Like substitute(), but returns original placeholder if missing (keyword args)
    [<Emit("$0.safe_substitute(**$1)")>]
    member _.safe_substituteKw(kwargs: obj) : string = nativeOnly

    /// Returns False if the template has invalid placeholders
    [<Emit("$0.is_valid()")>]
    member _.is_valid() : bool = nativeOnly

    /// Returns a list of valid identifiers in the template
    [<Emit("$0.get_identifiers()")>]
    member _.get_identifiers() : ResizeArray<string> = nativeOnly

[<Erase>]
type IExports =
    /// The concatenation of ascii_lowercase and ascii_uppercase
    abstract ascii_letters: string
    /// The lowercase letters 'abcdefghijklmnopqrstuvwxyz'
    abstract ascii_lowercase: string
    /// The uppercase letters 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
    abstract ascii_uppercase: string
    /// The string '0123456789'
    abstract digits: string
    /// The string '0123456789abcdefABCDEF'
    abstract hexdigits: string
    /// The string '01234567'
    abstract octdigits: string
    /// String of ASCII characters considered punctuation
    abstract punctuation: string
    /// String of ASCII characters considered printable
    abstract printable: string
    /// String containing all ASCII whitespace characters
    abstract whitespace: string

    /// Split the argument into words, capitalize each word, and join them
    abstract capwords: s: string -> string
    /// Split the argument into words using sep, capitalize each word, and join them
    abstract capwords: s: string * sep: string -> string

/// Python string module
[<ImportAll("string")>]
let pyString: IExports = nativeOnly
