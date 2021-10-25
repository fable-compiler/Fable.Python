module Fable.Python.Sys

open Fable.Core

// fsharplint:disable MemberNames

[<StringEnum>]
type ByteOrder =
    | Little
    | Big

type VersionInfo =
    abstract major : int
    abstract minor: int
    abstract micro: int
    abstract releaselevel: string
    abstract serial: int

type IExports =
    abstract argv : string array
    abstract byteorder: ByteOrder
    abstract hexversion: int
    abstract maxsize: int
    abstract maxunicode: int
    abstract path : string array
    abstract platform : string
    abstract prefix: string
    abstract version: string
    abstract version_info: VersionInfo

/// System-specific parameters and functions
[<ImportAll("sys")>]
let sys: IExports = nativeOnly
