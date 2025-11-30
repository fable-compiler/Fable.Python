/// Type bindings for Python math module: https://docs.python.org/3/library/math.html
module Fable.Python.Math

open System
open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Return the ceiling of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.ceil
    abstract ceil: x: int -> int
    /// Return the ceiling of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.ceil
    abstract ceil: x: float -> int
    /// Return the number of ways to choose k items from n items (n choose k)
    /// See https://docs.python.org/3/library/math.html#math.comb
    abstract comb: n: int -> k: int -> int
    /// Return a float with the magnitude of x and the sign of y
    /// See https://docs.python.org/3/library/math.html#math.copysign
    abstract copysign: x: float -> y: int -> float
    /// Return the absolute value of x
    /// See https://docs.python.org/3/library/math.html#math.fabs
    abstract fabs: x: float -> float
    /// Return the factorial of x
    /// See https://docs.python.org/3/library/math.html#math.factorial
    abstract factorial: x: float -> float
    /// Return the floor of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.floor
    abstract floor: x: int -> int
    /// Return the floor of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.floor
    abstract floor: x: float -> int
    /// Return the remainder of x / y
    /// See https://docs.python.org/3/library/math.html#math.fmod
    abstract fmod: x: int -> y: int -> int

    /// Return the greatest common divisor of the integers
    /// See https://docs.python.org/3/library/math.html#math.gcd
    abstract gcd: [<ParamArray>] ints: int[] -> int
    /// Check if x is neither an infinity nor a NaN
    /// See https://docs.python.org/3/library/math.html#math.isfinite
    abstract isfinite: x: float -> bool
    /// Check if x is neither an infinity nor a NaN
    /// See https://docs.python.org/3/library/math.html#math.isfinite
    abstract isfinite: x: int -> bool
    /// Check if x is a positive or negative infinity
    /// See https://docs.python.org/3/library/math.html#math.isinf
    abstract isinf: x: float -> bool
    /// Check if x is a positive or negative infinity
    /// See https://docs.python.org/3/library/math.html#math.isinf
    abstract isinf: x: int -> bool
    /// Check if x is a NaN (not a number)
    /// See https://docs.python.org/3/library/math.html#math.isnan
    abstract isnan: x: float -> bool
    /// Check if x is a NaN (not a number)
    /// See https://docs.python.org/3/library/math.html#math.isnan
    abstract isnan: x: int -> bool
    /// Return the least common multiple of the integers
    /// See https://docs.python.org/3/library/math.html#math.lcm
    abstract lcm: [<ParamArray>] ints: int[] -> int

    /// Return e raised to the power of x
    /// See https://docs.python.org/3/library/math.html#math.exp
    abstract exp: x: float -> float
    /// Return e**x - 1, computed in a way that is accurate for small x
    /// See https://docs.python.org/3/library/math.html#math.expm1
    abstract expm1: x: float -> float
    /// Return the natural logarithm of x
    /// See https://docs.python.org/3/library/math.html#math.log
    abstract log: x: float -> float
    /// Return the natural logarithm of 1+x (base e)
    /// See https://docs.python.org/3/library/math.html#math.log1p
    abstract log1p: x: float -> float
    /// Return the base-2 logarithm of x
    /// See https://docs.python.org/3/library/math.html#math.log2
    abstract log2: x: float -> float
    /// Return the base-10 logarithm of x
    /// See https://docs.python.org/3/library/math.html#math.log10
    abstract log10: x: float -> float
    /// Return x raised to the power y
    /// See https://docs.python.org/3/library/math.html#math.pow
    abstract pow: x: float -> y: float -> float

    /// Return the arc cosine of x in radians
    /// See https://docs.python.org/3/library/math.html#math.acos
    abstract acos: x: float -> float
    /// Return the arc sine of x in radians
    /// See https://docs.python.org/3/library/math.html#math.asin
    abstract asin: x: float -> float
    /// Return the arc tangent of x in radians
    /// See https://docs.python.org/3/library/math.html#math.atan
    abstract atan: x: float -> float
    /// Return the arc tangent of y/x in radians
    /// See https://docs.python.org/3/library/math.html#math.atan2
    abstract atan2: y: float -> x: float -> float
    /// Return the cosine of x radians
    /// See https://docs.python.org/3/library/math.html#math.cos
    abstract cos: x: float -> float
    /// Return the Euclidean distance between two points
    /// See https://docs.python.org/3/library/math.html#math.dist
    abstract dist: p: float -> q: float -> float
    /// Return the sine of x radians
    /// See https://docs.python.org/3/library/math.html#math.sin
    abstract sin: x: float -> float
    /// Return the tangent of x radians
    /// See https://docs.python.org/3/library/math.html#math.tan
    abstract tan: x: float -> float

/// Mathematical functions
[<ImportAll("math")>]
let math: IExports = nativeOnly
