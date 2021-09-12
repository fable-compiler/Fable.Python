module Fable.Python.Builtins

open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type TextIOBase =
    abstract read : unit -> string
    abstract read : __size: int -> string

type TextIOWrapper =
    inherit TextIOBase

[<StringEnum>]
type OpenTextModeUpdating =
    | [<CompiledName("r+")>] ReadUpdate
    | [<CompiledName("+r")>] UpdateRead
    | [<CompiledName("rt+")>] ReadTextUpdate
    | [<CompiledName("r+t")>] ReadUpdateText
    | [<CompiledName("+rt")>] UpdateReadText
    | [<CompiledName("tr+")>] TextReadUpdate
    | [<CompiledName("t+r")>] TextUpdateRead
    | [<CompiledName("+tr")>] UpdateTextRead
    | [<CompiledName("w+")>] WriteUpdate
    | [<CompiledName("+w")>] UpdateWrite
    | [<CompiledName("wt+")>] WriteTextUpdate
    | [<CompiledName("w+t")>] WriteUpdateText
    | [<CompiledName("+wt")>] UpdateWriteText
    | [<CompiledName("tw+")>] TextWriteUpdate
    | [<CompiledName("t+w")>] TextUpdateWrite
    | [<CompiledName("+tw")>] UpdateTextWrite
    | [<CompiledName("a+")>] AppendUpdate
    | [<CompiledName("+a")>] UpdateAppend
    | [<CompiledName("at+")>] AppendTextUpdate
    | [<CompiledName("a+t")>] AppendUpdateText
    | [<CompiledName("+at")>] UpdateAppendText
    | [<CompiledName("ta+")>] TextAppendUpdate
    | [<CompiledName("t+a")>] TextUpdateAppend
    | [<CompiledName("+ta")>] UpdateTextAppend
    | [<CompiledName("x+")>] CreateUpdate
    | [<CompiledName("+x")>] UpdateCreate
    | [<CompiledName("xt+")>] CreateTextUpdate
    | [<CompiledName("x+t")>] CreateUpdateText
    | [<CompiledName("+xt")>] UpdateCreateText
    | [<CompiledName("tx+")>] TextCreateUpdate
    | [<CompiledName("t+x")>] TextUpdateCreate
    | [<CompiledName("+tx")>] UpdateTextCreate

[<StringEnum>]
type OpenTextModeReading =
    | [<CompiledName("rt")>] Read
    | [<CompiledName("rt")>] ReadText
    | [<CompiledName("tr")>] TextRead

[<StringEnum>]
type OpenTextModeWriting =
    | [<CompiledName("w")>] Write
    | [<CompiledName("wt")>] WriteText
    | [<CompiledName("tw")>] TextWrite
    | [<CompiledName("a")>] Append
    | [<CompiledName("at")>] AppendText
    | [<CompiledName("ta")>] TextAppend
    | [<CompiledName("x")>] Create
    | [<CompiledName("xt")>] CreateText
    | [<CompiledName("tx")>] TextCreate

[<Erase>]
type OpenTextMode =
    | OpenTextModeUpdating
    | OpenTextModeWriting
    | OpenTextModeReading


[<Erase>]
type _OpenFile =
    | StrOrBytesPath of string
    | FileDescriptor of int

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
    abstract print : obj: obj -> unit
    abstract read : file: _OpenFile -> TextIOWrapper
    abstract read : file: _OpenFile * mode: OpenTextMode -> TextIOWrapper
    abstract read : file: _OpenFile * mode: OpenTextMode * buffering: int -> TextIOWrapper
    abstract read : file: _OpenFile * mode: OpenTextMode * buffering: int * encoding: string -> TextIOWrapper

    abstract read :
        file: _OpenFile * mode: OpenTextMode * buffering: int * encoding: string * errors: string -> TextIOWrapper

    abstract read :
        file: _OpenFile * mode: OpenTextMode * buffering: int * encoding: string * errors: string * newline: string ->
        TextIOWrapper

[<ImportDefault("builtins")>]
let builtins: IExports = nativeOnly
