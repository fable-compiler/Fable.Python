module Fable.Python.Tests.Time

open Fable.Python.Testing
open Fable.Python.Time

[<Fact>]
let ``test time.time returns positive float`` () =
    let t = time.time ()
    t > 0.0 |> equal true

[<Fact>]
let ``test time.monotonic returns positive float`` () =
    let t = time.monotonic ()
    t > 0.0 |> equal true

[<Fact>]
let ``test time.perf_counter returns positive float`` () =
    let t = time.perf_counter ()
    t > 0.0 |> equal true

[<Fact>]
let ``test time.process_time returns non-negative float`` () =
    let t = time.process_time ()
    t >= 0.0 |> equal true

[<Fact>]
let ``test time.monotonic increases over calls`` () =
    let t1 = time.monotonic ()
    let t2 = time.monotonic ()
    t2 >= t1 |> equal true

[<Fact>]
let ``test time.ctime returns non-empty string`` () =
    let s = time.ctime ()
    s.Length > 0 |> equal true

[<Fact>]
let ``test time.ctime with seconds returns non-empty string`` () =
    let s = time.ctime 0.0
    s.Length > 0 |> equal true

[<Fact>]
let ``test time.sleep does not throw`` () = time.sleep 0.0

[<Fact>]
let ``test time.timezone is int`` () =
    let tz = time.timezone
    (tz >= -50400 && tz <= 50400) |> equal true
