/// Type bindings for Python html module: https://docs.python.org/3/library/html.html
module Fable.Python.Html

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Convert characters &, <, >, ", ' to HTML-safe sequences
    /// See https://docs.python.org/3/library/html.html#html.escape
    abstract escape: s: string -> string
    /// Convert characters with optional quote handling
    /// See https://docs.python.org/3/library/html.html#html.escape
    abstract escape: s: string * quote: bool -> string
    /// Convert HTML entities to their corresponding characters
    /// See https://docs.python.org/3/library/html.html#html.unescape
    abstract unescape: s: string -> string

/// HTML escape and unescape utilities
[<ImportAll("html")>]
let html: IExports = nativeOnly
