namespace Giraffe

open System
open System.Text.RegularExpressions

open Giraffe.Python.FormatExpressions

[<RequireQualifiedAccess>]
module SubRouting =
    [<Literal>]
    let private RouteKey = "giraffe_route"

    let getSavedPartialPath (ctx: HttpContext) =
        if ctx.Items.ContainsKey RouteKey then
            ctx.Items.Item RouteKey |> string |> strOption
        else
            None


    let getNextPartOfPath (ctx: HttpContext) =
        match getSavedPartialPath ctx with
        | Some p when ctx.Request.Path.Value.Contains p -> ctx.Request.Path.Value.[p.Length ..]
        | _ -> ctx.Request.Path.Value

[<AutoOpen>]
module Routing =
    let route (path: string) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            if (SubRouting.getNextPartOfPath ctx).Equals path then
                next ctx
            else
                skipPipeline ()

    /// <summary>
    /// Filters an incoming HTTP request based on the request path (case insensitive).
    /// </summary>
    /// <param name="path">Request path.</param>
    /// <param name="next"></param>
    /// <param name="ctx"></param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    let routeCi (path: string) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            if String.Equals(SubRouting.getNextPartOfPath ctx, path, StringComparison.OrdinalIgnoreCase) then
                next ctx
            else
                skipPipeline ()

    /// <summary>
    /// Filters an incoming HTTP request based on the request path using Regex (case sensitive).
    /// </summary>
    /// <param name="path">Regex path.</param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    let routex (path: string) : HttpHandler =
        let pattern = sprintf "^%s$" path
        let regex = Regex(pattern, RegexOptions.Compiled)

        fun (next: HttpFunc) (ctx: HttpContext) ->
            let result = regex.Match(SubRouting.getNextPartOfPath ctx)

            match result.Success with
            | true -> next ctx
            | false -> skipPipeline ()

    /// <summary>
    /// Filters an incoming HTTP request based on the request path (case sensitive).
    /// If the route matches the incoming HTTP request then the arguments from the <see cref="Microsoft.FSharp.Core.PrintfFormat"/> will be automatically resolved and passed into the supplied routeHandler.
    ///
    /// Supported format chars**
    ///
    /// %b: bool
    /// %c: char
    /// %s: string
    /// %i: int
    /// %d: int64
    /// %f: float/double
    /// %O: Guid
    /// </summary>
    /// <param name="path">A format string representing the expected request path.</param>
    /// <param name="routeHandler">A function which accepts a tuple 'T of the parsed arguments and returns a <see cref="HttpHandler"/> function which will subsequently deal with the request.</param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    let routef (path: PrintfFormat<_, _, _, _, 'T>) (routeHandler: 'T -> HttpHandler) : HttpHandler =
        // validateFormat path
        fun (next: HttpFunc) (ctx: HttpContext) ->
            tryMatchInput path MatchOptions.Exact (SubRouting.getNextPartOfPath ctx)
            |> function
                | None -> skipPipeline ()
                | Some args -> routeHandler args next ctx
