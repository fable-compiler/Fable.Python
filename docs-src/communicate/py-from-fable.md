# Call Python from Fable

Interoperability is a matter of trust between your statically typed F# code and your untyped dynamic JS code. In order
to mitigate risks, Fable gives you several possibilities, amongst them type safety through interface contracts.
Sometimes it may sound more convenient to just call JS code in a more dynamic fashion, although be aware by doing this
potential bugs will not be discovered until runtime.

We'll describe both the safe way and the dynamic way and then it will be up to you to decide. Let's start!

## Adding the Python library to the project

The very first thing to do is add the library to our project. Since we always have a `pyproject.toml` file, we'll just
add the wanted library to our project using either `poetry add python-library`. The library will then be
available in the `.venv` folder.

> If your library is in a file, just skip this step.

## Type safety with Imports and Interfaces

To use code from Python libraries first you need to import it into F#.

There are two ways to declare Python imports in Fable: by using either the **Import attribute** or the **import
expressions**. The `ImportAttribute` can decorate members, types or modules and works as follows:

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
open Fable.Core.PyInterop

// Same as Import("*", "my-module")
[<ImportAll("my-module")>]
let myModule: obj = nativeOnly

// Same as Import("default", "my-module")
[<ImportDefault("my-module")>]
let myModuleDefaultExport: obj = nativeOnly

// The member name is taken from decorated value, here `myFunction`
[<ImportMember("my-module")>]
let myFunction(x: int): int = nativeOnly
```

If the value is globally accessible in JS, you can use the `Global` attribute with an optional name parameter instead.

```fsharp
let [<Global>] console: JS.Console = nativeOnly

// You can pass a string argument if you want to use a different name in your F# code
let [<Global("console")>] logger: JS.Console = nativeOnly
```

### Importing relative paths when using an output directory

If you are putting the generated Python files in an output directory using Fable's `-o` option, be aware that Fable will only move files corresponding to .fs sources, not external files like .py. Fable will automatically adjust relative paths in imports to the location of the generated file, but sometimes it's difficult to know where the generated file will end up exactly. In these cases, it's useful to use the `${outDir}` macro which will be replaced by the output directory. For example, if we are importing a Python module like this:

```fsharp
[<ImportDefault("${outDir}/../styles/styles.module.css")>]
let styles: CssModule = nativeOnly
```

Let's say we're compiling with the `-o build` option and the file ends up in the `build/Components` directory. The generated code will look like:

```js
import styles from "../../styles/styles.module.css";
```

### OOP Class definition and inheritance

Assuming we need to import a Python class, we can represent it in F# using a standard class declaration with an `Import`
attribute. In this case we use `nativeOnly` as a dummy implementation for its members as the actual implementation will
come from Python. When using `nativeOnly` don't forget to add the return type to the member!

```fsharp
[<Import("DataManager", from="library.data")>]
type DataManager<'Model> (conf: Config) =
    member _.delete(data: 'Model): Task<'Model> = nativeOnly
    member _.insert(data: 'Model): Task<'Model> = nativeOnly
    member _.update(data: 'Model): Task<'Model> = nativeOnly
```

From this point it is possible to use it or even to inherit from it as it is usually done on regular F#, [see the official F# documentation](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/inheritance). If you want to inherit and override a member, F# requires you to declare the member as `abstract` in the base class, with a default implementation if you want to make it also directly usable from the base class. For example:

```fsharp
// This class lives in Python
[<Import("DataManager", from="library.data")>]
type DataManager<'T> (conf: Config) =
    abstract update: data: 'T -> Promise<'T>
    default _.update(data: 'T): Promise<'T> = nativeOnly

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
If you have a Python type declaration for the Python code you want to import, you can use 
[ts2fable](https://github.com/fable-compiler/ts2fable) to help you write the F# bindings.
:::

### Let's practice! 1st try!

Let's say we have an `alert.py` file that we'd like to use in our Fable project.

```js
def trigger_alert(message):
  print(message)


some_string = "And I Like that!"

__all__ = ["trigger_alert", "some_string"]
```

As you can see we've got one function, `trigger_alert` and one constant `some_string`. Both are exported using the `__all__` keyword.

In order to use this in our Fable code, let's create an `interface` that will mimic this:

```fsharp
open Fable.Core.PyInterop // needed to call interop tools

[<Erase>]
type IAlert =
    abstract triggerAlert : message:string -> unit
    abstract someString: string
```

As you can see the process is quite easy. The `I` in `IAlert` is not mandatory but it's a precious hint that we're going
to use an interface. The `abstract` keyword only indicates that there's no actual implementation in F#. That's true,
since we rely on the Python one. The `Erase` attribute is used to remove the interface from the generated code. This is
different from Fable (JavaScript) where interfaces are not present in the generated code.

Now let's use this:

```fsharp
[<ImportAll("alert")>]
let mylib: IAlert = nativeOnly
```

Here we use the `ImportAll` attribute, to import the module.

- step 1: We specify the module we wish to use. Here `alert` means: "import the `alert.py` file".
- step 2: we create a let binding called `mylib` to map the js library.
- step 3: we use the `nativeOnly` keyword to say that `mylib` is just a placeholder for the JavaScript native implementation.

Now we can use this:

```fsharp
mylib.triggerAlert ("Hey I'm calling my js library from Fable > " + mylib.someString)
```

If everything's working correctly, it should see the output in your console!

### Let's practice! 2nd try!

Assuming we've got two exported functions from a `canvas.py` file: `draw_smiley` and `draw_bubble`:

```py
def draw_smiley(id):
    # do something

def draw_bubble(id):
    # do something
```

we could use the same method we used with `alert.py`:

```fsharp
open Fable.Core.JsInterop // needed to call interop tools

type ICanvas =
    abstract drawSmiley: (id:string) -> unit
    abstract drawBubble: (id:string) -> unit

[<ImportAll("canvas")>]
let mylib: ICanvas = nativeOnly

mylib.drawSmiley "smiley" // etc..
```

or we could use the `importMember` helper function to directly map the Python function to the F# function.

```fsharp
open Fable.Core.JsInterop // needed to call interop tools

module Canvas =
  // here we just import a member function from canvas.js called drawSmiley.
  let drawSmiley(id:string): unit = importMember "canvas"
  let drawBubble(id:string): unit = importMember "canvas"

Canvas.drawSmiley "smiley"
```

The result would be the same, but the philosophy is slightly different. That's basically up to you to make a choice 😉

### Miscellaneous import helpers

There are other interop helpers you can use thanks to `Fable.Core.PyInterop`:

```fsharp
open Fable.Core.PyInterop

let buttons: obj = importAll "my_lib.buttons"
// Py: import my_lib.buttons as my_lib

let myString: string = importMember "my_lib"
// Py: from my_lib import my_string

// Use just `import` to make the member name explicit
// as with the ImportAttribute
let aDifferentName: string = import "myString" "my-lib"
// Py: from my_lib import my_string
```

Occasionally, you may need to import Python purely for its side-effects. 

For this, use `importSideEffects`.

```fsharp
open Fable.Core.PyInterop

importSideEffects("mylibrary")
```

## Emit, when F# is not enough

You can use the `Emit` attribute to decorate a function. Every call to the function will then be replaced inline by the content of the attribute with the placeholders `$0, $1, $2...` replaced by the arguments. For example, the following code will generate Python as seen below.

```fsharp
open Fable.Core

[<Emit("$0 + $1")>]
let add (x: int) (y: string): string = nativeOnly

let result = add 1 "2"
```

When you don't know the exact number of arguments you can use the following syntax:

```fsharp
type Test() =
    [<Emit("$0($1...)")>]
    member __.Invoke([<ParamArray>] args: int[]): obj = nativeOnly
```

It's also possible to pass syntax conditioned to optional parameters:

```fsharp
type Test() =
    [<Emit("$0[$1]{{=$2}}")>]
    member __.Item with get(): float = nativeOnly and set(v: float): unit = nativeOnly

    // This syntax means: if second arg evals to true in JS print 'i' and nothing otherwise
    [<Emit("new RegExp($0,'g{{$1?i:}}')")>]
    member __.ParseRegex(pattern: string, ?ignoreCase: bool): Regex = nativeOnly
```

The content of `Emit` will not be checked by the F# compiler so it's not advised to abuse this method.

### Let's do it! Use Emit

Now let's work with Emit and look at a new example with the following `my_class.py`:

```py
import math 

class MyClass:
    # Note the constructor accepts an object
    # with the `value` and `awesomeness` fields
    def __init__(self, value, awesomeness):
        self._value = value
        self._awesomeness = awesomeness
  
    @property
    def value()
        return this._value
  

    @value.setter
    def set_value(self, new_alue)
        self._value = new_value
  

    def is_awesome(self):
      return this._value == this._awesomeness
  

    @staticmethod
    def getPI():
       return math.pi
  

```

Let's list its members:

- a `value` property member which returns the current value with a getter and a setter
- a method, `is_awesome`, that checks if the current value equals the awesome value
- a static method `getPi()` that just returns the value of `math.pi`

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
    [<Emit("$0(value=$1, awesomeness=$2)")>]
    abstract Create: 'T * 'T -> MyClass<'T>
    abstract getPI : unit-> float
```

:::{note}
We could have used a class declaration with dummy implementations as we did with DataManager above, but you will find that in Fable bindings it's common to split the instance and static parts of a Python type in two interfaces to overcome some restrictions of the F# type system or to be able to deal with Python classes as values. In this case, by convention `Create` denotes the constructor.
:::

Here we used the `Emit` attribute to build a Python object with the arguments, because that's what MyClass constructor accepts. Note that here `$0` represents the interface object (in this case, MyClass static).

Last but not least, let's import MyClass:

```fsharp
[<Import("MyClass", "public")>]
let MyClass : MyClassStatic = nativeOnly
```

Now it's possible to use our Python class. Let's see the complete code:

```fsharp
type MyClass<'T> =
    abstract value: 'T with get, set
    abstract isAwesome: unit -> bool

type MyClassStatic =
    [<Emit("$0(value=$1, awesomeness=$2)")>]
    abstract Create: 'T * 'T -> MyClass<'T>
    abstract getPI : unit-> float

[<Import("MyClass", "public")>]
let MyClass : MyClassStatic = nativeOnly

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

It's possible to combine the `Import` and `Emit` attributes. So we can import and build MyClass in one go. Note that in
this case `$0` is replaced by the imported element:

```fsharp
[<Import("MyClass", "public")>]
[<Emit("$0(value=$1, awesomeness=$2)")>]
let createMyClass(value: 'T, awesomeness: 'T) : MyClass<'T> = nativeOnly
```

## Other special attributes

`Fable.Core` includes other attributes for Python interop, like:

### Erase attribute

In TypeScript there's a concept of [Union
Types](https://www.typescriptlang.org/docs/handbook/advanced-types.html#union-types) which differs from union types in
F#. The former are just used to statically check a function argument accepting different types. In Fable, they're
translated as **Erased Union Types** whose cases must have one and only one single data field. After compilation, the
wrapping will be erased and only the data field will remain. To define an erased union type, just attach the `Erase`
attribute to the type. Example:

```fsharp
open Fable.Core

[<Erase>]
type MyErasedType =
    | String of string
    | Number of int

myLib.myMethod(String "test")
```

```js
myLib.myMethod("test");
```

`Fable.Core` already includes predefined erased types which can be used as follows:

```fsharp
open Fable.Core

type Test = abstract Value: string
let myMethod(arg: U3<string, int, Test>): unit = importMember "my_py_lib"

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

:::{note}
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
myLib.myMethod("vertical", "Horizontal");
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
open Fable.Core.PyInterop

type PyOption =
    | Flag1
    | Name of string
    | [<CompiledName("quantity")>] QTY of int

let inline sendToPy (opts: PyOption list) =
    keyValueList CaseRules.LowerFirst opts |> aNativePyFunction

sendToPy [
    Flag1
    Name "foo"
    QTY 5
]
// Py: {'flag1': True, 'name': 'foo'}
```

:::{note}
Fable can make the transformation at compile time when applying the list literal directly to `keyValueList`. That's why 
it's usually a good idea to inline the function containing the helper.
:::

### Dynamic typing: don't read this!

Through the use of the tools we just described above, Fable guarantees you shouldn't run into nasty bugs (as long as the interface contracts are correct) because all the code will be checked by the compiler. If it does not compile it either means your Python library does not exist or its path is not good or that your F# implementation lacks something. We do rely on Fable on systems that are used 24/7. We know that if it compiles, it means a 99% chance of running without any problems.

Our motto is: "If it compiles, it works!"

Still, like we stated, **interop is a question of trust**. If you trust your Python code and F# code, then maybe it's ok to do things together without further checks. Maybe.

:::{warning}
Disclaimer: use this at your own risk
:::

#### What is dynamic typing?

Fable.Core.PyInterop implements the F# dynamic operators so you can easily access an object property by name (without static check) as follows:

```fsharp
open Fable.Core.PyInterop

printfn "Value: %O" pyObject?myProperty
// Py: py_object.myProperty

let pname = "myProperty"

printfn "Value: %O" jsObject?(pname) // Access with a reference
// Py: py_object[pname]

pyObject?myProperty <- 5 // Assignment is also possible
```

When you combine the dynamic operator with application, Fable will destructure tuple arguments as with normal method calls. These operations can also be chained to replicate Python fluent APIs.

```fsharp
let result = pyObject?myMethod(1, 2)
// Py: py_object.my_method(1, 2)

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

If you prefer member extensions rather than operators for dynamic typing, you can open `Fable.Core.DynamicExtensions` to
have the methods `.Item` and `.Invoke` available on any object.

```fsharp
open Fable.Core.DynamicExtensions

let foo = obj()
let bar1 = foo.["b"]  // Same as foo.Item("b")
foo.["c"] <- 14
let bar2 = foo.Invoke(4, "a")
```

#### Dynamic casting

In some situations, when receiving an untyped object from Python you may want to cast it to a specific type. For this
you can use the F# `unbox` function or the `!!` operator in Fable.Core.PyInterop. This will bypass the F# type checker
but please note **Fable will not add any runtime check** to verify the cast is correct.
