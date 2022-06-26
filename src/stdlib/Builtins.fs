/// Type bindings for Python builtins: https://docs.python.org/3/library/functions.html#built-in-funcs
module Fable.Python.Builtins

open System
open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type TextIOBase =
    abstract read : unit -> string
    abstract read : __size: int -> string

type TextIOWrapper =
    inherit IDisposable
    inherit TextIOBase

module OpenTextMode =
    let [<Literal>] ReadUpdate = "r+"
    let [<Literal>] UpdateRead = "+r"
    let [<Literal>] ReadTextUpdate = "rt+"
    let [<Literal>] ReadUpdateText = "r+t"
    let [<Literal>] UpdateReadText = "+rt"
    let [<Literal>] TextReadUpdate = "tr+"
    let [<Literal>] TextUpdateRead = "t+r"
    let [<Literal>] UpdateTextRead = "+tr"
    let [<Literal>] WriteUpdate = "w+"
    let [<Literal>] UpdateWrite = "+w"
    let [<Literal>] WriteTextUpdate = "wt+"
    let [<Literal>] WriteUpdateText = "w+t"
    let [<Literal>] UpdateWriteText = "+wt"
    let [<Literal>] TextWriteUpdate = "tw+"
    let [<Literal>] TextUpdateWrite = "t+w"
    let [<Literal>] UpdateTextWrite = "+tw"
    let [<Literal>] AppendUpdate = "a+"
    let [<Literal>] UpdateAppend = "+a"
    let [<Literal>] AppendTextUpdate = "at+"
    let [<Literal>] AppendUpdateText = "a+t"
    let [<Literal>] UpdateAppendText = "+at"
    let [<Literal>] TextAppendUpdate = "ta+"
    let [<Literal>] TextUpdateAppend = "t+a"
    let [<Literal>] UpdateTextAppend = "+ta"
    let [<Literal>] CreateUpdate = "x+"
    let [<Literal>] UpdateCreate = "+x"
    let [<Literal>] CreateTextUpdate = "xt+"
    let [<Literal>] CreateUpdateText = "x+t"
    let [<Literal>] UpdateCreateText = "+xt"
    let [<Literal>] TextCreateUpdate = "tx+"
    let [<Literal>] TextUpdateCreate = "t+x"
    let [<Literal>] UpdateTextCreate = "+tx"

    let [<Literal>] Read = "rt"
    let [<Literal>] ReadText = "rt"
    let [<Literal>] TextRead = "tr"

    let [<Literal>] Write = "w"
    let [<Literal>] WriteText = "wt"
    let [<Literal>] TextWrite = "tw"
    let [<Literal>] Append = "a"
    let [<Literal>] AppendText = "at"
    let [<Literal>] TextAppend = "ta"
    let [<Literal>] Create = "x"
    let [<Literal>] CreateText = "xt"
    let [<Literal>] TextCreate = "tx"


type _Opener = Tuple<string, int> -> int

type IExports =
    /// Return the absolute value of the argument.
    abstract abs : int -> int
    /// Return the absolute value of the argument.
    abstract abs : float -> float
    /// Return a Unicode string of one character with ordinal i; 0 <= i <= 0x10ffff.
    abstract chr : int -> char
    /// Return the names in the current scope.
    abstract dir : unit -> string list
    /// Return an alphabetized list of names comprising (some of) the
    /// attributes of the given object, and of attributes reachable from
    /// it
    abstract dir : obj -> string list
    /// Return the identity of an object.
    abstract id : obj -> int
    ///Return the length (the number of items) of an object. The argument may
    ///be a sequence (such as a string, bytes, tuple, list, or range) or a
    ///collection (such as a dictionary, set, or frozen set).
    abstract len : obj -> int
    /// Make an iterator that computes the function using arguments from
    /// the iterable.  Stops when iterable is exhausted.
    abstract map : ('T1 -> 'T2) * IEnumerable<'T1> -> IEnumerable<'T2>
    /// Make an iterator that computes the function using arguments from each
    /// of the iterables.  Stops when the shortest iterable is exhausted.
    abstract map : ('T1 * 'T2 -> 'T3) * IEnumerable<'T1> * IEnumerable<'T2> -> IEnumerable<'T3>
    /// Make an iterator that computes the function using arguments from each
    /// of the iterables.  Stops when the shortest iterable is exhausted.
    abstract map : ('T1 * 'T2 * 'T3 -> 'T4) * IEnumerable<'T1> * IEnumerable<'T2> * IEnumerable<'T3> -> IEnumerable<'T4>
    /// Return the Unicode code point for a one-character string.
    abstract ord : char -> int
    /// Object to string
    abstract str : obj -> string
    /// Object to int
    abstract int : obj -> int
    /// Object to float
    abstract float : obj -> float
    abstract print : obj: obj -> unit

    [<NamedParams(fromIndex=1)>] abstract ``open`` : file: int * ?mode: string * ?buffering: int * ?encoding: string * ?errors: string * ?newline: string * ?closefd: bool * ?opener: _Opener -> TextIOWrapper
    [<NamedParams(fromIndex=1)>] abstract ``open`` : file: string * ?mode: string * ?buffering: int * ?encoding: string * ?errors: string * ?newline: string * ?closefd: bool * ?opener: _Opener -> TextIOWrapper

[<ImportAll("builtins")>]
let builtins: IExports = nativeOnly

// NOTE: Below we can add builtins that don't require overloads, and do not
// conflict with common F# or .NET functions. If they do we keep them in
// `IExports` above.

[<Emit("__name__")>]
let __name__: string = nativeOnly

/// Python print function. Takes a single argument, so can be used with e.g string interpolation.
let print obj = builtins.print obj
