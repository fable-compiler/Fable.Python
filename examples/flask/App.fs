module Safe

open Fable.Python.Builtins
open Flask

let app = Flask.Create(__name__)
let index = app.route("/")

let hello_world () =
    "<p>Hello, World!</p>"
    //flask.render_template()

hello_world |> index |> ignore