/// Type bindings for Python functools module: https://docs.python.org/3/library/functools.html
module Fable.Python.Functools

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    // ========================================================================
    // Higher-order functions
    // ========================================================================

    /// Apply a function of two arguments cumulatively to the items of an iterable,
    /// reducing it to a single value (fold-left without a seed).
    /// See https://docs.python.org/3/library/functools.html#functools.reduce
    abstract reduce: func: System.Func<'T, 'T, 'T> * iterable: 'T seq -> 'T

    /// Apply a function of two arguments cumulatively to the items of an iterable,
    /// starting with the initializer as the seed value (fold-left with a seed).
    /// See https://docs.python.org/3/library/functools.html#functools.reduce
    [<Emit("$0.reduce($1, $2, $3)")>]
    abstract reduce: func: System.Func<'State, 'T, 'State> * iterable: 'T seq * initializer: 'State -> 'State

    // ========================================================================
    // Caching decorators
    // ========================================================================

    /// Wrap func with an LRU (least-recently-used) cache of at most maxsize entries.
    /// Returns a memoised callable with the same signature as func.
    /// Requires Python 3.8+.
    /// See https://docs.python.org/3/library/functools.html#functools.lru_cache
    [<Emit("$0.lru_cache(maxsize=int($1))($2)")>]
    abstract lruCache: maxsize: int * func: ('T -> 'R) -> ('T -> 'R)

    /// Wrap func with an unbounded cache (equivalent to lru_cache(maxsize=None)).
    /// Requires Python 3.9+.
    /// See https://docs.python.org/3/library/functools.html#functools.cache
    [<Emit("$0.cache($1)")>]
    abstract cache: func: ('T -> 'R) -> ('T -> 'R)

/// Higher-order functions and operations on callable objects
[<ImportAll("functools")>]
let functools: IExports = nativeOnly
