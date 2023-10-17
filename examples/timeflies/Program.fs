// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module Program

open System
open FSharp.Control
open Fable.Python
open Fable.Python.TkInter
open Fable.Python.Queue
open Fable.Python.AsyncIO

type Msg =
    | Place of label: Label * x: int * y: int
    | Empty

let root = Tk()
root.title ("Fable Python Rocks on Tkinter!")
let queue = Queue<Msg>()

let source, mouseMoves: IAsyncObserver<int * int> * IAsyncObservable<int * int> =
    AsyncRx.subject ()

let workerAsync (mb: MailboxProcessor<Event>) =
    let rec messageLoop () =
        async {
            let! event = mb.Receive()
            do! source.OnNextAsync((event.x, event.y))

            return! messageLoop ()
        }

    messageLoop ()

let agent = MailboxProcessor<TkInter.Event>.Start (workerAsync)
let frame = Frame(root, width = 800, height = 600, bg = "white")
frame.bind ("<Motion>", agent.Post) |> ignore
frame.pack ()

let stream =
    "TIME FLIES LIKE AN ARROW"
    |> Seq.mapi (fun i c -> i, Label(frame, text = (string c), fg = "black", bg = "white"))
    |> AsyncRx.ofSeq
    |> AsyncRx.flatMap (fun (i, label) ->
        mouseMoves
        |> AsyncRx.delay (100 * i)
        |> AsyncRx.map (fun (x, y) -> label, x + i * 12 + 15, y))

let sink (ev: Notification<Label * int * int>) =
    async {
        match ev with
        | OnNext (label, x, y) -> label.place (x, y)
        | OnError (err) -> printfn $"Stream Error: {err}"
        | _ -> printfn "Stream Completed!"
    }

let mainAsync =
    async {
        use! disposable = stream.SubscribeAsync(sink)

        while true do
            while root.dooneevent(int Flags.DONT_WAIT) do
                ()

            do! Async.AwaitTask(asyncio.create_task(asyncio.sleep(0.005)))
    }

[<EntryPoint>]
let main argv =
    printfn "Started ..."
    Async.RunSynchronously mainAsync

    0 // return an integer exit code
