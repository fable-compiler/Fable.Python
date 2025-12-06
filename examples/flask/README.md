# Flask Example

This example demonstrates how to build a Flask web application using **Fable.Python** with the decorator-based API pattern and Feliz.ViewEngine for HTML rendering.

## Features

- Class-based routes using `[<APIClass>]` and `[<Get>]` decorators
- Server-side HTML rendering with Feliz.ViewEngine
- Bulma CSS framework for styling
- JSON API endpoints with `jsonify`

## Project Structure

```
examples/flask/
├── src/
│   ├── App.fs          # Main application with routes
│   ├── Model.fs        # Data model and CSS classes
│   ├── Head.fs         # HTML head component
│   ├── NavBar.fs       # Navigation component
│   ├── Hero.fs         # Hero section component
│   └── Flask.fsproj    # F# project file
├── build/              # Generated Python code (git-ignored)
└── README.md
```

## Building

From the repository root:

```bash
# Using justfile
just example-flask

# Or manually
cd examples/flask/src
dotnet fable --lang python --outDir ../build
```

## Running

```bash
cd examples/flask/build
uv run --group flask flask --app app run --reload
```

Then visit:
- http://localhost:5000 - Home page
- http://localhost:5000/about - About page
- http://localhost:5000/api/info - JSON API endpoint
- http://localhost:5000/health - Health check

## Routes

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Home page with Feliz.ViewEngine |
| GET | `/about` | About page |
| GET | `/api/info` | JSON API endpoint |
| GET | `/health` | Health check |

## How It Works

### Decorator-based Routes

Routes are defined using class attributes:

```fsharp
[<APIClass>]
type Routes() =
    [<Get("/")>]
    static member index() : string =
        "Hello World!"

    [<Get("/api/data")>]
    static member get_data() : obj =
        jsonify {| message = "Hello from F#!" |}
```

### HTML Rendering with Feliz.ViewEngine

The example uses Feliz.ViewEngine for type-safe HTML generation:

```fsharp
open Feliz.ViewEngine

let page =
    Html.html [
        Html.head [
            Html.title "My Page"
        ]
        Html.body [
            Html.h1 [ prop.text "Hello World!" ]
        ]
    ]
    |> Render.htmlDocument
```

## Development Mode

For hot-reloading during development:

```bash
# Terminal 1: Watch F# files
cd examples/flask/src
dotnet fable --lang python --outDir ../build --watch

# Terminal 2: Run Flask with reload
cd examples/flask/build
uv run --group flask flask --app app run --reload
```
