/// F# bindings for Pydantic models defined in Python.
///
/// This module demonstrates how to import and use Pydantic models
/// that are defined in Python code (models.py).
///
/// This pattern is useful when you want to:
/// - Use models generated from OpenAPI specs or other tools
/// - Use models maintained by a Python team
/// - Integrate with an existing Python codebase
/// - Share models between Python and F# code
module Models

open Fable.Core

// ============================================================================
// Import Pydantic models from Python
// ============================================================================

/// User model - imported from models.py
/// The interface defines the shape of the Python Pydantic model
[<Import("User", "models")>]
type User =
    abstract id: int with get, set
    abstract name: string with get, set
    abstract email: string option with get, set
    abstract age: int option with get, set

/// Product model - imported from models.py
[<Import("Product", "models")>]
type Product =
    abstract id: int with get, set
    abstract name: string with get, set
    abstract description: string with get, set
    abstract price: float with get, set
    abstract in_stock: bool with get, set
    abstract tags: string array with get, set

/// Request model for creating users
[<Import("CreateUserRequest", "models")>]
type CreateUserRequest =
    abstract name: string with get, set
    abstract email: string option with get, set
    abstract age: int option with get, set

/// Request model for creating products
[<Import("CreateProductRequest", "models")>]
type CreateProductRequest =
    abstract name: string with get, set
    abstract description: string with get, set
    abstract price: float with get, set
    abstract in_stock: bool with get, set
    abstract tags: string array with get, set

// ============================================================================
// Constructors for creating instances
// ============================================================================

/// Helper module for creating model instances
[<RequireQualifiedAccess>]
module User =
    /// Create a new User instance
    [<Import("User", "models")>]
    [<Emit("$0(id=$1, name=$2, email=$3, age=$4)")>]
    let create (id: int) (name: string) (email: string option) (age: int option) : User = nativeOnly

[<RequireQualifiedAccess>]
module Product =
    /// Create a new Product instance
    [<Import("Product", "models")>]
    [<Emit("$0(id=$1, name=$2, description=$3, price=$4, in_stock=$5, tags=$6)")>]
    let create (id: int) (name: string) (description: string) (price: float) (inStock: bool) (tags: string array) : Product = nativeOnly

[<RequireQualifiedAccess>]
module CreateUserRequest =
    /// Create a new CreateUserRequest instance
    [<Import("CreateUserRequest", "models")>]
    [<Emit("$0(name=$1, email=$2, age=$3)")>]
    let create (name: string) (email: string option) (age: int option) : CreateUserRequest = nativeOnly

[<RequireQualifiedAccess>]
module CreateProductRequest =
    /// Create a new CreateProductRequest instance
    [<Import("CreateProductRequest", "models")>]
    [<Emit("$0(name=$1, description=$2, price=$3, in_stock=$4, tags=$5)")>]
    let create (name: string) (description: string) (price: float) (inStock: bool) (tags: string array) : CreateProductRequest = nativeOnly
