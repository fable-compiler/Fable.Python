# FastAPI Example

This example demonstrates how to build a FastAPI application using **Fable.Python** with the decorator-based API pattern.

## Features

- Class-based API endpoints using `[<APIClass>]` attribute
- HTTP method decorators: `[<Get>]`, `[<Post>]`, `[<Put>]`, `[<Delete>]`
- Pydantic models for request/response validation
- Automatic OpenAPI documentation generation

## Project Structure

```
examples/fastapi/
├── App.fs              # Main application with API endpoints
├── Models.fs           # Pydantic model definitions
├── PydanticExample.fsproj
├── build/              # Generated Python code (git-ignored)
└── README.md
```

## Building

From the repository root:

```bash
# Using justfile
just example-fastapi

# Or manually
cd examples/fastapi
dotnet fable --lang python --outDir build
```

## Running

```bash
cd examples/fastapi/build
uv run --group fastapi uvicorn app:app --reload
```

Then visit:
- http://localhost:8000 - API root
- http://localhost:8000/docs - Interactive Swagger UI documentation
- http://localhost:8000/redoc - ReDoc documentation

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Welcome message |
| GET | `/health` | Health check |
| GET | `/users` | List all users |
| GET | `/users/{user_id}` | Get user by ID |
| POST | `/users` | Create a new user |
| GET | `/items` | List all items |
| GET | `/items/{item_id}` | Get item by ID |
| POST | `/items` | Create a new item |
| PUT | `/items/{item_id}` | Update an item |
| DELETE | `/items/{item_id}` | Delete an item |

## Example Usage

```bash
# Get all users
curl http://localhost:8000/users

# Create a new user
curl -X POST http://localhost:8000/users \
  -H "Content-Type: application/json" \
  -d '{"Name": "Charlie", "Email": "charlie@example.com"}'

# Get all items
curl http://localhost:8000/items

# Create a new item
curl -X POST http://localhost:8000/items \
  -H "Content-Type: application/json" \
  -d '{"Name": "Keyboard", "Price": 79.99, "InStock": true}'
```

## How It Works

### Pydantic Models

F# classes that inherit from `BaseModel` with the `[<Py.ClassAttributes>]` attribute compile to Pydantic models:

```fsharp
[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type User(Id: int, Name: string, Email: string) =
    inherit BaseModel()
    member val Id: int = Id with get, set
    member val Name: string = Name with get, set
    member val Email: string = Email with get, set
```

### API Endpoints

The `[<APIClass>]` attribute marks a class for FastAPI routing, and method decorators define the HTTP methods:

```fsharp
[<APIClass>]
type API() =
    [<Get("/users")>]
    static member get_users() : ResizeArray<User> =
        users

    [<Post("/users")>]
    static member create_user(request: CreateUserRequest) : obj =
        // ... create user logic
```

## Development Mode

For hot-reloading during development:

```bash
# Using justfile
just dev-fastapi

# Or manually in two terminals:
# Terminal 1: Watch F# files
cd examples/fastapi
dotnet fable --lang python --outDir build --watch

# Terminal 2: Run uvicorn with reload
cd examples/fastapi/build
uv run --group fastapi uvicorn app:app --reload
```
