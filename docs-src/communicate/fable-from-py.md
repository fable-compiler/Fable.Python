# Call Fable from Python

Sometimes, we'd like to use the power of Fable in our JavaScript apps. For instance, to create a new
[js node in Node-RED](https://nodered.org/docs/creating-nodes/first-node) or call some handy F# code from our new Node.js serverless function or even call some powerful json parsing into our JavaScript app.

It may allow you to play with Fable and add features, one at a time. So what does it take to call Fable from JavaScript? First you need to understand a bit how the generated JS code looks like so you can call it correctly.

> Remember you can use the [Fable REPL](https://fable.io/repl/) to easily check the generated JS for your F# code!

## Name mangling

Because JS doesn't support overloading or multiple modules in a single file, Fable needs to mangle the name of some members to avoid clashes, which makes it difficult to call such functions or values from JS. However, there're some cases where Fable guarantees names won't change in order to improve interop:

- Record fields
- Interface and abstract members
- Functions and values in the root module

What's a root module? Because F# accepts multiple modules in the same file, we consider the root module the first one containing actual members and is not nested by any other.

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

In F# it's possible to have more than one root module in a single file, in that case everything will be mangled. You should avoid this pattern if you want to expose code to JS:

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

- If you want to have all members attached to a class (as in standard JS classes) and not-mangled use the `AttachMembers` attribute. But be aware **overloads won't work** in this case.
- If you are not planning to use an interface to interact with JS and want to have overloaded members, you can decorate the interface declaration with the `Mangle` attribute. Note: Interfaces coming from .NET BCL (like System.Collections.IEnumerator) are mangled by default.

## Common types and objects

Some F#/.NET types have [counterparts in JS](../dotnet/compatibility.md). Fable takes advantage of this to compile to native types that are more performant and reduce bundle size. You can also use this to improve interop when exchanging data between F# and JS. The most important common types are:

- **Strings and booleans** behave the same in F# and JS.
- **Chars** are compiled as JS strings of length 1. This is mainly because string indexing in JS gives you another string. But you can use a char as a number with an explicit conversion like `int16 '家'`.
- **Numeric types** compile to JS numbers, except for `long`, `decimal` and `bigint`.
- **Arrays** (and `ResizeArray`) compile to JS arrays. _Numeric arrays_ compile to [Typed Arrays](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/TypedArray) in most situations, though this shouldn't make a difference for most common operations like indexing, iterating or mapping. You can disable this behavior with [the `typedArrays` option](https://www.npmjs.com/package/fable-loader#options).
- Any **IEnumerable** (or `seq`) can be traversed in JS as if it were an [Iterable](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Iterators_and_Generators#Iterables).
- **DateTime** compiles to JS `Date`.
- **Regex** compiles to JS `RegExp`.
- Mutable **dictionaries** (not F# maps) compile to [ES2015 Map](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Map).
- Mutable **hashsets** (not F# sets) compile to [ES2015 Set](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Set).

> If the dictionary or hashset requires custom or structural equality, Fable will generate a custom type, but it will share the same properties as JS maps and sets.

- **Objects**: As seen above, only record fields and interface members will be attached to objects without name mangling. Take this into account when sending to or receiving an object from JS.

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

```js
import { createRecord, createClass } from "./Tests.fs"

var record = createRecord(2);

// Ok, we're calling a record field
record.Add(record.Value, 2); // 4

// Fails, this member is not actually attached to the object
record.FiveTimes();

var myClass = createClass(5);

// Fails
myClass.Value;

// Ok, this is an interface member
myClass.Square(); // 25
```

## Functions: automatic uncurrying

Fable will automatically uncurry functions in many situations: when they're passed as functions, when set as a record field... So in most cases you can pass them to and from JS as if they were functions without curried arguments.

```fsharp
let execute (f: int->int->int) x y =
    f x y
```

```js
import { execute } from "./TestFunctions.fs"

execute(function (x, y) { return x * y }, 3, 5) // 15
```

> Check [this](https://fsharpforfunandprofit.com/posts/currying/) for more information on function currying in F# (and other functional languages).

### Using delegates for disambiguation

There are some situations where Fable uncurrying mechanism can get confused, particularly with functions that return other functions. Let's consider the following example:

```fsharp
open Fable.Core.JsInterop

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

The problem here is the compiler cannot tell `unit -> unit -> unit` apart from `unit -> (unit -> unit)`, it can only see a 2-arity lambda (a function accepting two arguments). This won't be an issue if all your code is in F#, but if you're sending the function to JS as in this case, Fable will incorrectly try to uncurry it causing unexpected results.

To disambiguate these cases, you can use [delegates](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/delegates), like `System.Func` which are not curried:

```fsharp
open System

// Remove the ambiguity by using a delegate
let useEffect (effect: Func<unit, (unit -> unit)>): unit =
    importMember "my-js-module"

// Works
useEffect(Func<_,_> myEffect)
```

## Call Fable source code from a JS file

Webpack makes it very easy to include files in different programming languages in your project by using loaders. Because in a Fable project we assume you're already using the [fable-loader](https://www.npmjs.com/package/fable-loader), if you have a file like the following:

```fsharp
module HelloFable

let sayHelloFable() = "Hello Fable!"
```

Importing and using it from JS is as simple as if it were another JS file:

```js
import { sayHelloFable } from "./HelloFable.fs"

console.log(sayHelloFable());
```

### Importing Fable code from Typescript

For better or worse, Typescript wants to check your imported modules and because it doesn't know a thing of F#, it will complain if you try to import an .fs file:

```ts
// Typescript will complain because it cannot read the file
import { sayHelloFable } from "./HelloFable.fs"
```

To appease the Typescript compiler, we need a [declaration file](https://www.typescriptlang.org/docs/handbook/declaration-files/introduction.html), which also gives us the opportunity to tell Typescript what is actually exported by our Fable code. If you place a `HelloFable.d.ts` file like the following, the import above will work:

```ts
// Unfortunately declaration files don't accept relative paths
// so we just use the * wildcard
declare module "*HelloFable.fs" {
    function sayHelloFable(): string;
}
```

## Call Fable compiled code as a library

### ..from your web app

If your project is a web app and you're using Webpack, it just takes two lines of code in the Webpack configuration in the `output` section of `module.exports`:

```js
libraryTarget: 'var',
library: 'MyFableLib'
```

For instance:

```js
    output: {
        path: path.join(__dirname, "./public"),
        filename: "bundle.js",
        libraryTarget: 'var',
        library: 'MyFableLib'
    },
```

This tells Webpack that we want our Fable code to be available from a global variable named `MyFableLib`. That's it!

:::{note}
Only the public functions and values in the **last file of the project** will be exposed.
:::

#### Let's try!

Let's compile the HelloFable app from above with a webpack.config.js that includes the following:

```js
output: {
    ...
    libraryTarget: 'var',
    library: 'OMGFable'
}
```

Now let's try this directly in our `index.html` file:

```html
<body>
    <script src="bundle.js"></script>
    <script type="text/JavaScript">
      alert( OMGFable.sayHelloFable() );
    </script>
</body>
```

Et voilà! We're done! You can find a [full sample here](https://github.com/fable-compiler/fable2-samples/tree/master/interopFableFromJS).

### ...from your Node.js app

Basically it's the same thing. If you want to see a complete sample using a `commonjs` output instead of `var`, please [check this project](https://github.com/fable-compiler/fable2-samples/tree/master/nodejsbundle). There you'll see that we've added the following lines to the Webpack config:

```js
    library:"app",
    libraryTarget: 'commonjs'
```

and then you can call your code from JavaScript like this:

```js
let app = require("./App.js");
```

### Learn more about Webpack `libraryTarget`

If you want to know what your options are, please consult the [official documentation](https://webpack.js.org/configuration/output/#outputlibrarytarget).
