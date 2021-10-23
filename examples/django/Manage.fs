#!/usr/bin/env python

module Django.Manage

//Django command-line utility for administrative tasks.

open Fable.Core


type IEnviron =
    [<Emit("$0.setdefault($1,$2)")>]
    abstract setdefault : string * string -> unit
    
[<ImportMember("os")>]
let environ: IEnviron = nativeOnly

    
[<ImportMember("django.core.management")>]
let  execute_from_command_line: string [] -> int = nativeOnly

[<EntryPoint>]
let main argv =
    environ.setdefault("DJANGO_SETTINGS_MODULE", "tproj.settings")
    execute_from_command_line argv
