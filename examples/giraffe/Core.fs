// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
namespace Giraffe

open System.Text
open System.Threading.Tasks

type HttpFuncResult = Task<HttpContext option>

type HttpFunc = HttpContext -> HttpFuncResult

type HttpHandler = HttpFunc -> HttpFunc


[<AutoOpen>]
module Core =
    let earlyReturn : HttpFunc = Some >> Task.FromResult
    let skipPipeline () : HttpFuncResult = Task.FromResult None

    let compose (handler1 : HttpHandler) (handler2 : HttpHandler) : HttpHandler =
        fun (final : HttpFunc) ->
            final |> handler2 |> handler1

    let (>=>) = compose

    let text (str : string) : HttpHandler =
        let bytes = Encoding.UTF8.GetBytes str
        fun (_ : HttpFunc) (ctx : HttpContext) ->
            ctx.SetContentType "text/plain; charset=utf-8"
            ctx.WriteBytesAsync bytes

