/// Flask bindings for F# to Python web framework
///
/// Flask is a lightweight WSGI web application framework for Python.
/// These bindings support the class-based approach using method decorators.
///
/// Usage: Create routes using classes with decorated methods:
///
/// ```fsharp
/// open Fable.Python.Flask
///
/// let app = Flask.Create(__name__)
///
/// [<APIClass>]
/// type Routes() =
///     [<Route("/")>]
///     static member index() =
///         "Hello World!"
///
///     [<Route("/users/<int:user_id>")>]
///     static member get_user(user_id: int) =
///         {| user_id = user_id |}
///
///     [<Route("/users", methods = [| "POST" |])>]
///     static member create_user() =
///         {| status = "created" |}
/// ```
module Fable.Python.Flask

open System
open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

// ============================================================================
// API Class Attribute
// ============================================================================

/// Marks a class as a Flask endpoint class with class-level attributes.
[<Erase; Py.ClassAttributesTemplate(Py.ClassAttributeStyle.Attributes, init = false)>]
type APIClassAttribute() =
    inherit Attribute()

// ============================================================================
// Route Decorator Attributes
// ============================================================================

/// Route decorator for Flask app
[<Erase; Py.DecorateTemplate("""app.route("{0}")""")>]
type RouteAttribute(path: string) =
    inherit Attribute()

/// Route decorator with HTTP methods
[<Erase; Py.DecorateTemplate("""app.route("{0}", methods={1})""")>]
type RouteMethodsAttribute(path: string, methods: string array) =
    inherit Attribute()

/// GET route decorator
[<Erase; Py.DecorateTemplate("""app.get("{0}")""")>]
type GetAttribute(path: string) =
    inherit Attribute()

/// POST route decorator
[<Erase; Py.DecorateTemplate("""app.post("{0}")""")>]
type PostAttribute(path: string) =
    inherit Attribute()

/// PUT route decorator
[<Erase; Py.DecorateTemplate("""app.put("{0}")""")>]
type PutAttribute(path: string) =
    inherit Attribute()

/// DELETE route decorator
[<Erase; Py.DecorateTemplate("""app.delete("{0}")""")>]
type DeleteAttribute(path: string) =
    inherit Attribute()

/// PATCH route decorator
[<Erase; Py.DecorateTemplate("""app.patch("{0}")""")>]
type PatchAttribute(path: string) =
    inherit Attribute()

// ============================================================================
// Blueprint Route Decorators
// ============================================================================

/// Blueprint route decorator
[<Erase; Py.DecorateTemplate("""bp.route("{0}")""")>]
type BpRouteAttribute(path: string) =
    inherit Attribute()

/// Blueprint GET route decorator
[<Erase; Py.DecorateTemplate("""bp.get("{0}")""")>]
type BpGetAttribute(path: string) =
    inherit Attribute()

/// Blueprint POST route decorator
[<Erase; Py.DecorateTemplate("""bp.post("{0}")""")>]
type BpPostAttribute(path: string) =
    inherit Attribute()

/// Blueprint PUT route decorator
[<Erase; Py.DecorateTemplate("""bp.put("{0}")""")>]
type BpPutAttribute(path: string) =
    inherit Attribute()

/// Blueprint DELETE route decorator
[<Erase; Py.DecorateTemplate("""bp.delete("{0}")""")>]
type BpDeleteAttribute(path: string) =
    inherit Attribute()

// ============================================================================
// Request Object
// ============================================================================

/// Flask request object containing information about the current HTTP request
[<Import("request", "flask")>]
type Request() =
    /// The request method (GET, POST, etc.)
    [<Emit("$0.method")>]
    member _.method: string = nativeOnly

    /// The full URL of the request
    [<Emit("$0.url")>]
    member _.url: string = nativeOnly

    /// The URL path
    [<Emit("$0.path")>]
    member _.path: string = nativeOnly

    /// Query string arguments as a dict
    [<Emit("$0.args")>]
    member _.args: obj = nativeOnly

    /// Form data from POST/PUT requests
    [<Emit("$0.form")>]
    member _.form: obj = nativeOnly

    /// Combined args and form data
    [<Emit("$0.values")>]
    member _.values: obj = nativeOnly

    /// Request headers
    [<Emit("$0.headers")>]
    member _.headers: obj = nativeOnly

    /// Cookies
    [<Emit("$0.cookies")>]
    member _.cookies: obj = nativeOnly

    /// The raw request data as bytes
    [<Emit("$0.data")>]
    member _.data: byte array = nativeOnly

    /// Get JSON data from request body
    [<Emit("$0.get_json()")>]
    member _.get_json() : obj = nativeOnly

    /// Get JSON data with options
    [<Emit("$0.get_json(force=$1, silent=$2)")>]
    member _.get_json_with_options(_force: bool, _silent: bool) : obj = nativeOnly

    /// Check if request is JSON
    [<Emit("$0.is_json")>]
    member _.is_json: bool = nativeOnly

    /// Content type of request
    [<Emit("$0.content_type")>]
    member _.content_type: string = nativeOnly

/// The current request object (context local)
[<Import("request", "flask")>]
let request: Request = nativeOnly

// ============================================================================
// Response Helpers
// ============================================================================

/// Create a JSON response
[<Import("jsonify", "flask")>]
[<Emit("$0($1)")>]
let jsonify (_data: obj) : obj = nativeOnly

/// Create a redirect response
[<Import("redirect", "flask")>]
[<Emit("$0($1)")>]
let redirect (_location: string) : obj = nativeOnly

/// Create a redirect response with status code
[<Import("redirect", "flask")>]
[<Emit("$0($1, code=$2)")>]
let redirect_with_code (_location: string) (_code: int) : obj = nativeOnly

/// Generate a URL for the given endpoint
[<Import("url_for", "flask")>]
[<Emit("$0($1)")>]
let url_for (_endpoint: string) : string = nativeOnly

/// Generate a URL for the given endpoint with filename
[<Import("url_for", "flask")>]
[<Emit("$0($1, filename=$2)")>]
let url_for_static (_endpoint: string) (_filename: string) : string = nativeOnly

/// Render a template
[<Import("render_template", "flask")>]
[<Emit("$0($1)")>]
let render_template (_template_name: string) : string = nativeOnly

/// Render a template with context
[<Import("render_template", "flask")>]
[<Emit("$0($1, **$2)")>]
let render_template_with_context (_template_name: string) (_context: obj) : string = nativeOnly

/// Abort request with HTTP status code
[<Import("abort", "flask")>]
[<Emit("$0($1)")>]
let abort (_code: int) : unit = nativeOnly

/// Abort request with status code and description
[<Import("abort", "flask")>]
[<Emit("$0($1, description=$2)")>]
let abort_with_message (_code: int) (_description: string) : unit = nativeOnly

// ============================================================================
// Response Class
// ============================================================================

/// Flask Response object
[<Import("Response", "flask")>]
type Response(?response: string, ?status: int, ?mimetype: string) =
    /// Set a header
    [<Emit("$0.headers[$1] = $2")>]
    member _.set_header(_name: string, _value: string) : unit = nativeOnly

    /// Set a cookie
    [<Emit("$0.set_cookie($1, $2)")>]
    member _.set_cookie(_key: string, _value: string) : unit = nativeOnly

    /// Set a cookie with options
    [<Emit("$0.set_cookie($1, $2, max_age=$3, httponly=$4)")>]
    member _.set_cookie_with_options(_key: string, _value: string, _max_age: int, _httponly: bool) : unit = nativeOnly

    /// Delete a cookie
    [<Emit("$0.delete_cookie($1)")>]
    member _.delete_cookie(_key: string) : unit = nativeOnly

/// Create a response with make_response
[<Import("make_response", "flask")>]
[<Emit("$0($1)")>]
let make_response (_body: obj) : Response = nativeOnly

/// Create a response with status code
[<Import("make_response", "flask")>]
[<Emit("$0($1, $2)")>]
let make_response_with_status (_body: obj) (_status: int) : Response = nativeOnly

// ============================================================================
// Blueprint
// ============================================================================

/// Flask Blueprint for modular applications
[<Import("Blueprint", "flask")>]
type Blueprint(name: string, import_name: string, ?url_prefix: string) =
    class end

// ============================================================================
// Flask Application
// ============================================================================

/// Flask application instance
[<Import("Flask", "flask")>]
type Flask(import_name: string, ?static_url_path: string, ?static_folder: string, ?template_folder: string) =
    /// Register a blueprint
    [<Emit("$0.register_blueprint($1)")>]
    member _.register_blueprint(_blueprint: Blueprint) : unit = nativeOnly

    /// Register a blueprint with URL prefix
    [<Emit("$0.register_blueprint($1, url_prefix=$2)")>]
    member _.register_blueprint_with_prefix(_blueprint: Blueprint, _url_prefix: string) : unit = nativeOnly

    /// Run the development server
    [<Emit("$0.run()")>]
    member _.run() : unit = nativeOnly

    /// Run the development server with options
    [<Emit("$0.run(host=$1, port=$2, debug=$3)")>]
    member _.run_with_options(_host: string, _port: int, _debug: bool) : unit = nativeOnly

    /// Application configuration
    [<Emit("$0.config")>]
    member _.config: obj = nativeOnly

    /// Add a URL rule
    [<Emit("$0.add_url_rule($1, $2, $3)")>]
    member _.add_url_rule(_rule: string, _endpoint: string, _view_func: obj) : unit = nativeOnly

    /// Register an error handler
    [<Emit("$0.errorhandler($1)")>]
    member _.errorhandler(_code: int) : (('T -> obj) -> unit) = nativeOnly

    /// Before request decorator
    [<Emit("$0.before_request")>]
    member _.before_request: ((unit -> unit) -> unit) = nativeOnly

    /// After request decorator
    [<Emit("$0.after_request")>]
    member _.after_request: ((Response -> Response) -> unit) = nativeOnly

/// Static constructor for Flask (alternative syntax)
module Flask =
    /// Create a Flask application
    [<Import("Flask", "flask")>]
    [<Emit("$0($1)")>]
    let Create (_import_name: string) : Flask = nativeOnly

    /// Create a Flask application with static URL path
    [<Import("Flask", "flask")>]
    [<Emit("$0($1, static_url_path=$2)")>]
    let CreateWithStaticPath (_import_name: string) (_static_url_path: string) : Flask = nativeOnly

// ============================================================================
// Session
// ============================================================================

/// Flask session object (requires secret key)
[<Import("session", "flask")>]
type Session() =
    /// Get a session value
    [<Emit("$0[$1]")>]
    member _.get(_key: string) : obj = nativeOnly

    /// Set a session value
    [<Emit("$0[$1] = $2")>]
    member _.set(_key: string, _value: obj) : unit = nativeOnly

    /// Remove a session value
    [<Emit("$0.pop($1, None)")>]
    member _.pop(_key: string) : obj = nativeOnly

    /// Clear the session
    [<Emit("$0.clear()")>]
    member _.clear() : unit = nativeOnly

/// The current session object
[<Import("session", "flask")>]
let session: Session = nativeOnly

// ============================================================================
// Flash Messages
// ============================================================================

/// Flash a message to the user
[<Import("flash", "flask")>]
[<Emit("$0($1)")>]
let flash (_message: string) : unit = nativeOnly

/// Flash a message with a category
[<Import("flash", "flask")>]
[<Emit("$0($1, $2)")>]
let flash_with_category (_message: string) (_category: string) : unit = nativeOnly

/// Get flashed messages
[<Import("get_flashed_messages", "flask")>]
[<Emit("$0()")>]
let get_flashed_messages () : string array = nativeOnly

/// Get flashed messages with categories
[<Import("get_flashed_messages", "flask")>]
[<Emit("$0(with_categories=True)")>]
let get_flashed_messages_with_categories () : (string * string) array = nativeOnly

// ============================================================================
// Application Context
// ============================================================================

/// Current application context
[<Import("current_app", "flask")>]
let current_app: Flask = nativeOnly

/// Application context globals
[<Import("g", "flask")>]
let g: obj = nativeOnly
