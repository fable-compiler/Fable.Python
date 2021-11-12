# Call Python from Fable

Interoperability is a matter of trust between your statically typed F# code and your untyped dynamic JS code. In order to mitigate risks, Fable gives you several possibilities, amongst them type safety through interface contracts. Sometimes it may sound more convenient to just call JS code in a more dynamic fashion, although be aware by doing this potential bugs will not be discovered until runtime.

We'll describe both the safe way and the dynamic way and then it will be up to you to decide. Let's start!

## Adding the JS library to the project

The very first thing to do is add the library to our project. Since we always have a `package.json` file, we'll just add the wanted library to our project using either `npm install my-awesome-js-library`. The library will then be available in the `node_modules` folder.

> If your library is in a file, just skip this step.

## Type safety with Imports and Interfaces

To use code from JS libraries first you need to import it into F#. For this Fable uses [ES2015 imports](https://developer.mozilla.org/en/docs/web/JavaScript/reference/statements/import), which can be later transformed to other JS module systems like `commonjs` or `amd` by [Babel](https://babeljs.io/docs/en/plugins#modules).

There are two ways to declare ES2015 imports in Fable: by using either the **Import attribute** or the **import expressions**. The `ImportAttribute` can decorate members, types or modules and works as follows:

```fsharp
// Namespace imports
[<Import("*", from="my-module")>]
// import * from "my-module"

// Member imports
[<Import("myFunction", from="my-module")>]
// import { myFunction } from "my-module"

// Default imports
[<Import("default", from="express")>]
// import Express from "express"
```

You can also use the following alias attributes:

```fsharp
open Fable.Core
open Fable.Core.JsInterop

// Same as Import("*", "my-module")
[<ImportAll("my-module")>]
let myModule: obj = jsNative

// Same as Import("default", "my-module")
[<ImportDefault("my-module")>]
let myModuleDefaultExport: obj = jsNative

// The member name is taken from decorated value, here `myFunction`
[<ImportMember("my-module")>]
let myFunction(x: int): int = jsNative
```

If the value is globally accessible in JS, you can use the `Global` attribute with an optional name parameter instead.

```fsharp
let [<Global>] console: JS.Console = jsNative

// You can pass a string argument if you want to use a different name in your F# code
let [<Global("console")>] logger: JS.Console = jsNative
```

### Importing relative paths when using an output directory

If you are putting the generated JS files in an output directory using Fable's `-o` option, be aware that Fable will only move files corresponding to .fs sources, not external files like .js or .css. Fable will automatically adjust relative paths in imports to the location of the generated file, but sometimes it's difficult to know where the generated file will end up exactly. In these cases, it's useful to use the `${outDir}` macro which will be replaced by the output directory. For example, if we are importing a CSS module like this:

```fsharp
[<ImportDefault("${outDir}/../styles/styles.module.css")>]
let styles: CssModule = jsNative
```

Let's say we're compiling with the `-o build` option and the file ends up in the `build/Components` directory. The generated code will look like:

```js
import styles from "../../styles/styles.module.css"
```

### OOP Class definition and inheritance

Assuming we need to import a JS class, we can represent it in F# using a standard class declaration with an `Import` attribute. In this case we use `jsNative` as a dummy implementation for its members as the actual implementation will come from JS. When using `jsNative` don't forget to add the return type to the member!

```fsharp
[<Import("DataManager", from="library/data")>]
type DataManager<'Model> (conf: Config) =
    member _.delete(data: 'Model): Promise<'Model> = jsNative
    member _.insert(data: 'Model): Promise<'Model> = jsNative
    member _.update(data: 'Model): Promise<'Model> = jsNative
```

From this point it is possible to use it or even to inherit from it as it is usually done on regular F#, [see the official F# documentation](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/inheritance). If you want to inherit and override a member, F# requires you to declare the member as `abstract` in the base class, with a default implementation if you want to make it also directly usable from the base class. For example:

```fsharp
// This class lives in Python
[<Import("DataManager", from="library.data")>]
type DataManager<'T> (conf: Config) =
    abstract update: data: 'T -> Promise<'T>
    default _.update(data: 'T): Promise<'T> = jsNative

// This class lives in our code
type MyDataManager<'T>(config) =
    inherit DataManager<'T>(config)
    // We can do something with data before sending it to the base class
    override _.update(data) =
        base.update(data)

let test (data: 'T) (manager: DataManager<'T>) =
    manager.update(data) |> ignore

// This will call MyDataManager.update even if test expects DataManager
MyDataManager(myConfig) |> test myData
```

:::{note}
If you have a Python type declaration for the Python code you want to import, you can use [ts2fable](https://github.com/fable-compiler/ts2fable) to help you write the F# bindings.
:::

### Let's practice! 1st try!

Now that we've seen this, let's review the code in the [interop](https://github.com/fable-compiler/fable3-samples/tree/master/interop) sample

Let's say we have an `alert.js` file that we'd like to use in our Fable project.

```js
function triggerAlert(message) {
  alert(message);
}

const someString = "And I Like that!";

export { triggerAlert, someString };
```

As you can see we've got one function, `triggerAlert` and one constant `someString`. Both are exported using the ES6 `export` keyword.

In order to use this in our Fable code, let's create an `interface` that will mimic this:

```fsharp
  open Fable.Core.JsInterop // needed to call interop tools

  type IAlert =
    abstract triggerAlert : message:string -> unit
    abstract someString: string
```

As you can see the process is quite easy. The `I` in `IAlert` is not mandatory but it's a precious hint that we're going to use an interface. The `abstract` keyword only indicates that there's no actual implementation in F#. That's true, since we rely on the JavaScript one.

Now let's use this:

```fsharp
  [<ImportAll("path/to/alert.js")>]
  let mylib: IAlert = jsNative
```

Here we use the `ImportAll` attribute, which is the same as `Import("*", "path/to/alert.js")` just like we described earlier.

- step 1: We specify the elements we wish to use. Here `*` means: "take everything that's been exported"
- step 2: we set the path to our js library.
- step 3: we create a let binding called `mylib` to map the js library.
- step 4: we use the `jsNative` keyword to say that `mylib` is just a placeholder for the JavaScript native implementation.

Now we can use this:

```fsharp
mylib.triggerAlert ("Hey I'm calling my js library from Fable > " + mylib.someString)
```

If everything's working correctly, it should create an alert popup in your browser! Of course this sample is intended for web apps, but you could do the same in a Node.js app.

### Let's practice! 2nd try!

Assuming we've got two exported functions from a `Canvas.js` file: `drawSmiley` and `drawBubble`:

```js
export function drawSmiley(id) {
  // do something
}
export function drawBubble(id) {
  // do something
}
```

we could use the same method we used with `alert.js`:

```fsharp
  open Fable.Core.JsInterop // needed to call interop tools

  type ICanvas =
    abstract drawSmiley: (id:string) -> unit
    abstract drawBubble: (id:string) -> unit

  [<ImportAll("path/to/Canvas.js")>]
  let mylib: ICanvas = jsNative

  mylib.drawSmiley "smiley" // etc..
```

or we could use the `importMember` helper function to directly map the js function to the F# function.

```fsharp
open Fable.Core.JsInterop // needed to call interop tools

module Canvas =
  // here we just import a member function from canvas.js called drawSmiley.
  let drawSmiley(id:string): unit = importMember "path/to/Canvas.js"
  let drawBubble(id:string): unit = importMember "path/to/Canvas.js"

Canvas.drawSmiley "smiley"
```

The result would be the same, but the philosophy is slightly different. That's basically up to you to make a choice 😉

### Miscellaneous import helpers

There are other interop helpers you can use thanks to `Fable.Core.JsInterop`:

```fsharp
open Fable.Core.JsInterop

let buttons: obj = importAll "my-lib/buttons"
// JS: import * as buttons from "my-lib/buttons"

// It works for function declarations too
let getTheme(x: int): IInterface = importDefault "my-lib"
// JS: import getTheme from "my-lib"

let myString: string = importMember "my-lib"
// JS: import { myString } from "my-lib"

// Use just `import` to make the member name explicit
// as with the ImportAttribute
let aDifferentName: string = import "myString" "my-lib"
// import { myString } from "my-lib"
```

Occasionally, you may need to import JS purely for its side-effects. For example, some browser polyfills don't export any functions, but extend the browser's built-in DOM types when executed.

For this, use `importSideEffects`.

```fsharp
open Fable.Core.JsInterop

importSideEffects("my-polyfill-library") // from npm package

importSideEffects("./my-polyfill.js") // from local .js file
```

## Emit, when F# is not enough

You can use the `Emit` attribute to decorate a function. Every call to the function will then be replaced inline by the content of the attribute with the placeholders `$0, $1, $2...` replaced by the arguments. For example, the following code will generate JavaScript as seen below.

```fsharp
open Fable.Core

[<Emit("$0 + $1")>]
let add (x: int) (y: string): string = jsNative

let result = add 1 "2"
```

When you don't know the exact number of arguments you can use the following syntax:

```fsharp
type Test() =
    [<Emit("$0($1...)")>]
    member __.Invoke([<ParamArray>] args: int[]): obj = jsNative
```

It's also possible to pass syntax conditioned to optional parameters:

```fsharp
type Test() =
    [<Emit("$0[$1]{{=$2}}")>]
    member __.Item with get(): float = jsNative and set(v: float): unit = jsNative

    // This syntax means: if second arg evals to true in JS print 'i' and nothing otherwise
    [<Emit("new RegExp($0,'g{{$1?i:}}')")>]
    member __.ParseRegex(pattern: string, ?ignoreCase: bool): Regex = jsNative
```

The content of `Emit` will actually be parsed by [Babel](https://babeljs.io/) so it will still be validated somehow. However, it's not advised to abuse this method, as the code won't be checked by the F# compiler.

### Let's do it! Use Emit

Now let's work with Emit and look at a new example with the following `MyClass.js`:

```js
export default class MyClass {
  // Note the constructor accepts an object
  // with the `value` and `awesomeness` fields
  constructor({ value, awesomeness }) {
    this._value = value;
    this._awesomeness = awesomeness;
  }

  get value() {
    return this._value;
  }

  set value( newValue ) {
    this._value = newValue;
  }

  isAwesome() {
    return this._value === this._awesomeness;
  }

  static getPI() {
    return Math.PI;
  }
}
```

Let's list its members:

- a `value` member which returns the current value with a getter and a setter
- a method, `isAwesome`, that checks if the current value equals the awesome value
- a static method `getPi()` that just returns the value of `Math.PI`

Here's the Fable implementation. Let's start with the members:

```fsharp
type MyClass<'T> =
  // Note we specify this property has also a setter
  abstract value: 'T with get, set
  abstract isAwesome: unit -> bool
```

Now we need to be able to call the static functions, including the constructor. So we write another interface for that:

```fsharp
type MyClassStatic =
  [<Emit("new $0({ value: $1, awesomeness: $2 })")>]
  abstract Create: 'T * 'T -> MyClass<'T>
  abstract getPI : unit-> float
```

:::info
We could have used a class declaration with dummy implementations as we did with DataManager above, but you will find that in Fable bindings it's common to split the instance and static parts of a JS type in two interfaces to overcome some restrictions of the F# type system or to be able to deal with JS classes as values. In this case, by convention `Create` denotes the constructor.
:::

Here we used the `Emit` attribute to apply the JS `new` keyword and to build a JS object with the arguments, because that's what MyClass constructor accepts. Note that here `$0` represents the interface object (in this case, MyClass static).

Last but not least, let's import MyClass:

```fsharp
[<ImportDefault("../public/MyClass.js")>]
let MyClass : MyClassStatic = jsNative
```

Now it's possible to use our JS class. Let's see the complete code:

```fsharp

type MyClass<'T> =
  abstract value: 'T with get, set
  abstract isAwesome: unit -> bool

type MyClassStatic =
  [<Emit("new $0({ value: $1, awesomeness: $2 })")>]
  abstract Create: 'T * 'T -> MyClass<'T>
  abstract getPI : unit-> float

[<ImportDefault("../public/MyClass.js")>]
let MyClass : MyClassStatic = jsNative

let myObject = MyClass.Create(40, 42)

// using getter
let whatDoIget = myObject.value // using getter
mylib.triggerAlert ("Hey I'm calling my js class from Fable. It gives " + (string whatDoIget))

// using setter
myObject.value <- 42
mylib.triggerAlert ("Now it's better. It gives " + (string myObject.value))

// calling member function
mylib.triggerAlert ("Isn't it awesome? " + (string (myObject.isAwesome())))

// call our static function
mylib.triggerAlert ("PI is " + (string (MyClass.getPI())))
```

It's possible to combine the `Import` and `Emit` attributes. So we can import and build MyClass in one go. Note that in this case `$0` is replaced by the imported element:

```fsharp
[<ImportDefault("../public/MyClass.js")>]
[<Emit("new $0({ value: $1, awesomeness: $2 })")>]
let createMyClass(value: 'T, awesomeness: 'T) : MyClass<'T> = jsNative
```

## Other special attributes

`Fable.Core` includes other attributes for JS interop, like:

### Erase attribute

In TypeScript there's a concept of [Union Types](https://www.typescriptlang.org/docs/handbook/advanced-types.html#union-types) which differs from union types in F#. The former are just used to statically check a function argument accepting different types. In Fable, they're translated as **Erased Union Types** whose cases must have one and only one single data field. After compilation, the wrapping will be erased and only the data field will remain. To define an erased union type, just attach the `Erase` attribute to the type. Example:

```fsharp
open Fable.Core

[<Erase>]
type MyErasedType =
    | String of string
    | Number of int

myLib.myMethod(String "test")
```

```js
myLib.myMethod("test")
```

`Fable.Core` already includes predefined erased types which can be used as follows:

```fsharp
open Fable.Core

type Test = abstract Value: string
let myMethod(arg: U3<string, int, Test>): unit = importMember "./myJsLib"

let testValue = { new Test with member __.Value = "Test" }
myMethod(U3.Case3 testValue)
```

When passing arguments to a method accepting `U2`, `U3`... you can use the `!^` as syntax sugar so you don't need to type the exact case (the argument will still be type checked):

```fsharp
open Fable.Core.JsInterop

myMethod !^5 // Equivalent to: myMethod(U3.Case2 5)
myMethod !^testValue

// This doesn't compile, myMethod doesn't accept floats
myMethod !^2.3
```

:::info
Please note erased unions are mainly intended for typing the signature of imported JS functions and not as a cheap replacement of `Choice`. It's possible to do pattern matching against an erased union type but this will be compiled as type testing, and since **type testing is very weak in Fable**, this is only recommended if the generic arguments of the erased union are types that can be easily told apart in the JS runtime (like a string, a number and an array).
:::

```fsharp
let test(arg: U3<string, int, float[]>) =
    match arg with
    | U3.Case1 x -> printfn "A string %s" x
    | U3.Case2 x -> printfn "An int %i" x
    | U3.Case3 xs -> Array.sum xs |> printfn "An array with sum %f"

// In JS roughly translated as:

// function test(arg) {
//   if (typeof arg === "number") {
//     toConsole(printf("An int %i"))(arg);
//   } else if (isArray(arg)) {
//     toConsole(printf("An array with sum %f"))(sum(arg));
//   } else {
//     toConsole(printf("A string %s"))(arg);
//   }
// }
```

### StringEnum

In TypeScript it is possible to define [String Literal Types](https://mariusschulz.com/blog/string-literal-types-in-typescript) which are similar to enumerations with an underlying string value. Fable allows the same feature by using union types and the `StringEnum` attribute. These union types must not have any data fields as they will be compiled to a string matching the name of the union case.

By default, the compiled string will have the first letter lowered. If you want to prevent this or use a different text than the union case name, use the `CompiledName` attribute:

```fsharp
open Fable.Core

[<StringEnum>]
type MyStrings =
    | Vertical
    | [<CompiledName("Horizontal")>] Horizontal

myLib.myMethod(Vertical, Horizontal)
```

```js
// js output
myLib.myMethod("vertical", "Horizontal")
```

## Plain Old JavaScript Objects

To create a plain JS object (aka POJO), use `createObj`:

```fsharp
open Fable.Core.JsInterop

let data =
    createObj [
        "todos" ==> Storage.fetch()
        "editedTodo" ==> Option<Todo>.None
        "visibility" ==> "all"
    ]
```

A similar effect can be achieved with the new F# [anonymous records](https://devblogs.microsoft.com/dotnet/announcing-f-4-6-preview/):

```fsharp
let data =
    {| todos = Storage.fetch()
       editedTodo = Option<Todo>.None
       visibility = "all" |}
```

:::{note}
Since fable-compiler 2.3.6, when using the dynamic cast operator `!!` to cast an anonymous record to an interface, Fable will raise a warning if the fields in the anonymous don't match those of the interface. Use this feature only to interop with JS, in F# code the proper way to instantiate an interface without an implementing type is an [object expression](https://fsharpforfunandprofit.com/posts/object-expressions/).
:::

```fsharp
type IMyInterface =
    abstract foo: string with get, set
    abstract bar: float with get, set
    abstract baz: int option with get, set

// Warning, "foo" must be a string
let x: IMyInterface = !!{| foo = 5; bar = 4.; baz = Some 0 |}

// Warning, "bar" field is missing
let y: IMyInterface = !!{| foo = "5"; bAr = 4.; baz = Some 0 |}

// Ok, "baz" can be missing because it's optional
let z: IMyInterface = !!{| foo = "5"; bar = 4. |}
```

You can also create a JS object from an interface by using `createEmpty` and then assigning the fields manually:

```fsharp
let x = createEmpty<IMyInterface> // var x = {}
x.foo <- "abc"                    // x.foo = "abc"
x.bar <- 8.5                      // val.bar = 8.5
```

A similar solution that can also be optimized by Fable directly into a JS object at compile time is to use the `jsOptions` helper:

```fsharp
let x = jsOptions<IMyInterface>(fun x ->
    x.foo <- "abc"
    x.bar <- 8.5)
```

Another option is to use a list (or any sequence) of union cases in combination with the `keyValueList` helper. This is often used to represent React props. You can specify the case rules for the transformations of the case names (usually lowering the first letter) and if necessary you can also decorate some cases with the `CompiledName` attribute to change its name in the JS runtime.

```fsharp
open Fable.Core.JsInterop

type JsOption =
    | Flag1
    | Name of string
    | [<CompiledName("quantity")>] QTY of int

let inline sendToJs (opts: JsOption list) =
    keyValueList CaseRules.LowerFirst opts |> aNativeJsFunction

sendToJs [
    Flag1
    Name "foo"
    QTY 5
]
// JS: { flag1: true, name: "foo", quantity: 5 }
```

:::{note}
Fable can make the transformation at compile time when applying the list literal directly to `keyValueList`. That's why it's usually a good idea to inline the function containing the helper.
:::

### Dynamic typing: don't read this!

Through the use of the tools we just described above, Fable guarantees you shouldn't run into nasty bugs (as long as the interface contracts are correct) because all the code will be checked by the compiler. If it does not compile it either means your JS library does not exist or its path is not good or that your F# implementation lacks something. We do rely on Fable on systems that are used 24/7, web apps or Node.js apps. We know that if it compiles, it means a 99% chance of running without any problems.

Our motto is: "If it compiles, it works!"

Still, like we stated, **interop is a question of trust**. If you trust your JS code and F# code, then maybe it's ok to do things together without further checks. Maybe.

:::{warning}
Disclaimer: use this at your own risk
:::

#### What is dynamic typing?

Fable.Core.JsInterop implements the F# dynamic operators so you can easily access an object property by name (without static check) as follows:

```fsharp
open Fable.Core.JsInterop

printfn "Value: %O" jsObject?myProperty
// JS: jsObject?myProperty

let pname = "myProperty"

printfn "Value: %O" jsObject?(pname) // Access with a reference
// JS: jsObject[pname]

jsObject?myProperty <- 5 // Assignment is also possible
```

When you combine the dynamic operator with application, Fable will destructure tuple arguments as with normal method calls. These operations can also be chained to replicate JS fluent APIs.

```fsharp
let result = jsObject?myMethod(1, 2)
// JS: jsObject.myMethod(1, 2)

chart
    ?width(768.)
    ?height(480.)
    ?group(speedSumGroup)
    ?on("renderlet", fun chart ->
        chart?selectAll("rect")?on("click", fun sender args ->
            Browser.console.log("click!", args))

// chart
//     .width(768)
//     .height(480)
//     .group(speedSumGroup)
//     .on("renderlet", function (chart) {
//         return chart.selectAll("rect").on("click", function (sender, args) {
//             return console.log("click!", args);
//         });
//      });
```

When you have to call a function with the new keyword in JS, use `createNew`.

```fsharp
open Fable.Core.JsInterop

let instance = createNew jsObject(1, 2)
// JS: new jsObject(1, 2)
```

If you prefer member extensions rather than operators for dynamic typing, you can open `Fable.Core.DynamicExtensions` to have the methods `.Item` and `.Invoke` available on any object.

```fsharp
open Fable.Core.DynamicExtensions

let foo = obj()
let bar1 = foo.["b"]  // Same as foo.Item("b")
foo.["c"] <- 14
let bar2 = foo.Invoke(4, "a")
```

#### Dynamic casting

In some situations, when receiving an untyped object from JS you may want to cast it to a specific type. For this you can use the F# `unbox` function or the `!!` operator in Fable.Core.JsInterop. This will bypass the F# type checker but please note **Fable will not add any runtime check** to verify the cast is correct.
