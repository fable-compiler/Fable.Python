# .NET and F# compatibility

Fable provides support for some classes of .NET BCL (Base Class Library) and most of FSharp.Core library. When possible, Fable translates .NET types and methods to native JavaScript APIs for minimum overhead.

## .NET Base Class Library

The following classes are translated to JS and most of their methods (static and instance) should be available in Fable.

.NET                                  | JavaScript
--------------------------------------|----------------------------
Numeric Types                         | number
Arrays                                | Array / Typed Arrays
Events                                | fable-core/Event
System.Boolean                        | boolean
System.Char                           | string
System.String                         | string
System.Guid                           | string
System.TimeSpan                       | number
System.DateTime                       | Date
System.DateTimeOffset                 | Date
System.Timers.Timer                   | fable-core/Timer
System.Collections.Generic.List       | Array
System.Collections.Generic.HashSet    | Set
System.Collections.Generic.Dictionary | Map
System.Text.RegularExpressions.Regex  | RegExp
System.Lazy                           | fable-core/Lazy
System.Random                         | {}
System.Math                           | (native JS functions)

The following static methods are also available:

- `System.Console.WriteLine` (also with formatting)
- `System.Diagnostics.Debug.WriteLine` (also with formatting)
- `System.Diagnostics.Debug.Assert(condition: bool)`
- `System.Diagnostics.Debugger.Break()`
- `System.Activator.CreateInstance<'T>()`

There is also support to convert between numeric types and to parse strings, check [the conversion tests](https://github.com/fable-compiler/Fable/blob/master/tests/Main/ConvertTests.fs).

### Caveats

- All numeric types become JS `number` (64-bit floating type), except for `int64`, `uint64`, `bigint` and `decimal`. Check the [Numeric Types](numbers.html) section to learn more about the differences between .NET and JS.
- Numeric arrays are compiled to [Typed Arrays](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/TypedArray) when possible.
- No bound checks for numeric types (unless you do explicit conversions like `byte 500`) nor for array indices.
- `Regex` will always behave as if passed `RegexOptions.ECMAScript` flag (e.g., no negative look-behind or named groups).

## FSharp.Core

Most of FSharp.Core operators are supported, as well as formatting with `sprintf`, `printfn` or `failwithf` (`String.Format` is also available).
The following types and/or corresponding modules from FSharp.Core lib will likewise translate to JS:

.NET              | JavaScript
------------------|----------------------------------------------------------
Tuples            | Array
Option            | (erased)
Choice            | fable-core/Choice
Result            | fable-core/Result
String            | fable-core/String (module)
Seq               | [Iterable](http://babeljs.io/docs/learn-es2015/#iterators-for-of)
List              | fable-core/List
Map               | fable-core/Map
Set               | fable-core/Set
Async             | fable-core/Async
Event             | fable-core/Event (module)
Observable        | fable-core/Observable (module)
Arrays            | Array / Typed Arrays
Events            | fable-core/Event
MailboxProcessor  | fable-core/MailboxProcessor (limited support)

### Caveats II

- Options are **erased** in JS (`Some 5` becomes just `5` in JS and `None` translates to `null`). This is needed for example, to represent TypeScript [optional properties](https://www.typescriptlang.org/docs/handbook/interfaces.html#optional-properties). However in a few cases (like nested options) there is an actual representation of the option in the runtime.
- `Async.RunSynchronously` is not supported.
- `MailboxProcessor` is single-threaded in JS and currently only `Start`, `Receive`, `Post` and `PostAndAsyncReply` are implemented (`cancellationToken` or `timeout` optional arguments are not supported).

## Object Oriented Programming

Most of F# OOP features are compatible with Fable: interfaces and abstract classes, structs, inheritance, overloading, etc. However, please note that due to some limitations of [ES2015 classes](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Classes) the generated code uses the [prototype chain](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Inheritance_and_the_prototype_chain) instead. Also note that instance members are not attached to the prototype, which means they won't be accessible from native JS code. The exception to this rule are the implementations of **interface and abstract members**.

### Caveats III

- It's not possible to type test against interfaces or generic types.

## Reflection and Generics

There is some reflection support in Fable, you can check the [reflection tests](https://github.com/fable-compiler/Fable/blob/master/tests/Main/ReflectionTests.fs) to see what is currently possible.

Generics are erased by default in the generated Python code. However, it is still possible to access generic information (like `typeof<'T>`) at runtime by marking functions with `inline`:

```fsharp
let doesNotCompileInFable(x: 'T) =
    typeof<'T>.FullName |> printfn "%s"

let inline doesWork(x: 'T) =
    typeof<'T>.FullName |> printfn "%s"

doesWork 5
```
