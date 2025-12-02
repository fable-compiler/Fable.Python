/// Type bindings for Python ast module: https://docs.python.org/3/library/ast.html
module rec Fable.Python.Ast

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

/// Type alias for Python identifier strings
type _identifier = string


/// Base class for all AST node types
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


/// Parse mode for ast.parse()
[<StringEnum>]
[<RequireQualifiedAccess>]
type Mode =
    /// Execute mode for parsing statements
    | Exec
    /// Function mode for parsing expressions
    | [<CompiledName("func_mode")>] FuncMode

[<Erase>]
type IExports =
    /// Parse Python source code into an AST node
    /// See https://docs.python.org/3/library/ast.html#ast.parse
    abstract parse: source: string -> AST
    /// Parse Python source code with a filename
    /// See https://docs.python.org/3/library/ast.html#ast.parse
    abstract parse: source: string * filename: string -> AST
    /// Parse Python source code with filename and mode
    /// See https://docs.python.org/3/library/ast.html#ast.parse
    abstract parse: source: string * filename: string * mode: Mode -> AST
    /// Convert an AST back to Python source code
    /// See https://docs.python.org/3/library/ast.html#ast.unparse
    abstract unparse: astObj: AST -> string
    /// Recursively yield all descendant nodes in the tree
    /// See https://docs.python.org/3/library/ast.html#ast.walk
    abstract walk: node: AST -> AST array
    /// Return a formatted dump of the AST tree in node
    /// See https://docs.python.org/3/library/ast.html#ast.dump
    abstract dump: node: AST -> string
    /// Return a formatted dump of the AST tree with field annotations
    /// See https://docs.python.org/3/library/ast.html#ast.dump
    abstract dump: node: AST * annotate_fields: bool -> string
    /// Return a formatted dump of the AST tree with field annotations and attributes
    /// See https://docs.python.org/3/library/ast.html#ast.dump
    abstract dump: node: AST * annotate_fields: bool * include_attributes: bool -> string

/// Abstract Syntax Trees - Parse and manipulate Python abstract syntax trees
[<ImportAll("ast")>]
let ast: IExports = nativeOnly
