/// Type bindings for Python sys module: https://docs.python.org/3/library/sys.html
module Fable.Python.Sys

open Fable.Core
open Fable.Python.Builtins

// fsharplint:disable MemberNames

/// System byte order (endianness)
[<StringEnum>]
[<RequireQualifiedAccess>]
type ByteOrder =
    | Little
    | Big

/// Python version information tuple
type VersionInfo =
    /// Major version number
    abstract major: int
    /// Minor version number
    abstract minor: int
    /// Micro version number (patch level)
    abstract micro: int
    /// Release level (e.g., 'final', 'alpha', 'beta')
    abstract releaselevel: string
    /// Serial number of the release
    abstract serial: int

[<Erase>]
type IExports =
    /// Command line arguments passed to the Python script
    abstract argv: ResizeArray<string>
    /// Native byte order (endianness) of the system
    abstract byteorder: ByteOrder
    /// Version information encoded as a single integer
    abstract hexversion: int
    /// Maximum value a variable of type int can take
    abstract maxsize: int
    /// Maximum Unicode code point value
    abstract maxunicode: int
    /// Module search path - list of directory names where Python looks for modules
    abstract path: ResizeArray<string>
    /// Platform identifier string
    abstract platform: string
    /// Site-specific directory prefix where platform-independent Python files are installed
    abstract prefix: string
    /// Python version as a string
    abstract version: string
    /// Python version information as a tuple
    abstract version_info: VersionInfo
    /// Standard input stream
    /// See https://docs.python.org/3/library/sys.html#sys.stdin
    abstract stdin: TextIOBase
    /// Standard output stream
    /// See https://docs.python.org/3/library/sys.html#sys.stdout
    abstract stdout: TextIOBase
    /// Standard error stream
    /// See https://docs.python.org/3/library/sys.html#sys.stderr
    abstract stderr: TextIOBase
    /// Exits with code 0, indicating success
    /// See https://docs.python.org/3/library/sys.html#sys.exit
    abstract exit: unit -> 'a
    /// Exits with provided status
    /// See https://docs.python.org/3/library/sys.html#sys.exit
    [<Emit("$0.exit(int($1))")>]
    abstract exit: status: int -> 'a
    /// Exits with provided status
    /// See https://docs.python.org/3/library/sys.html#sys.exit
    abstract exit: status: nativeint -> 'a
    /// Exits with exit status 1, printing message to stderr
    /// See https://docs.python.org/3/library/sys.html#sys.exit
    abstract exit: message: string -> 'a

/// System-specific parameters and functions
[<ImportAll("sys")>]
let sys: IExports = nativeOnly
