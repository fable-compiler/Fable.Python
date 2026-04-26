module Fable.Python.Tests.Regex

open Fable.Python.Testing
open Fable.Python.Regex

// ============================================================================
// Module-level match / search / fullmatch
// ============================================================================

[<Fact>]
let ``test match returns Some for matching string`` () =
    let m = re.``match`` ("hello", "hello world")
    m.IsSome |> equal true

[<Fact>]
let ``test match returns None for non-matching string`` () =
    let m = re.``match`` ("world", "hello world")
    m.IsNone |> equal true

[<Fact>]
let ``test match group 0 returns whole match`` () =
    let m = re.``match`` ("hel+o", "hello world") |> Option.get
    m.group () |> equal "hello"

[<Fact>]
let ``test search finds pattern not at start`` () =
    let m = re.search ("world", "hello world")
    m.IsSome |> equal true

[<Fact>]
let ``test search returns None when not found`` () =
    let m = re.search ("xyz", "hello world")
    m.IsNone |> equal true

[<Fact>]
let ``test fullmatch succeeds when pattern covers entire string`` () =
    let m = re.fullmatch ("[a-z]+", "hello")
    m.IsSome |> equal true

[<Fact>]
let ``test fullmatch fails when pattern does not cover entire string`` () =
    let m = re.fullmatch ("[a-z]+", "hello world")
    m.IsNone |> equal true

// ============================================================================
// Match object properties
// ============================================================================

[<Fact>]
let ``test match start and end positions`` () =
    let m = re.search ("world", "hello world") |> Option.get
    m.start () |> equal 6
    m.``end`` () |> equal 11

[<Fact>]
let ``test match span`` () =
    let m = re.search ("world", "hello world") |> Option.get
    m.span () |> equal (6, 11)

[<Fact>]
let ``test match string property`` () =
    let m = re.``match`` ("hello", "hello world") |> Option.get
    m.string |> equal "hello world"

[<Fact>]
let ``test match pos property`` () =
    let m = re.``match`` ("hello", "hello world") |> Option.get
    m.pos |> equal 0

// ============================================================================
// Capturing groups
// ============================================================================

[<Fact>]
let ``test match numbered group`` () =
    let m = re.``match`` ("(hello) (world)", "hello world") |> Option.get
    m.group 1 |> equal (Some "hello")
    m.group 2 |> equal (Some "world")

[<Fact>]
let ``test match named group`` () =
    let m = re.``match`` ("(?P<first>[a-z]+) (?P<second>[a-z]+)", "hello world") |> Option.get
    m.group "first" |> equal (Some "hello")
    m.group "second" |> equal (Some "world")

[<Fact>]
let ``test match groups returns all subgroups`` () =
    let m = re.``match`` ("([a-z]+) ([a-z]+)", "hello world") |> Option.get
    let gs = m.groups ()
    gs.Length |> equal 2
    gs.[0] |> equal (Some "hello")
    gs.[1] |> equal (Some "world")

[<Fact>]
let ``test match groupdict returns named groups`` () =
    let m = re.``match`` ("(?P<first>[a-z]+) (?P<second>[a-z]+)", "hello world") |> Option.get
    let d = m.groupdict ()
    d.["first"] |> equal (Some "hello")
    d.["second"] |> equal (Some "world")

// ============================================================================
// findall
// ============================================================================

[<Fact>]
let ``test findall returns all matches`` () =
    let results = re.findall ("[0-9]+", "there are 3 cats and 42 dogs")
    results.Length |> equal 2
    results.[0] |> equal "3"
    results.[1] |> equal "42"

[<Fact>]
let ``test findall returns empty array when no match`` () =
    let results = re.findall ("[0-9]+", "no numbers here")
    results.Length |> equal 0

// ============================================================================
// finditer
// ============================================================================

[<Fact>]
let ``test finditer yields Match objects`` () =
    let matches = re.finditer ("[0-9]+", "3 cats and 42 dogs") |> Seq.toArray
    matches.Length |> equal 2
    matches.[0].group () |> equal "3"
    matches.[1].group () |> equal "42"

// ============================================================================
// sub / subn
// ============================================================================

[<Fact>]
let ``test sub replaces all occurrences by default`` () =
    re.sub ("[aeiou]", "*", "hello world") |> equal "h*ll* w*rld"

[<Fact>]
let ``test sub with count limits replacements`` () =
    re.sub ("[aeiou]", "*", "hello world", 2) |> equal "h*ll* world"

[<Fact>]
let ``test subn returns new string and count`` () =
    let (result, count) = re.subn ("[aeiou]", "*", "hello world")
    result |> equal "h*ll* w*rld"
    count |> equal 3

// ============================================================================
// split
// ============================================================================

[<Fact>]
let ``test split on whitespace`` () =
    let parts = re.split (@"\s+", "hello   world  foo")
    parts.Length |> equal 3
    parts.[0] |> equal "hello"
    parts.[1] |> equal "world"
    parts.[2] |> equal "foo"

[<Fact>]
let ``test split with maxsplit`` () =
    let parts = re.split (@"\s+", "a b c d", 2)
    parts.Length |> equal 3
    parts.[0] |> equal "a"
    parts.[1] |> equal "b"
    parts.[2] |> equal "c d"

// ============================================================================
// escape
// ============================================================================

[<Fact>]
let ``test escape escapes special characters`` () =
    let escaped = re.escape ("a.b*c?")
    // escaped must not match the original special chars as metacharacters
    let m1 = re.``match`` (escaped, "a.b*c?")
    let m2 = re.``match`` (escaped, "aXbYcZ")
    m1.IsSome |> equal true
    m2.IsNone |> equal true

// ============================================================================
// compile / Pattern object
// ============================================================================

[<Fact>]
let ``test compile returns Pattern`` () =
    let pat = re.compile "[0-9]+"
    pat.pattern |> equal "[0-9]+"

[<Fact>]
let ``test pattern match works`` () =
    let pat = re.compile "[a-z]+"
    let m = pat.``match`` "hello"
    m.IsSome |> equal true

[<Fact>]
let ``test pattern search works`` () =
    let pat = re.compile "[0-9]+"
    let m = pat.search "there are 42 things"
    m.IsSome |> equal true
    (m |> Option.get).group () |> equal "42"

[<Fact>]
let ``test pattern fullmatch works`` () =
    let pat = re.compile "[a-z]+"
    pat.fullmatch("hello").IsSome |> equal true
    pat.fullmatch("hello world").IsNone |> equal true

[<Fact>]
let ``test pattern findall works`` () =
    let pat = re.compile "[0-9]+"
    let results = pat.findall "1 and 23 and 456"
    results.Length |> equal 3
    results.[2] |> equal "456"

[<Fact>]
let ``test pattern finditer works`` () =
    let pat = re.compile "[0-9]+"
    let ms = pat.finditer "10 20 30" |> Seq.toArray
    ms.Length |> equal 3
    ms.[1].group () |> equal "20"

[<Fact>]
let ``test pattern sub works`` () =
    let pat = re.compile "[0-9]+"
    pat.sub ("N", "there are 3 cats and 42 dogs") |> equal "there are N cats and N dogs"

[<Fact>]
let ``test pattern subn works`` () =
    let pat = re.compile "[0-9]+"
    let (s, n) = pat.subn ("N", "3 cats and 42 dogs")
    s |> equal "N cats and N dogs"
    n |> equal 2

[<Fact>]
let ``test pattern split works`` () =
    let pat = re.compile @"\s+"
    let parts = pat.split "a  b   c"
    parts.Length |> equal 3

[<Fact>]
let ``test pattern groups property`` () =
    let pat = re.compile "([a-z]+) ([0-9]+)"
    pat.groups |> equal 2

// ============================================================================
// Flags
// ============================================================================

[<Fact>]
let ``test IGNORECASE flag`` () =
    let m = re.``match`` ("hello", "HELLO", Flags.IGNORECASE)
    m.IsSome |> equal true

[<Fact>]
let ``test MULTILINE flag with anchors`` () =
    let results = re.findall ("^[a-z]+", "foo\nbar\nbaz", Flags.MULTILINE)
    results.Length |> equal 3

[<Fact>]
let ``test DOTALL flag makes dot match newline`` () =
    let m1 = re.``match`` ("a.b", "a\nb")
    let m2 = re.``match`` ("a.b", "a\nb", Flags.DOTALL)
    m1.IsNone |> equal true
    m2.IsSome |> equal true

[<Fact>]
let ``test compile with IGNORECASE flag`` () =
    let pat = re.compile ("hello", Flags.IGNORECASE)
    let m = pat.``match`` "HELLO WORLD"
    m.IsSome |> equal true
