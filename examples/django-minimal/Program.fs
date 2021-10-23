// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module Program

open System
open System.Collections.Generic
open Fable.Core
open Fable.Python
open Fable.Core.PyInterop

type ISettings =
    [<Emit("$0.configure(DEBUG=$1, ROOT_URLCONF=$2)")>]
    abstract configure : DEBUG: bool * ROOT_URLCONF : obj -> unit


[<ImportMember("django.conf")>]
let settings: ISettings = nativeOnly
type IPath =
    abstract path : url: string * view: obj -> obj
    
[<ImportMember("django")>]
let urls: IPath = nativeOnly


[<ImportMember("django.http")>]
let HttpResponse: string -> obj = nativeOnly


[<ImportMember("django.core.management")>]
let execute_from_command_line: string [] -> int = nativeOnly

[<Emit("sys.modules[__name__]")>]
let sysModules : unit -> obj = nativeOnly

settings.configure (
    DEBUG=true,
    ROOT_URLCONF = sysModules
)

let index request =
    HttpResponse("<h1>A minimal Django response!</h1>")

let urlpatterns = [|
    urls.path ("",index)
|]

[<EntryPoint>]
let main argv =
    execute_from_command_line (argv)
