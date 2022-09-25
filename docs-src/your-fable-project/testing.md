# Testing

You can use all tools of the Python ecosystem.

Several js libs already have a Fable binding:
- mocha: [https://github.com/Zaid-Ajaj/Fable.Mocha](https://github.com/Zaid-Ajaj/Fable.Mocha)
- jest: [https://github.com/Shmew/Fable.Jester](https://github.com/Shmew/Fable.Jester)

# Example with jest
## Setup
You should install js test runner :
```sh
  npm install jest --save-dev
```
And Fable binding :
```sh
  # nuget
  dotnet add package Fable.Jester
  # paket
  paket add Fable.Jester --project ./project/path
```

## Write tests
Now, you can write your first test :
```fsharp
open Fable.Jester

Jest.describe("can run basic tests", fun () ->
    Jest.test("running a test", fun () ->
        Jest.expect(1+1).toEqual(2)
    )
)
```
See Jester documentation to more informations : [https://shmew.github.io/Fable.Jester/](https://shmew.github.io/Fable.Jester/)

## Run
Before running the tests, you have to convert your project to JS, but you don't need to bundle with Webpack, because test runners generally prefer to have small files rather than a single big file. So we only need to run the Fable compiler and put the generated code in an output dir.

```sh
  dotnet fable src -o output
```

You should config Jest with a config file `jest.config.js` :
```js
module.exports = {
  moduleFileExtensions: ['js'],
  roots: ['./output'],
  testMatch: ['<rootDir>/**/*.Test.js'],
  coveragePathIgnorePatterns: ['/\.fable/', '/[fF]able.*/', '/node_modules/'],
  testEnvironment: 'node',
  transform: {}
};
```
`roots` should be equal to the `outDir` of the compiler.
`testMatch` indicate file pattern name with test.
`coveragePathIgnorePatterns`, `testEnvironment`, `transform` improve performance of runner.
You can read Jest doc to see more : [https://jestjs.io/docs/en/configuration](https://jestjs.io/docs/en/configuration)

Now, you can run then tests:
```sh
  npx jest --config=jest.config.js
```

Youhou! You can see the test result :)

You can specify this command on npm in `package.json` :
```json
{
  "scripts": {
    "test": "dotnet fable src -o output --run jest --config=jest.config.js",
  },
}
```
And now run with a single command:
```sh
  npm test
```

## Watch mode
Running tests each time is slow.
You can use the watch feature to take advantage of the compiler and runner cache, and run tests whenever a file changes.

Currently, Fable doesn't have official plugins for the different runners.
So you have to execute these two commands in parallel:
```sh
  dotnet fable watch src -o output
  npx jest --config=jest.config.js --watchAll
```

You add an npm script in `package.json` :
```json
{
  "scripts": {
    "test": "dotnet fable src -o output && jest --config=jest.config.js",
    "watch-test:build": "dotnet fable watch src -o output",
    "watch-test:run": "jest --config=jest.config.js --watchAll",
    "watch-test": "npm-run-all --parallel watch-test:*"
  },
}
```
I use `npm-run-all` to run several commands in parallel. You should install with:
```sh
  npm install --save-dev npm-run-all
```

Now, run
```sh
  npm run-script watch-test
```

Enjoy :)
