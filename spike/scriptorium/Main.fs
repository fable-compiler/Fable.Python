module SpikeMain

// SPIKE: prove Scriptorium's Nib assertions + Quill runner compile to Python via
// Fable.Python and can test real Fable.Python bindings.

open Scriptorium.Quill
open Scriptorium.Nib.Assertion

open type Scriptorium.Quill.Test
open type Scriptorium.Quill.Runner

open Fable.Python.Math

let tests =
    [
        // Plain F# values through the Nib fluent assertion chain.
        testList (
            "Nib basics",
            [
                test ("isEqualTo passes", fun _ -> assertThat 42 (isEqualTo 42))
                test (
                    "chained assertions",
                    fun _ -> assertThat 42 (isEqualTo 42 >> isGreaterThan 40 >> isLessThan 50)
                )
                test ("strings", fun _ -> assertThat "hello" (isEqualTo "hello"))
                test ("booleans", fun _ -> assertThat (1 = 1) isTrue)
            ]
        )

        // Exercise actual Fable.Python stdlib bindings (Python's math module) and
        // assert the results with Nib. This runs on real CPython at test time.
        testList (
            "Fable.Python.Math bindings",
            [
                test ("math.sqrt", fun _ -> assertThat (math.sqrt 9.0) (isEqualTo 3.0))
                test ("math.factorial", fun _ -> assertThat (math.factorial 5) (isEqualTo 120))
                test ("math.gcd", fun _ -> assertThat (math.gcd (12, 8)) (isEqualTo 4))
                test ("math.floor", fun _ -> assertThat (math.floor 2.9) (isEqualTo 2))
            ]
        )
    ]

// Fable 5.8.0+ emits `sys.exit(int(main(...)))`, so the entry point's int return
// propagates to the process exit code (1 on failure) with no extra plumbing.
[<EntryPoint>]
let main _ = runTests tests
