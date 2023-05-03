module Fable.Python.Time

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    abstract altzone: int
    abstract ctime: unit -> string
    abstract ctime: float -> string
    abstract daylight: int
    abstract monotonic: unit -> float
    abstract perf_counter: unit -> float
    abstract process_time: unit -> float
    abstract sleep: secs: float -> unit
    abstract time: unit -> float
    abstract timezone: int
    abstract tzname: string * string

/// Time access and conversions
[<ImportAll("time")>]
let time: IExports = nativeOnly
