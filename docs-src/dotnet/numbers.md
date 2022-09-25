# Numeric types

In Fable, we use F# numeric types, which are all translated to Python integers except for `float`, `double`, and
`decimal`.

Fable numbers are very nearly compatible with .NET semantics, but translating into Python types has consequences:

* (non-standard) All floating point numbers are implemented as 64 bit (`double`). This makes `float32` numbers more
  accurate than expected.
* (non-standard) Arithmetic integers of 32 bits or less are implemented with different truncation from that expected, as
  whole numbers embedded within `double`.
* (OK) Conversions between types are correctly truncated.
* (OK) Bitwise operations for 64 bit and 32 bit integers are correct and truncated to the appropriate number of bits.
* (non-standard) Bitwise operations for 16 bit and 8 bit integers use the underlying JavaScript 32 bit bitwise
  semantics. Results are not truncated as expected, and shift operands are not masked to fit the data type.
* (OK) Longs have a custom implementation which is identical in semantics to .NET and truncates in 64 bits, although it
  is slower.

32 bit integers thus differ from .NET in two ways:

* Underlying unlimited precision, without expected truncation to 32 bits on overflow. Truncation can be forced if needed
  by `>>> 0`.

The loss of precision can be seen in a single multiplication:

```fsharp
((1 <<< 28) + 1) * ((1 <<< 28) + 1) >>> 0
```

The multiply product will have internal double representation rounded to `0x0100_0000_2000_0000`. When it is truncated
to 32 bits by `>>> 0` the result will be  `0x2000_0000` not the .NET exact lower order bits value of `0x2000_0001`.

## Workarounds

* When accurate low-order bit arithmetic is needed and overflow can result in numbers larger than 2^53 use `int64`,
  `uint64`, which use exact 64 bits, instead of `int32`, `uint32`.
* Alternately, truncate all arithmetic with `>>> 0` or `>>> 0u` as appropriate before numbers can get larger than 2^53:
  `let rng (s:int32) = 10001*s + 12345 >>> 0`

## Printing

One small change from .NET in `printf`, `sprintf`, `ToString`. Negative signed integers are printed in hexadecimal
format as sign + magnitude, in .NET they are printed as two's complement bit patterns.
