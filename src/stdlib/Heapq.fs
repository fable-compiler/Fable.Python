/// Type bindings for Python heapq module: https://docs.python.org/3/library/heapq.html
module Fable.Python.Heapq

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Push the value item onto the heap, maintaining the heap invariant
    /// See https://docs.python.org/3/library/heapq.html#heapq.heappush
    abstract heappush: heap: ResizeArray<'T> * item: 'T -> unit
    /// Pop and return the smallest item from the heap, maintaining the heap invariant
    /// See https://docs.python.org/3/library/heapq.html#heapq.heappop
    abstract heappop: heap: ResizeArray<'T> -> 'T
    /// Push item on the heap, then pop and return the smallest item from the heap
    /// See https://docs.python.org/3/library/heapq.html#heapq.heappushpop
    abstract heappushpop: heap: ResizeArray<'T> * item: 'T -> 'T
    /// Transform list into a heap, in-place, in linear time
    /// See https://docs.python.org/3/library/heapq.html#heapq.heapify
    abstract heapify: x: ResizeArray<'T> -> unit
    /// Pop and return the smallest item from the heap, and also push the new item
    /// See https://docs.python.org/3/library/heapq.html#heapq.heapreplace
    abstract heapreplace: heap: ResizeArray<'T> * item: 'T -> 'T
    /// Return a list with the n largest elements from the dataset
    /// See https://docs.python.org/3/library/heapq.html#heapq.nlargest
    abstract nlargest: n: int * iterable: 'T seq -> ResizeArray<'T>
    /// Return a list with the n smallest elements from the dataset
    /// See https://docs.python.org/3/library/heapq.html#heapq.nsmallest
    abstract nsmallest: n: int * iterable: 'T seq -> ResizeArray<'T>

/// Heap queue algorithm
[<ImportAll("heapq")>]
let heapq: IExports = nativeOnly
