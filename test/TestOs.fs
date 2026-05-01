module Fable.Python.Tests.Os

open Fable.Python.Testing
open Fable.Python.Os

[<Fact>]
let ``test os.path.exists works`` () =
    os.path.exists "." |> equal true
    os.path.exists "nonexistent_path_xyz" |> equal false

[<Fact>]
let ``test os.path.isdir works`` () = os.path.isdir "." |> equal true

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

[<Fact>]
let ``test os.path.isabs works`` () =
    os.path.isabs "/absolute/path" |> equal true
    os.path.isabs "relative/path" |> equal false
    os.path.isabs "." |> equal false

[<Fact>]
let ``test os.path.realpath works`` () =
    let real = os.path.realpath "."
    real.StartsWith("/") |> equal true
    // realpath should not contain symlink components; at minimum equal to abspath for "."
    real.Length > 0 |> equal true

[<Fact>]
let ``test os.path.islink works`` () =
    // "." is never a symlink
    os.path.islink "." |> equal false

[<Fact>]
let ``test os.path.getsize works`` () =
    // The test directory itself has a positive size
    os.path.getsize "." > 0L |> equal true

[<Fact>]
let ``test os.getpid works`` () =
    let pid = os.getpid ()
    pid > 0 |> equal true

[<Fact>]
let ``test os.getppid works`` () =
    let ppid = os.getppid ()
    ppid > 0 |> equal true

[<Fact>]
let ``test os.makedirs with exist_ok works`` () =
    let dir = sprintf "/tmp/fable_test_makedirs_%d" (os.getpid ())
    os.makedirs (dir, true)
    os.path.isdir dir |> equal true
    // Second call must not raise when exist_ok=true
    os.makedirs (dir, true)
    os.path.isdir dir |> equal true
    os.rmdir dir

[<Fact>]
let ``test os.walk yields entries`` () =
    let entries = os.walk "." |> Seq.truncate 1 |> Seq.toList
    entries.Length |> equal 1
    let dirpath, subdirs, files = entries.[0]
    dirpath |> equal "."
    // The first walk entry's dirnames + filenames should equal listdir of the root
    let walkAll = Seq.append subdirs files |> Seq.sort |> Seq.toList
    let listdirAll = os.listdir "." |> Array.sort |> Array.toList
    walkAll |> equal listdirAll

[<Fact>]
let ``test os.walk with topdown=false works`` () =
    let root = sprintf "/tmp/fable_test_walk_%d" (os.getpid ())
    let nested = os.path.join (root, "nested")
    os.makedirs (nested, true)
    let entries = os.walk (root, false) |> Seq.toList
    // Bottom-up: deepest dir is yielded first, root is yielded last
    entries.Length |> equal 2
    let firstDir, _, _ = entries.[0]
    let lastDir, _, _ = List.last entries
    firstDir |> equal nested
    lastDir |> equal root
    os.rmdir nested
    os.rmdir root
