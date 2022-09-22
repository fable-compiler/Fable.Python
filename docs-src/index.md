# Python you can be proud of

````{panels}
<img src="/static/img/fsharp.png" /> **Functional programming and more**
^^^
Immutable by default. Powerful pattern matching. Lightweight syntax. Units of measure. Type providers. Enjoy.
---
<span class="icon is-small has-text-black"><i class="fas fa-lock fa-2x"></i></span> **Type safety without the hassle**
^^^
Type inference provides robustness and correctness, but without the cost of additional code. Let the compiler catch bugs for you.
````

<!-- Disable the copy-button on all the elements contained inside the container (all this page) -->
<div class="container mt-5" data-disable-copy-button="true">
    <!-- Class 'is-marginless' is needed otherwise the body placement is mess up -->
    <div class="columns is-marginless is-vcentered">
        <!-- Be careful when updating this div and it's content their is a script strongly dependant on the class names -->
        <div class="column is-offset-2-desktop is-8-desktop is-full-tablet" id="fable-main-header">
            <!-- Fable logo -->
            <figure class="image" style="max-width: 550px; margin: auto">
                <img class="fable-logo" src="/static/img/fable_logo.png" />
            </figure>
            <br />
            <!-- Quick links -->
            <div class="columns">
                <div class="column has-text-centered is-offset-2-tablet is-4-tablet is-offset-3-mobile is-6-mobile">
                    <a class="button is-fullwidth is-success is-outlined is-uppercase" href="https://fable.io/repl">
                        Try online
                    </a>
                </div>
                <div class="column has-text-centered is-4-tablet is-offset-3-mobile is-6-mobile">
                    <a class="button is-fullwidth is-primary is-outlined is-uppercase" href="/docs/2-steps/setup.html">
                        Get started
                    </a>
                </div>
            </div>
            <br />
            <p class="has-text-weight-light is-size-4 has-text-centered">
                Fable is a compiler that brings <a href="http://fsharp.org/">F#</a> into the JavaScript ecosystem
            </p>
        </div>
        <!--
            Hide the twitter feed on touch screens
            It would be better to not load it at all on mobile but for now that's better than nothing
            The not loading part could be handle with the next version of Nacara as it will be
            a dynamic application and not just static website
        -->
        <div class="column is-offset-1-desktop-only is-4-desktop is-3-widescreen is-hidden is-hidden-touch twitter-timeline-container">
            <a class="twitter-timeline" data-lang="en" data-height="520" data-theme="light" href="https://twitter.com/FableCompiler?ref_src=twsrc%5Etfw">Tweets by @FableCompiler</a> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
        </div>
    </div>
    <!--
        Selling points of Fable
        For the selling points of Fable we use CSS grid instead of Bulma columns
        because we want all the box to have the same height.
        This is not something possible to do dynamically using Flexbox / Bulma columns system
    -->
    <div class="section">
        <div class="selling-points">
            <div class="box selling-point">
                <div class="media">
                    <div class="media-left">
                        <figure class="image" style="height: 3em; width: 3em;">
                        <!-- 3em mimics the behavior of the fa-3x for font-awesome icons making the layout more consistent -->
                            <img src="/static/img/fsharp.png" />
                        </figure>
                    </div>
                    <div class="media-content">
                        <span class="title is-5">
                            Functional programming and more
                        </span>
                        <p>
                            Immutable by default. Powerful pattern matching. Lightweight syntax. Units of measure. Type providers. Enjoy.
                        </p>
                    </div>
                </div>
            </div>
            <div class="box selling-point has-background-white">
                <div class="media">
                    <div class="media-left">
                        <span class="icon is-large has-text-black">
                            <i class="fas fa-lock fa-3x"></i>
                        </span>
                    </div>
                    <div class="media-content">
                        <span class="title is-5">
                            Type safety without the hassle
                        </span>
                        <p>
                            Type inference provides robustness and correctness, but without the cost of additional code. Let the compiler catch bugs for you.
                        </p>
                    </div>
                </div>
            </div>
            <div class="box selling-point has-background-white">
                <div class="media">
                    <div class="media-left">
                        <span class="icon is-large has-text-black">
                            <i class="fas fa-wrench fa-3x"></i>
                        </span>
                    </div>
                    <div class="media-content">
                        <span class="title is-5">
                            Modern Javascript output
                        </span>
                        <p>
                            Fable produces readable JavaScript code compatible with ES2015 standards and popular tooling like <a href="https://webpack.js.org/">Webpack</a>.
                        </p>
                    </div>
                </div>
            </div>
            <div class="box selling-point has-background-white">
                <div class="media">
                    <div class="media-left">
                        <span class="icon is-large has-text-black">
                            <i class="fas fa-puzzle-piece fa-3x"></i>
                        </span>
                    </div>
                    <div class="media-content">
                        <span class="title is-5">
                            Easy JavaScript interop
                        </span>
                        <p>
                            Call <a href="/docs/communicate/js-from-fable.html">JavaScript from Fable</a> or <a href="/docs/communicate/fable-from-js.html">Fable from JS</a>. Use NPM packages. The entire JavaScript ecosystem is at your fingertips.
                        </p>
                    </div>
                </div>
            </div>
            <div class="box selling-point has-background-white">
                <div class="media">
                    <div class="media-left">
                        <span class="icon is-large has-text-black">
                            <i class="fas fa-edit fa-3x"></i>
                        </span>
                    </div>
                    <div class="media-content">
                        <span class="title is-5">
                            First-class editor tools
                        </span>
                        <p>
                            Choose your favorite tool: from <a href="https://ionide.io/">Visual Studio Code</a> to <a href="https://www.jetbrains.com/rider/">JetBrains Rider</a>. Check <a href="/docs/2-steps/setup.html#development-tools">the whole list here</a>.
                        </p>
                    </div>
                </div>
            </div>
            <div class="box selling-point has-background-white">
                <div class="media">
                    <div class="media-left">
                        <span class="icon is-large has-text-black">
                            <i class="fas fa-battery-full fa-3x"></i>
                        </span>
                    </div>
                    <div class="media-content">
                        <span class="title is-5">
                            Batteries included
                        </span>
                        <p>
                            Fable supports the <a href="docs/dotnet/compatibility.html">F# core library and some common .NET libraries</a> to supplement the JavaScript ecosystem.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <section class="section">
        <h2 class="title is-2 has-text-primary has-text-centered">
            Features
        </h2>
        <p class="content is-size-5 has-text-centered">
            These are some of the main F# features that you can use in your web apps with Fable.
        </p>
        <div class="columns is-vcentered mt-5">
            <div class="column is-4">
                <h4 class="title has-text-primary">
                    Powerful pattern matching
                </h4>
                <p class="content is-size-5">
                    These are some of the main F# features that you can use in your web apps with Fable.
                </p>
            </div>
            <div class="column is-6 is-offset-1 is-7-tablet">
                <div class="content has-code-block is-normal">

<!-- The indentation/format used has been chosen so the code is displayed
without scrollbar on almost any screen size -->
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
</div> <!-- Markdown is sensible to indentation -->
            </div>
        </div>
        <div class="columns is-vcentered mt-5">
            <div class="column is-4">
                <h4 class="title has-text-primary">
                    Computation expressions
                </h4>
                <p class="content is-size-5">
                    There's a lot of code involving continuations out there, like asynchronous or undeterministic operations. Other languages bake specific solutions into the syntax, with F# you can use built-in computation expressions and also extend them yourself.
                </p>
            </div>
            <div class="column is-6 is-offset-1 is-7-tablet">
                <div class="content has-code-block is-normal">

<!-- The indentation/format used has been chosen so the code is displayed
without scrollbar on almost any screen size -->
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
</div> <!-- Markdown is sensible to indentation -->
            </div>
        </div>
        <div class="columns is-vcentered mt-5">
            <div class="column is-4">
                <h4 class="title has-text-primary">
                    Units of measure
                </h4>
                <p class="content is-size-5">
                    These are some of the main F# features that you can use in your web apps with Fable.
                </p>
            </div>
            <div class="column is-6 is-offset-1 is-7-tablet">
                <div class="content has-code-block is-normal">

<!-- The indentation/format used has been chosen so the code is displayed
without scrollbar on almost any screen size -->
```fsharp
[<Measure>] type m
[<Measure>] type s

let distance = 12.0<m>
let time = 6.0<s>

let thisWillFail = distance + time
// ERROR: The unit of measure 'm' does
// not match the unit of measure 's'

let thisWorks = distance / time
// 2.0<m/s>
```
</div> <!-- Markdown is sensible to indentation -->
            </div>
        </div>
        <div class="columns is-vcentered mt-5">
            <div class="column is-4">
                <h4 class="title has-text-primary">
                    Type providers
                </h4>
                <p class="content is-size-5">
                    Build your types using real-world conditions and make the compiler warn you if those conditions change.
                </p>
            </div>
            <div class="column is-6 is-offset-1 is-7-tablet">
                <div class="content has-code-block is-normal">

<!-- The indentation/format used has been chosen so the code is displayed
without scrollbar on almost any screen size -->
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
