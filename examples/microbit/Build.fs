open System.IO
open Fake.Core
open Fake.IO

open Helpers

initializeContext()

let srcPath = Path.getFullName "src"
let appName = "app.py"

Target.create "Clean" (fun _ ->
    run dotnet "fable-py clean --yes" srcPath // Delete *.py files created by Fable
)

Target.create "Build" (fun _ ->
    run dotnet $"fable-py -c Release" srcPath

    // Rewrite imports to flat file system.
    let python = File.ReadAllText($"{srcPath}/{appName}")
    let python = python.Replace("fable_modules.fable_library.", "")
    File.WriteAllText($"{srcPath}/{appName}", python)
)

Target.create "Flash" (fun _ ->
    run flash appName srcPath
)

Target.create "FableLibrary" (fun _ ->
    run ufs $"put util.py" srcPath
)

open Fake.Core.TargetOperators

let dependencies = [
    "Clean"
        ==> "Build"
    "Clean"
        ==> "Build"
        ==> "Flash"
    "Clean"
        ==> "FableLibrary"
]

[<EntryPoint>]
let main args = runOrDefault args