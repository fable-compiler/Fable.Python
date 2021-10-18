module Fable.Python.Math

open System
open Fable.Core

// fsharplint:disable MemberNames

type IExports =
    abstract ceil : int -> int
    abstract ceil : float -> int
    abstract comb : int -> int -> int
    abstract copysign : float -> int -> float
    abstract fabs : float -> float
    abstract factorial : float -> float
    abstract floor : int -> int
    abstract floor : float -> int
    abstract fmod : int -> int -> int

    abstract gcd : [<ParamArray>] ints: int[] -> int
    abstract isfinite : float -> bool
    abstract isfinite : int -> bool
    abstract isinf : float -> bool
    abstract isinf : int -> bool
    abstract isnan : float -> bool
    abstract isnan : int -> bool
    abstract lcm : [<ParamArray>] ints: int[] -> int
    
    abstract exp : float -> float
    abstract expm1 : float -> float
    abstract log : float -> float
    abstract log1p : float -> float
    abstract log2 : float -> float
    abstract log10 : float -> float
    abstract pow : float -> float -> float

    abstract acos : float -> float
    abstract asin : float -> float
    abstract atan : float -> float
    abstract atan2 : float -> float -> float
    abstract cos : float -> float
    abstract dist : float -> float -> float
    abstract sin : float -> float
    abstract tan : float -> float

[<ImportAll("math")>]
let math: IExports = nativeOnly