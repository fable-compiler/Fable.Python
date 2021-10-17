module App

open Fable.Python.MicroBit

//display.scroll ("Fable Python!") |> ignore

let mutable time = 0
let mutable start = 0
let mutable running = false

while true do
    if running then
        display.show (Image.HEART)
        sleep (300)
        display.show (Image.HEART_SMALL)
        sleep (300)
    else
        display.show (Image.ASLEEP)

    if button_a.was_pressed () then
        running <- true
        start <- running_time ()

    if button_b.was_pressed () then
        if running then
            time <- time + running_time () - start

        running <- false

    if pin_logo.is_touched () then
        if not running then
            display.scroll (int (time / 1000))
