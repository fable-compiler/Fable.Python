module Django.tproj.Wsgi

open Fable.Core
//WSGI config for testproj project.
//
//It exposes the WSGI callable as a module-level variable named ``application``.
//
//For more information on this file, see
//https://docs.djangoproject.com/en/3.2/howto/deployment/wsgi/



type IEnviron =
    [<Emit("$0.setdefault($1,$2)")>]
    abstract setdefault : string * string -> unit
    
[<ImportMember("os")>]
let environ: IEnviron = nativeOnly
    
type IWSGI =
    [<Emit("$0.get_wsgi_application()")>]
    abstract get_wsgi_application: unit -> unit
    
[<ImportMember("django.core")>]
let wsgi: IWSGI= nativeOnly

environ.setdefault("DJANGO_SETTINGS_MODULE", "tproj.settings")
let application = wsgi.get_wsgi_application()
