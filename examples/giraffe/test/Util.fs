module Fable.Python.Tests.Util

module Testing =
#if FABLE_COMPILER
    open Fable.Core
    open Fable.Core.PyInterop

    /// Fable version of Xunit Assert
    type Assert =
        [<Emit("assert $0 == $1")>]
        static member Equal(actual: 'T, expected: 'T, ?msg: string) : unit = nativeOnly

        [<Emit("assert not $0 == $1")>]
        static member NotEqual(actual: 'T, expected: 'T, ?msg: string) : unit = nativeOnly

    let equal expected actual : unit = Assert.Equal(actual, expected)
    let notEqual expected actual : unit = Assert.NotEqual(actual, expected)

    type Fact () =
        inherit System.Attribute ()
#else
    open Xunit
    type FactAttribute = Xunit.FactAttribute

    let equal<'T> (expected: 'T) (actual: 'T) : unit = Assert.Equal(expected, actual)
    let notEqual<'T> (expected: 'T) (actual: 'T) : unit = Assert.NotEqual(expected, actual)

    type Assert = Xunit.Assert
#endif
    let rec sumFirstSeq (zs: seq<float>) (n: int) : float =
        match n with
        | 0 -> 0.
        | 1 -> Seq.head zs
        | _ ->
            (Seq.head zs)
            + sumFirstSeq (Seq.skip 1 zs) (n - 1)
