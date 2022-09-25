# Python you can be proud of

::::{grid}
:gutter: 3

:::{grid-item-card} <span class="image" style="height: 2em; width: 2em;"><img style="height: 2em; width: 2em;" src="https://fable.io/static/img/fsharp.png" /></span>Functional programming and more
Immutable by default. Powerful pattern matching. Lightweight syntax. Units of measure. Type providers. Enjoy.
:::

:::{grid-item-card} <span class="icon is-small has-text-black"><i class="fas fa-lock fa-2x"></i></span> **Type safety without the hassle**
Type inference provides robustness and correctness, but without the cost of additional code. Let the compiler catch bugs for you.
:::
::::

::::{grid}
:gutter: 3

:::{grid-item-card} <span class="icon is-large has-text-black"><i class="fas fa-wrench fa-2x"></i></span> Modern Python output
Fable produces readable Python code compatible with PEP8 standards and also uses type-hints.
:::

:::{grid-item-card} <span class="icon is-large has-text-black"><i class="fas fa-puzzle-piece fa-2x"></i></span> Easy JavaScript interop
Call Python from Fable or Fable from Python. Use PyPI packages. The entire Python ecosystem is at your fingertips.
:::
::::

::::{grid}
:gutter: 3

:::{grid-item-card} <span class="icon is-large has-text-black"><i class="fas fa-edit fa-2x"></i></span> First-class editor tools
Choose your favorite tool: from <a href="https://ionide.io/">Visual Studio Code</a> to <a href="https://www.jetbrains.com/rider/">JetBrains Rider</a>. 
Check <a href="/docs/2-steps/setup.html#development-tools">the whole list here</a>.
:::

:::{grid-item-card} <span class="icon is-large has-text-black"><i class="fas fa-battery-full fa-2x"></i></span> Batteries included
Fable supports the <a href="docs/dotnet/compatibility.html">F# core library and some common .NET libraries</a> to supplement the JavaScript ecosystem.
:::
::::

```{seealso}
[Try online](https://fable.io/repl")
```

## Features

These are some of the main F# features that you can use in your web apps with Fable.

```fsharp
type Face =
    | Ace | King | Queen | Jack
    | Number of int
type Color =
    | Spades | Hearts | Diamonds | Clubs
type Card =
    | Face * Color

let aceOfHearts = Ace,Hearts
let tenOfSpades = (Number 10), Spades

match card with
| Ace, Hearts ->
    printfn "Ace Of Hearts!"
| _, Hearts ->
    printfn "A lovely heart"
| (Number 10), Spades ->
    printfn "10 of Spades"
| _, (Diamonds|Clubs) ->
    printfn "Diamonds or clubs"
// Warning:
// Incomplete pattern matches on this expression.
// For example, the value '(_,Spades)' may indicate
// a case not covered by the pattern(s).
```

## Computational expressions

There's a lot of code involving continuations out there, like asynchronous or
undeterministic operations. Other languages bake specific solutions into the syntax,
with F# you can use built-in computation expressions and also extend them yourself.

```fsharp
// Python async made easy
task {
    let! res = Fetch.fetch url []
    let! txt = res.text()
    return txt.Length
}

// Declare your own computation expression
type OptionBuilder() =
    member __.Bind(opt, binder) =
        match opt with
        | Some value -> binder value
        | None -> None
    member __.Return(value) =
        Some value

let option = OptionBuilder()

option {
    let! x = trySomething()
    let! y = trySomethingElse()
    let! z = andYetTrySomethingElse()
    // Code will only hit this point if the three
    // operations above return Some
    return x + y + z
}
```

## Units of measure

These are some of the main F# features that you can use in your web apps with Fable.

```fsharp
[<Measure>] type m
[<Measure>] type s

let distance = 12.0<m>
let time = 6.0<s>

let thisWillFail = distance + time
// ERROR: The unit of measure 'm' does
// not match the unit of measure 's'

let thisWorks = distance / time
```

## Type providers

Build your types using real-world conditions and make the compiler warn you if those conditions change.

```fsharp
[<Literal>]
let JSON_URL = "https://jsonplaceholder.typicode.com/todos"

// Type is created automatically from the url
type Todos = Fable.JsonProvider.Generator<JSON_URL>

async {
    let! (_, res) = Fable.SimpleHttp.Http.get url
    let todos = Todos.ParseArray res
    for todo in todos do
        // Compilation fail if the JSON schema changes
        printfn "ID %i, USER: %i, TITLE %s, COMPLETED %b"
            todo.id
            todo.userId
            todo.title
            todo.completed
}
```
