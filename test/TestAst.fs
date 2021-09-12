module Fable.Python.Tests.Ast

open Util.Testing
open Fable.Python.Ast

[<Fact>]
let ``test ast parse empty module works`` () =
    let result = ast.parse ""
    result.GetType () |> equal typeof<AST>

[<Fact>]
let ``test ast parse assign works`` () =
    let result = ast.parse "a = 10" :?> Module

    // Result is a list of statements
    result.body.[0].GetType () |> equal typeof<stmt>

    // The statement is an assign statement
    let assign = (result.body.[0] :?> Assign)
    assign.GetType () |> equal typeof<Assign>


