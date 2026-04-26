module Fable.Python.Tests.Heapq

open Fable.Python.Testing
open Fable.Python.Heapq

[<Fact>]
let ``test heappush and heappop work`` () =
    let heap = ResizeArray<int>()
    heapq.heappush (heap, 3)
    heapq.heappush (heap, 1)
    heapq.heappush (heap, 2)
    heapq.heappop heap |> equal 1
    heapq.heappop heap |> equal 2
    heapq.heappop heap |> equal 3

[<Fact>]
let ``test heapify works`` () =
    let heap = ResizeArray [ 5; 3; 1; 4; 2 ]
    heapq.heapify heap
    heapq.heappop heap |> equal 1

[<Fact>]
let ``test heappushpop works`` () =
    let heap = ResizeArray [ 2; 4; 6 ]
    heapq.heapify heap
    // Push 1, then pop smallest (1)
    heapq.heappushpop (heap, 1) |> equal 1
    // Push 3, then pop smallest (2)
    heapq.heappushpop (heap, 3) |> equal 2

[<Fact>]
let ``test nlargest works`` () =
    let result = heapq.nlargest (3, [ 1; 5; 2; 8; 3; 7 ])
    result |> equal (ResizeArray [ 8; 7; 5 ])

[<Fact>]
let ``test nsmallest works`` () =
    let result = heapq.nsmallest (3, [ 1; 5; 2; 8; 3; 7 ])
    result |> equal (ResizeArray [ 1; 2; 3 ])

[<Fact>]
let ``test heapreplace pops smallest and pushes new item`` () =
    let heap = ResizeArray [ 1; 3; 5; 7 ]
    heapq.heapify heap
    // Replace root (1) with 4; returns old root (1)
    heapq.heapreplace (heap, 4) |> equal 1
    // Smallest is now 3
    heapq.heappop heap |> equal 3

[<Fact>]
let ``test heapreplace with larger item maintains heap`` () =
    let heap = ResizeArray [ 2; 6; 10 ]
    heapq.heapify heap
    // Replace root (2) with 8; returns old root (2)
    heapq.heapreplace (heap, 8) |> equal 2
    // Remaining heap elements sorted: 6, 8, 10
    heapq.heappop heap |> equal 6
    heapq.heappop heap |> equal 8
    heapq.heappop heap |> equal 10
