/// Micro:Bit stubs for Fable Python
module Fable.Python.MicroBit

open Fable.Core


[<Import("Image", "microbit")>]
type Image (image: string) =
    /// Gets the number of columns in an image
    member _.width() : int = nativeOnly

    /// Gets the number of rows in an image
    member _.height() : int = nativeOnly

    /// Sets the brightness of a pixel at the given position Cannot be used on inbuilt images.
    member _.set_pixel(x: int, y: int, value: int) : unit = nativeOnly
    member _.get_pixel(x: int, y: int) : int = nativeOnly

    member _.shift_left(n: int) : unit = nativeOnly
    member _.shift_right(n: int) : unit = nativeOnly
    member _.shift_up(n: int) : unit = nativeOnly
    member _.shift_down(n: int) : unit = nativeOnly

    /// Return an exact copy of the image.
    member _.copy() : Image = nativeOnly
    /// Return a new image by inverting the brightness of the pixels in the source image.
    member _.invert() : Image = nativeOnly

    /// Return a new image by inverting the brightness of the pixels in the
    /// source image. Cannot be used on inbuilt images.
    member _.fill(value: int) : Image = nativeOnly

    static member HEART: Image = nativeOnly
    static member HEART_SMALL: Image = nativeOnly
    static member HAPPY: Image = nativeOnly
    static member SMILE: Image = nativeOnly
    static member SAD: Image = nativeOnly
    static member CONFUSED: Image = nativeOnly
    static member ANGRY: Image = nativeOnly
    static member ASLEEP: Image = nativeOnly
    static member SURPRISED: Image = nativeOnly
    static member SILLY: Image = nativeOnly
    static member FABULOUS: Image = nativeOnly
    static member MEH: Image = nativeOnly

    static member YES: Image = nativeOnly
    static member NO: Image = nativeOnly

    static member ALL_CLOCKS: Image array = nativeOnly
    static member ALL_ARROWS: Image array = nativeOnly

type IDisplay =
    /// Clear the display.
    abstract clear : unit -> unit

    /// Shows the image on the display
    abstract show : image: Image array * delay: int -> unit
    abstract show : image: Image -> unit
    abstract show : letter: char -> unit
    abstract show : number: int -> unit

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
    abstract scroll : value: int -> unit
    abstract scroll : value: float -> unit

    /// Turn on the display.
    abstract on : unit -> unit

    /// Turn off the display.
    abstract off : unit -> unit

    /// Returns true if the display is on.
    abstract is_on : unit -> bool

[<Import("display", "microbit")>]
let display: IDisplay = nativeOnly

type IButton =
    /// Returns true if the button is being pressed.
    abstract is_pressed : unit -> bool

    /// Returns true if the button has been pressed since this was last called.
    abstract was_pressed : unit -> bool

    /// Returns the number of times the button has been pressed since this
    /// method was last called, then resets the count.
    abstract get_presses : unit -> int

[<Import("button", "microbit")>]
let button: IButton = nativeOnly

type ICompass =
    /// Starts the calibration process. An instructive message will be scrolled
    /// to the user after which they will need to rotate the device in order to
    /// draw a circle on the LED display.
    abstract calibrate : unit -> unit
    /// Gives the compass heading, calculated from the above readings, as an
    /// integer in the range from 0 to 360, representing the angle in degrees,
    /// clockwise, with north as 0.
    abstract heading : unit -> int

[<Import("compass", "microbit")>]
let compass: ICompass = nativeOnly

type IAccelerometer =
    /// Get the acceleration measurement in the x axis, as a positive or
    /// negative integer, depending on the direction. The measurement is given
    /// in milli-g. By default the accelerometer is configured with a range of
    /// +/- 2g, and so this method will return within the range of +/- 2000mg.
    abstract get_x : unit -> int
    /// Get the acceleration measurement in the y axis, as a positive or
    /// negative integer, depending on the direction. The measurement is given
    /// in milli-g. By default the accelerometer is configured with a range of
    /// +/- 2g, and so this method will return within the range of +/- 2000mg.
    abstract get_y : unit -> int
    /// Get the acceleration measurement in the z axis, as a positive or
    /// negative integer, depending on the direction. The measurement is given
    /// in milli-g. By default the accelerometer is configured with a range of
    /// +/- 2g, and so this method will return within the range of +/- 2000mg.
    abstract get_z : unit -> int

type IPinLogo =
    abstract is_touched : unit -> bool

[<Import("accelerometer", "microbit")>]
let accelerometer: IAccelerometer = nativeOnly

/// Wait for n milliseconds. One second is 1000 milliseconds".
[<Import("sleep", "microbit")>]
let sleep (milliseconds: int) = nativeOnly


/// Return the temperature of the micro:bit in degrees Celcius.
[<Import("temperature", "microbit")>]
let temperature () : float = nativeOnly


/// Return the number of milliseconds since the board was switched on or restarted."""
[<Import("running_time", "microbit")>]
let running_time () : int = nativeOnly

[<Import("button_a", "microbit")>]
let button_a: IButton = nativeOnly

[<Import("button_b", "microbit")>]
let button_b: IButton = nativeOnly

[<Import("pin_logo", "microbit")>]
let pin_logo: IPinLogo = nativeOnly
