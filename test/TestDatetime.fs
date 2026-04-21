module Fable.Python.Tests.Datetime

open Fable.Python.Testing
open Fable.Python.Datetime

// ============================================================================
// timedelta tests
// ============================================================================

[<Fact>]
let ``test timedelta days property`` () =
    let td = timedelta.ofDays 3.0
    td.days |> equal 3

[<Fact>]
let ``test timedelta hours to seconds`` () =
    let td = timedelta.ofHours 2.0
    td.seconds |> equal 7200

[<Fact>]
let ``test timedelta minutes to seconds`` () =
    let td = timedelta.ofMinutes 90.0
    td.seconds |> equal 5400

[<Fact>]
let ``test timedelta total_seconds`` () =
    let td = (timedelta.ofDays 1.0).add (timedelta.ofHours 2.0)
    td.total_seconds () |> equal 93600.0

[<Fact>]
let ``test timedelta zero`` () =
    let td = timedelta ()
    td.days |> equal 0
    td.seconds |> equal 0
    td.microseconds |> equal 0

[<Fact>]
let ``test timedelta add`` () =
    let sum = (timedelta.ofHours 1.0).add (timedelta.ofMinutes 30.0)
    sum.total_seconds () |> equal 5400.0

[<Fact>]
let ``test timedelta sub`` () =
    let diff = (timedelta.ofHours 2.0).sub (timedelta.ofMinutes 30.0)
    diff.total_seconds () |> equal 5400.0

[<Fact>]
let ``test timedelta neg`` () =
    let n = (timedelta.ofHours 1.0).neg ()
    n.total_seconds () |> equal -3600.0

// ============================================================================
// date tests
// ============================================================================

[<Fact>]
let ``test date year month day properties`` () =
    let d = date (2026, 4, 21)
    d.year |> equal 2026
    d.month |> equal 4
    d.day |> equal 21

[<Fact>]
let ``test date isoformat`` () =
    let d = date (2026, 4, 21)
    d.isoformat () |> equal "2026-04-21"

[<Fact>]
let ``test date strftime`` () =
    let d = date (2026, 4, 21)
    d.strftime ("%Y/%m/%d") |> equal "2026/04/21"

[<Fact>]
let ``test date fromisoformat`` () =
    let d = date.fromisoformat "2026-04-21"
    d.year |> equal 2026
    d.month |> equal 4
    d.day |> equal 21

[<Fact>]
let ``test date today returns date`` () =
    let d = date.today ()
    d.year > 2024 |> equal true

[<Fact>]
let ``test date weekday monday is 0`` () =
    // 2026-04-20 is a Monday
    let d = date (2026, 4, 20)
    d.weekday () |> equal 0

[<Fact>]
let ``test date isoweekday monday is 1`` () =
    // 2026-04-20 is a Monday
    let d = date (2026, 4, 20)
    d.isoweekday () |> equal 1

[<Fact>]
let ``test date replace all fields`` () =
    let d = date (2026, 4, 21)
    let d2 = d.replace (year = 2027, month = 1, day = 15)
    d2.year |> equal 2027
    d2.month |> equal 1
    d2.day |> equal 15

[<Fact>]
let ``test date replace single field`` () =
    let d = date (2026, 4, 21)
    let d2 = d.replace (year = 2030)
    d2.year |> equal 2030
    d2.month |> equal 4
    d2.day |> equal 21

[<Fact>]
let ``test date add timedelta`` () =
    let d = date (2026, 4, 21)
    let d2 = d.add (timedelta.ofDays 10.0)
    d2.day |> equal 1
    d2.month |> equal 5

[<Fact>]
let ``test date sub date returns timedelta`` () =
    let d1 = date (2026, 4, 21)
    let d2 = date (2026, 4, 11)
    let td = d1.sub d2
    td.days |> equal 10

[<Fact>]
let ``test date sub timedelta returns date`` () =
    let d = date (2026, 4, 21)
    let d2 = d.sub (timedelta.ofDays 21.0)
    d2.month |> equal 3
    d2.day |> equal 31

// ============================================================================
// time tests
// ============================================================================

[<Fact>]
let ``test time hour minute second properties`` () =
    let t = time (14, 30, 45)
    t.hour |> equal 14
    t.minute |> equal 30
    t.second |> equal 45

[<Fact>]
let ``test time isoformat`` () =
    let t = time (9, 5, 3)
    t.isoformat () |> equal "09:05:03"

[<Fact>]
let ``test time fromisoformat`` () =
    let t = time.fromisoformat "14:30:00"
    t.hour |> equal 14
    t.minute |> equal 30
    t.second |> equal 0

[<Fact>]
let ``test time defaults to zero`` () =
    let t = time 0
    t.hour |> equal 0
    t.minute |> equal 0
    t.second |> equal 0
    t.microsecond |> equal 0

[<Fact>]
let ``test time replace single field`` () =
    let t = time (9, 30, 0)
    let t2 = t.replace (hour = 14)
    t2.hour |> equal 14
    t2.minute |> equal 30
    t2.second |> equal 0

// ============================================================================
// datetime tests
// ============================================================================

[<Fact>]
let ``test datetime year month day properties`` () =
    let dt = datetime (2026, 4, 21)
    dt.year |> equal 2026
    dt.month |> equal 4
    dt.day |> equal 21

[<Fact>]
let ``test datetime hour minute second properties`` () =
    let dt = datetime (2026, 4, 21, 14, 30, 59)
    dt.hour |> equal 14
    dt.minute |> equal 30
    dt.second |> equal 59

[<Fact>]
let ``test datetime isoformat`` () =
    let dt = datetime (2026, 4, 21, 12, 0, 0)
    dt.isoformat () |> equal "2026-04-21T12:00:00"

[<Fact>]
let ``test datetime fromisoformat`` () =
    let dt = datetime.fromisoformat "2026-04-21T14:30:00"
    dt.year |> equal 2026
    dt.month |> equal 4
    dt.day |> equal 21
    dt.hour |> equal 14
    dt.minute |> equal 30

[<Fact>]
let ``test datetime now returns current datetime`` () =
    let dt = datetime.now ()
    dt.year > 2024 |> equal true

[<Fact>]
let ``test datetime now with timezone`` () =
    let dt = datetime.now (timezone.utc)
    dt.year > 2024 |> equal true
    dt.tzname () |> equal (Some "UTC")

[<Fact>]
let ``test datetime strptime`` () =
    let dt = datetime.strptime ("21/04/2026", "%d/%m/%Y")
    dt.year |> equal 2026
    dt.month |> equal 4
    dt.day |> equal 21

[<Fact>]
let ``test datetime combine date and time`` () =
    let d = date (2026, 4, 21)
    let t = time (10, 0, 0)
    let dt = datetime.combine (d, t)
    dt.year |> equal 2026
    dt.hour |> equal 10

[<Fact>]
let ``test datetime date method returns date part`` () =
    let dt = datetime (2026, 4, 21, 14, 30, 0)
    let d = dt.date ()
    d.year |> equal 2026
    d.month |> equal 4
    d.day |> equal 21

[<Fact>]
let ``test datetime time method returns time part`` () =
    let dt = datetime (2026, 4, 21, 14, 30, 59)
    let t = dt.time ()
    t.hour |> equal 14
    t.minute |> equal 30
    t.second |> equal 59

[<Fact>]
let ``test datetime replace date fields`` () =
    let dt = datetime (2026, 4, 21, 12, 0, 0)
    let dt2 = dt.replace (year = 2027, month = 6, day = 15)
    dt2.year |> equal 2027
    dt2.month |> equal 6
    dt2.day |> equal 15
    dt2.hour |> equal 12

[<Fact>]
let ``test datetime replace time fields`` () =
    let dt = datetime (2026, 4, 21, 12, 0, 0)
    let dt2 = dt.replace (hour = 9, minute = 30, second = 0)
    dt2.hour |> equal 9
    dt2.minute |> equal 30
    dt2.year |> equal 2026

[<Fact>]
let ``test datetime timestamp roundtrip`` () =
    let dt1 = datetime.fromisoformat "2026-01-01T00:00:00"
    let ts = dt1.timestamp ()
    let dt2 = datetime.fromtimestamp ts
    dt2.year |> equal dt1.year
    dt2.month |> equal dt1.month
    dt2.day |> equal dt1.day

[<Fact>]
let ``test datetime add timedelta`` () =
    let dt = datetime (2026, 4, 21, 12, 0, 0)
    let dt2 = dt.add (timedelta.ofHours 3.0)
    dt2.hour |> equal 15

[<Fact>]
let ``test datetime sub datetime returns timedelta`` () =
    let dt1 = datetime (2026, 4, 21, 15, 0, 0)
    let dt2 = datetime (2026, 4, 21, 12, 0, 0)
    let td = dt1.sub dt2
    td.total_seconds () |> equal 10800.0

[<Fact>]
let ``test datetime sub timedelta returns datetime`` () =
    let dt = datetime (2026, 4, 21, 12, 0, 0)
    let dt2 = dt.sub (timedelta.ofHours 2.0)
    dt2.hour |> equal 10

// ============================================================================
// timezone tests
// ============================================================================

[<Fact>]
let ``test timezone utc name`` () =
    let utcName = timezone.utc.tzname (null)
    utcName |> equal "UTC"

[<Fact>]
let ``test timezone create with offset`` () =
    let offset = (timedelta.ofHours 5.0).add (timedelta.ofMinutes 30.0)
    let tz = timezone offset
    let name = tz.tzname (null)
    name |> equal "UTC+05:30"

[<Fact>]
let ``test timezone create with name`` () =
    let offset = timedelta.ofHours -5.0
    let tz = timezone (offset, "EST")
    let name = tz.tzname (null)
    name |> equal "EST"
