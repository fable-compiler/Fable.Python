# Fable.Python.Flask

F# bindings for [Flask](https://flask.palletsprojects.com/), a lightweight WSGI web application framework for Python.

## Features

- Class-based routes using decorator attributes
- HTTP method decorators: `Route`, `Get`, `Post`, `Put`, `Delete`, `Patch`
- Blueprint support for modular applications
- Full Flask feature support: request, response, session, flash messages, etc.

## Basic Usage

```fsharp
open Fable.Python.Builtins
open Fable.Python.Flask

// Create Flask app
let app = Flask(__name__)

// Define routes using class-based pattern
[<APIClass>]
type Routes() =
    [<Get("/")>]
    static member index() : string =
        "Hello World!"

    [<Get("/users/<int:user_id>")>]
    static member get_user(user_id: int) : obj =
        jsonify {| user_id = user_id; name = "Alice" |}

    [<Post("/users")>]
    static member create_user() : obj =
        let data = request.get_json()
        jsonify {| status = "created"; data = data |}
```

## Route Decorators

### Basic Route

```fsharp
[<Route("/path")>]
static member handler() = "response"

// With methods
[<RouteMethods("/path", [| "GET"; "POST" |])>]
static member handler() = "response"
```

### HTTP Method Shortcuts

```fsharp
[<Get("/users")>]
static member list_users() = ...

[<Post("/users")>]
static member create_user() = ...

[<Put("/users/<int:id>")>]
static member update_user(id: int) = ...

[<Delete("/users/<int:id>")>]
static member delete_user(id: int) = ...

[<Patch("/users/<int:id>")>]
static member patch_user(id: int) = ...
```

## Blueprint Routes

For modular applications using Blueprints:

```fsharp
let bp = Blueprint("users", __name__, url_prefix = "/users")

[<APIClass>]
type UserRoutes() =
    [<BpGet("/")>]
    static member list_users() = ...

    [<BpPost("/")>]
    static member create_user() = ...

    [<BpGet("/<int:user_id>")>]
    static member get_user(user_id: int) = ...

// Register blueprint
app.register_blueprint(bp)
```

## Request Object

Access the current request:

```fsharp
open Fable.Python.Flask

[<Post("/data")>]
static member handle_data() =
    // Get JSON body
    let data = request.get_json()

    // Access request properties
    let method = request.method      // "POST"
    let path = request.path          // "/data"
    let isJson = request.is_json     // true

    // Query parameters
    let args = request.args

    // Form data
    let form = request.form

    jsonify {| received = data |}
```

## Response Helpers

```fsharp
// JSON response
let response = jsonify {| message = "Hello" |}

// Redirect
let redir = redirect "/other-page"
let redir2 = redirect_with_code "/other" 301

// Abort with error
abort 404
abort_with_message 404 "User not found"

// Custom response
let resp = make_response "Hello"
let resp2 = make_response_with_status "Created" 201
```

## Templates

```fsharp
// Render a template
let html = render_template "index.html"

// With context variables
let html = render_template_with_context "user.html" {| name = "Alice"; age = 30n |}
```

## Session

```fsharp
// Set secret key first
app.config?SECRET_KEY <- "your-secret-key"

[<Get("/login")>]
static member login() =
    session.set("user_id", 123)
    jsonify {| status = "logged in" |}

[<Get("/profile")>]
static member profile() =
    let userId = session.get("user_id")
    jsonify {| user_id = userId |}

[<Get("/logout")>]
static member logout() =
    session.clear()
    redirect "/"
```

## Flash Messages

```fsharp
[<Post("/action")>]
static member do_action() =
    flash "Action completed successfully!"
    flash_with_category "Please verify your email" "warning"
    redirect "/"

// In template or route
let messages = get_flashed_messages()
let messagesWithCategories = get_flashed_messages_with_categories()
```

## Error Handlers

```fsharp
// Register error handler
app.errorhandler 404 (fun error ->
    jsonify {| error = "Not found" |}
)

app.errorhandler 500 (fun error ->
    jsonify {| error = "Internal server error" |}
)
```

## Middleware

```fsharp
// Before each request
app.before_request (fun () ->
    printfn "Request: %s %s" request.method request.path
)

// After each request
app.after_request (fun response ->
    response.set_header("X-Custom-Header", "value")
    response
)
```

## Running the Application

```fsharp
// Development server
app.run()

// With options
app.run_with_options("0.0.0.0", 5000, true)
```

Or use the Flask CLI:

```bash
# Build F# to Python
dotnet fable --lang python

# Run with Flask
flask --app app run --reload
```

## URL Building

```fsharp
// Generate URL for endpoint
let url = url_for "index"

// For static files
let staticUrl = url_for_static "static" "style.css"
```

## Important Notes

### Serialization

Flask's `jsonify` does not handle Fable types like `int32`. Use the Fable.Python `json` module which includes a `fableDefault` handler:

```fsharp
open Fable.Python.Json

// Use json.dumps with fableDefault for Fable type support
[<Get("/data")>]
static member get_data() =
    json.dumps({| name = "Alice"; age = 30 |}, ``default`` = fableDefault)

// Or use the convenience function
[<Get("/data")>]
static member get_data() =
    Json.dumps {| name = "Alice"; age = 30 |}
```

For simple cases where you're only using native Python types (strings, native ints), Flask's `jsonify` works:

```fsharp
[<Get("/simple")>]
static member simple() =
    jsonify {| message = "Hello"; count = 42n |}  // Use 'n' suffix for native int
```

## Development Mode

For hot-reloading during development:

```bash
# Terminal 1: Watch F# files
dotnet fable --lang python --watch

# Terminal 2: Run Flask with reload
flask --app app run --reload
```
