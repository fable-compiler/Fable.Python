module Fable.Python.Sys

open Fable.Core

// fsharplint:disable MemberNames

[<StringEnum>]
type ByteOrder =
    | Little
    | Big

type VersionInfo =
    abstract major: int
    abstract minor: int
    abstract micro: int
    abstract releaselevel: string
    abstract serial: int

[<Erase>]
type IExports =
    abstract argv: string array
    abstract byteorder: ByteOrder
    abstract hexversion: int
    abstract maxsize: int
    abstract maxunicode: int
    abstract path: string array
    abstract platform: string
    abstract prefix: string
    abstract version: string
    abstract version_info: VersionInfo
    /// Exits with code 0, indicating success
    /// See https://docs.python.org/3/library/sys.html#sys.exit
    abstract exit: unit -> 'a
    /// Exits with provided status
    /// See https://docs.python.org/3/library/sys.html#sys.exit
    abstract exit: status: int -> 'a
    /// Exits with exit status 1, printing message to stderr
    /// See https://docs.python.org/3/library/sys.html#sys.exit
    abstract exit: message: string -> 'a

/// System-specific parameters and functions
[<ImportAll("sys")>]
let sys: IExports = nativeOnly
