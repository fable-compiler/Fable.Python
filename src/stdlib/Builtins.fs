/// Type bindings for Python builtins: https://docs.python.org/3/library/functions.html#built-in-funcs
module Fable.Python.Builtins

open System
open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type TextIOBase =
    abstract read: unit -> string
    abstract read: __size: int -> string
    abstract write: __s: string -> int
    abstract writelines: __lines: string seq -> unit
    abstract readline: __size: int -> string
    abstract readlines: __hint: int -> string list
    abstract tell: unit -> int

type TextIOWrapper =
    inherit IDisposable
    inherit TextIOBase

module OpenTextMode =
    [<Literal>]
    let ReadUpdate = "r+"

    [<Literal>]
    let UpdateRead = "+r"

    [<Literal>]
    let ReadTextUpdate = "rt+"

    [<Literal>]
    let ReadUpdateText = "r+t"

    [<Literal>]
    let UpdateReadText = "+rt"

    [<Literal>]
    let TextReadUpdate = "tr+"

    [<Literal>]
    let TextUpdateRead = "t+r"

    [<Literal>]
    let UpdateTextRead = "+tr"

    [<Literal>]
    let WriteUpdate = "w+"

    [<Literal>]
    let UpdateWrite = "+w"

    [<Literal>]
    let WriteTextUpdate = "wt+"

    [<Literal>]
    let WriteUpdateText = "w+t"

    [<Literal>]
    let UpdateWriteText = "+wt"

    [<Literal>]
    let TextWriteUpdate = "tw+"

    [<Literal>]
    let TextUpdateWrite = "t+w"

    [<Literal>]
    let UpdateTextWrite = "+tw"

    [<Literal>]
    let AppendUpdate = "a+"

    [<Literal>]
    let UpdateAppend = "+a"

    [<Literal>]
    let AppendTextUpdate = "at+"

    [<Literal>]
    let AppendUpdateText = "a+t"

    [<Literal>]
    let UpdateAppendText = "+at"

    [<Literal>]
    let TextAppendUpdate = "ta+"

    [<Literal>]
    let TextUpdateAppend = "t+a"

    [<Literal>]
    let UpdateTextAppend = "+ta"

    [<Literal>]
    let CreateUpdate = "x+"

    [<Literal>]
    let UpdateCreate = "+x"

    [<Literal>]
    let CreateTextUpdate = "xt+"

    [<Literal>]
    let CreateUpdateText = "x+t"

    [<Literal>]
    let UpdateCreateText = "+xt"

    [<Literal>]
    let TextCreateUpdate = "tx+"

    [<Literal>]
    let TextUpdateCreate = "t+x"

    [<Literal>]
    let UpdateTextCreate = "+tx"

    [<Literal>]
    let Read = "rt"

    [<Literal>]
    let ReadText = "rt"

    [<Literal>]
    let TextRead = "tr"

    [<Literal>]
    let Write = "w"

    [<Literal>]
    let WriteText = "wt"

    [<Literal>]
    let TextWrite = "tw"

    [<Literal>]
    let Append = "a"

    [<Literal>]
    let AppendText = "at"

    [<Literal>]
    let TextAppend = "ta"

    [<Literal>]
    let Create = "x"

    [<Literal>]
    let CreateText = "xt"

    [<Literal>]
    let TextCreate = "tx"


type _Opener = Tuple<string, int> -> int

[<Erase>]
type IExports =
    /// Return the absolute value of the argument.
    abstract abs: int -> int
    /// Return the absolute value of the argument.
    abstract abs: float -> float
    /// Return a Unicode string of one character with ordinal i; 0 <= i <= 0x10ffff.
    abstract chr: int -> char
    /// Return the names in the current scope.
    abstract dir: unit -> string list

    /// Return an alphabetized list of names comprising (some of) the
    /// attributes of the given object, and of attributes reachable from
    /// it
    abstract dir: obj -> string list

    /// Return the identity of an object.
    abstract id: obj -> int

    ///Return the length (the number of items) of an object. The argument may
    ///be a sequence (such as a string, bytes, tuple, list, or range) or a
    ///collection (such as a dictionary, set, or frozen set).
    abstract len: obj -> int

    /// Make an iterator that computes the function using arguments from
    /// the iterable.  Stops when iterable is exhausted.
    abstract map: ('T1 -> 'T2) * IEnumerable<'T1> -> IEnumerable<'T2>

    /// Make an iterator that computes the function using arguments from each
    /// of the iterables.  Stops when the shortest iterable is exhausted.
    abstract map: ('T1 * 'T2 -> 'T3) * IEnumerable<'T1> * IEnumerable<'T2> -> IEnumerable<'T3>

    /// Make an iterator that computes the function using arguments from each
    /// of the iterables.  Stops when the shortest iterable is exhausted.
    abstract map: ('T1 * 'T2 * 'T3 -> 'T4) * IEnumerable<'T1> * IEnumerable<'T2> * IEnumerable<'T3> -> IEnumerable<'T4>

    /// Return the Unicode code point for a one-character string.
    abstract ord: char -> int
    /// Object to string
    abstract str: obj -> string
    /// Object to int
    abstract int: obj -> int
    /// Object to float
    abstract float: obj -> float
    /// Convert to bytes
    abstract bytes: byte[] -> byte[]
    /// Convert string to bytes with encoding
    abstract bytes: string * encoding: string -> byte[]

    /// Return the largest item in an iterable or the largest of two or more arguments.
    abstract max: 'T * 'T -> 'T
    /// Return the largest item in an iterable or the largest of two or more arguments.
    abstract max: 'T * 'T * 'T -> 'T
    /// Return the largest item in an iterable or the largest of two or more arguments.
    abstract max: IEnumerable<'T> -> 'T

    /// Return the smallest item in an iterable or the smallest of two or more arguments.
    abstract min: 'T * 'T -> 'T
    /// Return the smallest item in an iterable or the smallest of two or more arguments.
    abstract min: 'T * 'T * 'T -> 'T
    /// Return the smallest item in an iterable or the smallest of two or more arguments.
    abstract min: IEnumerable<'T> -> 'T

    /// Return the sum of a 'start' value (default: 0) plus an iterable of numbers.
    abstract sum: IEnumerable<'T> -> 'T

    /// Return True if bool(x) is True for all values x in the iterable.
    abstract all: IEnumerable<bool> -> bool

    /// Return True if bool(x) is True for any x in the iterable.
    abstract any: IEnumerable<bool> -> bool

    abstract print: obj: obj -> unit

    /// Print with custom end string (default is newline)
    [<NamedParams(fromIndex = 1)>]
    abstract print: obj: obj * ``end``: string -> unit

    /// Print with custom separator and end string
    [<NamedParams(fromIndex = 1)>]
    abstract print: obj: obj * sep: string * ``end``: string -> unit

    /// Print to a file-like object (e.g., sys.stderr)
    [<NamedParams(fromIndex = 1)>]
    abstract print: obj: obj * file: TextIOBase -> unit

    /// Print to a file-like object with custom end string
    [<NamedParams(fromIndex = 1)>]
    abstract print: obj: obj * ``end``: string * file: TextIOBase -> unit

    [<NamedParams(fromIndex = 1)>]
    abstract ``open``:
        file: int *
        ?mode: string *
        ?buffering: int *
        ?encoding: string *
        ?errors: string *
        ?newline: string *
        ?closefd: bool *
        ?opener: _Opener ->
            TextIOWrapper

    [<NamedParams(fromIndex = 1)>]
    abstract ``open``:
        file: string *
        ?mode: string *
        ?buffering: int *
        ?encoding: string *
        ?errors: string *
        ?newline: string *
        ?closefd: bool *
        ?opener: _Opener ->
            TextIOWrapper

[<ImportAll("builtins")>]
let builtins: IExports = nativeOnly

// ============================================================================
// Python Built-in Exceptions
// https://docs.python.org/3/library/exceptions.html
// ============================================================================

// BaseException subclasses (not Exception subclasses)
// These require special handling in try/catch as they don't inherit from Exception

/// Raised when the user hits the interrupt key (normally Control-C or Delete).
/// Note: Inherits from BaseException, not Exception.
/// https://docs.python.org/3/library/exceptions.html#KeyboardInterrupt
[<Import("KeyboardInterrupt", "builtins")>]
type KeyboardInterrupt() =
    inherit exn()

/// Raised by the sys.exit() function.
/// Note: Inherits from BaseException, not Exception.
/// https://docs.python.org/3/library/exceptions.html#SystemExit
[<Import("SystemExit", "builtins")>]
type SystemExit() =
    inherit exn()

/// Raised when a generator or coroutine is closed.
/// Note: Inherits from BaseException, not Exception.
/// https://docs.python.org/3/library/exceptions.html#GeneratorExit
[<Import("GeneratorExit", "builtins")>]
type GeneratorExit() =
    inherit exn()

// Exception subclasses

/// Raised when a system function returns a system-related error.
/// https://docs.python.org/3/library/exceptions.html#OSError
[<Import("OSError", "builtins")>]
type OSError() =
    inherit exn()

/// Raised when an operation runs out of memory.
/// https://docs.python.org/3/library/exceptions.html#MemoryError
[<Import("MemoryError", "builtins")>]
type MemoryError() =
    inherit exn()

/// Raised when the interpreter finds an internal error.
/// https://docs.python.org/3/library/exceptions.html#SystemError
[<Import("SystemError", "builtins")>]
type SystemError() =
    inherit exn()

// NOTE: Below we can add builtins that don't require overloads, and do not
// conflict with common F# or .NET functions. If they do we keep them in
// `IExports` above.

[<Emit("__name__")>]
let __name__: string = nativeOnly

/// Python print function. Takes a single argument, so can be used with e.g string interpolation.
let print obj = builtins.print obj
