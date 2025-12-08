# Fable.Python.FastAPI

F# bindings for [FastAPI](https://fastapi.tiangolo.com/), a modern, fast web framework for building APIs with Python.

## Features

- Class-based API endpoints using decorator attributes
- HTTP method decorators: `Get`, `Post`, `Put`, `Delete`, `Patch`, `Options`, `Head`
- Router-based API organization with `RouterGet`, `RouterPost`, etc.
- Pydantic model integration for request/response validation
- Full FastAPI feature support: dependency injection, security, responses, etc.

## Basic Usage

```fsharp
open Fable.Python.FastAPI
open Fable.Python.Pydantic

// Create FastAPI app
let app = FastAPI(title = "My API", version = "1.0.0")

// Define a Pydantic model
[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type User(Id: int, Name: string, Email: string) =
    inherit BaseModel()
    member val Id: int = Id with get, set
    member val Name: string = Name with get, set
    member val Email: string = Email with get, set

// Define API endpoints
[<APIClass>]
type API() =
    [<Get("/")>]
    static member root() : obj =
        {| message = "Hello World" |}

    [<Get("/users/{user_id}")>]
    static member get_user(user_id: int) : User =
        User(Id = user_id, Name = "Alice", Email = "alice@example.com")

    [<Post("/users")>]
    static member create_user(user: User) : obj =
        {| status = "created"; user = user |}
```

## Router-based APIs

```fsharp
let router = APIRouter(prefix = "/users", tags = ResizeArray ["users"])

[<APIClass>]
type UsersAPI() =
    [<RouterGet("/")>]
    static member list_users() : obj =
        {| users = [] |}

    [<RouterGet("/{user_id}")>]
    static member get_user(user_id: int) : obj =
        {| user_id = user_id |}
```

## Important: Serialization with Anonymous Records

When returning anonymous records (`{| ... |}`) from API endpoints, be aware that F#'s `int` type compiles to Fable's `int32`, which is not a native Python `int`. This can cause serialization errors with FastAPI's `jsonable_encoder`.

### The Problem

```fsharp
// This may fail with: "Unable to serialize unknown type: Int32"
[<Delete("/items/{item_id}")>]
static member delete_item(item_id: int) : obj =
    {| status = "deleted"; item_id = item_id |}  // item_id is int32, not native int
```

### Why It Happens

- Pydantic models work fine because `int32` has `__get_pydantic_core_schema__`
- Anonymous records compile to plain Python dicts
- FastAPI's `jsonable_encoder` doesn't use Pydantic schemas for plain dicts
- `int32` is not a true Python `int` subclass due to [PyO3 limitations](https://github.com/PyO3/pyo3/issues/991)

### Solutions

**Option 1: Use `nativeint` for values in anonymous records**

```fsharp
[<Delete("/items/{item_id}")>]
static member delete_item(item_id: int) : obj =
    {| status = "deleted"; item_id = nativeint item_id |}
```

**Option 2: Define response models as Pydantic BaseModels**

```fsharp
[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type DeleteResponse(Status: string, ItemId: int) =
    inherit BaseModel()
    member val Status: string = Status with get, set
    member val ItemId: int = ItemId with get, set

[<Delete("/items/{item_id}")>]
static member delete_item(item_id: int) : DeleteResponse =
    DeleteResponse(Status = "deleted", ItemId = item_id)
```

**Option 3: Return Pydantic models directly**

Pydantic models serialize correctly because FastAPI uses the model's schema:

```fsharp
[<Get("/users")>]
static member get_users() : ResizeArray<User> =
    users  // ResizeArray<User> works, User is a BaseModel
```

### Summary

| Return Type | int32 Serialization |
|-------------|---------------------|
| Pydantic BaseModel | Works (uses `__get_pydantic_core_schema__`) |
| Anonymous record `{| ... |}` | Fails (use `nativeint` or response models) |
| `ResizeArray<T>` where T is BaseModel | Works |

## Collections

Use `ResizeArray<T>` instead of F# arrays or lists for API responses:

```fsharp
let users = ResizeArray<User>()

[<Get("/users")>]
static member get_users() : ResizeArray<User> =
    users  // Compiles to Python list, serializes correctly
```

F# arrays (`[| ... |]`) compile to Fable's `FSharpArray` which may not serialize correctly with FastAPI.

## Available Attributes

### Route Decorators (for `app`)

- `[<Get(path)>]` - HTTP GET
- `[<Post(path)>]` - HTTP POST
- `[<Put(path)>]` - HTTP PUT
- `[<Delete(path)>]` - HTTP DELETE
- `[<Patch(path)>]` - HTTP PATCH
- `[<Options(path)>]` - HTTP OPTIONS
- `[<Head(path)>]` - HTTP HEAD
- `[<WebSocket(path)>]` - WebSocket endpoint

### Router Decorators (for `router`)

- `[<RouterGet(path)>]`
- `[<RouterPost(path)>]`
- `[<RouterPut(path)>]`
- `[<RouterDelete(path)>]`
- `[<RouterPatch(path)>]`
- `[<RouterWebSocket(path)>]`

### Class Attribute

- `[<APIClass>]` - Marks a class for FastAPI routing (equivalent to `[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]`)

## Running the Server

```bash
# Build F# to Python
dotnet fable --lang python

# Run with uvicorn
uvicorn app:app --reload
```

## Development Mode

For hot-reloading during development:

```bash
# Terminal 1: Watch F# files
dotnet fable --lang python --watch

# Terminal 2: Run uvicorn with reload
uvicorn app:app --reload
```

Or use the justfile:

```bash
just dev-fastapi
```
