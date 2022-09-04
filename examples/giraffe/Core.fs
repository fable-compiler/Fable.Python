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
    /// The warbler function is a <see cref="HttpHandler"/> wrapper function which prevents a <see cref="HttpHandler"/> to be pre-evaluated at startup.
    /// </summary>
    /// <param name="f">A function which takes a HttpFunc * HttpContext tuple and returns a <see cref="HttpHandler"/> function.</param>
    /// <param name="next"></param>
    /// <param name="source"></param>
    /// <example>
    /// <code>
    /// warbler(fun _ -> someHttpHandler)
    /// </code>
    /// </example>
    /// <returns>Returns a <see cref="HttpHandler"/> function.</returns>
    let inline warbler f (source: HttpHandler) (next: HttpFunc) =
        fun (ctx: HttpContext) -> f (next, ctx) id next ctx
        |> source

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

    /// <summary>
    /// Sets the HTTP status code of the response.
    /// </summary>
    /// <param name="statusCode">The status code to be set in the response. For convenience you can use the static <see cref="Microsoft.AspNetCore.Http.StatusCodes"/> class for passing in named status codes instead of using pure int values.</param>
    /// <param name="next"></param>
    /// <param name="ctx"></param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    let setStatusCode (statusCode: int) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            ctx.SetStatusCode statusCode
            next ctx


    /// <summary>
    /// Adds or sets a HTTP header in the response.
    /// </summary>
    /// <param name="key">The HTTP header name. For convenience you can use the static <see cref="Microsoft.Net.Http.Headers.HeaderNames"/> class for passing in strongly typed header names instead of using pure string values.</param>
    /// <param name="value">The value to be set. Non string values will be converted to a string using the object's ToString() method.</param>
    /// <param name="next"></param>
    /// <param name="ctx"></param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    let setHttpHeader (key: string) (value: obj) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            ctx.SetHttpHeader(key, value)
            next ctx

    /// <summary>
    /// Serializes an object to JSON and writes the output to the body of the HTTP response.
    /// It also sets the HTTP Content-Type header to application/json and sets the Content-Length header accordingly.
    /// The JSON serializer can be configured in the ASP.NET Core startup code by registering a custom class of type <see cref="Json.ISerializer"/>.
    /// </summary>
    /// <param name="dataObj">The object to be send back to the client.</param>
    /// <param name="ctx"></param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>A Giraffe <see cref="HttpHandler" /> function which can be composed into a bigger web application.</returns>
    let json<'T> (dataObj: 'T) : HttpHandler =
        fun (_: HttpFunc) (ctx: HttpContext) -> ctx.WriteJsonAsync dataObj
