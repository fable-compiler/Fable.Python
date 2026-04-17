/// Type bindings for Python itertools module: https://docs.python.org/3/library/itertools.html
module Fable.Python.Itertools

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    // ========================================================================
    // Infinite iterators
    // ========================================================================

    /// Make an iterator that returns evenly spaced values starting with number start
    /// See https://docs.python.org/3/library/itertools.html#itertools.count
    [<Emit("$0.count(int($1))")>]
    abstract count: start: int -> seq<int>

    /// Make an iterator that returns evenly spaced values starting with number start,
    /// incrementing by step
    /// See https://docs.python.org/3/library/itertools.html#itertools.count
    [<Emit("$0.count(int($1), int($2))")>]
    abstract count: start: int * step: int -> seq<int>

    /// Make an iterator that returns evenly spaced float values
    /// See https://docs.python.org/3/library/itertools.html#itertools.count
    [<Emit("$0.count($1)")>]
    abstract count: start: float -> seq<float>

    /// Make an iterator that returns evenly spaced float values starting with start,
    /// incrementing by step
    /// See https://docs.python.org/3/library/itertools.html#itertools.count
    [<Emit("$0.count($1, $2)")>]
    abstract count: start: float * step: float -> seq<float>

    /// Make an iterator returning elements from the iterable and saving a copy of each.
    /// When the iterable is exhausted, return elements from the saved copy. Repeats indefinitely.
    /// See https://docs.python.org/3/library/itertools.html#itertools.cycle
    abstract cycle: iterable: 'T seq -> seq<'T>

    /// Make an iterator that returns object elem over and over again. Runs indefinitely.
    /// See https://docs.python.org/3/library/itertools.html#itertools.repeat
    abstract repeat: elem: 'T -> seq<'T>

    /// Make an iterator that returns object elem, times times.
    /// See https://docs.python.org/3/library/itertools.html#itertools.repeat
    [<Emit("$0.repeat($1, int($2))")>]
    abstract repeat: elem: 'T * times: int -> seq<'T>

    // ========================================================================
    // Finite iterators
    // ========================================================================

    /// Make an iterator that returns accumulated sums
    /// See https://docs.python.org/3/library/itertools.html#itertools.accumulate
    abstract accumulate: iterable: 'T seq -> seq<'T>

    /// Make an iterator that returns accumulated results of a binary function.
    /// See https://docs.python.org/3/library/itertools.html#itertools.accumulate
    abstract accumulate: iterable: 'T seq * func: System.Func<'T, 'T, 'T> -> seq<'T>

    /// Make an iterator that returns accumulated results of a binary function,
    /// with an initial value.
    /// See https://docs.python.org/3/library/itertools.html#itertools.accumulate
    [<Emit("$0.accumulate($1, $2, initial=$3)")>]
    abstract accumulate: iterable: 'T seq * func: System.Func<'T, 'T, 'T> * initial: 'T -> seq<'T>

    /// Make an iterator that returns elements from the first iterable until it is exhausted,
    /// then proceeds to the next iterable, until all of the iterables are exhausted.
    /// See https://docs.python.org/3/library/itertools.html#itertools.chain
    abstract chain: a: 'T seq * b: 'T seq -> seq<'T>

    /// Make an iterator that chains three iterables together.
    /// See https://docs.python.org/3/library/itertools.html#itertools.chain
    abstract chain: a: 'T seq * b: 'T seq * c: 'T seq -> seq<'T>

    /// Make an iterator that chains four iterables together.
    /// See https://docs.python.org/3/library/itertools.html#itertools.chain
    abstract chain: a: 'T seq * b: 'T seq * c: 'T seq * d: 'T seq -> seq<'T>

    /// Make an iterator that chains iterables from a single iterable of iterables.
    /// Equivalent to Python's itertools.chain.from_iterable.
    /// See https://docs.python.org/3/library/itertools.html#itertools.chain.from_iterable
    [<Emit("$0.chain.from_iterable($1)")>]
    abstract chainFromIterable: iterable: 'T seq seq -> seq<'T>

    /// Make an iterator that filters elements from data returning only those that have
    /// a corresponding element in selectors that evaluates to True.
    /// See https://docs.python.org/3/library/itertools.html#itertools.compress
    abstract compress: data: 'T seq * selectors: bool seq -> seq<'T>

    /// Make an iterator that drops elements from the iterable as long as the predicate is true;
    /// afterwards, returns every element.
    /// See https://docs.python.org/3/library/itertools.html#itertools.dropwhile
    abstract dropwhile: predicate: ('T -> bool) * iterable: 'T seq -> seq<'T>

    /// Make an iterator that filters elements from iterable returning only those for which
    /// the predicate returns False.
    /// See https://docs.python.org/3/library/itertools.html#itertools.filterfalse
    abstract filterfalse: predicate: ('T -> bool) * iterable: 'T seq -> seq<'T>

    /// Make an iterator that returns consecutive keys and groups from the iterable
    /// (using identity as the key function).
    /// Note: The group iterators share the underlying iterable — consume each group before advancing.
    /// See https://docs.python.org/3/library/itertools.html#itertools.groupby
    abstract groupby: iterable: 'T seq -> seq<'T * seq<'T>>

    /// Make an iterator that returns consecutive keys and groups from the iterable.
    /// The key is computed for each element; elements with equal consecutive keys are grouped.
    /// Note: The group iterators share the underlying iterable — consume each group before advancing.
    /// See https://docs.python.org/3/library/itertools.html#itertools.groupby
    abstract groupby: iterable: 'T seq * key: ('T -> 'K) -> seq<'K * seq<'T>>

    /// Make an iterator that returns selected elements from the iterable. Stops at position stop.
    /// See https://docs.python.org/3/library/itertools.html#itertools.islice
    [<Emit("$0.islice($1, int($2))")>]
    abstract islice: iterable: 'T seq * stop: int -> seq<'T>

    /// Make an iterator that returns selected elements from the iterable, starting at start.
    /// See https://docs.python.org/3/library/itertools.html#itertools.islice
    [<Emit("$0.islice($1, int($2), int($3))")>]
    abstract islice: iterable: 'T seq * start: int * stop: int -> seq<'T>

    /// Make an iterator that returns selected elements from the iterable, with a step.
    /// See https://docs.python.org/3/library/itertools.html#itertools.islice
    [<Emit("$0.islice($1, int($2), int($3), int($4))")>]
    abstract islice: iterable: 'T seq * start: int * stop: int * step: int -> seq<'T>

    /// Return successive overlapping pairs taken from the iterable.
    /// Requires Python 3.10+.
    /// See https://docs.python.org/3/library/itertools.html#itertools.pairwise
    abstract pairwise: iterable: 'T seq -> seq<'T * 'T>

    /// Make an iterator that returns elements from the iterable as long as the predicate is true.
    /// See https://docs.python.org/3/library/itertools.html#itertools.takewhile
    abstract takewhile: predicate: ('T -> bool) * iterable: 'T seq -> seq<'T>

    /// Make an iterator that aggregates elements from each of the iterables.
    /// If the iterables are of uneven length, missing values are filled with None (option).
    /// See https://docs.python.org/3/library/itertools.html#itertools.zip_longest
    abstract zip_longest: a: 'T seq * b: 'U seq -> seq<'T option * 'U option>

    /// Make an iterator that aggregates elements from each of the iterables.
    /// If the iterables are of uneven length, missing values are filled with fillvalue.
    /// See https://docs.python.org/3/library/itertools.html#itertools.zip_longest
    [<Emit("$0.zip_longest($1, $2, fillvalue=$3)")>]
    abstract zip_longest: a: 'T seq * b: 'T seq * fillvalue: 'T -> seq<'T * 'T>

    // ========================================================================
    // Combinatoric iterators
    // ========================================================================

    /// Return successive r-length permutations of elements in the iterable.
    /// All elements are used (no r limit). Each group is a Python tuple, exposed as seq<'T>.
    /// See https://docs.python.org/3/library/itertools.html#itertools.permutations
    [<Emit("$0.permutations($1)")>]
    abstract permutations: iterable: 'T seq -> seq<'T seq>

    /// Return successive r-length permutations of elements in the iterable.
    /// Each group is a Python tuple, exposed as seq<'T>.
    /// See https://docs.python.org/3/library/itertools.html#itertools.permutations
    [<Emit("$0.permutations($1, int($2))")>]
    abstract permutations: iterable: 'T seq * r: int -> seq<'T seq>

    /// Return successive r-length combinations of elements in the iterable (no repeated elements).
    /// Each group is a Python tuple, exposed as seq<'T>.
    /// See https://docs.python.org/3/library/itertools.html#itertools.combinations
    [<Emit("$0.combinations($1, int($2))")>]
    abstract combinations: iterable: 'T seq * r: int -> seq<'T seq>

    /// Return successive r-length combinations of elements in the iterable, allowing individual
    /// elements to be repeated. Each group is a Python tuple, exposed as seq<'T>.
    /// See https://docs.python.org/3/library/itertools.html#itertools.combinations_with_replacement
    [<Emit("$0.combinations_with_replacement($1, int($2))")>]
    abstract combinationsWithReplacement: iterable: 'T seq * r: int -> seq<'T seq>

    /// Return the Cartesian product of the two input iterables.
    /// See https://docs.python.org/3/library/itertools.html#itertools.product
    abstract product: a: 'T seq * b: 'U seq -> seq<'T * 'U>

    /// Return the Cartesian product of the iterable with itself, repeated r times.
    /// Each group is a Python tuple, exposed as seq<'T>.
    /// See https://docs.python.org/3/library/itertools.html#itertools.product
    [<Emit("$0.product($1, repeat=int($2))")>]
    abstract product: iterable: 'T seq * repeat: int -> seq<'T seq>

/// Functions creating iterators for efficient looping
[<ImportAll("itertools")>]
let itertools: IExports = nativeOnly
