/// Type bindings for Flask web framework: https://flask.palletsprojects.com/
module Fable.Python.Flask

open Fable.Core

// fsharplint:disable MemberNames

/// Base interface for Flask request objects
type RequestBase =
    /// The URL of the current request
    abstract url: string

/// Flask request object containing information about the current HTTP request
type Request =
    inherit RequestBase

/// Flask application object
type Flask =
    /// Decorator to register a view function for a given URL rule
    abstract route: rule: string -> ((unit -> string) -> Flask)
    /// Decorator to register a view function for a given URL rule with specific HTTP methods
    abstract route: rule: string * methods: string array -> ((unit -> string) -> Flask)

/// Static constructor interface for Flask application
type FlaskStatic =
    /// Create a Flask application with a static URL path
    [<Emit("$0($1, static_url_path=$2)")>]
    abstract Create: name: string * static_url_path: string -> Flask

/// Flask application constructor
[<Import("Flask", "flask")>]
let Flask: FlaskStatic = nativeOnly

[<Erase>]
type IExports =
    /// Render a template by name
    /// See https://flask.palletsprojects.com/api/#flask.render_template
    abstract render_template: template_name_or_list: string -> string
    /// Render a template from a sequence of names (tries each until one succeeds)
    /// See https://flask.palletsprojects.com/api/#flask.render_template
    abstract render_template: template_name_or_list: string seq -> string
    /// The current request object
    /// See https://flask.palletsprojects.com/api/#flask.request
    abstract request: Request

    /// Generate a URL for the given endpoint with the given filename
    /// See https://flask.palletsprojects.com/api/#flask.url_for
    [<Emit("flask.url_for($0, filename=$1)")>]
    abstract url_for: endpoint: string * filename: string -> string

/// Flask web framework module
[<ImportAll("flask")>]
let flask: IExports = nativeOnly
