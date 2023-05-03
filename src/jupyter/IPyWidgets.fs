module Fable.Python.IPyWidgets

open Fable.Core


type IWidget =
    abstract close: unit -> unit

type IIntSlider =
    inherit IWidget

    abstract value: int
    abstract min: int
    abstract max: int

type IFloatSlider =
    inherit IWidget

    abstract value: float
    abstract min: float
    abstract max: float

type IExports =
    [<Emit("$0.interact($1, x=$2)")>]
    abstract interact<'T1, 'T2> : fn: ('T1 -> 'T2) * x: 'T1 -> 'T2

    [<Emit("$0.interact($1, x=$2, y=$3)")>]
    abstract interact<'T1, 'T2, 'T3> : fn: ('T1 * 'T2 -> 'T3) * x: 'T1 * y: 'T2 -> 'T3

    abstract IntSlider: unit -> IIntSlider
    abstract IntSlider: value: int -> IIntSlider
    abstract IntSlider: value: int * min: int * max: int * step: int * description: string -> IIntSlider

    abstract FloatSlider: unit -> IFloatSlider
    abstract FloatSlider: value: float -> IFloatSlider
    abstract FloatSlider: value: float * min: int * max: int * step: int * description: string -> IFloatSlider

[<ImportAll("ipywidgets")>]
let widgets: IExports = nativeOnly
