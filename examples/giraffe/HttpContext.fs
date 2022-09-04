namespace Giraffe

open System.Collections.Generic
open System.Threading.Tasks


type Scope = Dictionary<string, obj>
type Request = Dictionary<string, obj>
type Response = Dictionary<string, obj>


module HeaderNames =
    [<Literal>]
    let ContentType = "content-type"

    [<Literal>]
    let ContentLength = "content-length"

module HttpMethods =
    [<Literal>]
    let Head = "head"

    let IsGet (method: string) = method = "GET"
    let IsPost (method: string) = method = "POST"
    let IsPatch (method: string) = method = "PATCH"
    let IsPut (method: string) = method = "PUT"
    let IsDelete (method: string) = method = "DELETE"
    let IsHead (method: string) = method = "HEAD"
    let IsOptions (method: string) = method = "OPTIONS"
    let IsTrace (method: string) = method = "TRACE"
    let IsConnect (method: string) = method = "CONNECT"

type HttpRequest(scope: Scope) =
    member x.Path: string option = scope["path"] :?> string |> Some

    member x.Method: string = scope["method"] :?> string

type HttpResponse(send: Request -> Task<unit>) =
    let responseStart = Dictionary<string, obj>()
    let responseBody = Dictionary<string, obj>()

    do
        responseStart["type"] <- "http.response.start"
        responseStart["status"] <- 200
        responseStart["headers"] <- ResizeArray<_>()

        responseBody["type"] <- "http.response.body"

    member val HasStarted: bool = false with get, set

    member x.WriteAsync(bytes: byte []) =
        task {
            // printfn "HttpResponse.WriteAsync()"
            responseBody["body"] <- bytes

            if not x.HasStarted then
                do! send responseStart
                x.HasStarted <- true

            do! send responseBody
        }

    member x.SetHttpHeader(key: string, value: obj) =
        let headers = responseStart["headers"] :?> ResizeArray<string * obj>
        headers.Add((key, value.ToString()))

type HttpContext(scope: Scope, receive: unit -> Task<Response>, send: Request -> Task<unit>) =
    let scope = scope
    let send = send

    let items = Dictionary<string, obj>()

    let request = HttpRequest(scope)
    let response = HttpResponse(send)

    member ctx.Items = items
    member ctx.Request = request
    member ctx.Response = response

    member ctx.WriteBytesAsync(bytes: byte []) =
        // printfn "WriteBytesAsync"
        task {
            ctx.SetHttpHeader(HeaderNames.ContentLength, bytes.Length)

            if ctx.Request.Method <> HttpMethods.Head then
                do! ctx.Response.WriteAsync(bytes)

            return Some ctx
        }

    member ctx.SetHttpHeader(key: string, value: obj) = ctx.Response.SetHttpHeader(key, value)

    member ctx.SetContentType(contentType: string) =
        // printfn "SetContentType"
        ctx.SetHttpHeader(HeaderNames.ContentType, contentType)