// dotnet fsi run_flask_app flask_sample.fsx
#r "nuget: Fli"
//we can use this package to run python commands together with the script
open Fli

// using vscode F# highlight ext can also execute inline python 
let python (p : string)= p

let create_py_venv () =
    cli {
        Shell Shells.BASH
        Command ("python3 -m venv .venv")
    }
    |> Command.execute
    |> Output.printText

let activate_py_venv () =
    cli {
        Shell Shells.BASH
        Command ("source .venv/bin/activate && which python")
    }
    |> Command.execute
    |> Output.printText

let which_python () =
    cli {
        Shell Shells.BASH
        Command ("which python")
    }
    |> Command.execute
    |> Output.printText


/// pip install flask
let pip_install_flask() = 
    cli {
        Shell Shells.BASH
        Command ("python3 -m pip install flask")
    }
    |> Command.execute
    |> Output.printText

/// pip install extra dependencies if needed, specify them as extra arg ',' separated
let pip_install_extras() = 
    let dependencies = 
        match fsi.CommandLineArgs |> Seq.toList with
        |_::_::trd::[] -> trd.Split(",")
        |_ -> [||]

    for dep in dependencies do 
        cli {
            Shell Shells.BASH
            Command ($"python3 -m pip install {dep}")
        }
        |> Command.execute
        |> Output.printText

let fable_compile_flask_app() =

    let flaskScriptName = 
        match fsi.CommandLineArgs |> Seq.toList with
        |_::snd::[] -> snd
        |_ -> "app.fsx"

    cli {
        Shell Shells.BASH
        Command ($"dotnet fable {flaskScriptName} --lang Python --noCache")
    }
    |> Command.execute
    |> Output.printText

let remove_old_app() = 
    cli {
        Shell Shells.BASH
        Command "rm app.py"
    }
    |> Command.execute
    |> Output.printText

let rename_and_cleanup() = 
    cli {
        Shell Shells.BASH
        Command "mv *.py app.py"
    }
    |> Command.execute
    |> Output.printText

let run_flask_app() =
    try
        printfn "starting app to listen on http://127.0.0.1:5000"
        cli {
            Shell Shells.BASH
            Command ("python3 -m flask run")
        }
        |> Command.execute
        |> Output.throwIfErrored 
        |> ignore 
    with ex -> 
        printfn $"script exited with code {ex.Message}"
        ()

let deactivate_py_venv() =
    cli {
        Shell Shells.BASH
        Command ("python3 -m deactivate")
    }
    |> Command.execute
    |> Output.printText


// execution
create_py_venv()
|> activate_py_venv
|> which_python
|> pip_install_flask
|> pip_install_extras
|> fable_compile_flask_app
|> remove_old_app
|> rename_and_cleanup
|> run_flask_app

// need a strategy to not break on CTRL-C for this...
// maybe use `fg` ?
// |> deactivate_py_venv
// |> which_python