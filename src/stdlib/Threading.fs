/// Type bindings for Python threading module: https://docs.python.org/3/library/threading.html
module Fable.Python.Threading

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Return the 'thread identifier' of the current thread
    /// See https://docs.python.org/3/library/threading.html#threading.get_ident
    abstract get_ident: unit -> int
    /// Return the main Thread object
    /// See https://docs.python.org/3/library/threading.html#threading.main_thread
    abstract main_thread: unit -> Thread
    /// Return the current Thread object
    /// See https://docs.python.org/3/library/threading.html#threading.current_thread
    abstract current_thread: unit -> Thread
    /// Return the number of Thread objects currently alive
    /// See https://docs.python.org/3/library/threading.html#threading.active_count
    abstract active_count: unit -> int
    /// Return a list of all Thread objects currently active
    /// See https://docs.python.org/3/library/threading.html#threading.enumerate
    abstract enumerate: unit -> Thread list
    /// Return a new thread-local data object
    /// See https://docs.python.org/3/library/threading.html#threading.local
    abstract local: unit -> obj

/// A thread of execution
/// See https://docs.python.org/3/library/threading.html#threading.Thread
and [<Import("Thread", "threading")>] Thread(?target: unit -> unit, ?name: string, ?daemon: bool) =
    /// Start the thread's activity
    member _.start() : unit = nativeOnly
    /// Wait until the thread terminates
    member _.join(?timeout: float) : unit = nativeOnly
    /// A boolean value indicating whether this thread is a daemon thread
    member _.daemon: bool = nativeOnly
    /// The thread's name
    member _.name: string = nativeOnly
    /// The 'thread identifier' of this thread
    member _.ident: int option = nativeOnly
    /// Whether the thread is alive
    member _.is_alive() : bool = nativeOnly

/// A lock object (mutual exclusion)
/// See https://docs.python.org/3/library/threading.html#threading.Lock
[<Import("Lock", "threading")>]
type Lock() =
    /// Acquire the lock
    member _.acquire(?blocking: bool, ?timeout: float) : bool = nativeOnly
    /// Release the lock
    member _.release() : unit = nativeOnly
    /// Return whether the lock is locked
    member _.locked() : bool = nativeOnly

/// A reentrant lock object
/// See https://docs.python.org/3/library/threading.html#threading.RLock
[<Import("RLock", "threading")>]
type RLock() =
    /// Acquire the lock
    member _.acquire(?blocking: bool, ?timeout: float) : bool = nativeOnly
    /// Release the lock
    member _.release() : unit = nativeOnly

/// An event object for thread synchronization
/// See https://docs.python.org/3/library/threading.html#threading.Event
[<Import("Event", "threading")>]
type Event() =
    /// Set the internal flag to true
    member _.set() : unit = nativeOnly
    /// Reset the internal flag to false
    member _.clear() : unit = nativeOnly
    /// Return true if and only if the internal flag is true
    member _.is_set() : bool = nativeOnly
    /// Block until the internal flag is true or timeout
    member _.wait(?timeout: float) : bool = nativeOnly

/// Threading module access and utilities
[<ImportAll("threading")>]
let threading: IExports = nativeOnly
