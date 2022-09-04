namespace Giraffe

open System
open System.Collections.Generic

[<RequireQualifiedAccess>]
module HttpHandler =
    // Core handlers

    let inline choose handlers (source: HttpHandler) : HttpHandler = source >=> choose handlers

    let inline GET (source: HttpHandler) : HttpHandler = source >=> GET
    let inline POST (source: HttpHandler) : HttpHandler = source >=> POST
    let inline PUT (source: HttpHandler) : HttpHandler = source >=> PUT
    let inline PATCH (source: HttpHandler) : HttpHandler = source >=> PATCH
    let inline DELETE (source: HttpHandler) : HttpHandler = source >=> DELETE
    let inline HEAD (source: HttpHandler) : HttpHandler = source >=> HEAD
    let inline OPTIONS (source: HttpHandler) : HttpHandler = source >=> OPTIONS
    let inline TRACE (source: HttpHandler) : HttpHandler = source >=> TRACE
    let inline CONNECT (source: HttpHandler) : HttpHandler = source >=> CONNECT

    let inline GET_HEAD (source: HttpHandler) : HttpHandler = source >=> GET_HEAD


    /// <summary>
    /// Writes an UTF-8 encoded string to the body of the HTTP response and sets the HTTP Content-Length header
    /// accordingly, as well as the Content-Type header to text/plain.
    /// </summary>
    /// <param name="str">The string value to be send back to the client.</param>
    /// <param name="source">The previous HTTP handler to compose.</param>
    /// <returns>
    /// A Giraffe <see cref="HttpHandler" /> function which can be composed into a bigger web application.
    /// </returns>
    let inline text (str: string) (source: HttpHandler) : HttpHandler = source >=> text str

    let inline route (path: string) (source: HttpHandler) : HttpHandler = source >=> route path

[<AutoOpen>]
module HttpHandlerExtensions =
    /// Subscribe the source handler. Adds the handler as the next handler to process after the source handler. Same as
    /// Giraffe compose i.e `>=>`.
    let inline subscribe (source: HttpHandler) (handler: HttpHandler) : HttpHandler = handler |> compose source