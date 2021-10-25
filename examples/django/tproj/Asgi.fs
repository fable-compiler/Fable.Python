module Django.tproj.Asgi

open Fable.Core
//ASGI config for testproj project.
//
//It exposes the ASGI callable as a module-level variable named ``application``.
//
//For more information on this file, see
//https://docs.djangoproject.com/en/3.2/howto/deployment/asgi/

type IEnviron =
    [<Emit("$0.setdefault($1,$2)")>]
    abstract setdefault : string * string -> unit
    
[<ImportMember("os")>]
let environ: IEnviron = nativeOnly
    
type IASGI =
    [<Emit("$0.get_asgi_application()")>]
    abstract get_asgi_application: unit -> unit
    
[<ImportMember("django.core")>]
let asgi: IASGI= nativeOnly

environ.setdefault("DJANGO_SETTINGS_MODULE", "tproj.settings")
let application = asgi.get_asgi_application()