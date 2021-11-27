# Are you a Python developer?

## Welcome to Fable!

Hi!

We're happy you decided to try Fable. Since F# is a language originally created for the .NET environment, Fable uses some tools that come from there.

Don't panic! There is enough documentation to explain how the .NET tools integrate in your environment. And there are basically only two things you need to know:

1. Fable uses F# project files (`.fsproj`) to list your F# code files and libraries.
2. Fable uses NuGet to load F# libraries, which is the equivalent of NPM for the .NET environment

Voilà. Nothing else. We'll come to explanations later in the docs. But we promise, there's nothing you won't understand right away. Apart from these facts, it's all JavaScript!

**Welcome home!**

- Fable transpiles F# to ES2015 JavaScript, Fable uses the great [Babel](https://babeljs.io/) tool for that.
- Fable integrates with the popular [Webpack](https://webpack.js.org/). And it's not hard to use another bundler.
- JS Dependencies are listed in your common `package.json` file.
- Unit testing is available through [Fable.Mocha](https://github.com/Zaid-Ajaj/Fable.Mocha) (but you can use another test runner if you wish).
- In most cases, building and running a Fable project only requires to call `npm install` and `npm start`.

So since we're mainly using JavaScript tools, you won't be lost with Fable!
