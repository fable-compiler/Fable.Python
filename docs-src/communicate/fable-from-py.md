# Call Fable from Python

Sometimes, we'd like to use the power of Fable in our Python apps. For instance, to call some handy F# code from our new
Python function or even call some powerful json parsing into our Python app.

It may allow you to play with Fable and add features, one at a time. So what does it take to call Fable from Python?
First you need to understand a bit how the generated Python code looks like so you can call it correctly.

> Remember you can use the [Fable REPL](https://fable.io/repl/) to easily check the generated Python for your F# code!
> Just switch to Python on the options panel.

## Name mangling

Because Python doesn't support overloading or multiple modules in a single file, Fable needs to mangle the name of some
members to avoid clashes, which makes it difficult to call such functions or values from Python. However, there're some
cases where Fable guarantees names won't change in order to improve interop:

- Record fields
- Interface and abstract members
- Functions and values in the root module

What's a root module? Because F# accepts multiple modules in the same file, we consider the root module the first one
containing actual members and is not nested by any other.

```fsharp
// It doesn't matter if we have a long namespace, Fable only starts
// counting when it finds actual code
module A.Long.Namespace.RootModule

// The name of this function will be the same in JS
let add (x: int) (y: int) = x + y

module Nested =
    // This will be prefixed with the name of the module in JS
    let add (x: int) (y: int) = x * y
```

In F# it's possible to have more than one root module in a single file, in that case everything will be mangled. You
should avoid this pattern if you want to expose code to Python:

```fsharp
namespace SharedNamespace

// Here both functions will be mangled
// to prevent name conflicts
module Foo =
    let add x y = x + y

module Bar =
    let add x y = x * y
```

### Custom behaviour

In some cases, it's possible to change the default behavior towards name mangling:

- If you want to have all members attached to a class (as in standard Python classes) and not-mangled use the
  `AttachMembers` attribute. But be aware **overloads won't work** in this case.
- If you are not planning to use an interface to interact with Python and want to have overloaded members, you can
  decorate the interface declaration with the `Mangle` attribute. Note: Interfaces coming from .NET BCL (like
  System.Collections.IEnumerator) are mangled by default.

## Common types and objects

Some F#/.NET types have [counterparts in Python](../dotnet/compatibility.md). Fable takes advantage of this to compile
to native types that are more performant and reduce bundle size. You can also use this to improve interop when
exchanging data between F# and Python. The most important common types are:

- **Strings and booleans** behave the same in F# and Python.
- **Chars** are compiled as Python strings of length 1. This is mainly because string indexing in Python gives you
  another string. But you can use a char as a number with an explicit conversion like `int16 '家'`.
- **Numeric types** compile to Python integers, except for `long`, `decimal` and `bigint`.
- **Arrays** (and `ResizeArray`) compile to Python lists. _Numeric arrays_ compile to [array](https://docs.python.org/3/library/array.html) in most
  situations, though this shouldn't make a difference for most common operations like indexing, iterating or mapping.
- Any **IEnumerable** (or `seq`) can be traversed in Python as if it were an [Iterable](https://docs.python.org/3/library/collections.abc.html#collections.abc.Iterable).
- **DateTime** compiles to Python `datetime`.
- **Regex** compiles to Python `re`.
- Mutable **dictionaries** (not F# maps) compile to Python dictionaries.
- Mutable **hashsets** (not F# sets) compile to a custom HashSet type.

> If the dictionary or hashset requires custom or structural equality, Fable will generate a custom type, but it will
> share the same properties as JS maps and sets.

- **Objects**: As seen above, only record fields and interface members will be attached to objects without name
  mangling. Take this into account when sending to or receiving an object from Python.

```fsharp
type MyRecord =
    { Value: int
      Add: int -> int -> int }
    member this.FiveTimes() =
        this.Value * 5

type IMyInterface =
    abstract Square: unit -> float

type MyClass(value: float) =
    member __.Value = value
    interface IMyInterface with
        member __.Square() = value * value

let createRecord(value: int) =
    { Value = value
      Add = fun x y -> x + y }

let createClass(value: float) =
    MyClass(value)
```

```py
import tests import create_record, create_class

record = create_record(2)

# Ok, we're calling a record field
record.Add(record.Value, 2) # 4

# Fails, this member is not actually attached to the object
record.FiveTimes()

var myClass = create_class(5)

# Fails
myClass.Value

# Ok, this is an interface member
myClass.Square() # 25
```

## Functions: automatic uncurrying

Fable will automatically uncurry functions in many situations: when they're passed as functions, when set as a record
field... So in most cases you can pass them to and from Python as if they were functions without curried arguments.

```fsharp
let execute (f: int->int->int) x y =
    f x y
```

```py
from test_functions import execute

execute(lambda x, y: x * y , 3, 5) # 15
```

> Check [this](https://fsharpforfunandprofit.com/posts/currying/) for more information on function currying in F# (and
> other functional languages).

### Using delegates for disambiguation

There are some situations where Fable uncurrying mechanism can get confused, particularly with functions that return
other functions. Let's consider the following example:

```fsharp
open Fable.Core.PyInterop

let myEffect() =
    printfn "Effect!"
    fun () -> printfn "Cleaning up"

// Method from a JS module, expects a function
// that returns another function for disposing
let useEffect (effect: unit -> (unit -> unit)): unit =
    importMember "my-js-module"

// Fails, Fable thinks this is a 2-arity function
useEffect myEffect
```

The problem here is the compiler cannot tell `unit -> unit -> unit` apart from `unit -> (unit -> unit)`, it can only see
a 2-arity lambda (a function accepting two arguments). This won't be an issue if all your code is in F#, but if you're
sending the function to JS as in this case, Fable will incorrectly try to uncurry it causing unexpected results.

To disambiguate these cases, you can use
[delegates](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/delegates), like `System.Func` which are
not curried:

```fsharp
open System

// Remove the ambiguity by using a delegate
let useEffect (effect: Func<unit, (unit -> unit)>): unit =
    importMember "my-js-module"

// Works
useEffect(Func<_,_> myEffect)
```