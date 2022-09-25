# .NET and F# compatibility

Fable provides support for some classes of .NET BCL (Base Class Library) and most of FSharp.Core library. When possible, Fable translates .NET types and methods to native JavaScript APIs for minimum overhead.

## .NET Base Class Library

The following classes are translated to JS and most of their methods (static and instance) should be available in Fable.

.NET                                  | Python
--------------------------------------|----------------------------
Numeric Types                         | int
Arrays                                | array / bytearray
Events                                | fable-core/Event
System.Boolean                        | bool
System.Char                           | str
System.String                         | str
System.Guid                           | guid
System.TimeSpan                       | timedelta
System.DateTime                       | datetime
System.DateTimeOffset                 | timedelta
System.Timers.Timer                   | fable-core/Timer
System.Collections.Generic.List       | List
System.Collections.Generic.HashSet    | Set
System.Collections.Generic.Dictionary | dict
System.Text.RegularExpressions.Regex  | re
System.Lazy                           | fable-core/Lazy
System.Random                         | {}
System.Math                           | math

The following static methods are also available:

- `System.Console.WriteLine` (also with formatting)
- `System.Diagnostics.Debug.WriteLine` (also with formatting)
- `System.Diagnostics.Debug.Assert(condition: bool)`
- `System.Diagnostics.Debugger.Break()`
- `System.Activator.CreateInstance<'T>()`

There is also support to convert between numeric types and to parse
strings, check [the conversion
tests](https://github.com/fable-compiler/Fable/blob/master/tests/Main/ConvertTests.fs).

### Caveats

- All numeric types become Python `int` (64-bit floating type), except for `decimal`. Check the [Numeric
  Types](numbers.md) section to learn more about the differences between .NET and Python.
- Numeric arrays are compiled to [array](https://docs.python.org/3/library/array.html) when possible. Numeric arrays of
  `uint8` are compiled to [bytearray](https://docs.python.org/3/library/array.html#bytearrays) in order to support
  libraries that expect e.g `bytes` as input.
- No bound checks for numeric types (unless you do explicit conversions like `byte 500`) nor for array indices.

## FSharp.Core

Most of FSharp.Core operators are supported, as well as formatting with `sprintf`, `printfn` or `failwithf`
(`String.Format` is also available). The following types and/or corresponding modules from FSharp.Core lib will likewise
translate to JS:

.NET              | Python
------------------|----------------------------------------------------------
Tuples            | tuple
Option            | (erased)
Choice            | fable-core/Choice
Result            | fable-core/Result
String            | fable-core/String (module)
Seq               | [Iterable](ttps://docs.python.org/3/library/collections.abc.html#collections.abc.Iterable)
List              | fable-core/List
Map               | fable-core/Map
Set               | fable-core/Set
Async             | fable-core/Async
Task              | [Awaitable](https://docs.python.org/3/library/asyncio-task.html#awaitables)
Event             | fable-core/Event (module)
Observable        | fable-core/Observable (module)
Arrays            | array / bytearray
Events            | fable-core/Event
MailboxProcessor  | fable-core/MailboxProcessor (limited support)

### Caveats II

- Options are **erased** in Python (`Some 5` becomes just `5` in Python and `None` translates to `None`). This is needed
  for example, to represent Python [optional properties](https://docs.python.org/3/library/typing.html#typing.Optional).
  However in a few cases (like nested options) there is an actual representation of the option in the runtime.
- `Async.RunSynchronously` is not supported.
- `MailboxProcessor` is single-threaded in Python and currently only `Start`, `Receive`, `Post` and `PostAndAsyncReply`
  are implemented (`cancellationToken` or `timeout` optional arguments are not supported).

## Object Oriented Programming

Most of F# OOP features are compatible with Fable: interfaces and abstract classes, structs, inheritance, overloading,
etc. However, please note that instance members are not attached to the prototype, which means they won't be accessible
from native JS code. The exception to this rule are the implementations of **interface and abstract members**.

### Caveats III

- It's not possible to type test against interfaces or generic types.

## Reflection and Generics

There is some reflection support in Fable, you can check the [reflection tests](https://github.com/fable-compiler/Fable/blob/master/tests/Main/ReflectionTests.fs) to see what is currently possible.

Generics are erased by default in the generated Python code. However, it is still possible to access generic information
(like `typeof<'T>`) at runtime by marking functions with `inline`:

```fsharp
let doesNotCompileInFable(x: 'T) =
    typeof<'T>.FullName |> printfn "%s"

let inline doesWork(x: 'T) =
    typeof<'T>.FullName |> printfn "%s"

doesWork 5
```
