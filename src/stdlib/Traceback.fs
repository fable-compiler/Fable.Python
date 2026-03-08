/// Type bindings for Python traceback module: https://docs.python.org/3/library/traceback.html
module Fable.Python.Traceback

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Print exception information and stack trace entries
    /// See https://docs.python.org/3/library/traceback.html#traceback.print_exc
    abstract print_exc: unit -> unit
    /// Format exception information and stack trace entries as a string
    /// See https://docs.python.org/3/library/traceback.html#traceback.format_exc
    abstract format_exc: unit -> string
    /// Print stack trace entries
    /// See https://docs.python.org/3/library/traceback.html#traceback.print_stack
    abstract print_stack: unit -> unit
    /// Format stack trace entries as a list of strings
    /// See https://docs.python.org/3/library/traceback.html#traceback.format_stack
    abstract format_stack: unit -> ResizeArray<string>

/// Extract, format and print exceptions and their tracebacks
[<ImportAll("traceback")>]
let traceback: IExports = nativeOnly
