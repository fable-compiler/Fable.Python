# Spike: Scriptorium on Fable.Python

**Question:** Can [Scriptorium](https://github.com/fable-hub/Scriptorium) (Maxime Mangel's
F#/Fable testing stack — Nib assertions + Quill runner) be used to write and run tests for
Fable.Python, compiled to Python?

**Answer: Yes.** It works out of the box, because Scriptorium already ships explicit
`#if FABLE_COMPILER_PYTHON` support.

## What this spike contains

- `Spike.fsproj` — an `Exe` project referencing the local `../../../Scriptorium` checkout
  (Quill, which transitively pulls in Nib, Ink, Parchment) **and** the real `Fable.Python`
  bindings.
- `Main.fs` — a small Quill/Nib suite that exercises plain F# assertions **and** actual
  `Fable.Python.Math` bindings, then exits with the runner's status code.

## Run it

```bash
just spike-scriptorium
# or directly:
dotnet fable spike/scriptorium --lang python -o spike/scriptorium/build \
  --run uv run python spike/scriptorium/build/main.py
```

Expected output: Quill's colored reporter, `8 passed (8)`, exit code 0. Break an assertion
and the suite reports a colored diff with a clickable `Main.fs:NN` source link and exits 1.

## Findings

1. **Scriptorium compiles cleanly to Python.** All of Ink, Parchment, Nib, Quill and Quill's
   DSL compiled via `--lang python` with no errors. The maintainer already guards every
   platform-specific spot (`cwd` → `os.getcwd()`, stopwatch → `time.perf_counter()`,
   stdout → `sys.stdout.write`, `isCI`, etc.) behind `FABLE_COMPILER_PYTHON`. The JS-only
   `performance.now()` / `setTimeout` emits sit in dead branches gated by `Compiler.isJavaScript`
   and are eliminated for Python.

2. **The runner works on real CPython** — colored dots, per-test timing, a summary table, and
   a proper failure report with a unified diff and OSC-8 source hyperlink.

3. **Exit-code gotcha (now fixed upstream in Fable 5.8.0).** Quill correctly *returns* exit code
   1 on failure, but Fable's Python backend used to emit `main(sys.argv)` and **ignore the return
   value**, so the process always exited 0 — CI would never see failures. This spike originally
   carried a `Fable.Python.Sys.sys.exit exitCode` shim to work around it. Fable 5.8.0 fixes it:
   the generated entry point now emits `sys.exit(int(main(...)))` (the `int(...)` coercion matters
   because fable-library-python's `Int32` is not an `int` subclass), so the shim was removed and
   `Main.fs` is now just `let main _ = runTests tests`. Verified: passing suite exits 0, a failing
   assertion exits 1.

4. **Snapshot / Browser / Hedgehog are out of scope / blocked.** `Nib.Browser` is Playwright
   (JS-only). Scriptorium's own `fable-repros/` notes that `Nib.Snapshot` and `Hedgehog.Derive`
   hit fable-library-python runtime gaps (Hedgehog: 13/21 tests fail on Python). For Fable.Python
   the useful surface is **Nib + Quill** (+ Ink/Parchment).

## How this compares to the current test setup

Today `test/` uses a home-grown `Fable.Python.Testing` module (`[<Fact>]` + `equal`/`throws*`)
compiled to Python and run under **pytest**. Scriptorium is a different execution model: it is
its own runner with an `[<EntryPoint>]`, run as `python main.py` (not pytest). Migrating the
suite would mean rewriting `[<Fact>]` functions into `test(...)`/`testList(...)` trees and
swapping `equal`/`throws*` for Nib's `assertThat ... (isEqualTo ...)` combinators. This spike
proves that path is viable before committing to it.

## Notes for productionizing (not done in this spike)

- Reference published NuGet packages (`Scriptorium.Quill` etc.) via paket instead of the local
  source checkout, once the versions with Python support are on nuget.org.
- Decide runner strategy: keep pytest for existing tests and add Scriptorium alongside, or
  migrate wholesale. Losing pytest means losing its discovery/reporting/CI integrations, so the
  `sys.exit` propagation above is the minimum needed for CI.
