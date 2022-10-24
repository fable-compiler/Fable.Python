namespace Fable.Python.XUnit

open Fable
open Fable.AST
open Fable.AST.Fable

// Tell Fable to scan for plugins in this assembly
[<assembly:ScanForPlugins>]
do()

[<AutoOpen>]
module Helpers =
    let emit r t args isStatement macro =
        let emitInfo =
            { Macro = macro
              IsStatement = isStatement
              CallInfo = CallInfo.Create(args=args) }
        Emit(emitInfo, t, r)

    let emitExpr r t args macro =
        emit r t args false macro

    let emitStatement r t args macro =
        emit r t args true macro

    let hasAttribute fullName (funcOrValue: Fable.MemberFunctionOrValue) =
        funcOrValue.Attributes
        |> Seq.exists (fun att -> att.Entity.FullName = fullName)

    //let decorate expr =
/// <summary>Transforms a function into a React function component. Make sure the function is defined at the module level</summary>
type FactAttribute(?exportDefault: bool, ?import: string, ?from:string, ?memo: bool) =
    inherit MemberDeclarationPluginAttribute()
    override _.FableMinimumVersion = "4.0"

    new() = FactAttribute(exportDefault=false)

    /// <summary>Transforms call-site into createElement calls</summary>
    override this.TransformCall(compiler, memb, expr) =
        expr

    override this.Transform(compiler, file, decl) =
        let info = compiler.GetMember(decl.MemberRef)
        printfn "Transform: %A" (info.Attributes |> Seq.map (fun a -> a.Entity.FullName))
        printfn "Transform: %A" (info.)

        match info.IsValue, decl with
        | true, _ ->
            failwith "FactAttribute can only be applied to functions"
        | _, { Name = name } when not (name.ToLower().StartsWith("test")) ->
            let attr = info.Attributes
            { decl with Name = "test_" + name; Tags= [ "@pytest.mark.unit" ] }
        | _ -> decl
        