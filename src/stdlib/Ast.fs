module rec Fable.Python.Ast

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

type _identifier = string


[<Import("AST", "ast")>]
type AST =
    abstract foo: int

[<Import("mod", "ast")>]
type ``mod`` =
    inherit AST

[<Import("expr", "ast")>]
type expr =
    inherit AST

[<Import("Module", "ast")>]
type Module =
    inherit ``mod``
    abstract body: stmt array

[<Import("Expression", "ast")>]
type Expression =
    inherit ``mod``
    abstract body: expr

[<Import("Module", "ast")>]
type stmt =
    inherit AST


[<Import("FunctionDef", "ast")>]
type FunctionDef =
    inherit stmt

    abstract name: _identifier
    abstract args: arguments
    abstract body: stmt array
    abstract decorator_list: expr array
    abstract returns: expr option

[<Import("ClassDef", "ast")>]
type ClassDef =
    inherit stmt
    abstract name: _identifier
    abstract bases: expr array
    abstract keywords: keyword array
    abstract body: stmt array
    abstract decorator_list: expr array

[<Import("Return", "ast")>]
type Return =
    inherit stmt
    abstract value: expr option

[<Import("Delete", "ast")>]
type Delete =
    inherit stmt
    abstract targets: expr array

[<Import("Assign", "ast")>]
type Assign =
    inherit stmt
    abstract targets: expr array
    abstract value: expr

[<Import("Import", "ast")>]
type Import =
    inherit stmt
    abstract names: alias array

[<Import("ImportFrom", "ast")>]
type ImportFrom =
    inherit stmt
    abstract ``module``: _identifier option
    abstract names: alias array
    abstract level: int

[<Import("If", "ast")>]
type If =
    inherit stmt
    abstract test: expr
    abstract body: stmt array
    abstract orelse: stmt array

[<Import("arguments", "ast")>]
type arguments =
    inherit AST

    abstract posonlyargs: arg array
    abstract args: arg array
    abstract vararg: arg option
    abstract kwonlyargs: arg array
    abstract kw_defaults: expr option list
    abstract kwarg: arg option
    abstract defaults: expr array

[<Import("arg", "ast")>]
type arg =
    inherit AST
    abstract arg: _identifier
    abstract annotation: expr option

[<Import("keyword", "ast")>]
type keyword =
    inherit AST
    abstract arg: _identifier option
    abstract value: expr

[<Import("alias", "ast")>]
type alias =
    inherit AST
    abstract name: _identifier
    abstract asname: _identifier option


[<StringEnum>]
type Mode =
    | Exec
    | [<CompiledName("func_mode")>] FuncMode

[<Erase>]
type IExports =
    /// Parse the source into an AST node
    abstract parse: string -> AST
    abstract parse: string * filename: string -> AST
    abstract parse: string * filename: string * mode: Mode -> AST
    abstract unparse: astObj: AST -> string
    abstract walk: node: AST -> AST array
    /// Return a formatted dump of the tree in node.
    abstract dump: node: AST -> string
    /// Return a formatted dump of the tree in node.
    abstract dump: node: AST * annotate_fields: bool -> string
    /// Return a formatted dump of the tree in node.
    abstract dump: node: AST * annotate_fields: bool * include_attributes: bool -> string

[<ImportAll("ast")>]
let ast: IExports = nativeOnly
