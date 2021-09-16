open Fake.Core
open Fake.IO

open Helpers

initializeContext()

let buildPath = Path.getFullName "build"
let srcPath = Path.getFullName "stdlib"
let deployPath = Path.getFullName "deploy"
let testsPath = Path.getFullName "test"

// Until Fable (beyond) is released, we need to compile with locally installed Fable branch.
let cliPath = Path.getFullName "../Fable/src/Fable.Cli"

Target.create "Clean" (fun _ ->
    Shell.cleanDir buildPath
    // run dotnet "fable clean --yes" buildPath // Delete *.py files created by Fable
    // run dotnet $"run -c Release -p {cliPath} -- clean --yes --lang Python " buildPath
)

Target.create "Build" (fun _ ->
    Shell.mkdir buildPath
    run dotnet $"run -c Release -p {cliPath} -- --lang Python --exclude Fable.Core --outDir {buildPath}/lib" srcPath
)

Target.create "Run" (fun _ ->
    run dotnet "build" srcPath
)

Target.create "Test" (fun _ ->
    run dotnet "build" testsPath
    [ "native", dotnet "run" testsPath
      "python", dotnet $"run -c Release -p {cliPath} -- --lang Python --exclude Fable.Core --outDir {buildPath}/tests" testsPath
      ]
    |> runParallel
    Shell.Exec("touch", $"{buildPath}/tests/__init__.py") |> ignore
    Shell.Exec("touch", $"{buildPath}/tests/stdlib/__init__.py") |> ignore
    run pytest $"{buildPath}/tests" ""
)

Target.create "Format" (fun _ ->
    run dotnet "fantomas . -r" "src"
)

open Fake.Core.TargetOperators

let dependencies = [
    "Clean"
        ==> "Build"

    "Clean"
        ==> "Run"

    "Build"
        ==> "Test"
]

[<EntryPoint>]
let main args = runOrDefault args