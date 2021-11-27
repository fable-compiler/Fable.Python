# What is Fable?

Fable is a compiler that lets you use [F#](https://fsharp.org/) to build applications that run in the Python ecosystem.

```fsharp
type Shape =
    | Square of side: double
    | Rectangle of width: double * length: double

let getArea shape =
    match shape with
    | Square side -> side * side
    | Rectangle (width, length) -> width * length

let square = Square 2.0
printfn $"The area of the square is {getArea square}"
```

## What is F#?

F# (pronounced f-sharp) is a strongly typed Functional programming language which offers many great features to build robust and maintable code such as:

- Lightweight syntax
- Immutability baked into the language by default
- Rich types which let you represent easily your data or your domain
- Powerful pattern matching to define complex behaviors
- And so much more...

F# is already used on the server for web and cloud apps, and it's also used quite a lot for data science and machine learning. It's a great general-purpose language, and also a great fit for building beautiful UIs that run in the browser.

## Why use F# for your next Python project?

F# is a great choice to build beautiful apps that run in the browser. F# is:

* Succinct with lightweight syntax
* Robust with a great type system and pattern matching
* Safe with immutability baked into the language
* Supported by large companies (such as Microsoft and Jetbrains) and comes with commercial tooling support

When compared with Python, F# is safer, more robust, and more pleasant to read and write.

F# is a mature language with functional programming and object programming capabilities, but it doesn't sacrifice readability or simplicity to offer these things. Because of that, we think it's a great choice for your next Python application.
