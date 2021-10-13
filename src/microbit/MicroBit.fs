module Fable.Python.MicroBit

open Fable.Core

/// Micro:Bit stubs for Fable Python
type IDisplay =
    /// Clear the display.
    abstract clear : unit -> unit

    /// Scrolls value horizontally on the display. If value is an integer or
    /// float it is first converted to a string using str(). The delay
    /// parameter controls how fast the text is scrolling. If wait is True,
    /// this function will block until the animation is finished, otherwise the
    /// animation will happen in the background. If loop is True, the animation
    /// will repeat forever. If monospace is True, the characters will all take
    /// up 5 pixel-columns in width, otherwise there will be exactly 1 blank
    /// pixel-column between each character as they scroll. Note that the wait,
    /// loop and monospace arguments must be specified using their keyword.
    abstract scroll : value: string -> unit
    abstract scroll : value: string * delay: int -> unit

    /// Turn on the display.
    abstract on : unit -> unit

    /// Turn off the display.
    abstract off : unit -> unit

    /// Returns true if the display is on.
    abstract is_on : unit -> bool

[<Import("display", "microbit")>]
let display: IDisplay = nativeOnly