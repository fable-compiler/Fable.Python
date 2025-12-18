/// Type bindings for Python random module: https://docs.python.org/3/library/random.html
module Fable.Python.Random

open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Initialize the random number generator
    /// See https://docs.python.org/3/library/random.html#random.seed
    [<Emit("$0.seed(int($1))")>]
    abstract seed: a: int -> unit

    /// Initialize the random number generator
    /// See https://docs.python.org/3/library/random.html#random.seed
    abstract seed: a: nativeint -> unit
    /// Initialize the random number generator
    /// See https://docs.python.org/3/library/random.html#random.seed
    abstract seed: a: float -> unit
    /// Initialize the random number generator
    /// See https://docs.python.org/3/library/random.html#random.seed
    abstract seed: a: string -> unit
    /// Initialize the random number generator with current time
    /// See https://docs.python.org/3/library/random.html#random.seed
    abstract seed: unit -> unit

    /// Return a random floating point number in the range [0.0, 1.0)
    /// See https://docs.python.org/3/library/random.html#random.random
    abstract random: unit -> float

    /// Return a random floating point number N such that a <= N <= b
    /// See https://docs.python.org/3/library/random.html#random.uniform
    abstract uniform: a: float * b: float -> float

    /// Return a random floating point number N such that low <= N <= high with triangular distribution
    /// See https://docs.python.org/3/library/random.html#random.triangular
    abstract triangular: low: float * high: float * mode: float -> float
    /// Return a random floating point number N such that 0 <= N <= 1 with triangular distribution
    /// See https://docs.python.org/3/library/random.html#random.triangular
    abstract triangular: unit -> float

    /// Return a random integer N such that a <= N <= b
    /// See https://docs.python.org/3/library/random.html#random.randint
    abstract randint: a: int * b: int -> int

    /// Return a randomly selected element from range(start, stop, step)
    /// See https://docs.python.org/3/library/random.html#random.randrange
    abstract randrange: stop: int -> int
    /// Return a randomly selected element from range(start, stop, step)
    /// See https://docs.python.org/3/library/random.html#random.randrange
    abstract randrange: start: int * stop: int -> int
    /// Return a randomly selected element from range(start, stop, step)
    /// See https://docs.python.org/3/library/random.html#random.randrange
    abstract randrange: start: int * stop: int * step: int -> int

    /// Return a random element from the non-empty sequence
    /// See https://docs.python.org/3/library/random.html#random.choice
    [<Emit("$0.choice(list($1))")>]
    abstract choice: seq: 'T[] -> 'T

    /// Return a random element from the non-empty sequence
    /// See https://docs.python.org/3/library/random.html#random.choice
    [<Emit("$0.choice(list($1))")>]
    abstract choice: seq: 'T list -> 'T

    /// Return a random element from the non-empty sequence
    /// See https://docs.python.org/3/library/random.html#random.choice
    abstract choice: seq: ResizeArray<'T> -> 'T

    /// Return a k length list of unique elements chosen from the population sequence
    /// See https://docs.python.org/3/library/random.html#random.sample
    [<Emit("$0.sample(list($1), int($2))")>]
    abstract sample: population: 'T[] * k: int -> ResizeArray<'T>

    /// Return a k length list of unique elements chosen from the population sequence
    /// See https://docs.python.org/3/library/random.html#random.sample
    [<Emit("$0.sample(list($1), int($2))")>]
    abstract sample: population: 'T list * k: int -> ResizeArray<'T>

    /// Return a k length list of unique elements chosen from the population sequence
    /// See https://docs.python.org/3/library/random.html#random.sample
    [<Emit("$0.sample($1, int($2))")>]
    abstract sample: population: ResizeArray<'T> * k: int -> ResizeArray<'T>

    /// Return a k sized list of elements chosen from the population with replacement
    /// See https://docs.python.org/3/library/random.html#random.choices
    [<Emit("$0.choices(list($1), k=int($2))")>]
    abstract choices: population: 'T[] * k: int -> ResizeArray<'T>

    /// Return a k sized list of elements chosen from the population with replacement
    /// See https://docs.python.org/3/library/random.html#random.choices
    [<Emit("$0.choices(list($1), k=int($2))")>]
    abstract choices: population: 'T list * k: int -> ResizeArray<'T>

    /// Return a k sized list of elements chosen from the population with replacement
    /// See https://docs.python.org/3/library/random.html#random.choices
    [<Emit("$0.choices($1, k=int($2))")>]
    abstract choices: population: ResizeArray<'T> * k: int -> ResizeArray<'T>

    /// Shuffle the sequence x in place
    /// See https://docs.python.org/3/library/random.html#random.shuffle
    abstract shuffle: x: 'T[] -> unit
    /// Shuffle the sequence x in place
    /// See https://docs.python.org/3/library/random.html#random.shuffle
    abstract shuffle: x: ResizeArray<'T> -> unit

    /// Return a random integer with k random bits
    /// See https://docs.python.org/3/library/random.html#random.getrandbits
    abstract getrandbits: k: int -> int

    /// Beta distribution
    /// See https://docs.python.org/3/library/random.html#random.betavariate
    abstract betavariate: alpha: float * beta: float -> float

    /// Exponential distribution
    /// See https://docs.python.org/3/library/random.html#random.expovariate
    abstract expovariate: lambd: float -> float

    /// Gamma distribution
    /// See https://docs.python.org/3/library/random.html#random.gammavariate
    abstract gammavariate: alpha: float * beta: float -> float

    /// Gaussian distribution (same as normalvariate but faster)
    /// See https://docs.python.org/3/library/random.html#random.gauss
    abstract gauss: mu: float * sigma: float -> float

    /// Log normal distribution
    /// See https://docs.python.org/3/library/random.html#random.lognormvariate
    abstract lognormvariate: mu: float * sigma: float -> float

    /// Normal distribution
    /// See https://docs.python.org/3/library/random.html#random.normalvariate
    abstract normalvariate: mu: float * sigma: float -> float

    /// Von Mises distribution
    /// See https://docs.python.org/3/library/random.html#random.vonmisesvariate
    abstract vonmisesvariate: mu: float * kappa: float -> float

    /// Pareto distribution
    /// See https://docs.python.org/3/library/random.html#random.paretovariate
    abstract paretovariate: alpha: float -> float

    /// Weibull distribution
    /// See https://docs.python.org/3/library/random.html#random.weibullvariate
    abstract weibullvariate: alpha: float * beta: float -> float

/// Random variable generators
[<ImportAll("random")>]
let random: IExports = nativeOnly
