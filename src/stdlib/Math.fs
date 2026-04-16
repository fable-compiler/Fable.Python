/// Type bindings for Python math module: https://docs.python.org/3/library/math.html
module Fable.Python.Math

open System
open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    // ========================================================================
    // Constants
    // ========================================================================

    /// The mathematical constant π = 3.141592…
    /// See https://docs.python.org/3/library/math.html#math.pi
    abstract pi: float
    /// The mathematical constant e = 2.718281…
    /// See https://docs.python.org/3/library/math.html#math.e
    abstract e: float
    /// The mathematical constant τ = 6.283185… (2π)
    /// See https://docs.python.org/3/library/math.html#math.tau
    abstract tau: float
    /// Floating-point positive infinity
    /// See https://docs.python.org/3/library/math.html#math.inf
    abstract inf: float
    /// A floating-point "not a number" (NaN) value
    /// See https://docs.python.org/3/library/math.html#math.nan
    abstract nan: float

    // ========================================================================
    // Number-theoretic and representation functions
    // ========================================================================

    /// Return the ceiling of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.ceil
    abstract ceil: x: int -> int
    /// Return the ceiling of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.ceil
    abstract ceil: x: float -> int
    /// Return the number of ways to choose k items from n items (n choose k)
    /// See https://docs.python.org/3/library/math.html#math.comb
    abstract comb: n: int * k: int -> int
    /// Return a float with the magnitude of x and the sign of y
    /// See https://docs.python.org/3/library/math.html#math.copysign
    abstract copysign: x: float * y: float -> float
    /// Return the absolute value of x
    /// See https://docs.python.org/3/library/math.html#math.fabs
    abstract fabs: x: float -> float
    /// Return the factorial of n as an integer. Raises ValueError if n is negative.
    /// Note: float arguments were deprecated in Python 3.9 and removed in Python 3.12.
    /// See https://docs.python.org/3/library/math.html#math.factorial
    abstract factorial: n: int -> int
    /// Return the floor of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.floor
    abstract floor: x: int -> int
    /// Return the floor of x as an integer
    /// See https://docs.python.org/3/library/math.html#math.floor
    abstract floor: x: float -> int
    /// Return the floating-point remainder of x / y
    /// See https://docs.python.org/3/library/math.html#math.fmod
    abstract fmod: x: float * y: float -> float
    /// Return an accurate floating-point sum of values in the iterable
    /// See https://docs.python.org/3/library/math.html#math.fsum
    abstract fsum: iterable: float seq -> float
    /// Return the greatest common divisor of the integers
    /// See https://docs.python.org/3/library/math.html#math.gcd
    abstract gcd: [<ParamArray>] ints: int[] -> int
    /// Return the integer square root of the non-negative integer n
    /// See https://docs.python.org/3/library/math.html#math.isqrt
    abstract isqrt: n: int -> int
    /// Return x * (2**i) accurately
    /// See https://docs.python.org/3/library/math.html#math.ldexp
    abstract ldexp: x: float * i: nativeint -> float
    /// Return the mantissa and exponent of x as the pair (m, e)
    /// See https://docs.python.org/3/library/math.html#math.frexp
    abstract frexp: x: float -> float * int
    /// Return the fractional and integer parts of x
    /// See https://docs.python.org/3/library/math.html#math.modf
    abstract modf: x: float -> float * float
    /// Return IEEE 754-style remainder of x with respect to y (Python 3.8+)
    /// See https://docs.python.org/3/library/math.html#math.remainder
    abstract remainder: x: float * y: float -> float
    /// Return True if the values a and b are close to each other
    /// See https://docs.python.org/3/library/math.html#math.isclose
    abstract isclose: a: float * b: float -> bool
    /// Return True if the values a and b are close to each other with custom tolerances
    /// See https://docs.python.org/3/library/math.html#math.isclose
    [<NamedParams(fromIndex = 2)>]
    abstract isclose: a: float * b: float * ?rel_tol: float * ?abs_tol: float -> bool
    /// Return the least common multiple of the integers
    /// See https://docs.python.org/3/library/math.html#math.lcm
    abstract lcm: [<ParamArray>] ints: int[] -> int
    /// Return the number of ways to choose k items from n items without repetition and with order
    /// See https://docs.python.org/3/library/math.html#math.perm
    abstract perm: n: int -> int
    /// Return the number of ways to choose k items from n items without repetition and with order
    /// See https://docs.python.org/3/library/math.html#math.perm
    abstract perm: n: int * k: int -> int
    /// Return the product of all elements in the iterable
    /// See https://docs.python.org/3/library/math.html#math.prod
    abstract prod: iterable: 'T seq -> 'T
    /// Truncate x to an integer (towards zero)
    /// See https://docs.python.org/3/library/math.html#math.trunc
    abstract trunc: x: float -> int

    // ========================================================================
    // Floating-point predicates
    // ========================================================================

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
    /// Return the next floating-point value after x towards y (Python 3.9+)
    /// See https://docs.python.org/3/library/math.html#math.nextafter
    abstract nextafter: x: float * y: float -> float
    /// Return the value of the least significant bit of the float x (Python 3.9+)
    /// See https://docs.python.org/3/library/math.html#math.ulp
    abstract ulp: x: float -> float

    // ========================================================================
    // Power and logarithmic functions
    // ========================================================================

    /// Return e raised to the power of x
    /// See https://docs.python.org/3/library/math.html#math.exp
    abstract exp: x: float -> float
    /// Return e**x - 1, computed in a way that is accurate for small x
    /// See https://docs.python.org/3/library/math.html#math.expm1
    abstract expm1: x: float -> float
    /// Return the natural logarithm of x
    /// See https://docs.python.org/3/library/math.html#math.log
    abstract log: x: float -> float
    /// Return the logarithm of x to the given base
    /// See https://docs.python.org/3/library/math.html#math.log
    abstract log: x: float * ``base``: float -> float
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
    abstract pow: x: float * y: float -> float
    /// Return the square root of x
    /// See https://docs.python.org/3/library/math.html#math.sqrt
    abstract sqrt: x: float -> float
    /// Return 2 raised to the power x (Python 3.11+)
    /// See https://docs.python.org/3/library/math.html#math.exp2
    abstract exp2: x: float -> float
    /// Return the cube root of x (Python 3.11+)
    /// See https://docs.python.org/3/library/math.html#math.cbrt
    abstract cbrt: x: float -> float

    // ========================================================================
    // Trigonometric functions
    // ========================================================================

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
    abstract atan2: y: float * x: float -> float
    /// Return the cosine of x radians
    /// See https://docs.python.org/3/library/math.html#math.cos
    abstract cos: x: float -> float
    /// Return the sine of x radians
    /// See https://docs.python.org/3/library/math.html#math.sin
    abstract sin: x: float -> float
    /// Return the tangent of x radians
    /// See https://docs.python.org/3/library/math.html#math.tan
    abstract tan: x: float -> float
    /// Return the Euclidean norm of a sequence of coordinates
    /// See https://docs.python.org/3/library/math.html#math.hypot
    abstract hypot: [<ParamArray>] coordinates: float[] -> float
    /// Convert angle x from radians to degrees
    /// See https://docs.python.org/3/library/math.html#math.degrees
    abstract degrees: x: float -> float
    /// Convert angle x from degrees to radians
    /// See https://docs.python.org/3/library/math.html#math.radians
    abstract radians: x: float -> float

    // ========================================================================
    // Hyperbolic functions
    // ========================================================================

    /// Return the inverse hyperbolic cosine of x
    /// See https://docs.python.org/3/library/math.html#math.acosh
    abstract acosh: x: float -> float
    /// Return the inverse hyperbolic sine of x
    /// See https://docs.python.org/3/library/math.html#math.asinh
    abstract asinh: x: float -> float
    /// Return the inverse hyperbolic tangent of x
    /// See https://docs.python.org/3/library/math.html#math.atanh
    abstract atanh: x: float -> float
    /// Return the hyperbolic cosine of x
    /// See https://docs.python.org/3/library/math.html#math.cosh
    abstract cosh: x: float -> float
    /// Return the hyperbolic sine of x
    /// See https://docs.python.org/3/library/math.html#math.sinh
    abstract sinh: x: float -> float
    /// Return the hyperbolic tangent of x
    /// See https://docs.python.org/3/library/math.html#math.tanh
    abstract tanh: x: float -> float

    // ========================================================================
    // Special functions
    // ========================================================================

    /// Return the error function at x
    /// See https://docs.python.org/3/library/math.html#math.erf
    abstract erf: x: float -> float
    /// Return the complementary error function at x
    /// See https://docs.python.org/3/library/math.html#math.erfc
    abstract erfc: x: float -> float
    /// Return the Gamma function at x
    /// See https://docs.python.org/3/library/math.html#math.gamma
    abstract gamma: x: float -> float
    /// Return the natural logarithm of the absolute value of the Gamma function at x
    /// See https://docs.python.org/3/library/math.html#math.lgamma
    abstract lgamma: x: float -> float

    // ========================================================================
    // Distance and geometry
    // ========================================================================

    /// Return the Euclidean distance between two points p and q
    /// See https://docs.python.org/3/library/math.html#math.dist
    abstract dist: p: float[] * q: float[] -> float

/// Mathematical functions
[<ImportAll("math")>]
let math: IExports = nativeOnly
