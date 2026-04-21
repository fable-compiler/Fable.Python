module Fable.Python.Tests.Datetime

open Fable.Python.Testing
open Fable.Python.Datetime

// ============================================================================
// timedelta tests
// ============================================================================

[<Fact>]
let ``test timedelta days property`` () =
    let td = timedelta.Create(days = 3.0)
    td.days |> equal 3

[<Fact>]
let ``test timedelta hours to seconds`` () =
    let td = timedelta.Create(hours = 2.0)
    td.seconds |> equal 7200

[<Fact>]
let ``test timedelta minutes to seconds`` () =
    let td = timedelta.Create(minutes = 90.0)
    td.seconds |> equal 5400

[<Fact>]
let ``test timedelta total_seconds`` () =
    let td = timedelta.Create(days = 1.0, hours = 2.0)
    td.total_seconds() |> equal 93600.0

[<Fact>]
let ``test timedelta zero`` () =
    let td = timedelta.Create()
    td.days |> equal 0
    td.seconds |> equal 0
    td.microseconds |> equal 0

// ============================================================================
// date tests
// ============================================================================

[<Fact>]
let ``test date year month day properties`` () =
    let d = date.Create(2026, 4, 21)
    d.year |> equal 2026
    d.month |> equal 4
    d.day |> equal 21

[<Fact>]
let ``test date isoformat`` () =
    let d = date.Create(2026, 4, 21)
    d.isoformat() |> equal "2026-04-21"

[<Fact>]
let ``test date strftime`` () =
    let d = date.Create(2026, 4, 21)
    d.strftime("%Y/%m/%d") |> equal "2026/04/21"

[<Fact>]
let ``test date fromisoformat`` () =
    let d = date.fromisoformat "2026-04-21"
    d.year |> equal 2026
    d.month |> equal 4
    d.day |> equal 21

[<Fact>]
let ``test date today returns date`` () =
    let d = date.today()
    d.year > 2024 |> equal true

[<Fact>]
let ``test date weekday monday is 0`` () =
    // 2026-04-20 is a Monday
    let d = date.Create(2026, 4, 20)
    d.weekday() |> equal 0

[<Fact>]
let ``test date isoweekday monday is 1`` () =
    // 2026-04-20 is a Monday
    let d = date.Create(2026, 4, 20)
    d.isoweekday() |> equal 1

[<Fact>]
let ``test date replace`` () =
    let d = date.Create(2026, 4, 21)
    let d2 = d.replace(2027, 1, 15)
    d2.year |> equal 2027
    d2.month |> equal 1
    d2.day |> equal 15

// ============================================================================
// time tests
// ============================================================================

[<Fact>]
let ``test time hour minute second properties`` () =
    let t = time.Create(hour = 14, minute = 30, second = 45)
    t.hour |> equal 14
    t.minute |> equal 30
    t.second |> equal 45

[<Fact>]
let ``test time isoformat`` () =
    let t = time.Create(hour = 9, minute = 5, second = 3)
    t.isoformat() |> equal "09:05:03"

[<Fact>]
let ``test time fromisoformat`` () =
    let t = time.fromisoformat "14:30:00"
    t.hour |> equal 14
    t.minute |> equal 30
    t.second |> equal 0

[<Fact>]
let ``test time defaults to zero`` () =
    let t = time.Create()
    t.hour |> equal 0
    t.minute |> equal 0
    t.second |> equal 0
    t.microsecond |> equal 0

// ============================================================================
// datetime tests
// ============================================================================

[<Fact>]
let ``test datetime year month day properties`` () =
    let dt = datetime.Create(2026, 4, 21)
    dt.year |> equal 2026
    dt.month |> equal 4
    dt.day |> equal 21

[<Fact>]
let ``test datetime hour minute second properties`` () =
    let dt = datetime.Create(2026, 4, 21, 14, 30, 59)
    dt.hour |> equal 14
    dt.minute |> equal 30
    dt.second |> equal 59

[<Fact>]
let ``test datetime isoformat`` () =
    let dt = datetime.Create(2026, 4, 21, 12, 0, 0)
    dt.isoformat() |> equal "2026-04-21T12:00:00"

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
    let dt = datetime.now()
    dt.year > 2024 |> equal true

[<Fact>]
let ``test datetime strptime`` () =
    let dt = datetime.strptime("21/04/2026", "%d/%m/%Y")
    dt.year |> equal 2026
    dt.month |> equal 4
    dt.day |> equal 21

[<Fact>]
let ``test datetime combine date and time`` () =
    let d = date.Create(2026, 4, 21)
    let t = time.Create(hour = 10, minute = 0, second = 0)
    let dt = datetime.combine(d, t)
    dt.year |> equal 2026
    dt.hour |> equal 10

[<Fact>]
let ``test datetime date method returns date part`` () =
    let dt = datetime.Create(2026, 4, 21, 14, 30, 0)
    let d = dt.date()
    d.year |> equal 2026
    d.month |> equal 4
    d.day |> equal 21

[<Fact>]
let ``test datetime time method returns time part`` () =
    let dt = datetime.Create(2026, 4, 21, 14, 30, 59)
    let t = dt.time()
    t.hour |> equal 14
    t.minute |> equal 30
    t.second |> equal 59

[<Fact>]
let ``test datetime replaceDate`` () =
    let dt = datetime.Create(2026, 4, 21, 12, 0, 0)
    let dt2 = dt.replaceDate(2027, 6, 15)
    dt2.year |> equal 2027
    dt2.month |> equal 6
    dt2.day |> equal 15

[<Fact>]
let ``test datetime replaceTime`` () =
    let dt = datetime.Create(2026, 4, 21, 12, 0, 0)
    let dt2 = dt.replaceTime(9, 30, 0)
    dt2.hour |> equal 9
    dt2.minute |> equal 30

[<Fact>]
let ``test datetime timestamp roundtrip`` () =
    let dt1 = datetime.fromisoformat "2026-01-01T00:00:00"
    let ts = dt1.timestamp()
    let dt2 = datetime.fromtimestamp ts
    dt2.year |> equal dt1.year
    dt2.month |> equal dt1.month
    dt2.day |> equal dt1.day

// ============================================================================
// timezone tests
// ============================================================================

[<Fact>]
let ``test timezone utc name`` () =
    let utcName = timezone.utc.tzname(null)
    utcName |> equal "UTC"

[<Fact>]
let ``test timezone create with offset`` () =
    let offset = timedelta.Create(hours = 5.0, minutes = 30.0)
    let tz = timezone.Create offset
    let name = tz.tzname(null)
    name |> equal "UTC+05:30"

[<Fact>]
let ``test timezone create with name`` () =
    let offset = timedelta.Create(hours = -5.0)
    let tz = timezone.Create(offset, "EST")
    let name = tz.tzname(null)
    name |> equal "EST"
