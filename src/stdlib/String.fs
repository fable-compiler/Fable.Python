module Fable.Python.String

open System
open Fable.Core

// fsharplint:disable MemberNames

type System.String with
    [<Emit("$0.format($1...)")>]
    member self.format([<ParamArray>] args: Object[]) = nativeOnly
