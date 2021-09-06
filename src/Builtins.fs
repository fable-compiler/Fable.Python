module Fable.Python.Builtins

open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type Builtins =
    abstract print : obj: obj -> unit
    abstract map : ('T1 -> 'T2) * IEnumerable<'T1> -> IEnumerable<'T2>
    abstract map : ('T1 * 'T2 -> 'T3) * IEnumerable<'T1> * IEnumerable<'T2> -> IEnumerable<'T3>
    abstract map : ('T1 * 'T2 * 'T3 -> 'T4) * IEnumerable<'T1> * IEnumerable<'T2> * IEnumerable<'T3> -> IEnumerable<'T4>

let builtins: Builtins = nativeOnly
