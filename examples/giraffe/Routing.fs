namespace Giraffe

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