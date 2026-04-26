module Fable.Python.Tests.Pathlib

open Fable.Python.Testing
open Fable.Python.Pathlib
open Fable.Python.Builtins

// ============================================================================
// Construction
// ============================================================================

[<Fact>]
let ``test Path construction from string`` () =
    let p = Path "/tmp"
    p.str () |> equal "/tmp"

[<Fact>]
let ``test Path construction from multiple segments`` () =
    let p = Path("/foo", "bar", "baz.txt")
    p.str () |> equal "/foo/bar/baz.txt"

[<Fact>]
let ``test Path cwd returns absolute path`` () =
    let p = Path.cwd ()
    p.is_absolute () |> equal true

[<Fact>]
let ``test Path home returns absolute path`` () =
    let p = Path.home ()
    p.is_absolute () |> equal true

// ============================================================================
// Properties
// ============================================================================

[<Fact>]
let ``test Path name`` () =
    let p = Path "/foo/bar/baz.txt"
    p.name |> equal "baz.txt"

[<Fact>]
let ``test Path stem`` () =
    let p = Path "/foo/bar/baz.txt"
    p.stem |> equal "baz"

[<Fact>]
let ``test Path suffix`` () =
    let p = Path "/foo/bar/baz.txt"
    p.suffix |> equal ".txt"

[<Fact>]
let ``test Path suffix absent`` () =
    let p = Path "/foo/bar/README"
    p.suffix |> equal ""

[<Fact>]
let ``test Path suffixes multiple`` () =
    let p = Path "/foo/bar/archive.tar.gz"
    p.suffixes |> Seq.toList |> equal [ ".tar"; ".gz" ]

[<Fact>]
let ``test Path parent`` () =
    let p = Path "/foo/bar/baz.txt"
    p.parent.str () |> equal "/foo/bar"

[<Fact>]
let ``test Path root on absolute path`` () =
    let p = Path "/foo/bar"
    p.root |> equal "/"

[<Fact>]
let ``test Path anchor`` () =
    let p = Path "/foo/bar"
    p.anchor |> equal "/"

// ============================================================================
// Path arithmetic
// ============================================================================

[<Fact>]
let ``test Path slash operator with string`` () =
    let p = Path "/foo" / "bar"
    p.str () |> equal "/foo/bar"

[<Fact>]
let ``test Path slash operator chained`` () =
    let p = Path "/foo" / "bar" / "baz"
    p.str () |> equal "/foo/bar/baz"

[<Fact>]
let ``test Path slash operator with Path`` () =
    let base' = Path "/foo"
    let child = Path "bar"
    let p = base' / child
    p.str () |> equal "/foo/bar"

[<Fact>]
let ``test Path joinpath`` () =
    let p = Path("/foo").joinpath ("bar", "baz")
    p.str () |> equal "/foo/bar/baz"

// ============================================================================
// Transformations (pure)
// ============================================================================

[<Fact>]
let ``test Path with_name`` () =
    let p = Path "/foo/bar/baz.txt"
    p.with_name("qux.txt").str () |> equal "/foo/bar/qux.txt"

[<Fact>]
let ``test Path with_stem`` () =
    let p = Path "/foo/bar/baz.txt"
    p.with_stem("qux").str () |> equal "/foo/bar/qux.txt"

[<Fact>]
let ``test Path with_suffix`` () =
    let p = Path "/foo/bar/baz.txt"
    p.with_suffix(".py").str () |> equal "/foo/bar/baz.py"

[<Fact>]
let ``test Path with_suffix empty removes extension`` () =
    let p = Path "/foo/bar/baz.txt"
    p.with_suffix("").str () |> equal "/foo/bar/baz"

[<Fact>]
let ``test Path as_posix`` () =
    let p = Path "/foo/bar/baz.txt"
    p.as_posix () |> equal "/foo/bar/baz.txt"

// ============================================================================
// is_* predicates
// ============================================================================

[<Fact>]
let ``test Path is_absolute true`` () =
    let p = Path "/foo/bar"
    p.is_absolute () |> equal true

[<Fact>]
let ``test Path is_absolute false`` () =
    let p = Path "foo/bar"
    p.is_absolute () |> equal false

[<Fact>]
let ``test Path is_relative_to`` () =
    let p = Path "/foo/bar/baz"
    p.is_relative_to (Path "/foo") |> equal true

[<Fact>]
let ``test Path is_relative_to string`` () =
    let p = Path "/foo/bar/baz"
    p.is_relative_to "/foo" |> equal true

[<Fact>]
let ``test Path is_relative_to false`` () =
    let p = Path "/foo/bar"
    p.is_relative_to (Path "/baz") |> equal false

// ============================================================================
// I/O queries on a real temp file
// ============================================================================

[<Fact>]
let ``test Path exists true for cwd`` () =
    Path.cwd().exists () |> equal true

[<Fact>]
let ``test Path exists false for nonexistent`` () =
    let p = Path "/nonexistent_repo_assist_xyz"
    p.exists () |> equal false

[<Fact>]
let ``test Path is_dir true for cwd`` () =
    Path.cwd().is_dir () |> equal true

[<Fact>]
let ``test Path is_file false for cwd`` () =
    Path.cwd().is_file () |> equal false

[<Fact>]
let ``test Path resolve returns absolute`` () =
    let p = Path "."
    p.resolve().is_absolute () |> equal true

[<Fact>]
let ``test Path relative_to`` () =
    let p = Path "/foo/bar/baz"
    p.relative_to(Path "/foo/bar").str () |> equal "baz"

[<Fact>]
let ``test Path relative_to string`` () =
    let p = Path "/foo/bar/baz"
    p.relative_to("/foo/bar").str () |> equal "baz"

// ============================================================================
// File round-trip: write_text / read_text / unlink
// ============================================================================

[<Fact>]
let ``test Path write_text and read_text`` () =
    let tmp = Path.cwd () / "__pathlib_test_write__.txt"
    tmp.write_text "hello pathlib" |> ignore
    let content = tmp.read_text ()
    tmp.unlink (missing_ok = true)
    content |> equal "hello pathlib"

[<Fact>]
let ``test Path write_bytes and read_bytes`` () =
    let tmp = Path.cwd () / "__pathlib_test_bytes__.bin"
    let data = builtins.bytes [| 0uy; 1uy; 2uy; 255uy |]
    tmp.write_bytes data |> ignore
    let result = tmp.read_bytes ()
    tmp.unlink (missing_ok = true)
    result |> equal data

[<Fact>]
let ``test Path unlink missing_ok suppresses error`` () =
    let tmp = Path "/nonexistent_repo_assist_unlink_xyz.txt"
    // Should not raise
    tmp.unlink (missing_ok = true)
    true |> equal true

// ============================================================================
// Directory operations
// ============================================================================

[<Fact>]
let ``test Path mkdir_p and rmdir`` () =
    let dir = Path.cwd () / "__pathlib_test_dir__"
    dir.mkdir_p ()
    dir.is_dir () |> equal true
    dir.rmdir ()
    dir.exists () |> equal false

[<Fact>]
let ``test Path iterdir returns entries`` () =
    let dir = Path.cwd ()
    let entries = dir.iterdir () |> Seq.toList
    entries.Length > 0 |> equal true

[<Fact>]
let ``test Path glob`` () =
    // Create a file, glob for it, clean up
    let tmp = Path.cwd () / "__pathlib_glob_test__.txt"
    tmp.write_text "glob" |> ignore
    let matches = (Path.cwd ()).glob "__pathlib_glob_test__*.txt" |> Seq.toList
    tmp.unlink (missing_ok = true)
    matches.Length >= 1 |> equal true
