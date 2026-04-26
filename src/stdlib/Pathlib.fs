/// Type bindings for Python pathlib module: https://docs.python.org/3/library/pathlib.html
module Fable.Python.Pathlib

open System
open Fable.Core

// fsharplint:disable MemberNames

/// Represents a filesystem path on the current OS (POSIX or Windows).
/// Paths are immutable; operations return new Path instances.
/// See https://docs.python.org/3/library/pathlib.html#pathlib.Path
[<Import("Path", "pathlib")>]
type Path(path: string) =
    /// Construct a Path by joining multiple path segments.
    /// Equivalent to Path(parts[0]) / parts[1] / …
    [<Emit("Path($0...)")>]
    new([<ParamArray>] paths: string[]) = Path("")

    // -------------------------------------------------------------------------
    // Properties
    // -------------------------------------------------------------------------

    /// The final path component (file or directory name).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.name
    member _.name: string = nativeOnly

    /// The final path component without its last suffix (file extension).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.stem
    member _.stem: string = nativeOnly

    /// The last file extension of the final component (e.g. ".py"), or "" if none.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.suffix
    member _.suffix: string = nativeOnly

    /// All file extensions of the final component (e.g. [".tar"; ".gz"]).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.suffixes
    member _.suffixes: ResizeArray<string> = nativeOnly

    /// The logical parent of the path (directory containing this path).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.parent
    member _.parent: Path = nativeOnly

    /// Immutable sequence of the logical ancestors of the path.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.parents
    member _.parents: ResizeArray<Path> = nativeOnly

    /// The path's components as a tuple of strings.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.parts
    member _.parts: ResizeArray<string> = nativeOnly

    /// The root component of the path (e.g. "/" on POSIX), or "".
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.root
    member _.root: string = nativeOnly

    /// The concatenation of drive and root (e.g. "/" or "C:\\"), or "".
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.anchor
    member _.anchor: string = nativeOnly

    /// The drive letter or name (relevant on Windows), or "".
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.drive
    member _.drive: string = nativeOnly

    // -------------------------------------------------------------------------
    // Path arithmetic
    // -------------------------------------------------------------------------

    /// Return a new Path by appending a child string segment.
    /// This mirrors Python's ``path / "child"`` operator.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.__truediv__
    [<Emit("$0 / $1")>]
    static member (/) (left: Path, right: string) : Path = nativeOnly

    /// Return a new Path by appending another Path.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.__truediv__
    [<Emit("$0 / $1")>]
    static member (/) (left: Path, right: Path) : Path = nativeOnly

    /// Join one or more path segments to this path and return the result.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.joinpath
    [<Emit("$0.joinpath($1...)")>]
    member _.joinpath([<ParamArray>] parts: string[]) : Path = nativeOnly

    // -------------------------------------------------------------------------
    // Tests (pure — no I/O)
    // -------------------------------------------------------------------------

    /// Return True if the path is absolute (has both a root and, if applicable, a drive).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.is_absolute
    member _.is_absolute() : bool = nativeOnly

    /// Return True if this path is relative to other (3.9+).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.is_relative_to
    member _.is_relative_to(other: Path) : bool = nativeOnly

    /// Return True if this path is relative to the string path other (3.9+).
    [<Emit("$0.is_relative_to($1)")>]
    member _.is_relative_to(other: string) : bool = nativeOnly

    // -------------------------------------------------------------------------
    // Transformations (pure — no I/O)
    // -------------------------------------------------------------------------

    /// Return a new path with the name component replaced.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.with_name
    member _.with_name(name: string) : Path = nativeOnly

    /// Return a new path with the stem component replaced (3.9+).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.with_stem
    member _.with_stem(stem: string) : Path = nativeOnly

    /// Return a new path with the suffix component replaced.
    /// Pass "" to remove the suffix.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.with_suffix
    member _.with_suffix(suffix: string) : Path = nativeOnly

    /// Return a string representation of the path.
    /// Equivalent to Python's ``str(path)``.
    [<Emit("str($0)")>]
    member _.str() : string = nativeOnly

    /// Return the path as a POSIX string (forward slashes).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.as_posix
    member _.as_posix() : string = nativeOnly

    /// Return the path as a URI (file:// scheme). Only absolute paths are supported.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.as_uri
    member _.as_uri() : string = nativeOnly

    // -------------------------------------------------------------------------
    // I/O queries
    // -------------------------------------------------------------------------

    /// Return True if the path points to an existing filesystem entry.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.exists
    member _.exists() : bool = nativeOnly

    /// Return True if the path points to a regular file (follows symlinks).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.is_file
    member _.is_file() : bool = nativeOnly

    /// Return True if the path points to a directory (follows symlinks).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.is_dir
    member _.is_dir() : bool = nativeOnly

    /// Return True if the path points to a symbolic link.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.is_symlink
    member _.is_symlink() : bool = nativeOnly

    /// Return True if the path is a mount point.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.is_mount
    member _.is_mount() : bool = nativeOnly

    // -------------------------------------------------------------------------
    // I/O operations
    // -------------------------------------------------------------------------

    /// Make the path absolute and resolve any symlinks or ``..`` components.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.resolve
    member _.resolve() : Path = nativeOnly

    /// Make the path relative to other, raising ValueError if impossible.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.PurePath.relative_to
    member _.relative_to(other: Path) : Path = nativeOnly

    /// Make the path relative to a string path.
    [<Emit("$0.relative_to($1)")>]
    member _.relative_to(other: string) : Path = nativeOnly

    /// Return the decoded contents of the file as a string (UTF-8 by default).
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.read_text
    member _.read_text() : string = nativeOnly

    /// Return the decoded contents of the file using the given encoding.
    [<Emit("$0.read_text(encoding=$1)")>]
    member _.read_text(encoding: string) : string = nativeOnly

    /// Return the binary contents of the file as a bytes object.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.read_bytes
    member _.read_bytes() : byte[] = nativeOnly

    /// Write a string to the file, returning the number of characters written.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.write_text
    [<Emit("$0.write_text($1)")>]
    member _.write_text(data: string) : int = nativeOnly

    /// Write a string to the file with the given encoding.
    [<Emit("$0.write_text($1, encoding=$2)")>]
    member _.write_text(data: string, encoding: string) : int = nativeOnly

    /// Write binary data to the file, returning the number of bytes written.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.write_bytes
    member _.write_bytes(data: byte[]) : int = nativeOnly

    /// Iterate over the directory contents, yielding Path objects.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.iterdir
    member _.iterdir() : Path seq = nativeOnly

    /// Glob the given relative pattern in the directory, returning matching paths.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.glob
    member _.glob(pattern: string) : Path seq = nativeOnly

    /// Like glob() but descends into sub-directories recursively.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.rglob
    member _.rglob(pattern: string) : Path seq = nativeOnly

    /// Create this directory. Raises FileExistsError if it already exists.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.mkdir
    member _.mkdir() : unit = nativeOnly

    /// Create this directory with options.
    /// parents=true creates any missing parent directories.
    /// exist_ok=true suppresses FileExistsError.
    [<Emit("$0.mkdir(mode=$1, parents=$2, exist_ok=$3)")>]
    member _.mkdir(mode: int, parents: bool, exist_ok: bool) : unit = nativeOnly

    /// Create this directory and all missing parents (equivalent to ``mkdir -p``).
    [<Emit("$0.mkdir(parents=True, exist_ok=True)")>]
    member _.mkdir_p() : unit = nativeOnly

    /// Remove this directory. The directory must be empty.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.rmdir
    member _.rmdir() : unit = nativeOnly

    /// Remove this file (or symbolic link). Raises FileNotFoundError if missing.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.unlink
    member _.unlink() : unit = nativeOnly

    /// Remove this file; if missing_ok is true, no error is raised.
    [<Emit("$0.unlink(missing_ok=$1)")>]
    member _.unlink(missing_ok: bool) : unit = nativeOnly

    /// Rename the file or directory to target, returning the new Path.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.rename
    member _.rename(target: Path) : Path = nativeOnly

    /// Rename the file or directory to a string target, returning the new Path.
    [<Emit("$0.rename($1)")>]
    member _.rename(target: string) : Path = nativeOnly

    /// Rename to target, overwriting any existing destination.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.replace
    member _.replace(target: Path) : Path = nativeOnly

    /// Rename to a string target, overwriting any existing destination.
    [<Emit("$0.replace($1)")>]
    member _.replace(target: string) : Path = nativeOnly

    /// Expand the ``~`` user home directory shortcut.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.expanduser
    member _.expanduser() : Path = nativeOnly

    // -------------------------------------------------------------------------
    // Static factory methods
    // -------------------------------------------------------------------------

    /// Return a new Path representing the current working directory.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.cwd
    static member cwd() : Path = nativeOnly

    /// Return a new Path representing the user's home directory.
    /// See https://docs.python.org/3/library/pathlib.html#pathlib.Path.home
    static member home() : Path = nativeOnly
