open Fake.Core
open Fake.IO

open Helpers

initializeContext()

let srcPath = Path.getFullName "src"

Target.create "Clean" (fun _ ->
    run dotnet "fable-py clean --yes" srcPath // Delete *.py files created by Fable
)

Target.create "Build" (fun _ ->
    run dotnet $"fable-py -c Release" srcPath
)

Target.create "Flash" (fun _ ->
    run flash "" $"{srcPath}/app.py"
)

open Fake.Core.TargetOperators

let dependencies = [
    "Clean"
        ==> "Build"

    "Clean"
        ==> "Flash"
]

[<EntryPoint>]
let main args = runOrDefault args