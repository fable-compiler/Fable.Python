module Safe

open Fable.Python.Builtins
open Flask

let app = Flask(__name__)
let index = app.route("/")

let a = builtins.len

let hello_world () =
    "<p>Hello, World!</p>"
    //flask.render_template()

hello_world |> index |> ignore

[<EntryPoint>]
let main args =
    1