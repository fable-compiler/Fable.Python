module Fable.Python.Tests.Os

open Util.Testing
open Fable.Python.Os

[<Fact>]
let ``test os.path.exists works`` () =
    os.path.exists "." |> equal true
    os.path.exists "nonexistent_path_xyz" |> equal false

[<Fact>]
let ``test os.path.isdir works`` () =
    os.path.isdir "." |> equal true

[<Fact>]
let ``test os.path.join works`` () =
    os.path.join ("a", "b", "c") |> equal "a/b/c"

[<Fact>]
let ``test os.path.dirname works`` () =
    os.path.dirname "/foo/bar/baz.txt" |> equal "/foo/bar"

[<Fact>]
let ``test os.path.basename works`` () =
    os.path.basename "/foo/bar/baz.txt" |> equal "baz.txt"

[<Fact>]
let ``test os.path.expanduser works`` () =
    let expanded = os.path.expanduser "~"
    // The expanded path should not start with ~ anymore
    expanded.StartsWith("~") |> equal false

[<Fact>]
let ``test os.path.abspath works`` () =
    let absPath = os.path.abspath "."
    // Absolute path should start with /
    absPath.StartsWith("/") |> equal true

[<Fact>]
let ``test os.path.split works`` () =
    let head, tail = os.path.split "/foo/bar/baz.txt"
    head |> equal "/foo/bar"
    tail |> equal "baz.txt"

[<Fact>]
let ``test os.path.splitext works`` () =
    let root, ext = os.path.splitext "/foo/bar/baz.txt"
    root |> equal "/foo/bar/baz"
    ext |> equal ".txt"

[<Fact>]
let ``test os.getcwd works`` () =
    let cwd = os.getcwd ()
    // Current working directory should be a non-empty string
    cwd.Length > 0 |> equal true

[<Fact>]
let ``test os.listdir works`` () =
    let entries = os.listdir "."
    // Current directory should have at least some entries
    entries.Length > 0 |> equal true
