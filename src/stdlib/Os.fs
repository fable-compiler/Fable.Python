module Fable.Python.Os

open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames

type IExports =
    abstract chdir : string -> unit
    abstract chroot : string -> unit
    abstract close : fd: int -> unit
    abstract environ : Dictionary<string, string>
    abstract getcwd : unit -> string
    abstract getenv : key: string -> string option
    abstract getenv : key: string * ``default``: string -> string
    abstract kill : pid: int * ``sig``: int -> unit
    abstract putenv : key: string * value: string -> unit


/// Miscellaneous operating system interfaces
[<ImportAll("os")>]
let os: IExports = nativeOnly
