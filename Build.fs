open Fake.Core
open Fake.IO

open Helpers

initializeContext()

let buildPath = Path.getFullName "build"
let srcPath = Path.getFullName "src"
let deployPath = Path.getFullName "deploy"
let testsPath = Path.getFullName "test"

// Until Fable (beyond) is released, we need to compile with locally installed Fable branch.
let cliPath = Path.getFullName "../Fable/src/Fable.Cli"

Target.create "Clean" (fun _ ->
    Shell.cleanDir buildPath
)

Target.create "Build" (fun _ ->
    Shell.mkdir buildPath
    run dotnet $"fable --exclude Fable.Core --lang Python --outDir {buildPath}" srcPath
)

Target.create "Docs" (fun _ ->
    run poetry $"run jb build docs-src --path-output {buildPath}/docs" "."
    Fake.IO.Shell.copyDir "docs" $"{buildPath}/docs/_build/html" (fun _ -> true)
)

Target.create "Run" (fun _ ->
    run dotnet "build" srcPath
)

Target.create "Test" (fun _ ->
    run dotnet "build" testsPath
    [ "native", dotnet "run" testsPath
      "python", dotnet $"fable --lang Python --outDir {buildPath}/tests --exclude Testing" testsPath
      ]
    |> runParallel
    run poetry $"run pytest {buildPath}/tests" ""
)

Target.create "Pack" (fun _ ->
    run dotnet "pack -c Release" srcPath
)

Target.create "Format" (fun _ ->
    run dotnet "fantomas . -r" srcPath
    run dotnet "fantomas . -r" testsPath
)

open Fake.Core.TargetOperators

let dependencies = [
    "Clean"
        ==> "Build"

    "Clean"
        ==> "Run"

    "Build"
        ==> "Test"

    "Build"
        ==> "Docs"

    "Build"
        ==> "Pack"
]

[<EntryPoint>]
let main args = runOrDefault args