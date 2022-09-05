namespace Giraffe

open System.Collections.Generic
open System.Threading.Tasks

open Fable.Python.Json


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

type ISerializer =
    abstract SerializeToBytes: obj -> byte array
    abstract SerializeToString: obj -> string
    abstract Deserialize: string -> obj

type JsonSerializer() =
    interface ISerializer with
        member this.SerializeToBytes obj : byte array =
            let str = (this :> ISerializer).SerializeToString obj
            System.Text.Encoding.UTF8.GetBytes str

        member this.SerializeToString obj : string = json.dumps obj
        member this.Deserialize(str: string) = json.loads str

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

    member x.WriteAsync(bytes: byte[]) =
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

    member x.SetStatusCode(status: int) = responseStart["status"] <- status

type HttpContext(scope: Scope, receive: unit -> Task<Response>, send: Request -> Task<unit>) =
    let scope = scope
    let send = send

    let items = Dictionary<string, obj>()

    let request = HttpRequest(scope)
    let response = HttpResponse(send)

    member ctx.Items = items
    member ctx.Request = request
    member ctx.Response = response

    member ctx.WriteBytesAsync(bytes: byte[]) =
        // printfn "WriteBytesAsync"
        task {
            ctx.SetHttpHeader(HeaderNames.ContentLength, bytes.Length)

            if ctx.Request.Method <> HttpMethods.Head then
                do! ctx.Response.WriteAsync(bytes)

            return Some ctx
        }

    member x.GetJsonSerializer() : ISerializer = JsonSerializer() :> _

    member ctx.WriteJsonAsync<'T>(dataObj: 'T) =
        ctx.SetContentType "application/json; charset=utf-8"
        let serializer = ctx.GetJsonSerializer()
        serializer.SerializeToBytes dataObj |> ctx.WriteBytesAsync

    member ctx.SetStatusCode(statusCode: int) = ctx.Response.SetStatusCode(statusCode)

    member ctx.SetHttpHeader(key: string, value: obj) = ctx.Response.SetHttpHeader(key, value)

    member ctx.SetContentType(contentType: string) =
        ctx.SetHttpHeader(HeaderNames.ContentType, contentType)
