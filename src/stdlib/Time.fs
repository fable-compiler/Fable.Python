/// Type bindings for Python time module: https://docs.python.org/3/library/time.html
module Fable.Python.Time

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// The offset of the local DST timezone, in seconds west of UTC
    /// See https://docs.python.org/3/library/time.html#time.altzone
    abstract altzone: int
    /// Convert a time expressed in seconds since the epoch to a string
    /// See https://docs.python.org/3/library/time.html#time.ctime
    abstract ctime: unit -> string
    /// Convert a time expressed in seconds since the epoch to a string
    /// See https://docs.python.org/3/library/time.html#time.ctime
    abstract ctime: secs: float -> string
    /// Nonzero if a DST timezone is defined
    /// See https://docs.python.org/3/library/time.html#time.daylight
    abstract daylight: int
    /// Return the value of a monotonic clock (in fractional seconds)
    /// See https://docs.python.org/3/library/time.html#time.monotonic
    abstract monotonic: unit -> float
    /// Return the value of a performance counter (in fractional seconds)
    /// See https://docs.python.org/3/library/time.html#time.perf_counter
    abstract perf_counter: unit -> float
    /// Return the sum of the system and user CPU time (in fractional seconds)
    /// See https://docs.python.org/3/library/time.html#time.process_time
    abstract process_time: unit -> float
    /// Suspend execution of the calling thread for the given number of seconds
    /// See https://docs.python.org/3/library/time.html#time.sleep
    abstract sleep: secs: float -> unit
    /// Return the time in seconds since the epoch as a floating point number
    /// See https://docs.python.org/3/library/time.html#time.time
    abstract time: unit -> float
    /// The offset of the local (non-DST) timezone, in seconds west of UTC
    /// See https://docs.python.org/3/library/time.html#time.timezone
    abstract timezone: int
    /// A tuple of two strings: the first is the name of the local non-DST timezone, the second is the name of the local DST timezone
    /// See https://docs.python.org/3/library/time.html#time.tzname
    abstract tzname: string * string

/// Time access and conversions
[<ImportAll("time")>]
let time: IExports = nativeOnly
