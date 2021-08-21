open Fake.Core
open Fake.IO

open Helpers

initializeContext()

let buildPath = Path.getFullName "src"
let deployPath = Path.getFullName "deploy"
let testsPath = Path.getFullName "test"

Target.create "Clean" (fun _ ->
    Shell.cleanDir deployPath
    // run dotnet "fable clean --yes" buildPath // Delete *.py files created by Fable
    run dotnet "run -c Release -p /Users/dbrattli/Developer/GitHub/Fable/src/Fable.Cli -- clean --yes --lang Python " buildPath
)

Target.create "InstallClient" (fun _ -> run npm "install" ".")

Target.create "Run" (fun _ ->
    run dotnet "build" buildPath
)

Target.create "RunTests" (fun _ ->
    run dotnet "build" testsPath
    [ "native", dotnet "watch run" testsPath
      "python", dotnet "run -c Release -p /Users/dbrattli/Developer/GitHub/Fable/src/Fable.Cli -- --lang Python --exclude Fable.Core" testsPath
      ]
    |> runParallel
)

Target.create "Format" (fun _ ->
    run dotnet "fantomas . -r" "src"
)

open Fake.Core.TargetOperators

let dependencies = [
    "Clean"
        ==> "InstallClient"
        //==> "Bundle"
        //==> "Azure"

    "Clean"
        ==> "InstallClient"
        ==> "Run"

    "InstallClient"
        ==> "RunTests"
]

[<EntryPoint>]
let main args = runOrDefault args