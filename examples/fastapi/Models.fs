/// Pydantic Models for the FastAPI Example
/// Demonstrates how to create Pydantic models using F# classes
module Models

open Fable.Core
open Fable.Python.Pydantic

// ============================================================================
// User Models
// ============================================================================

/// User model - a Pydantic BaseModel
[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type User(Id: int, Name: string, Email: string) =
    inherit BaseModel()
    member val Id: int = Id with get, set
    member val Name: string = Name with get, set
    member val Email: string = Email with get, set

/// Request model for creating a new user
[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type CreateUserRequest(Name: string, Email: string) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Email: string = Email with get, set

// ============================================================================
// Item Models
// ============================================================================

/// Item model - a Pydantic BaseModel
[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type Item(Id: int, Name: string, Price: float, InStock: bool) =
    inherit BaseModel()
    member val Id: int = Id with get, set
    member val Name: string = Name with get, set
    member val Price: float = Price with get, set
    member val InStock: bool = InStock with get, set

/// Request model for creating/updating an item
[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type CreateItemRequest(Name: string, Price: float, InStock: bool) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Price: float = Price with get, set
    member val InStock: bool = InStock with get, set
