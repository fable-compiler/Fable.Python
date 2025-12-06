/// FastAPI bindings for F# to Python web framework
///
/// FastAPI is a modern, fast web framework for building APIs with Python.
/// These bindings support the class-based approach using method decorators.
///
/// Usage: Create API endpoints using classes with decorated methods:
///
/// ```fsharp
/// open Fable.Python.FastAPI
///
/// let app = FastAPI()
///
/// [<APIClass>]
/// type API() =
///     [<Get("/")>]
///     static member root() =
///         {| message = "Hello World" |}
///
///     [<Get("/items/{item_id}")>]
///     static member get_item(item_id: int) =
///         {| item_id = item_id |}
///
///     [<Post("/items")>]
///     static member create_item(item: Item) =
///         {| status = "created"; item = item |}
/// ```
///
/// For routers, use the RouterGet, RouterPost, etc. attributes:
///
/// ```fsharp
/// let router = APIRouter(prefix = "/users")
///
/// [<APIClass>]
/// type UsersAPI() =
///     [<RouterGet("/")>]
///     static member list_users() = {| users = [] |}
/// ```
module Fable.Python.FastAPI

open System
open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

// ============================================================================
// API Class Attribute
// ============================================================================

/// Marks a class as a FastAPI endpoint class with class-level attributes.
/// This is equivalent to [<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
[<Erase; Py.ClassAttributesTemplate(Py.ClassAttributeStyle.Attributes, init = false)>]
type APIClassAttribute() =
    inherit Attribute()

// ============================================================================
// Route Decorator Attributes (using Py.DecorateTemplate)
// ============================================================================

/// GET route decorator for app
[<Erase; Py.DecorateTemplate("""app.get("{0}")""")>]
type GetAttribute(path: string) =
    inherit Attribute()

/// POST route decorator for app
[<Erase; Py.DecorateTemplate("""app.post("{0}")""")>]
type PostAttribute(path: string) =
    inherit Attribute()

/// PUT route decorator for app
[<Erase; Py.DecorateTemplate("""app.put("{0}")""")>]
type PutAttribute(path: string) =
    inherit Attribute()

/// DELETE route decorator for app
[<Erase; Py.DecorateTemplate("""app.delete("{0}")""")>]
type DeleteAttribute(path: string) =
    inherit Attribute()

/// PATCH route decorator for app
[<Erase; Py.DecorateTemplate("""app.patch("{0}")""")>]
type PatchAttribute(path: string) =
    inherit Attribute()

/// OPTIONS route decorator for app
[<Erase; Py.DecorateTemplate("""app.options("{0}")""")>]
type OptionsAttribute(path: string) =
    inherit Attribute()

/// HEAD route decorator for app
[<Erase; Py.DecorateTemplate("""app.head("{0}")""")>]
type HeadAttribute(path: string) =
    inherit Attribute()

/// WebSocket route decorator for app
[<Erase; Py.DecorateTemplate("""app.websocket("{0}")""")>]
type WebSocketAttribute(path: string) =
    inherit Attribute()

// ============================================================================
// Router Decorator Attributes
// ============================================================================

/// GET route decorator for router
[<Erase; Py.DecorateTemplate("""router.get("{0}")""")>]
type RouterGetAttribute(path: string) =
    inherit Attribute()

/// POST route decorator for router
[<Erase; Py.DecorateTemplate("""router.post("{0}")""")>]
type RouterPostAttribute(path: string) =
    inherit Attribute()

/// PUT route decorator for router
[<Erase; Py.DecorateTemplate("""router.put("{0}")""")>]
type RouterPutAttribute(path: string) =
    inherit Attribute()

/// DELETE route decorator for router
[<Erase; Py.DecorateTemplate("""router.delete("{0}")""")>]
type RouterDeleteAttribute(path: string) =
    inherit Attribute()

/// PATCH route decorator for router
[<Erase; Py.DecorateTemplate("""router.patch("{0}")""")>]
type RouterPatchAttribute(path: string) =
    inherit Attribute()

/// WebSocket route decorator for router
[<Erase; Py.DecorateTemplate("""router.websocket("{0}")""")>]
type RouterWebSocketAttribute(path: string) =
    inherit Attribute()

// ============================================================================
// Response Classes
// ============================================================================

/// JSON response class
[<Import("JSONResponse", "fastapi.responses")>]
type JSONResponse(content: obj, ?status_code: int) =
    class end

/// HTML response class
[<Import("HTMLResponse", "fastapi.responses")>]
type HTMLResponse(content: string, ?status_code: int) =
    class end

/// Plain text response class
[<Import("PlainTextResponse", "fastapi.responses")>]
type PlainTextResponse(content: string, ?status_code: int) =
    class end

/// Redirect response class
[<Import("RedirectResponse", "fastapi.responses")>]
type RedirectResponse(url: string, ?status_code: int) =
    class end

/// Streaming response class
[<Import("StreamingResponse", "fastapi.responses")>]
type StreamingResponse(content: obj, ?media_type: string) =
    class end

/// File response class
[<Import("FileResponse", "fastapi.responses")>]
type FileResponse(path: string, ?filename: string, ?media_type: string) =
    class end

// ============================================================================
// Request and WebSocket
// ============================================================================

/// FastAPI Request object
[<Import("Request", "fastapi")>]
type Request() =
    /// The request method (GET, POST, etc.)
    [<Emit("$0.method")>]
    member _.method: string = nativeOnly

    /// The request URL
    [<Emit("$0.url")>]
    member _.url: obj = nativeOnly

    /// The request headers
    [<Emit("$0.headers")>]
    member _.headers: obj = nativeOnly

    /// The query parameters
    [<Emit("$0.query_params")>]
    member _.query_params: obj = nativeOnly

    /// The path parameters
    [<Emit("$0.path_params")>]
    member _.path_params: obj = nativeOnly

    /// The cookies
    [<Emit("$0.cookies")>]
    member _.cookies: obj = nativeOnly

    /// The client information
    [<Emit("$0.client")>]
    member _.client: obj = nativeOnly

    /// Get the request body as JSON
    [<Emit("$0.json()")>]
    member _.json() : Async<obj> = nativeOnly

    /// Get the request body as bytes
    [<Emit("$0.body()")>]
    member _.body() : Async<byte array> = nativeOnly

    /// Get form data
    [<Emit("$0.form()")>]
    member _.form() : Async<obj> = nativeOnly

/// WebSocket connection
[<Import("WebSocket", "fastapi")>]
type WebSocket() =
    /// Accept the WebSocket connection
    [<Emit("$0.accept()")>]
    member _.accept() : Async<unit> = nativeOnly

    /// Close the WebSocket connection
    [<Emit("$0.close()")>]
    member _.close() : Async<unit> = nativeOnly

    /// Close with a specific code
    [<Emit("$0.close($1)")>]
    member _.close_with_code(_code: int) : Async<unit> = nativeOnly

    /// Send text data
    [<Emit("$0.send_text($1)")>]
    member _.send_text(_data: string) : Async<unit> = nativeOnly

    /// Send bytes data
    [<Emit("$0.send_bytes($1)")>]
    member _.send_bytes(_data: byte array) : Async<unit> = nativeOnly

    /// Send JSON data
    [<Emit("$0.send_json($1)")>]
    member _.send_json(_data: obj) : Async<unit> = nativeOnly

    /// Receive text data
    [<Emit("$0.receive_text()")>]
    member _.receive_text() : Async<string> = nativeOnly

    /// Receive bytes data
    [<Emit("$0.receive_bytes()")>]
    member _.receive_bytes() : Async<byte array> = nativeOnly

    /// Receive JSON data
    [<Emit("$0.receive_json()")>]
    member _.receive_json() : Async<obj> = nativeOnly

// ============================================================================
// HTTP Exceptions
// ============================================================================

/// HTTP exception for returning error responses
[<Import("HTTPException", "fastapi")>]
type HTTPException(status_code: int, ?detail: string) =
    class end

// ============================================================================
// Dependency Injection
// ============================================================================

/// Dependency injection marker
[<Import("Depends", "fastapi")>]
[<Emit("$0($1)")>]
let Depends (_dependency: unit -> 'T) : 'T = nativeOnly

/// Dependency injection with callable
[<Import("Depends", "fastapi")>]
[<Emit("$0($1)")>]
let DependsOn (_dependency: obj) : obj = nativeOnly

// ============================================================================
// Path, Query, Body, Header, Cookie parameters
// ============================================================================

/// Path parameter with metadata
[<Import("Path", "fastapi")>]
type PathParam =
    /// Create a path parameter with default value
    [<Emit("$0(default=$1)")>]
    static member Default(_value: 'T) : 'T = nativeOnly

    /// Create a path parameter with title
    [<Emit("$0(title=$1)")>]
    static member Title(_title: string) : 'T = nativeOnly

    /// Create a path parameter with description
    [<Emit("$0(description=$1)")>]
    static member Description(_description: string) : 'T = nativeOnly

    /// Create a path parameter with ge constraint
    [<Emit("$0(ge=$1)")>]
    static member Ge(_value: float) : 'T = nativeOnly

    /// Create a path parameter with le constraint
    [<Emit("$0(le=$1)")>]
    static member Le(_value: float) : 'T = nativeOnly

    /// Create a path parameter with gt constraint
    [<Emit("$0(gt=$1)")>]
    static member Gt(_value: float) : 'T = nativeOnly

    /// Create a path parameter with lt constraint
    [<Emit("$0(lt=$1)")>]
    static member Lt(_value: float) : 'T = nativeOnly

/// Query parameter with metadata
[<Import("Query", "fastapi")>]
type QueryParam =
    /// Create a query parameter with default value
    [<Emit("$0(default=$1)")>]
    static member Default(_value: 'T) : 'T = nativeOnly

    /// Create an optional query parameter (default None)
    [<Emit("$0(default=None)")>]
    static member Optional() : 'T option = nativeOnly

    /// Create a query parameter with title
    [<Emit("$0(title=$1)")>]
    static member Title(_title: string) : 'T = nativeOnly

    /// Create a query parameter with description
    [<Emit("$0(description=$1)")>]
    static member Description(_description: string) : 'T = nativeOnly

    /// Create a query parameter with alias
    [<Emit("$0(alias=$1)")>]
    static member Alias(_alias: string) : 'T = nativeOnly

    /// Create a query parameter with min_length constraint
    [<Emit("$0(min_length=$1)")>]
    static member MinLength(_length: int) : 'T = nativeOnly

    /// Create a query parameter with max_length constraint
    [<Emit("$0(max_length=$1)")>]
    static member MaxLength(_length: int) : 'T = nativeOnly

    /// Create a query parameter with regex pattern
    [<Emit("$0(pattern=$1)")>]
    static member Pattern(_pattern: string) : 'T = nativeOnly

/// Body parameter with metadata
[<Import("Body", "fastapi")>]
type BodyParam =
    /// Create a body parameter with embed flag
    [<Emit("$0(embed=$1)")>]
    static member Embed(_embed: bool) : 'T = nativeOnly

    /// Create a body parameter with title
    [<Emit("$0(title=$1)")>]
    static member Title(_title: string) : 'T = nativeOnly

    /// Create a body parameter with description
    [<Emit("$0(description=$1)")>]
    static member Description(_description: string) : 'T = nativeOnly

/// Header parameter with metadata
[<Import("Header", "fastapi")>]
type HeaderParam =
    /// Create a header parameter with default value
    [<Emit("$0(default=$1)")>]
    static member Default(_value: 'T) : 'T = nativeOnly

    /// Create an optional header parameter
    [<Emit("$0(default=None)")>]
    static member Optional() : 'T option = nativeOnly

    /// Create a header parameter with alias
    [<Emit("$0(alias=$1)")>]
    static member Alias(_alias: string) : 'T = nativeOnly

/// Cookie parameter with metadata
[<Import("Cookie", "fastapi")>]
type CookieParam =
    /// Create a cookie parameter with default value
    [<Emit("$0(default=$1)")>]
    static member Default(_value: 'T) : 'T = nativeOnly

    /// Create an optional cookie parameter
    [<Emit("$0(default=None)")>]
    static member Optional() : 'T option = nativeOnly

// ============================================================================
// File Upload
// ============================================================================

/// Uploaded file type
[<Import("UploadFile", "fastapi")>]
type UploadFile() =
    /// The filename
    [<Emit("$0.filename")>]
    member _.filename: string = nativeOnly

    /// The content type
    [<Emit("$0.content_type")>]
    member _.content_type: string = nativeOnly

    /// The file size
    [<Emit("$0.size")>]
    member _.size: int = nativeOnly

    /// Read the file content
    [<Emit("$0.read()")>]
    member _.read() : Async<byte array> = nativeOnly

    /// Write content to the file
    [<Emit("$0.write($1)")>]
    member _.write(_data: byte array) : Async<unit> = nativeOnly

    /// Seek to a position
    [<Emit("$0.seek($1)")>]
    member _.seek(_pos: int) : Async<unit> = nativeOnly

    /// Close the file
    [<Emit("$0.close()")>]
    member _.close() : Async<unit> = nativeOnly

/// File parameter marker
[<Import("File", "fastapi")>]
[<Emit("$0()")>]
let File() : UploadFile = nativeOnly

/// Form parameter marker
[<Import("Form", "fastapi")>]
type FormParam =
    /// Create a form parameter with default value
    [<Emit("$0(default=$1)")>]
    static member Default(_value: 'T) : 'T = nativeOnly

    /// Create a form parameter
    [<Emit("$0()")>]
    static member Create() : 'T = nativeOnly

// ============================================================================
// APIRouter
// ============================================================================

/// API Router for modular route organization
[<Import("APIRouter", "fastapi")>]
type APIRouter(?prefix: string, ?tags: ResizeArray<string>) =
    /// Include another router
    [<Emit("$0.include_router($1)")>]
    member _.include_router(_router: APIRouter) : unit = nativeOnly

    /// Include another router with prefix
    [<Emit("$0.include_router($1, prefix=$2)")>]
    member _.include_router_with_prefix(_router: APIRouter, _prefix: string) : unit = nativeOnly

    /// Include another router with prefix and tags
    [<Emit("$0.include_router($1, prefix=$2, tags=$3)")>]
    member _.include_router_with_prefix_and_tags(_router: APIRouter, _prefix: string, _tags: ResizeArray<string>) : unit = nativeOnly

// ============================================================================
// FastAPI Application
// ============================================================================

/// FastAPI application instance
[<Import("FastAPI", "fastapi")>]
type FastAPI(?title: string, ?description: string, ?version: string) =
    /// Include a router
    [<Emit("$0.include_router($1)")>]
    member _.include_router(_router: APIRouter) : unit = nativeOnly

    /// Include a router with prefix
    [<Emit("$0.include_router($1, prefix=$2)")>]
    member _.include_router_with_prefix(_router: APIRouter, _prefix: string) : unit = nativeOnly

    /// Include a router with prefix and tags
    [<Emit("$0.include_router($1, prefix=$2, tags=$3)")>]
    member _.include_router_with_prefix_and_tags(_router: APIRouter, _prefix: string, _tags: ResizeArray<string>) : unit = nativeOnly

    /// Add middleware
    [<Emit("$0.add_middleware($1)")>]
    member _.add_middleware(_middleware: obj) : unit = nativeOnly

    /// Add exception handler
    [<Emit("$0.add_exception_handler($1, $2)")>]
    member _.add_exception_handler(_exc_class: obj, _handler: obj) : unit = nativeOnly

    /// Get the OpenAPI schema
    [<Emit("$0.openapi()")>]
    member _.openapi() : obj = nativeOnly

    /// The OpenAPI schema (cached)
    [<Emit("$0.openapi_schema")>]
    member _.openapi_schema: obj = nativeOnly

    /// The app title
    [<Emit("$0.title")>]
    member _.title: string = nativeOnly

    /// The app description
    [<Emit("$0.description")>]
    member _.description: string = nativeOnly

    /// The app version
    [<Emit("$0.version")>]
    member _.version: string = nativeOnly

// ============================================================================
// Status Codes (commonly used)
// ============================================================================

/// HTTP status codes module
module status =
    let HTTP_200_OK = 200
    let HTTP_201_CREATED = 201
    let HTTP_204_NO_CONTENT = 204
    let HTTP_301_MOVED_PERMANENTLY = 301
    let HTTP_302_FOUND = 302
    let HTTP_304_NOT_MODIFIED = 304
    let HTTP_307_TEMPORARY_REDIRECT = 307
    let HTTP_308_PERMANENT_REDIRECT = 308
    let HTTP_400_BAD_REQUEST = 400
    let HTTP_401_UNAUTHORIZED = 401
    let HTTP_403_FORBIDDEN = 403
    let HTTP_404_NOT_FOUND = 404
    let HTTP_405_METHOD_NOT_ALLOWED = 405
    let HTTP_409_CONFLICT = 409
    let HTTP_422_UNPROCESSABLE_ENTITY = 422
    let HTTP_429_TOO_MANY_REQUESTS = 429
    let HTTP_500_INTERNAL_SERVER_ERROR = 500
    let HTTP_502_BAD_GATEWAY = 502
    let HTTP_503_SERVICE_UNAVAILABLE = 503
    let HTTP_504_GATEWAY_TIMEOUT = 504

// ============================================================================
// Background Tasks
// ============================================================================

/// Background tasks for running tasks after response
[<Import("BackgroundTasks", "fastapi")>]
type BackgroundTasks() =
    /// Add a task to run in the background
    [<Emit("$0.add_task($1)")>]
    member _.add_task(_func: unit -> unit) : unit = nativeOnly

    /// Add a task with arguments
    [<Emit("$0.add_task($1, $2...)")>]
    member _.add_task_with_args(_func: obj, [<ParamArray>] _args: obj array) : unit = nativeOnly

// ============================================================================
// Security
// ============================================================================

/// OAuth2 password bearer scheme
[<AbstractClass>]
type OAuth2PasswordBearer =
    [<Import("OAuth2PasswordBearer", "fastapi.security")>]
    [<Emit("$0(tokenUrl=$1)")>]
    static member Create(_tokenUrl: string) : OAuth2PasswordBearer = nativeOnly

/// OAuth2 password request form
[<Import("OAuth2PasswordRequestForm", "fastapi.security")>]
type OAuth2PasswordRequestForm() =
    /// The username
    [<Emit("$0.username")>]
    member _.username: string = nativeOnly

    /// The password
    [<Emit("$0.password")>]
    member _.password: string = nativeOnly

    /// The scope
    [<Emit("$0.scope")>]
    member _.scope: string = nativeOnly

/// HTTP Basic authentication
[<Import("HTTPBasic", "fastapi.security")>]
type HTTPBasic() =
    class end

/// HTTP Basic credentials
[<Import("HTTPBasicCredentials", "fastapi.security")>]
type HTTPBasicCredentials() =
    /// The username
    [<Emit("$0.username")>]
    member _.username: string = nativeOnly

    /// The password
    [<Emit("$0.password")>]
    member _.password: string = nativeOnly

/// HTTP Bearer authentication
[<Import("HTTPBearer", "fastapi.security")>]
type HTTPBearer() =
    class end

/// HTTP Bearer credentials (token in Authorization header)
[<Import("HTTPAuthorizationCredentials", "fastapi.security")>]
type HTTPAuthorizationCredentials() =
    /// The scheme (e.g., "Bearer")
    [<Emit("$0.scheme")>]
    member _.scheme: string = nativeOnly

    /// The credentials (the token)
    [<Emit("$0.credentials")>]
    member _.credentials: string = nativeOnly

/// API Key in header
[<Import("APIKeyHeader", "fastapi.security")>]
type APIKeyHeader(name: string) =
    class end

/// API Key in query parameter
[<Import("APIKeyQuery", "fastapi.security")>]
type APIKeyQuery(name: string) =
    class end

/// API Key in cookie
[<Import("APIKeyCookie", "fastapi.security")>]
type APIKeyCookie(name: string) =
    class end

// ============================================================================
// CORS Middleware
// ============================================================================

/// CORS middleware for handling Cross-Origin Resource Sharing
[<Import("CORSMiddleware", "fastapi.middleware.cors")>]
type CORSMiddleware =
    class end

/// CORS middleware configuration helper
module CORSMiddleware =
    /// Create CORS middleware configuration
    [<Import("CORSMiddleware", "fastapi.middleware.cors")>]
    [<Emit("$0")>]
    let middleware: obj = nativeOnly

    /// Add CORS middleware to app with all origins allowed
    [<Emit("$0.add_middleware($1, allow_origins=$2, allow_credentials=$3, allow_methods=$4, allow_headers=$5)")>]
    let addToApp (_app: FastAPI) (_middleware: obj) (_origins: string array) (_credentials: bool) (_methods: string array) (_headers: string array) : unit = nativeOnly

// ============================================================================
// Encoders
// ============================================================================

/// JSON-compatible encoder for converting objects
[<Import("jsonable_encoder", "fastapi.encoders")>]
[<Emit("$0($1)")>]
let jsonable_encoder (_obj: 'T) : obj = nativeOnly

/// JSON-compatible encoder with options
[<Import("jsonable_encoder", "fastapi.encoders")>]
[<Emit("$0($1, exclude_unset=$2)")>]
let jsonable_encoder_exclude_unset (_obj: 'T) (_exclude_unset: bool) : obj = nativeOnly

// ============================================================================
// Testclient
// ============================================================================

/// Test client for testing FastAPI applications
[<Import("TestClient", "fastapi.testclient")>]
type TestClient(app: FastAPI) =
    /// Make a GET request
    [<Emit("$0.get($1)")>]
    member _.get(_url: string) : obj = nativeOnly

    /// Make a POST request
    [<Emit("$0.post($1)")>]
    member _.post(_url: string) : obj = nativeOnly

    /// Make a POST request with JSON body
    [<Emit("$0.post($1, json=$2)")>]
    member _.post_json(_url: string, _json: obj) : obj = nativeOnly

    /// Make a PUT request
    [<Emit("$0.put($1)")>]
    member _.put(_url: string) : obj = nativeOnly

    /// Make a PUT request with JSON body
    [<Emit("$0.put($1, json=$2)")>]
    member _.put_json(_url: string, _json: obj) : obj = nativeOnly

    /// Make a DELETE request
    [<Emit("$0.delete($1)")>]
    member _.delete(_url: string) : obj = nativeOnly

    /// Make a PATCH request
    [<Emit("$0.patch($1)")>]
    member _.patch(_url: string) : obj = nativeOnly

    /// Make a PATCH request with JSON body
    [<Emit("$0.patch($1, json=$2)")>]
    member _.patch_json(_url: string, _json: obj) : obj = nativeOnly

// ============================================================================
// Response type for test client
// ============================================================================

/// Response from test client
type TestResponse =
    abstract status_code: int
    abstract json: unit -> obj
    abstract text: string
    abstract headers: obj
