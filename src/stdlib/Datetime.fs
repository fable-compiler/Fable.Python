/// Type bindings for Python datetime module: https://docs.python.org/3/library/datetime.html
///
/// Note: this module exposes a `time` class binding for Python's `datetime.time`. If you also
/// open `Fable.Python.Time` (the `time` module), the `time` identifier will collide — qualify
/// one of them, e.g. `Fable.Python.Time.time.time()` vs. `Fable.Python.Datetime.time(...)`.
module Fable.Python.Datetime

open Fable.Core

// fsharplint:disable MemberNames

// ============================================================================
// timedelta
// ============================================================================

/// A duration expressing the difference between two date, time, or datetime instances.
/// See https://docs.python.org/3/library/datetime.html#datetime.timedelta
///
/// The empty `timedelta()` ctor creates a zero duration. For other durations, use the
/// single-unit factories `ofDays`, `ofHours`, `ofMinutes`, `ofSeconds`, `ofWeeks`,
/// `ofMilliseconds`, `ofMicroseconds`, and combine via `.add` / `.sub`.
[<Import("timedelta", "datetime")>]
type timedelta() =
    /// Number of full days (may be negative)
    member _.days: int = nativeOnly
    /// Remaining seconds after full days have been removed; 0 <= seconds < 86400
    member _.seconds: int = nativeOnly
    /// Remaining microseconds; 0 <= microseconds < 1000000
    member _.microseconds: int = nativeOnly
    /// Return the total duration represented in fractional seconds
    /// See https://docs.python.org/3/library/datetime.html#datetime.timedelta.total_seconds
    member _.total_seconds() : float = nativeOnly

    /// Return the sum of two timedeltas
    [<Emit("$0 + $1")>]
    member _.add(other: timedelta) : timedelta = nativeOnly

    /// Return the difference between two timedeltas
    [<Emit("$0 - $1")>]
    member _.sub(other: timedelta) : timedelta = nativeOnly

    /// Return the negation of this timedelta
    [<Emit("-$0")>]
    member _.neg() : timedelta = nativeOnly

    /// The most negative timedelta representable
    static member min: timedelta = nativeOnly
    /// The most positive timedelta representable
    static member max: timedelta = nativeOnly
    /// The smallest positive difference between non-equal timedelta objects
    static member resolution: timedelta = nativeOnly

    /// Create a timedelta of N days
    [<Emit("timedelta(days=float($0))")>]
    static member ofDays(days: float) : timedelta = nativeOnly

    /// Create a timedelta of N seconds
    [<Emit("timedelta(seconds=float($0))")>]
    static member ofSeconds(seconds: float) : timedelta = nativeOnly

    /// Create a timedelta of N microseconds
    [<Emit("timedelta(microseconds=float($0))")>]
    static member ofMicroseconds(microseconds: float) : timedelta = nativeOnly

    /// Create a timedelta of N milliseconds
    [<Emit("timedelta(milliseconds=float($0))")>]
    static member ofMilliseconds(milliseconds: float) : timedelta = nativeOnly

    /// Create a timedelta of N minutes
    [<Emit("timedelta(minutes=float($0))")>]
    static member ofMinutes(minutes: float) : timedelta = nativeOnly

    /// Create a timedelta of N hours
    [<Emit("timedelta(hours=float($0))")>]
    static member ofHours(hours: float) : timedelta = nativeOnly

    /// Create a timedelta of N weeks
    [<Emit("timedelta(weeks=float($0))")>]
    static member ofWeeks(weeks: float) : timedelta = nativeOnly

// ============================================================================
// timezone  (defined before datetime so datetime members can reference it)
// ============================================================================

/// A fixed-offset implementation of tzinfo.
/// See https://docs.python.org/3/library/datetime.html#datetime.timezone
[<Import("timezone", "datetime")>]
type timezone(offset: timedelta, ?name: string) =
    /// Return the UTC offset for this timezone
    member _.utcoffset(dt: obj) : timedelta = nativeOnly
    /// Return the timezone name string for this timezone
    member _.tzname(dt: obj) : string = nativeOnly
    /// The UTC timezone singleton (offset zero, name "UTC")
    static member utc: timezone = nativeOnly

// ============================================================================
// date
// ============================================================================

/// A naive date (year, month, day) with no time or timezone component.
/// See https://docs.python.org/3/library/datetime.html#datetime.date
[<Import("date", "datetime")>]
type date(year: int, month: int, day: int) =
    /// Year in range [MINYEAR, MAXYEAR]
    member _.year: int = nativeOnly
    /// Month in range [1, 12]
    member _.month: int = nativeOnly
    /// Day in range [1, number of days in the month and year]
    member _.day: int = nativeOnly
    /// Return a string in ISO 8601 format, e.g. "2026-04-21"
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.isoformat
    member _.isoformat() : string = nativeOnly
    /// Return a string representing the date, formatted with format
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.strftime
    member _.strftime(format: string) : string = nativeOnly
    /// Return the day of the week as an integer; Monday is 0 and Sunday is 6
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.weekday
    member _.weekday() : int = nativeOnly
    /// Return the day of the week as an integer; Monday is 1 and Sunday is 7
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.isoweekday
    member _.isoweekday() : int = nativeOnly
    /// Return the proleptic Gregorian ordinal of the date; January 1 of year 1 has ordinal 1
    member _.toordinal() : int = nativeOnly

    /// Return a date with the given fields replaced (any subset of year/month/day)
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.replace
    [<NamedParams>]
    member _.replace(?year: int, ?month: int, ?day: int) : date = nativeOnly

    /// Return the timedelta between this date and other (self - other)
    [<Emit("$0 - $1")>]
    member _.sub(other: date) : timedelta = nativeOnly

    /// Return a date offset by the given timedelta (self - delta)
    [<Emit("$0 - $1")>]
    member _.sub(delta: timedelta) : date = nativeOnly

    /// Return a date offset by the given timedelta (self + delta)
    [<Emit("$0 + $1")>]
    member _.add(delta: timedelta) : date = nativeOnly

    /// Return the current local date
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.today
    static member today() : date = nativeOnly
    /// Return the local date corresponding to a POSIX timestamp
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.fromtimestamp
    static member fromtimestamp(timestamp: float) : date = nativeOnly
    /// Return the date corresponding to the proleptic Gregorian ordinal
    static member fromordinal(ordinal: int) : date = nativeOnly
    /// Return a date from a string in any valid ISO 8601 format
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.fromisoformat
    static member fromisoformat(date_string: string) : date = nativeOnly
    /// The earliest representable date
    static member min: date = nativeOnly
    /// The latest representable date
    static member max: date = nativeOnly

// ============================================================================
// time
// ============================================================================

/// A naive or aware time of day (hour, minute, second, microsecond, tzinfo).
/// See https://docs.python.org/3/library/datetime.html#datetime.time
///
/// Ctor args are positional: `time(h)`, `time(h, m)`, `time(h, m, s)`, `time(h, m, s, us)`.
[<Import("time", "datetime")>]
type time(hour: int, ?minute: int, ?second: int, ?microsecond: int) =
    /// Hour in range [0, 23]
    member _.hour: int = nativeOnly
    /// Minute in range [0, 59]
    member _.minute: int = nativeOnly
    /// Second in range [0, 59]
    member _.second: int = nativeOnly
    /// Microsecond in range [0, 999999]
    member _.microsecond: int = nativeOnly
    /// Fold value (0 or 1) for disambiguating wall-clock times that repeat during DST transitions
    member _.fold: int = nativeOnly
    /// Return a string in ISO 8601 format, e.g. "14:30:00"
    /// See https://docs.python.org/3/library/datetime.html#datetime.time.isoformat
    member _.isoformat() : string = nativeOnly
    /// Return a string representing the time, formatted with format
    /// See https://docs.python.org/3/library/datetime.html#datetime.time.strftime
    member _.strftime(format: string) : string = nativeOnly
    /// Return the UTC offset as a timedelta for aware times; None for naive times
    member _.utcoffset() : timedelta option = nativeOnly
    /// Return the timezone abbreviation string for aware times; None for naive times
    member _.tzname() : string option = nativeOnly

    /// Return a time with the given fields replaced (any subset)
    /// See https://docs.python.org/3/library/datetime.html#datetime.time.replace
    [<NamedParams>]
    member _.replace(?hour: int, ?minute: int, ?second: int, ?microsecond: int) : time = nativeOnly

    /// Return a time from a string in ISO 8601 format
    /// See https://docs.python.org/3/library/datetime.html#datetime.time.fromisoformat
    static member fromisoformat(time_string: string) : time = nativeOnly
    /// The earliest representable time, time(0, 0, 0, 0)
    static member min: time = nativeOnly
    /// The latest representable time, time(23, 59, 59, 999999)
    static member max: time = nativeOnly

// ============================================================================
// datetime
// ============================================================================

/// A naive or aware date and time (year, month, day, hour, minute, second, microsecond, tzinfo).
/// See https://docs.python.org/3/library/datetime.html#datetime.datetime
[<Import("datetime", "datetime")>]
type datetime
    (
        year: int,
        month: int,
        day: int,
        ?hour: int,
        ?minute: int,
        ?second: int,
        ?microsecond: int,
        ?tzinfo: timezone,
        ?fold: int
    ) =
    /// Year in range [MINYEAR, MAXYEAR]
    member _.year: int = nativeOnly
    /// Month in range [1, 12]
    member _.month: int = nativeOnly
    /// Day in range [1, number of days in the month and year]
    member _.day: int = nativeOnly
    /// Hour in range [0, 23]
    member _.hour: int = nativeOnly
    /// Minute in range [0, 59]
    member _.minute: int = nativeOnly
    /// Second in range [0, 59]
    member _.second: int = nativeOnly
    /// Microsecond in range [0, 999999]
    member _.microsecond: int = nativeOnly
    /// Fold value (0 or 1) for disambiguating wall-clock times that repeat during DST transitions
    member _.fold: int = nativeOnly
    /// Return the date part as a date object
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.date
    member _.date() : date = nativeOnly
    /// Return the time part as a time object (tzinfo is not included)
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.time
    member _.time() : time = nativeOnly
    /// Return a string in ISO 8601 format, e.g. "2026-04-21T14:30:00"
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.isoformat
    member _.isoformat() : string = nativeOnly

    /// Return a string in ISO 8601 format with a custom separator between date and time
    [<Emit("$0.isoformat(sep=$1)")>]
    member _.isoformat(sep: string) : string = nativeOnly

    /// Return a string representing the datetime, formatted with format
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.strftime
    member _.strftime(format: string) : string = nativeOnly
    /// Return the POSIX timestamp corresponding to this datetime as a float
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.timestamp
    member _.timestamp() : float = nativeOnly
    /// Return the UTC offset as a timedelta for aware datetimes; None for naive
    member _.utcoffset() : timedelta option = nativeOnly
    /// Return the timezone abbreviation string for aware datetimes; None for naive
    member _.tzname() : string option = nativeOnly
    /// Return the day of the week as an integer; Monday is 0 and Sunday is 6
    member _.weekday() : int = nativeOnly
    /// Return the day of the week as an integer; Monday is 1 and Sunday is 7
    member _.isoweekday() : int = nativeOnly

    /// Return a datetime with the given fields replaced (any subset)
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.replace
    [<NamedParams>]
    member _.replace
        (?year: int, ?month: int, ?day: int, ?hour: int, ?minute: int, ?second: int, ?microsecond: int)
        : datetime =
        nativeOnly

    /// Return a datetime converted to the given timezone
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.astimezone
    member _.astimezone(tz: timezone) : datetime = nativeOnly

    /// Return the timedelta between this datetime and other (self - other)
    [<Emit("$0 - $1")>]
    member _.sub(other: datetime) : timedelta = nativeOnly

    /// Return a datetime offset by the given timedelta (self - delta)
    [<Emit("$0 - $1")>]
    member _.sub(delta: timedelta) : datetime = nativeOnly

    /// Return a datetime offset by the given timedelta (self + delta)
    [<Emit("$0 + $1")>]
    member _.add(delta: timedelta) : datetime = nativeOnly

    /// Return the current local date and time
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.now
    static member now() : datetime = nativeOnly

    /// Return the current local date and time in the given timezone
    [<Emit("datetime.now(tz=$0)")>]
    static member now(tz: timezone) : datetime = nativeOnly

    /// Return the current UTC date and time as a naive datetime.
    /// Deprecated in Python 3.12; prefer `datetime.now(tz = timezone.utc)`.
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.utcnow
    [<System.Obsolete("Use datetime.now(tz = timezone.utc) instead. Python datetime.utcnow is deprecated since 3.12.")>]
    static member utcnow() : datetime = nativeOnly

    /// Return the local datetime corresponding to a POSIX timestamp
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.fromtimestamp
    static member fromtimestamp(timestamp: float) : datetime = nativeOnly

    /// Return the datetime corresponding to a POSIX timestamp in the given timezone
    [<Emit("datetime.fromtimestamp($0, tz=$1)")>]
    static member fromtimestamp(timestamp: float, tz: timezone) : datetime = nativeOnly

    /// Return a datetime from a string in any valid ISO 8601 format
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.fromisoformat
    static member fromisoformat(datetime_string: string) : datetime = nativeOnly
    /// Return a datetime parsed from date_string according to format
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.strptime
    static member strptime(date_string: string, format: string) : datetime = nativeOnly
    /// Combine a date and time into a single datetime
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.combine
    static member combine(date: date, time: time) : datetime = nativeOnly
    /// The earliest representable datetime
    static member min: datetime = nativeOnly
    /// The latest representable datetime
    static member max: datetime = nativeOnly
