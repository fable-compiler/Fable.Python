/// FastAPI Example Application
/// Demonstrates how to use Fable.Python.FastAPI with decorator attributes
module App

open System.Threading.Tasks
open Fable.Core
open Fable.Python.FastAPI
open Fable.Python.Pydantic
open Models

// Create the FastAPI application instance
let app = FastAPI(title = "Fable.Python FastAPI Example", version = "1.0.0")

// ============================================================================
// Sample Data Store (in-memory for demonstration)
// ============================================================================

let users = ResizeArray<User>()
let items = ResizeArray<Item>()

// Initialize with sample data
let user1 = User(Id = 1, Name = "Alice", Email = "alice@example.com")
let user2 = User(Id = 2, Name = "Bob", Email = "bob@example.com")
users.Add(user1)
users.Add(user2)

let item1 = Item(Id = 1, Name = "Laptop", Price = 999.99, InStock = true)
let item2 = Item(Id = 2, Name = "Mouse", Price = 29.99, InStock = true)
items.Add(item1)
items.Add(item2)

// ============================================================================
// API Endpoints using Class-based Pattern with Decorators
// ============================================================================

[<APIClass>]
type API() =
    /// Root endpoint - welcome message
    [<Get("/")>]
    static member root() : Task<obj> = task {
        return {|
           message = "Welcome to Fable.Python + FastAPI!"
           version = "1.0.0" |}
    }

    /// Health check endpoint
    [<Get("/health")>]
    static member health() : Task<obj> = task {
        return {|
           status = "healthy"
           users_count = users.Count
           items_count = items.Count |}
        }

    /// Get all users
    [<Get("/users")>]
    static member get_users() : Task<ResizeArray<User>> = task {
        return users
    }

    /// Get a user by ID
    [<Get("/users/{user_id}")>]
    static member get_user(user_id: int) : Task<obj> = task {
        match users |> Seq.tryFind (fun u -> u.Id = user_id) with
        | Some user -> return user :> obj
        | None -> return {| error = "User not found" |}
    }
    /// Create a new user
    [<Post("/users")>]
    static member create_user(request: CreateUserRequest) = task {
        let newId = if users.Count = 0 then 1 else (users |> Seq.map (fun u -> u.Id) |> Seq.max) + 1
        let newUser = User(Id = newId, Name = request.Name, Email = request.Email)
        users.Add(newUser)
        return {| status = "created"; user = newUser |}
    }

    /// Get all items
    [<Get("/items")>]
    static member get_items() : ResizeArray<Item> =
        items

    /// Get an item by ID
    [<Get("/items/{item_id}")>]
    static member get_item(item_id: int) : Task<obj> = task {
        match items |> Seq.tryFind (fun i -> i.Id = item_id) with
        | Some item -> return item :> obj
        | None -> return {| error = "Item not found" |}
    }

    /// Create a new item
    [<Post("/items")>]
    static member create_item(request: CreateItemRequest) : Task<obj> = task {
        let newId = if items.Count = 0 then 1 else (items |> Seq.map (fun i -> i.Id) |> Seq.max) + 1
        let newItem = Item(Id = newId, Name = request.Name, Price = request.Price, InStock = request.InStock)
        items.Add(newItem)
        return {| status = "created"; item = newItem |}
    }

    /// Update an item
    [<Put("/items/{item_id}")>]
    static member update_item(item_id: int, request: CreateItemRequest) : Task<obj> = task {
        match items |> Seq.tryFindIndex (fun i -> i.Id = item_id) with
        | Some index ->
            let updatedItem = Item(Id = item_id, Name = request.Name, Price = request.Price, InStock = request.InStock)
            items.[index] <- updatedItem
            return {| status = "updated"; item = updatedItem |}
        | None -> return {| error = "Item not found" |}
    }

    /// Delete an item
    [<Delete("/items/{item_id}")>]
    static member delete_item(item_id: int) : Task<obj> = task {
        match items |> Seq.tryFindIndex (fun i -> i.Id = item_id) with
        | Some index ->
            items.RemoveAt(index)
            return {| status = "deleted"; deleted_id = nativeint item_id |}
        | None -> return {| error = "Item not found" |}
    }