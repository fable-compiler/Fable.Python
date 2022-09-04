// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
namespace Giraffe

open System.Text
open System.Threading.Tasks

type HttpFuncResult = Task<HttpContext option>

type HttpFunc = HttpContext -> HttpFuncResult

type HttpHandler = HttpFunc -> HttpFunc


[<AutoOpen>]
module Core =
    let earlyReturn: HttpFunc = Some >> Task.FromResult
    let skipPipeline () : HttpFuncResult = Task.FromResult None

    let compose (handler1: HttpHandler) (handler2: HttpHandler) : HttpHandler =
        fun (final: HttpFunc) ->
            let func = final |> handler2 |> handler1

            fun (ctx: HttpContext) ->
                match ctx.Response.HasStarted with
                | true -> final ctx
                | false -> func ctx

    let (>=>) = compose

    /// <summary>
    /// Iterates through a list of `HttpFunc` functions and returns the result of the first `HttpFunc` of which the outcome is `Some HttpContext`.
    /// </summary>
    /// <param name="funcs"></param>
    /// <param name="ctx"></param>
    /// <returns>A <see cref="HttpFuncResult"/>.</returns>
    let rec private chooseHttpFunc (funcs: HttpFunc list) : HttpFunc =
        fun (ctx: HttpContext) ->
            task {
                match funcs with
                | [] -> return None
                | func :: tail ->
                    let! result = func ctx

                    match result with
                    | Some c -> return Some c
                    | None -> return! chooseHttpFunc tail ctx
            }

    /// <summary>
    /// Iterates through a list of <see cref="HttpHandler"/> functions and returns the result of the first <see cref="HttpHandler"/> of which the outcome is Some HttpContext.
    /// Please mind that all <see cref="HttpHandler"/> functions will get pre-evaluated at runtime by applying the next (HttpFunc) parameter to each handler.
    /// </summary>
    /// <param name="handlers"></param>
    /// <param name="next"></param>
    /// <returns>A <see cref="HttpFunc"/>.</returns>
    let choose (handlers: HttpHandler list) : HttpHandler =
        fun (next: HttpFunc) ->
            let funcs = handlers |> List.map (fun h -> h next)
            fun (ctx: HttpContext) -> chooseHttpFunc funcs ctx


    let text (str: string) : HttpHandler =
        let bytes = Encoding.UTF8.GetBytes str

        fun (_: HttpFunc) (ctx: HttpContext) ->
            ctx.SetContentType "text/plain; charset=utf-8"
            ctx.WriteBytesAsync bytes


    /// <summary>
    /// Filters an incoming HTTP request based on the HTTP verb.
    /// </summary>
    /// <param name="validate">A validation function which checks for a single HTTP verb.</param>
    /// <param name="next"></param>
    /// <param name="ctx"></param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    let private httpVerb (validate: string -> bool) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            if validate ctx.Request.Method then
                next ctx
            else
                skipPipeline ()

    let GET: HttpHandler = httpVerb HttpMethods.IsGet
    let POST: HttpHandler = httpVerb HttpMethods.IsPost
    let PUT: HttpHandler = httpVerb HttpMethods.IsPut
    let PATCH: HttpHandler = httpVerb HttpMethods.IsPatch
    let DELETE: HttpHandler = httpVerb HttpMethods.IsDelete
    let HEAD: HttpHandler = httpVerb HttpMethods.IsHead
    let OPTIONS: HttpHandler = httpVerb HttpMethods.IsOptions
    let TRACE: HttpHandler = httpVerb HttpMethods.IsTrace
    let CONNECT: HttpHandler = httpVerb HttpMethods.IsConnect

    let GET_HEAD: HttpHandler = choose [ GET; HEAD ]