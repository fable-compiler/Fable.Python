// Bindings for Python asynchronous context managers (the `async with` protocol:
// __aenter__ / __aexit__).
//
// F# `use`/`use!` only supports IDisposable (synchronous Dispose), so there is
// no built-in way to consume a Python async context manager, and `async with`
// cannot be expressed directly because F# `task`/`async` cannot `await` inside a
// `finally`. Cast a value returning such an object to IAsyncContextManager and
// drive the protocol from a `task { }` by awaiting __aexit__ on the success and
// error paths (never in a finalizer) — see AsyncContextManager.using.
namespace Fable.Python.AsyncIO

open System.Threading.Tasks
open Fable.Core

/// A Python asynchronous context manager: an object implementing `__aenter__`
/// and `__aexit__`. Bind (or unbox) library values returning such objects to
/// this interface to drive the `async with` protocol from F#.
type IAsyncContextManager<'T> =
    /// `__aenter__()` — acquire the resource. Await the result in a `task`.
    [<Emit("$0.__aenter__()")>]
    abstract member AEnter: unit -> Task<'T>

    /// `__aexit__(None, None, None)` — release after the body succeeded.
    [<Emit("$0.__aexit__(None, None, None)")>]
    abstract member AExit: unit -> Task<bool>

    /// `__aexit__(type(e), e, e.__traceback__)` — release after the body raised.
    /// A truthy result means the exception was handled and should be suppressed.
    [<Emit("$0.__aexit__(type($1), $1, getattr($1, '__traceback__', None))")>]
    abstract member AExit: error: exn -> Task<bool>

[<RequireQualifiedAccess>]
module AsyncContextManager =

    /// Acquire the resource, run the body, and await `__aexit__` exactly once.
    /// Returns `Ok result` when the body succeeds, or `Error (error, suppress)`
    /// when it raises — where `suppress` is the truthy/falsy value `__aexit__`
    /// returned for that error.
    ///
    /// The body's outcome is captured in an inner `task` so that `__aexit__` is
    /// awaited *outside* the `try`. Awaiting the success-path `__aexit__` inside
    /// the `try` would route an exception it raises into the error branch and
    /// call `__aexit__` a second time — Python's `async with` never does this.
    let private run (manager: IAsyncContextManager<'T>) (body: 'T -> Task<'U>) : Task<Result<'U, exn * bool>> =
        task {
            let! resource = manager.AEnter()

            let! outcome =
                task {
                    try
                        let! result = body resource
                        return Ok result
                    with error ->
                        return Error error
                }

            match outcome with
            | Ok result ->
                let! _ = manager.AExit()
                return Ok result
            | Error error ->
                let! suppress = manager.AExit(error)
                return Error(error, suppress)
        }

    /// Run the body within a Python asynchronous context manager, mirroring
    /// Python's `async with manager as resource: ...`.
    ///
    /// `__aenter__()` is awaited to acquire the resource, `body` is run with it,
    /// and `__aexit__(...)` is awaited afterwards on both the success and error
    /// paths. If the body raises, the exception is **always re-raised** after
    /// `__aexit__` runs — a truthy `__aexit__` return is ignored so the result
    /// type stays a plain `'U`. Use `tryUsing` if you need to honor suppression.
    ///
    /// ```fsharp
    /// task {
    ///     let! rows =
    ///         AsyncContextManager.using (pool.acquire ()) (fun conn ->
    ///             task { return! conn.fetch "SELECT 1" })
    ///     return rows
    /// }
    /// ```
    let using (manager: IAsyncContextManager<'T>) (body: 'T -> Task<'U>) : Task<'U> =
        task {
            let! outcome = run manager body

            match outcome with
            | Ok result -> return result
            | Error(error, _) -> return raise error
        }

    /// Like `using`, but honors `__aexit__`'s suppression signal — matching the
    /// full `async with` contract.
    ///
    /// Returns `Some result` when the body succeeds. If the body raises and
    /// `__aexit__` returns a truthy value (the exception is handled), returns
    /// `None`; otherwise the exception is re-raised.
    let tryUsing (manager: IAsyncContextManager<'T>) (body: 'T -> Task<'U>) : Task<'U option> =
        task {
            let! outcome = run manager body

            match outcome with
            | Ok result -> return Some result
            | Error(error, suppress) ->
                if suppress then
                    return None
                else
                    return raise error
        }
