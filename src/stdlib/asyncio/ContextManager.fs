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

    /// Run the body within a Python asynchronous context manager, mirroring
    /// Python's `async with manager as resource: ...`.
    ///
    /// `__aenter__()` is awaited to acquire the resource, `body` is run with it,
    /// and `__aexit__(...)` is awaited afterwards on both the success and error
    /// paths. If the body raises and `__aexit__` returns a truthy value the
    /// exception is suppressed (as `async with` does); otherwise it is re-raised.
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
            let! resource = manager.AEnter()

            try
                let! result = body resource
                let! _ = manager.AExit()
                return result
            with error ->
                let! suppress = manager.AExit(error)

                if suppress then
                    return Unchecked.defaultof<'U>
                else
                    return raise error
        }
