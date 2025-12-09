# Pydantic Example (Importing Python Models)

This example demonstrates how to **import and use Pydantic models defined in Python** from F# code using Fable.Python.

> **Note:** This is the opposite approach from the [FastAPI example](../fastapi/), which shows how to **define Pydantic models in F#** that compile to Python.

## Use Cases

This pattern is useful when you want to:

- Use models generated from OpenAPI specs or other tools
- Use models maintained by a Python team
- Integrate with an existing Python codebase
- Share models between Python and F# code

## Project Structure

```txt
examples/pydantic/
├── App.fs              # F# application using the models
├── Models.fs           # F# bindings for Python models
├── models.py           # Handwritten Python Pydantic models
├── PydanticExample.fsproj
├── build/              # Generated Python code (git-ignored)
└── README.md
```

## How It Works

### 1. Define Pydantic Models in Python

Create your models in `models.py`:

```python
from pydantic import BaseModel

class User(BaseModel):
    id: int
    name: str
    email: str | None = None
    age: int | None = None
```

### 2. Create F# Bindings

Import the Python models using `[<Import>]`:

```fsharp
[<Import("User", "models")>]
type User =
    abstract id: int with get, set
    abstract name: string with get, set
    abstract email: string option with get, set
    abstract age: int option with get, set
```

### 3. Create Constructors

Use `[<Emit>]` to call the Python constructor with named arguments:

```fsharp
[<RequireQualifiedAccess>]
module User =
    [<Import("User", "models")>]
    [<Emit("$0(id=$1, name=$2, email=$3, age=$4)")>]
    let create (id: int) (name: string) (email: string option) (age: int option) : User = nativeOnly
```

### 4. Use in F# Code

```fsharp
let user = User.create 1 "Alice" (Some "alice@example.com") (Some 30)
printfn "User: %s (id=%d)" user.name user.id
```

## Building

From the repository root:

```bash
# Using justfile
just example-pydantic

# Or manually
cd examples/pydantic
dotnet fable --lang python --outDir build
cp models.py build/
cd build && python app.py
```

## Output

```txt
User 1: Alice (id=1)
User 1 email: alice@example.com
User 1 age: 30

User 2: Bob (id=2)
User 2 email: None

Product: Laptop - $1299.99
In stock: true
Tags: ['electronics', 'computers']

Updated email: alice.updated@example.com

Create request for: Charlie
```

## See Also

- [FastAPI example](../fastapi/) - Shows the opposite pattern: defining Pydantic models in F# that compile to Python
