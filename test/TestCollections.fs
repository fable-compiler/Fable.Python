module Fable.Python.Tests.Collections

open Fable.Python.Testing
open Fable.Python.Collections

// ============================================================================
// Counter tests
// ============================================================================

[<Fact>]
let ``test Counter empty counter has zero count for missing key`` () =
    let c = Counter<string>()
    c.Item("x") |> equal 0

[<Fact>]
let ``test Counter ofSeq counts elements`` () =
    let c = Counter.ofSeq [ "a"; "b"; "a"; "c"; "a"; "b" ]
    c.Item("a") |> equal 3
    c.Item("b") |> equal 2
    c.Item("c") |> equal 1

[<Fact>]
let ``test Counter missing key returns 0`` () =
    let c = Counter.ofSeq [ "a"; "b" ]
    c.Item("z") |> equal 0

[<Fact>]
let ``test Counter most_common returns all elements sorted by count`` () =
    let c = Counter.ofSeq [ "a"; "b"; "a"; "c"; "a"; "b" ]
    let top = c.most_common() |> Seq.head
    top |> equal ("a", 3)

[<Fact>]
let ``test Counter most_common n returns top n elements`` () =
    let c = Counter.ofSeq [ "a"; "b"; "a"; "c"; "a"; "b" ]
    let topTwo = c.most_common(2) |> Seq.toList
    topTwo |> List.length |> equal 2
    topTwo |> List.head |> equal ("a", 3)

[<Fact>]
let ``test Counter elements returns repeated sequence`` () =
    let c = Counter.ofSeq [ "a"; "a"; "b" ]
    let elems = c.elements() |> Seq.toList |> List.sort
    elems |> equal [ "a"; "a"; "b" ]

[<Fact>]
let ``test Counter total sums all counts`` () =
    let c = Counter.ofSeq [ "a"; "b"; "a"; "c" ]
    c.total() |> equal 4

[<Fact>]
let ``test Counter update adds counts`` () =
    let c = Counter.ofSeq [ "a"; "b" ]
    c.update([ "a"; "c" ])
    c.Item("a") |> equal 2
    c.Item("c") |> equal 1

[<Fact>]
let ``test Counter subtract reduces counts`` () =
    let c = Counter.ofSeq [ "a"; "a"; "b" ]
    c.subtract([ "a" ])
    c.Item("a") |> equal 1

[<Fact>]
let ``test Counter contains reflects key presence`` () =
    let c = Counter.ofSeq [ "a"; "b" ]
    c.contains("a") |> equal true
    c.contains("z") |> equal false

[<Fact>]
let ``test Counter keys and values enumerate the counter`` () =
    let c = Counter.ofSeq [ "a"; "b"; "a" ]
    c.keys() |> Seq.toList |> List.sort |> equal [ "a"; "b" ]
    c.values() |> Seq.sum |> equal 3

[<Fact>]
let ``test Counter pop removes and returns count`` () =
    let c = Counter.ofSeq [ "a"; "a"; "b" ]
    c.pop("a") |> equal 2
    c.contains("a") |> equal false

// ============================================================================
// defaultdict tests
// ============================================================================

[<Fact>]
let ``test defaultdict missing key invokes factory`` () =
    let d = defaultdict<string, ResizeArray<int>>.withFactory(fun () -> ResizeArray())
    let list = d.Item("key")
    list.Count |> equal 0

[<Fact>]
let ``test defaultdict factory creates separate instances`` () =
    let d = defaultdict<string, ResizeArray<int>>.withFactory(fun () -> ResizeArray())
    let list1 = d.Item("a")
    list1.Add(1)
    let list2 = d.Item("b")
    list2.Count |> equal 0

[<Fact>]
let ``test defaultdict int factory starts at zero`` () =
    let d = defaultdict<string, int>.withFactory(fun () -> 0)
    d.Item("key") |> equal 0

[<Fact>]
let ``test defaultdict get returns None for missing key without invoking factory`` () =
    let mutable factoryCalled = false
    let d = defaultdict<string, int>.withFactory(fun () -> factoryCalled <- true; 0)
    let result = d.get("missing")
    result |> equal None
    factoryCalled |> equal false

[<Fact>]
let ``test defaultdict get with default returns default for missing key`` () =
    let d = defaultdict<string, int>.withFactory(fun () -> 0)
    d.get("missing", 42) |> equal 42

[<Fact>]
let ``test defaultdict contains returns false for missing key`` () =
    let d = defaultdict<string, int>.withFactory(fun () -> 0)
    d.contains("key") |> equal false

[<Fact>]
let ``test defaultdict contains returns true after access`` () =
    let d = defaultdict<string, int>.withFactory(fun () -> 99)
    let _ = d.Item("key")
    d.contains("key") |> equal true

// ============================================================================
// deque tests
// ============================================================================

[<Fact>]
let ``test deque empty deque has length 0`` () =
    let d = deque<int>()
    d.length() |> equal 0

[<Fact>]
let ``test deque ofSeq creates deque from sequence`` () =
    let d = deque.ofSeq [ 1; 2; 3 ]
    d.length() |> equal 3

[<Fact>]
let ``test deque append adds to right`` () =
    let d = deque.ofSeq [ 1; 2 ]
    d.append(3)
    d.Item(2) |> equal 3

[<Fact>]
let ``test deque appendleft adds to left`` () =
    let d = deque.ofSeq [ 1; 2 ]
    d.appendleft(0)
    d.Item(0) |> equal 0
    d.length() |> equal 3

[<Fact>]
let ``test deque pop removes from right`` () =
    let d = deque.ofSeq [ 1; 2; 3 ]
    let v = d.pop()
    v |> equal 3
    d.length() |> equal 2

[<Fact>]
let ``test deque popleft removes from left`` () =
    let d = deque.ofSeq [ 1; 2; 3 ]
    let v = d.popleft()
    v |> equal 1
    d.length() |> equal 2

[<Fact>]
let ``test deque rotate shifts elements right`` () =
    let d = deque.ofSeq [ 1; 2; 3; 4; 5 ]
    d.rotate(2)
    d.Item(0) |> equal 4
    d.Item(1) |> equal 5

[<Fact>]
let ``test deque maxlen is None for unbounded deque`` () =
    let d = deque.ofSeq [ 1; 2; 3 ]
    d.maxlen |> equal None

[<Fact>]
let ``test deque withMaxlen creates bounded deque`` () =
    let d = deque<int>.withMaxlen(3)
    d.append(1)
    d.append(2)
    d.append(3)
    d.append(4) // should push out 1
    d.length() |> equal 3
    d.Item(0) |> equal 2

[<Fact>]
let ``test deque ofSeq with maxlen creates bounded deque`` () =
    let d = deque.ofSeq ([ 1; 2; 3; 4; 5 ], 3)
    d.length() |> equal 3
    d.maxlen |> equal (Some 3)

[<Fact>]
let ``test deque count occurrences`` () =
    let d = deque.ofSeq [ 1; 2; 1; 3; 1 ]
    d.count(1) |> equal 3

[<Fact>]
let ``test deque extendleft reverses iterable order`` () =
    let d = deque.ofSeq [ 3 ]
    d.extendleft([ 1; 2 ])
    // Each element pushed onto the left in turn => final order [2; 1; 3]
    d.Item(0) |> equal 2
    d.Item(1) |> equal 1
    d.Item(2) |> equal 3

// ============================================================================
// OrderedDict tests
// ============================================================================

[<Fact>]
let ``test OrderedDict preserves insertion order`` () =
    let od = OrderedDict<string, int>()
    od.set("a", 1)
    od.set("b", 2)
    od.set("c", 3)
    od.keys() |> Seq.toList |> equal [ "a"; "b"; "c" ]

[<Fact>]
let ``test OrderedDict get existing key`` () =
    let od = OrderedDict<string, int>()
    od.set("x", 42)
    od.Item("x") |> equal 42

[<Fact>]
let ``test OrderedDict get returns None for missing key`` () =
    let od = OrderedDict<string, int>()
    od.get("missing") |> equal None

[<Fact>]
let ``test OrderedDict move_to_end moves last element`` () =
    let od = OrderedDict<string, int>()
    od.set("a", 1)
    od.set("b", 2)
    od.set("c", 3)
    od.move_to_end("a")
    od.keys() |> Seq.toList |> equal [ "b"; "c"; "a" ]

[<Fact>]
let ``test OrderedDict move_to_end with last false moves to front`` () =
    let od = OrderedDict<string, int>()
    od.set("a", 1)
    od.set("b", 2)
    od.set("c", 3)
    od.move_to_end("c", false)
    od.keys() |> Seq.toList |> equal [ "c"; "a"; "b" ]

[<Fact>]
let ``test OrderedDict contains returns correct result`` () =
    let od = OrderedDict<string, int>()
    od.set("a", 1)
    od.contains("a") |> equal true
    od.contains("b") |> equal false
