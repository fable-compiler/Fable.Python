module Fable.Python.String

open System
open Fable.Core

// fsharplint:disable MemberNames

type System.String with
    [<Emit("$0.format($1...)")>]
    member _.format([<ParamArray>] args: Object []) = nativeOnly
