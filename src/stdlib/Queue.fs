module Fable.Python.Queue

open Fable.Core

// fsharplint:disable MemberNames

[<Import("Queue", "queue")>]
type Queue<'T>() =
    /// Return the approximate size of the queue. Note, qsize() > 0 doesn’t guarantee that a subsequent get() will not
    /// block, nor will qsize() < maxsize guarantee that put() will not block.
    member x.qsize() : int = nativeOnly
    /// Return True if the queue is empty, False otherwise. If empty() returns True it doesn’t guarantee that a
    /// subsequent call to put() will not block. Similarly, if empty() returns False it doesn’t guarantee that a
    /// subsequent call to get() will not block.
    member x.empty() : bool = nativeOnly
    /// Return True if the queue is full, False otherwise. If full() returns True it doesn’t guarantee that a subsequent
    /// call to get() will not block. Similarly, if full() returns False it doesn’t guarantee that a subsequent call to
    /// put() will not block.
    member x.full() : bool = nativeOnly
    /// Put item into the queue. If optional args block is true and timeout is None (the default), block if necessary
    /// until a free slot is available. If timeout is a positive number, it blocks at most timeout seconds and raises
    /// the Full exception if no free slot was available within that time. Otherwise (block is false), put an item on
    /// the queue if a free slot is immediately available, else raise the Full exception (timeout is ignored in that
    /// case).
    member x.put(item: 'T, ?block: bool, ?timeout: float) : unit = nativeOnly
    /// Remove and return an item from the queue. If optional args block is true and timeout is None (the default),
    /// block if necessary until an item is available. If timeout is a positive number, it blocks at most timeout
    /// seconds and raises the Empty exception if no item was available within that time. Otherwise (block is false),
    /// return an item if one is immediately available, else raise the Empty exception (timeout is ignored in that
    /// case).
    ///
    /// Prior to 3.0 on POSIX systems, and for all versions on Windows, if block is true and timeout is None, this
    /// operation goes into an uninterruptible wait on an underlying lock. This means that no exceptions can occur, and
    /// in particular a SIGINT will not trigger a KeyboardInterrupt.
    member x.get(?block: bool, ?timeout: float) : 'T = nativeOnly
    /// Blocks until all items in the queue have been gotten and processed.
    ///
    /// The count of unfinished tasks goes up whenever an item is added to the queue. The count goes down whenever a
    /// consumer thread calls task_done() to indicate that the item was retrieved and all work on it is complete. When
    /// the count of unfinished tasks drops to zero, join() unblocks.
    member x.join() : unit = nativeOnly

    member x.task_done() : unit = nativeOnly

[<Import("PriorityQueue", "queue")>]
type PriorityQueue<'T>() =
    inherit Queue<'T>()

[<Import("LifoQueue", "queue")>]
type LifoQueue<'T>() =
    inherit Queue<'T>()

[<Import("SimpleQueue", "queue")>]
type SimpleQueue<'T>() =
    /// Return the approximate size of the queue. Note, qsize() > 0 doesn’t guarantee that a subsequent get() will not
    /// block, nor will qsize() < maxsize guarantee that put() will not block.
    member x.qsize() : int = nativeOnly
    /// Return True if the queue is empty, False otherwise. If empty() returns True it doesn’t guarantee that a
    /// subsequent call to put() will not block. Similarly, if empty() returns False it doesn’t guarantee that a
    /// subsequent call to get() will not block.
    member x.empty() : bool = nativeOnly
    /// Return True if the queue is full, False otherwise. If full() returns True it doesn’t guarantee that a subsequent
    /// call to get() will not block. Similarly, if full() returns False it doesn’t guarantee that a subsequent call to
    /// put() will not block.
    member x.put(item: 'T, ?block: bool, ?timeout: float) : unit = nativeOnly
    /// Remove and return an item from the queue. If optional args block is true and timeout is None (the default),
    /// block if necessary until an item is available. If timeout is a positive number, it blocks at most timeout
    /// seconds and raises the Empty exception if no item was available within that time. Otherwise (block is false),
    /// return an item if one is immediately available, else raise the Empty exception (timeout is ignored in that
    /// case).
    ///
    /// Prior to 3.0 on POSIX systems, and for all versions on Windows, if block is true and timeout is None, this
    /// operation goes into an uninterruptible wait on an underlying lock. This means that no exceptions can occur, and
    /// in particular a SIGINT will not trigger a KeyboardInterrupt.
    member x.get(?block: bool, ?timeout: float) : 'T = nativeOnly
