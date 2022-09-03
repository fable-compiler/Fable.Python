namespace Giraffe

open System.Collections.Generic
open System.Threading.Tasks

type HttpResponse = Dictionary<string, obj>

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

type HttpRequest (scope: Scope) =
    member x.Path: string option = scope["path"] :?> string |> Some

type HttpContext (scope: Scope, receive: unit -> Task<Response>, send: Request -> Task<unit>) =
    let scope = scope
    let receive = receive
    let send = send

    let items = Dictionary<string, obj> ()
    let responseStart = Dictionary<string, obj>()

    let responseBody = Dictionary<string, obj>()
    let request = HttpRequest(scope)

    do
        responseStart["type"] <- "http.response.start"
        responseStart["status"] <- 200
        responseStart["headers"] <- ResizeArray<_>()
        responseBody["type"] <- "http.response.body"

    member ctx.Items = items
    member ctx.Request = request

    member ctx.WriteBytesAsync (bytes : byte[]) =
        //printfn "WriteBytesAsync"
        task {
            ctx.SetHttpHeader(HeaderNames.ContentLength, bytes.Length)
            if scope["method"] <> HttpMethods.Head then
                responseBody["body"] <- bytes

            do! send responseStart
            do! send responseBody
            return Some ctx
        }

    member ctx.SetHttpHeader (key : string, value : obj) =
        let headers = responseStart["headers"] :?> ResizeArray<string*obj>
        headers.Add((key, value.ToString()))

    member ctx.SetContentType (contentType: string) =
        //printfn "SetContentType"
        ctx.SetHttpHeader(HeaderNames.ContentType, contentType)

