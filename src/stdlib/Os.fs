/// Type bindings for Python os module: https://docs.python.org/3/library/os.html
module Fable.Python.Os

open System
open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Change the current working directory to the specified path
    /// See https://docs.python.org/3/library/os.html#os.chdir
    abstract chdir: path: string -> unit
    /// Change the root directory of the current process to path
    /// See https://docs.python.org/3/library/os.html#os.chroot
    abstract chroot: path: string -> unit
    /// Close file descriptor fd
    /// See https://docs.python.org/3/library/os.html#os.close
    abstract close: fd: int -> unit
    /// A mapping object representing the environment variables
    /// See https://docs.python.org/3/library/os.html#os.environ
    abstract environ: Dictionary<string, string>
    /// Return a string representing the current working directory
    /// See https://docs.python.org/3/library/os.html#os.getcwd
    abstract getcwd: unit -> string
    /// Return the value of the environment variable key as an option
    /// See https://docs.python.org/3/library/os.html#os.getenv
    abstract getenv: key: string -> string option
    /// Return the value of the environment variable key or default if not set
    /// See https://docs.python.org/3/library/os.html#os.getenv
    abstract getenv: key: string * ``default``: string -> string
    /// Send signal sig to the process pid
    /// See https://docs.python.org/3/library/os.html#os.kill
    abstract kill: pid: int * ``sig``: int -> unit
    /// Create a directory named path
    /// See https://docs.python.org/3/library/os.html#os.mkdir
    abstract mkdir: path: string -> unit
    /// Create a directory named path with optional mode
    /// See https://docs.python.org/3/library/os.html#os.mkdir
    abstract mkdir: path: string * mode: int -> unit
    /// Recursive directory creation function
    /// See https://docs.python.org/3/library/os.html#os.makedirs
    abstract makedirs: path: string -> unit
    /// Recursive directory creation with optional mode and exist_ok flag
    /// See https://docs.python.org/3/library/os.html#os.makedirs
    abstract makedirs: path: string * mode: int * exist_ok: bool -> unit
    /// Set the environment variable named key to the string value
    /// See https://docs.python.org/3/library/os.html#os.putenv
    abstract putenv: key: string * value: string -> unit
    /// Return a list containing the names of the entries in the directory
    /// See https://docs.python.org/3/library/os.html#os.listdir
    abstract listdir: path: string -> string array
    /// Remove (delete) the file path
    /// See https://docs.python.org/3/library/os.html#os.remove
    abstract remove: path: string -> unit
    /// Rename the file or directory src to dst
    /// See https://docs.python.org/3/library/os.html#os.rename
    abstract rename: src: string * dst: string -> unit
    /// Remove (delete) the directory path
    /// See https://docs.python.org/3/library/os.html#os.rmdir
    abstract rmdir: path: string -> unit
    /// Test whether a path exists
    /// See https://docs.python.org/3/library/os.path.html#os.path.exists
    abstract path: PathModule

/// Operations on pathnames
and [<Erase>] PathModule =
    /// Return True if path refers to an existing path
    /// See https://docs.python.org/3/library/os.path.html#os.path.exists
    abstract exists: path: string -> bool
    /// Return True if path is an existing regular file
    /// See https://docs.python.org/3/library/os.path.html#os.path.isfile
    abstract isfile: path: string -> bool
    /// Return True if path is an existing directory
    /// See https://docs.python.org/3/library/os.path.html#os.path.isdir
    abstract isdir: path: string -> bool
    /// Join one or more path components intelligently
    /// See https://docs.python.org/3/library/os.path.html#os.path.join
    abstract join: [<ParamArray>] paths: string array -> string
    /// Return the directory name of pathname path
    /// See https://docs.python.org/3/library/os.path.html#os.path.dirname
    abstract dirname: path: string -> string
    /// Return the base name of pathname path
    /// See https://docs.python.org/3/library/os.path.html#os.path.basename
    abstract basename: path: string -> string
    /// Return the argument with an initial component of ~ or ~user replaced
    /// See https://docs.python.org/3/library/os.path.html#os.path.expanduser
    abstract expanduser: path: string -> string
    /// Return a normalized absolutized version of the pathname path
    /// See https://docs.python.org/3/library/os.path.html#os.path.abspath
    abstract abspath: path: string -> string
    /// Split the pathname path into a pair (head, tail)
    /// See https://docs.python.org/3/library/os.path.html#os.path.split
    abstract split: path: string -> string * string
    /// Split the pathname path into a pair (root, ext)
    /// See https://docs.python.org/3/library/os.path.html#os.path.splitext
    abstract splitext: path: string -> string * string


/// Miscellaneous operating system interfaces
[<ImportAll("os")>]
let os: IExports = nativeOnly
