/// Type bindings for Python collections module: https://docs.python.org/3/library/collections.html
module Fable.Python.Collections

open Fable.Core

// fsharplint:disable MemberNames

// ============================================================================
// Counter
// ============================================================================

/// A dict subclass for counting hashable objects.
/// Elements are stored as dictionary keys and their counts are stored as values.
/// Counts are allowed to be any integer value including zero or negative counts.
/// See https://docs.python.org/3/library/collections.html#collections.Counter
[<Import("Counter", "collections")>]
type Counter<'T>() =
    /// Get the count for key; missing keys return 0 (unlike a regular dict)
    [<Emit("$0[$1]")>]
    member _.Item(key: 'T) : int = nativeOnly

    /// Return elements and their counts as key-value pairs
    member _.items() : seq<'T * int> = nativeOnly

    /// Return the elements (keys) of the counter
    member _.keys() : seq<'T> = nativeOnly

    /// Return the counts (values) of the counter
    member _.values() : seq<int> = nativeOnly

    /// Return an iterator over elements, repeating each as many times as its count.
    /// Elements with counts <= 0 are not included.
    /// See https://docs.python.org/3/library/collections.html#collections.Counter.elements
    member _.elements() : seq<'T> = nativeOnly

    /// Return all elements and their counts, ordered from most common to least common.
    /// See https://docs.python.org/3/library/collections.html#collections.Counter.most_common
    member _.most_common() : seq<'T * int> = nativeOnly

    /// Return the n most common elements and their counts (most common first).
    /// See https://docs.python.org/3/library/collections.html#collections.Counter.most_common
    member _.most_common(n: int) : seq<'T * int> = nativeOnly

    /// Return the total of all counts (requires Python 3.10+).
    /// See https://docs.python.org/3/library/collections.html#collections.Counter.total
    member _.total() : int = nativeOnly

    /// Add counts from the iterable; count becomes sum of old and new counts.
    /// See https://docs.python.org/3/library/collections.html#collections.Counter.update
    member _.update(iterable: 'T seq) : unit = nativeOnly

    /// Subtract counts from the iterable; count becomes difference. Counts can become negative.
    /// See https://docs.python.org/3/library/collections.html#collections.Counter.subtract
    member _.subtract(iterable: 'T seq) : unit = nativeOnly

    /// Remove and return the count for key, or raise KeyError if missing.
    member _.pop(key: 'T) : int = nativeOnly

    /// Remove and return the count for key, or return defaultValue if missing.
    [<Emit("$0.pop($1, $2)")>]
    member _.pop(key: 'T, defaultValue: int) : int = nativeOnly

    /// Remove all items
    member _.clear() : unit = nativeOnly

    /// Check if a key is present in the counter
    [<Emit("$1 in $0")>]
    member _.contains(key: 'T) : bool = nativeOnly

    /// Return a Counter from a sequence of elements.
    /// See https://docs.python.org/3/library/collections.html#collections.Counter
    [<Emit("Counter($0)")>]
    static member ofSeq(iterable: 'T seq) : Counter<'T> = nativeOnly

// ============================================================================
// defaultdict
// ============================================================================

/// A dict subclass that calls a factory to supply missing values.
/// When a key is not found, the factory function (called with no arguments)
/// is called to produce a new value, which is then stored and returned.
///
/// Use the `withFactory` static method to attach a factory; the empty
/// constructor produces a defaultdict with no factory (missing keys raise KeyError).
/// See https://docs.python.org/3/library/collections.html#collections.defaultdict
[<Import("defaultdict", "collections")>]
type defaultdict<'TKey, 'TValue>() =
    /// Create a defaultdict with the given factory for missing keys.
    /// The factory is invoked with no arguments and must return a new value of type 'TValue.
    [<Emit("defaultdict($0)")>]
    static member withFactory(defaultFactory: unit -> 'TValue) : defaultdict<'TKey, 'TValue> = nativeOnly

    /// Get or set the value for key; missing keys invoke the factory
    [<Emit("$0[$1]")>]
    member _.Item(key: 'TKey) : 'TValue = nativeOnly

    /// Set value for key
    [<Emit("$0[$1] = $2")>]
    member _.set(key: 'TKey, value: 'TValue) : unit = nativeOnly

    /// Return key-value pairs
    member _.items() : seq<'TKey * 'TValue> = nativeOnly

    /// Return keys
    member _.keys() : seq<'TKey> = nativeOnly

    /// Return values
    member _.values() : seq<'TValue> = nativeOnly

    /// Return value for key if present, otherwise None.
    /// Does NOT invoke the factory.
    member _.get(key: 'TKey) : 'TValue option = nativeOnly

    /// Return value for key if present, otherwise defaultValue.
    /// Does NOT invoke the factory.
    [<Emit("$0.get($1, $2)")>]
    member _.get(key: 'TKey, defaultValue: 'TValue) : 'TValue = nativeOnly

    /// If key is in the dict, return its value.
    /// If not, insert key with the factory's value and return that value.
    member _.setdefault(key: 'TKey) : 'TValue = nativeOnly

    /// Remove and return the value for key, or raise KeyError.
    member _.pop(key: 'TKey) : 'TValue = nativeOnly

    /// Remove and return the value for key, or return defaultValue.
    [<Emit("$0.pop($1, $2)")>]
    member _.pop(key: 'TKey, defaultValue: 'TValue) : 'TValue = nativeOnly

    /// Merge another dict into this one
    member _.update(other: System.Collections.Generic.IDictionary<'TKey, 'TValue>) : unit = nativeOnly

    /// Merge an iterable of key-value pairs into this dict
    member _.update(items: seq<'TKey * 'TValue>) : unit = nativeOnly

    /// Remove all items
    member _.clear() : unit = nativeOnly

    /// Return a shallow copy
    member _.copy() : defaultdict<'TKey, 'TValue> = nativeOnly

    /// Check if a key is present (does NOT invoke factory)
    [<Emit("$1 in $0")>]
    member _.contains(key: 'TKey) : bool = nativeOnly

// ============================================================================
// deque
// ============================================================================

/// A double-ended queue with O(1) appends and pops from either end.
/// If maxlen is set, the deque is bounded to that maximum length; items are
/// discarded from the opposite end when the bound is reached.
/// See https://docs.python.org/3/library/collections.html#collections.deque
[<Import("deque", "collections")>]
type deque<'T>() =
    /// Number of elements in the deque
    [<Emit("len($0)")>]
    member _.length() : int = nativeOnly

    /// Get element at index
    [<Emit("$0[$1]")>]
    member _.Item(index: int) : 'T = nativeOnly

    /// Maximum length of the deque, or None if unbounded
    member _.maxlen : int option = nativeOnly

    /// Add item to the right end
    member _.append(item: 'T) : unit = nativeOnly

    /// Add item to the left end
    member _.appendleft(item: 'T) : unit = nativeOnly

    /// Remove and return item from the right end
    member _.pop() : 'T = nativeOnly

    /// Remove and return item from the left end
    member _.popleft() : 'T = nativeOnly

    /// Extend the right side of the deque by appending elements from iterable
    member _.extend(iterable: 'T seq) : unit = nativeOnly

    /// Extend the left side of the deque by appending elements from iterable.
    /// Note: each element is appended to the left, reversing the iterable order.
    member _.extendleft(iterable: 'T seq) : unit = nativeOnly

    /// Rotate the deque n steps to the right. If n is negative, rotate left.
    member _.rotate(n: int) : unit = nativeOnly

    /// Count the number of occurrences of value
    member _.count(value: 'T) : int = nativeOnly

    /// Return the position of value (raise ValueError if not found)
    member _.index(value: 'T) : int = nativeOnly

    /// Insert value before position i
    member _.insert(i: int, value: 'T) : unit = nativeOnly

    /// Remove the first occurrence of value (raise ValueError if not found)
    member _.remove(value: 'T) : unit = nativeOnly

    /// Reverse the deque in-place
    member _.reverse() : unit = nativeOnly

    /// Remove all elements
    member _.clear() : unit = nativeOnly

    /// Return a shallow copy
    member _.copy() : deque<'T> = nativeOnly

    /// Create a deque from a sequence
    [<Emit("deque($0)")>]
    static member ofSeq(iterable: 'T seq) : deque<'T> = nativeOnly

    /// Create a bounded deque from a sequence with maximum length
    [<Emit("deque($0, maxlen=int($1))")>]
    static member ofSeq(iterable: 'T seq, maxlen: int) : deque<'T> = nativeOnly

    /// Create an empty bounded deque with maximum length
    [<Emit("deque(maxlen=int($0))")>]
    static member withMaxlen(maxlen: int) : deque<'T> = nativeOnly

// ============================================================================
// OrderedDict
// ============================================================================

/// A dict subclass that remembers insertion order. Since Python 3.7, all dicts
/// maintain insertion order, but OrderedDict has a few extra features:
/// `move_to_end` and order-sensitive equality.
/// See https://docs.python.org/3/library/collections.html#collections.OrderedDict
[<Import("OrderedDict", "collections")>]
type OrderedDict<'TKey, 'TValue>() =
    /// Get or set value for key
    [<Emit("$0[$1]")>]
    member _.Item(key: 'TKey) : 'TValue = nativeOnly

    /// Set value for key
    [<Emit("$0[$1] = $2")>]
    member _.set(key: 'TKey, value: 'TValue) : unit = nativeOnly

    /// Return key-value pairs in insertion order
    member _.items() : seq<'TKey * 'TValue> = nativeOnly

    /// Return keys in insertion order
    member _.keys() : seq<'TKey> = nativeOnly

    /// Return values in insertion order
    member _.values() : seq<'TValue> = nativeOnly

    /// Get value for key, or None if missing
    member _.get(key: 'TKey) : 'TValue option = nativeOnly

    /// Get value for key, or defaultValue if missing
    [<Emit("$0.get($1, $2)")>]
    member _.get(key: 'TKey, defaultValue: 'TValue) : 'TValue = nativeOnly

    /// Remove and return the value for key (or raise KeyError)
    member _.pop(key: 'TKey) : 'TValue = nativeOnly

    /// Remove and return the value for key, or return defaultValue
    [<Emit("$0.pop($1, $2)")>]
    member _.pop(key: 'TKey, defaultValue: 'TValue) : 'TValue = nativeOnly

    /// Move key to the end. If last is False, move to the beginning.
    /// See https://docs.python.org/3/library/collections.html#collections.OrderedDict.move_to_end
    member _.move_to_end(key: 'TKey) : unit = nativeOnly

    /// Move key to the end (last=True) or beginning (last=False).
    [<Emit("$0.move_to_end($1, last=$2)")>]
    member _.move_to_end(key: 'TKey, last: bool) : unit = nativeOnly

    /// Remove and return a (key, value) pair. last=True removes from the end.
    /// See https://docs.python.org/3/library/collections.html#collections.OrderedDict.popitem
    member _.popitem() : 'TKey * 'TValue = nativeOnly

    /// Remove and return from end (last=True) or beginning (last=False).
    [<Emit("$0.popitem(last=$1)")>]
    member _.popitem(last: bool) : 'TKey * 'TValue = nativeOnly

    /// Merge another dict into this one
    member _.update(other: System.Collections.Generic.IDictionary<'TKey, 'TValue>) : unit = nativeOnly

    /// Merge an iterable of key-value pairs into this dict
    member _.update(items: seq<'TKey * 'TValue>) : unit = nativeOnly

    /// Remove all items
    member _.clear() : unit = nativeOnly

    /// Return a shallow copy
    member _.copy() : OrderedDict<'TKey, 'TValue> = nativeOnly

    /// Check if key is present
    [<Emit("$1 in $0")>]
    member _.contains(key: 'TKey) : bool = nativeOnly
