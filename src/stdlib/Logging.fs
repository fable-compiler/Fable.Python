/// Type bindings for Python logging module: https://docs.python.org/3/library/logging.html
module Fable.Python.Logging

open Fable.Core

// fsharplint:disable MemberNames

/// Logging levels
[<RequireQualifiedAccess>]
module Level =
    [<Literal>]
    let CRITICAL = 50

    [<Literal>]
    let FATAL = 50

    [<Literal>]
    let ERROR = 40

    [<Literal>]
    let WARNING = 30

    [<Literal>]
    let WARN = 30

    [<Literal>]
    let INFO = 20

    [<Literal>]
    let DEBUG = 10

    [<Literal>]
    let NOTSET = 0

/// Formatter for log records
[<Import("Formatter", "logging")>]
type Formatter(fmt: string, ?datefmt: string) =
    member _.format(record: obj) : string = nativeOnly

/// Handler base type
[<Import("Handler", "logging")>]
type Handler() =
    /// Set the logging level
    member _.setLevel(level: int) : unit = nativeOnly
    /// Set the formatter for this handler
    member _.setFormatter(formatter: Formatter) : unit = nativeOnly

/// StreamHandler - logs to a stream (stderr by default)
[<Import("StreamHandler", "logging")>]
type StreamHandler(?stream: obj) =
    inherit Handler()

/// FileHandler - logs to a file
[<Import("FileHandler", "logging")>]
type FileHandler(filename: string, ?mode: string) =
    inherit Handler()

/// Logger instance
[<Import("Logger", "logging")>]
type Logger(name: string, ?level: int) =
    /// Log a message with severity DEBUG
    member _.debug(msg: string) : unit = nativeOnly
    /// Log a message with severity INFO
    member _.info(msg: string) : unit = nativeOnly
    /// Log a message with severity WARNING
    member _.warning(msg: string) : unit = nativeOnly
    /// Log a message with severity ERROR
    member _.error(msg: string) : unit = nativeOnly
    /// Log a message with severity CRITICAL
    member _.critical(msg: string) : unit = nativeOnly
    /// Log a message with severity ERROR and exception info
    member _.``exception``(msg: string) : unit = nativeOnly

    /// Log a message with the specified level
    [<Emit("$0.log(int($1), $2)")>]
    member _.log(level: int, msg: string) : unit = nativeOnly

    /// Set the logging level
    [<Emit("$0.setLevel(int($1))")>]
    member _.setLevel(level: int) : unit = nativeOnly

    /// Get the effective logging level
    [<Emit("$0.getEffectiveLevel()")>]
    member _.getEffectiveLevel() : int = nativeOnly

    /// Check if the logger is enabled for the specified level
    [<Emit("$0.isEnabledFor(int($1))")>]
    member _.isEnabledFor(level: int) : bool = nativeOnly

    /// Add the specified handler to this logger
    [<Emit("$0.addHandler($1)")>]
    member _.addHandler(handler: Handler) : unit = nativeOnly

    /// Remove the specified handler from this logger
    [<Emit("$0.removeHandler($1)")>]
    member _.removeHandler(handler: Handler) : unit = nativeOnly

    /// Check if this logger has any handlers configured
    [<Emit("$0.hasHandlers()")>]
    member _.hasHandlers() : bool = nativeOnly

    /// The name of the logger
    member _.name: string = nativeOnly

[<Erase>]
type IExports =
    /// Log a message with severity DEBUG on the root logger
    abstract debug: msg: string -> unit
    /// Log a message with severity INFO on the root logger
    abstract info: msg: string -> unit
    /// Log a message with severity WARNING on the root logger
    abstract warning: msg: string -> unit
    /// Log a message with severity ERROR on the root logger
    abstract error: msg: string -> unit
    /// Log a message with severity CRITICAL on the root logger
    abstract critical: msg: string -> unit
    /// Log a message with severity ERROR and exception info on the root logger
    abstract ``exception``: msg: string -> unit
    /// Log a message with the specified level on the root logger
    abstract log: level: int * msg: string -> unit

    /// Return a logger with the specified name
    [<Emit("$0.getLogger($1)")>]
    abstract getLogger: name: string -> Logger

    /// Return the root logger
    [<Emit("$0.getLogger()")>]
    abstract getLogger: unit -> Logger

    /// Do basic configuration for the logging system
    [<Emit("$0.basicConfig($1...)")>]
    [<NamedParams(fromIndex = 0)>]
    abstract basicConfig:
        ?filename: string *
        ?filemode: string *
        ?format: string *
        ?datefmt: string *
        ?style: string *
        ?level: int *
        ?stream: obj *
        ?force: bool ->
            unit

    /// Disable all logging calls of severity level and below
    [<Emit("$0.disable($1)")>]
    abstract disable: level: int -> unit

    /// Logging level constants
    abstract DEBUG: int
    abstract INFO: int
    abstract WARNING: int
    abstract ERROR: int
    abstract CRITICAL: int

/// Logging facility for Python
[<ImportAll("logging")>]
let logging: IExports = nativeOnly
