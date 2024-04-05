// dotnet fsi run_flask_app flask_sample.fsx [--global](to uses pip-run and venv by default)
#r "nuget: Fli"
#r "nuget: EluciusFTW.SpectreCoff"
//we can use this package to run python commands together with the script
open Fli
open SpectreCoff

"Python + F#" |> figlet |> toConsole

// using vscode F# highlight ext can also execute inline python 
let python (p : string)= p

let create_py_venv () =
    cli {
        Shell Shells.BASH
        Command ("python3 -m venv .venv")
    }
    |> Command.execute
    |> Output.printText

let install_pip_run_package() =
    "install pip-run" |> C |> toConsole
    cli {
        Shell Shells.BASH
        Command ("pipx install pip-run")
    }
    |> Command.execute
    |> Output.printText


let isGlobal  = 
    match fsi.CommandLineArgs |> Seq.toList with
    |_::_::"--global"::[] -> true
    |_::"--global"::_ -> true
    |_ -> false

let install_pipreqs_package() =
    "install pipreqs" |> C |> toConsole
    cli {
        Shell Shells.BASH
        Command ("pipx install pipreqs")
    }
    |> Command.execute
    |> Output.printText

// FAILS...
let gen_pip_requirements_file() =
    "execute pipreqs to generate requirements.txt" |> C |> toConsole
    cli {
        Shell Shells.BASH
        Command ("python3 -m pipreqs .")
    }
    |> Command.execute
    |> Output.throwIfErrored
    |> Output.printText

let create_requirements() = 
    cli {
        Shell Shells.BASH
        Command ("touch requirements.txt")
    }
    |> Command.execute
    |> Output.printText

let gen_pip_requirements_file_local() =
    "execute local pipreqs to generate requirements.txt" |> C |> toConsole
    cli {
        Shell Shells.BASH
        Command ("pipreqs .")
    }
    |> Command.execute
    |> Output.throwIfErrored
    |> Output.printText

let install_pip_requirements() =
    "pip install requirements.txt" |> C |> toConsole
    cli {
        Shell Shells.BASH
        Command ("python3 -m pip install -r requirements.txt ")
    }
    |> Command.execute
    |> Output.throwIfErrored
    |> Output.printText

let activate_py_venv () =
    cli {
        Shell Shells.BASH
        Command ("source .venv/bin/activate")
    }
    |> Command.execute
    |> Output.throwIfErrored
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
    "FABLE" |> figlet |> toConsole

    let flaskScriptName = 
        printfn $"args: {fsi.CommandLineArgs}"
        match fsi.CommandLineArgs |> Seq.toList with
        |_::snd::[] -> snd
        |_ -> "app.fsx" 

    cli {
        Shell Shells.BASH
        Command ($"dotnet tool restore && dotnet fable {flaskScriptName} --lang Python --noCache")
    }
    |> Command.execute
    |> Output.throwIfErrored
    |> Output.printText

let remove_old_app() = 
    "rm app.py" |> C |> toConsole
    cli {
        Shell Shells.BASH
        Command "rm app.py"
    }
    |> Command.execute
    |> Output.throwIfErrored
    |> Output.printText

let rename_and_cleanup() = 
    "rename latest script to app.py" |> C |> toConsole
    cli {
        Shell Shells.BASH
        Command "mv *.py app.py"
    }
    |> Command.execute
    |> Output.throwIfErrored
    |> Output.printText

let deactivate_py_venv() =
    cli {
        Shell Shells.BASH
        Command ("python3 -m deactivate")
    }
    |> Command.execute
    |> Output.printText

let run_flask_app() =
    try
        "starting app to listen on http://127.0.0.1:5000" |> P |> toConsole
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

let one_shot_run_flask_app() =
    try
        "starting ONE-SHOT app to listen on http://127.0.0.1:5000" |> P |> toConsole
        cli {
            Shell Shells.BASH
            Command ("pip-run flask -r requirements.txt -- -m flask run")
        }
        |> Command.execute
        |> Output.throwIfErrored 
        |> ignore 
    with ex -> 
        $"script exited with code {ex.Message}" |> P |> toConsole
        ()


let isVenv  = 
    match fsi.CommandLineArgs |> Seq.toList with
    |_::_::"--venv"::[] -> true
    |_::"--venv"::_ -> true
    |_ -> false

// execution

if isVenv then
    create_py_venv()
    |> activate_py_venv
    |> which_python
    printfn "remember to run deactivate at the end of the script, or which python"

//install_pipreqs_package()
//|> if isGlobal then gen_pip_requirements_file else gen_pip_requirements_file_local
()
|> create_requirements
|> if isGlobal then id else install_pip_run_package
|> fable_compile_flask_app
|> remove_old_app
|> rename_and_cleanup
|> if isGlobal then run_flask_app else one_shot_run_flask_app

// need a strategy to not break on CTRL-C for this...
// maybe use `fg` ?
// |> deactivate_py_venv
// |> which_python