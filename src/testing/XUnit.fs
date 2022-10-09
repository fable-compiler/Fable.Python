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


/// <summary>Transforms a function into a React function component. Make sure the function is defined at the module level</summary>
type FactAttribute(?exportDefault: bool, ?import: string, ?from:string, ?memo: bool) =
    inherit MemberDeclarationPluginAttribute()
    override _.FableMinimumVersion = "4.0"

    new() = FactAttribute(exportDefault=false)

    /// <summary>Transforms call-site into createElement calls</summary>
    override this.TransformCall(compiler, memb, expr) =
        printfn "TransformCall"
        expr

    override this.Transform(compiler, file, decl) =
        printfn "Transform: %A" decl
        let info = compiler.GetMember(decl.MemberRef)

        if info.IsValue then
            failwith "FactAttribute can only be applied to functions"
        else
            decl