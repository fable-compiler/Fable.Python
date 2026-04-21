/// Type bindings for Python datetime module: https://docs.python.org/3/library/datetime.html
module Fable.Python.Datetime

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

// ============================================================================
// timedelta
// ============================================================================

/// A duration expressing the difference between two date, time, or datetime instances.
/// See https://docs.python.org/3/library/datetime.html#datetime.timedelta
[<Import("timedelta", "datetime")>]
type timedelta =
    /// Number of full days (may be negative)
    abstract days: int
    /// Remaining seconds after full days have been removed; 0 <= seconds < 86400
    abstract seconds: int
    /// Remaining microseconds; 0 <= microseconds < 1000000
    abstract microseconds: int
    /// Return the total duration represented in fractional seconds
    /// See https://docs.python.org/3/library/datetime.html#datetime.timedelta.total_seconds
    abstract total_seconds: unit -> float

/// Static factory for timedelta instances
[<Import("timedelta", "datetime")>]
type timedeltaStatic =
    /// Create a timedelta; all arguments default to 0, may be floats, and may be negative.
    /// See https://docs.python.org/3/library/datetime.html#datetime.timedelta
    [<Emit("$0($1...)")>]
    [<NamedParams>]
    abstract Create:
        ?days: float *
        ?seconds: float *
        ?microseconds: float *
        ?milliseconds: float *
        ?minutes: float *
        ?hours: float *
        ?weeks: float ->
            timedelta

/// Factory for creating timedelta values
[<Import("timedelta", "datetime")>]
let timedelta: timedeltaStatic = nativeOnly

// ============================================================================
// date
// ============================================================================

/// A naive date (year, month, day) with no time or timezone component.
/// See https://docs.python.org/3/library/datetime.html#datetime.date
[<Import("date", "datetime")>]
type date =
    /// Year in range [MINYEAR, MAXYEAR]
    abstract year: int
    /// Month in range [1, 12]
    abstract month: int
    /// Day in range [1, number of days in the month and year]
    abstract day: int
    /// Return a string in ISO 8601 format, e.g. "2026-04-21"
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.isoformat
    abstract isoformat: unit -> string
    /// Return a string representing the date, formatted with format
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.strftime
    abstract strftime: format: string -> string
    /// Return the day of the week as an integer; Monday is 0 and Sunday is 6
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.weekday
    abstract weekday: unit -> int
    /// Return the day of the week as an integer; Monday is 1 and Sunday is 7
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.isoweekday
    abstract isoweekday: unit -> int
    /// Return the proleptic Gregorian ordinal of the date; January 1 of year 1 has ordinal 1
    abstract toordinal: unit -> int
    /// Return a date with the given fields replaced
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.replace
    [<Emit("$0.replace(year=int($1), month=int($2), day=int($3))")>]
    abstract replace: year: int * month: int * day: int -> date

/// Static factory for date instances
[<Import("date", "datetime")>]
type dateStatic =
    /// Create a date for the given year, month and day
    /// See https://docs.python.org/3/library/datetime.html#datetime.date
    [<Emit("$0(int($1), int($2), int($3))")>]
    abstract Create: year: int * month: int * day: int -> date
    /// Return the current local date
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.today
    abstract today: unit -> date
    /// Return the local date corresponding to a POSIX timestamp
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.fromtimestamp
    abstract fromtimestamp: timestamp: float -> date
    /// Return the date corresponding to the proleptic Gregorian ordinal
    abstract fromordinal: ordinal: int -> date
    /// Return a date from a string in any valid ISO 8601 format
    /// See https://docs.python.org/3/library/datetime.html#datetime.date.fromisoformat
    abstract fromisoformat: date_string: string -> date
    /// The earliest representable date
    abstract min: date
    /// The latest representable date
    abstract max: date

/// Factory for creating date values
[<Import("date", "datetime")>]
let date: dateStatic = nativeOnly

// ============================================================================
// time
// ============================================================================

/// A naive or aware time of day (hour, minute, second, microsecond, tzinfo).
/// See https://docs.python.org/3/library/datetime.html#datetime.time
[<Import("time", "datetime")>]
type time =
    /// Hour in range [0, 23]
    abstract hour: int
    /// Minute in range [0, 59]
    abstract minute: int
    /// Second in range [0, 59]
    abstract second: int
    /// Microsecond in range [0, 999999]
    abstract microsecond: int
    /// Fold value (0 or 1) for disambiguating wall-clock times that repeat during DST transitions
    abstract fold: int
    /// Return a string in ISO 8601 format, e.g. "14:30:00"
    /// See https://docs.python.org/3/library/datetime.html#datetime.time.isoformat
    abstract isoformat: unit -> string
    /// Return a string representing the time, formatted with format
    /// See https://docs.python.org/3/library/datetime.html#datetime.time.strftime
    abstract strftime: format: string -> string
    /// Return the UTC offset as a timedelta for aware times; None for naive times
    abstract utcoffset: unit -> timedelta option
    /// Return the timezone abbreviation string for aware times; None for naive times
    abstract tzname: unit -> string option

/// Static factory for time instances
[<Import("time", "datetime")>]
type timeStatic =
    /// Create a time-of-day value; all arguments default to 0
    /// See https://docs.python.org/3/library/datetime.html#datetime.time
    [<Emit("$0($1...)")>]
    [<NamedParams>]
    abstract Create:
        ?hour: int *
        ?minute: int *
        ?second: int *
        ?microsecond: int ->
            time
    /// Return a time from a string in ISO 8601 format
    /// See https://docs.python.org/3/library/datetime.html#datetime.time.fromisoformat
    abstract fromisoformat: time_string: string -> time
    /// The earliest representable time, time(0, 0, 0, 0)
    abstract min: time
    /// The latest representable time, time(23, 59, 59, 999999)
    abstract max: time

/// Factory for creating time-of-day values
[<Import("time", "datetime")>]
let time: timeStatic = nativeOnly

// ============================================================================
// datetime
// ============================================================================

/// A naive or aware date and time (year, month, day, hour, minute, second, microsecond, tzinfo).
/// See https://docs.python.org/3/library/datetime.html#datetime.datetime
[<Import("datetime", "datetime")>]
type datetime =
    /// Year in range [MINYEAR, MAXYEAR]
    abstract year: int
    /// Month in range [1, 12]
    abstract month: int
    /// Day in range [1, number of days in the month and year]
    abstract day: int
    /// Hour in range [0, 23]
    abstract hour: int
    /// Minute in range [0, 59]
    abstract minute: int
    /// Second in range [0, 59]
    abstract second: int
    /// Microsecond in range [0, 999999]
    abstract microsecond: int
    /// Fold value (0 or 1) for disambiguating wall-clock times that repeat during DST transitions
    abstract fold: int
    /// Return the date part as a date object
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.date
    abstract date: unit -> date
    /// Return the time part as a time object (tzinfo is not included)
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.time
    abstract time: unit -> time
    /// Return a string in ISO 8601 format, e.g. "2026-04-21T14:30:00"
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.isoformat
    abstract isoformat: unit -> string
    /// Return a string in ISO 8601 format with a custom separator between date and time
    [<Emit("$0.isoformat(sep=$1)")>]
    abstract isoformat: sep: string -> string
    /// Return a string representing the datetime, formatted with format
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.strftime
    abstract strftime: format: string -> string
    /// Return the POSIX timestamp corresponding to this datetime as a float
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.timestamp
    abstract timestamp: unit -> float
    /// Return the UTC offset as a timedelta for aware datetimes; None for naive
    abstract utcoffset: unit -> timedelta option
    /// Return the timezone abbreviation string for aware datetimes; None for naive
    abstract tzname: unit -> string option
    /// Return the day of the week as an integer; Monday is 0 and Sunday is 6
    abstract weekday: unit -> int
    /// Return the day of the week as an integer; Monday is 1 and Sunday is 7
    abstract isoweekday: unit -> int
    /// Return a datetime with the date fields replaced
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.replace
    [<Emit("$0.replace(year=int($1), month=int($2), day=int($3))")>]
    abstract replaceDate: year: int * month: int * day: int -> datetime
    /// Return a datetime with the time fields replaced
    [<Emit("$0.replace(hour=int($1), minute=int($2), second=int($3))")>]
    abstract replaceTime: hour: int * minute: int * second: int -> datetime
    /// Return a datetime converted to the given timezone
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.astimezone
    abstract astimezone: tz: obj -> datetime

/// Static factory for datetime instances
[<Import("datetime", "datetime")>]
type datetimeStatic =
    /// Create a datetime for the given date components (time components default to 0)
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime
    [<Emit("$0(int($1), int($2), int($3))")>]
    abstract Create: year: int * month: int * day: int -> datetime
    /// Create a datetime for the given date and time components
    [<Emit("$0(int($1), int($2), int($3), int($4), int($5), int($6))")>]
    abstract Create: year: int * month: int * day: int * hour: int * minute: int * second: int -> datetime
    /// Return the current local date and time
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.now
    abstract now: unit -> datetime
    /// Return the current local date and time in the given timezone
    [<Emit("$0.now(tz=$1)")>]
    abstract now: tz: obj -> datetime
    /// Return the current UTC date and time as a naive datetime
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.utcnow
    abstract utcnow: unit -> datetime
    /// Return the local datetime corresponding to a POSIX timestamp
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.fromtimestamp
    abstract fromtimestamp: timestamp: float -> datetime
    /// Return the datetime corresponding to a POSIX timestamp in the given timezone
    [<Emit("$0.fromtimestamp($1, tz=$2)")>]
    abstract fromtimestamp: timestamp: float * tz: obj -> datetime
    /// Return a datetime from a string in any valid ISO 8601 format
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.fromisoformat
    abstract fromisoformat: datetime_string: string -> datetime
    /// Return a datetime parsed from date_string according to format
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.strptime
    abstract strptime: date_string: string * format: string -> datetime
    /// Combine a date and time into a single datetime
    /// See https://docs.python.org/3/library/datetime.html#datetime.datetime.combine
    abstract combine: date: date * time: time -> datetime
    /// The earliest representable datetime
    abstract min: datetime
    /// The latest representable datetime
    abstract max: datetime

/// Factory for creating datetime values
[<Import("datetime", "datetime")>]
let datetime: datetimeStatic = nativeOnly

// ============================================================================
// timezone
// ============================================================================

/// A fixed-offset implementation of tzinfo.
/// See https://docs.python.org/3/library/datetime.html#datetime.timezone
[<Import("timezone", "datetime")>]
type timezone =
    /// Return the UTC offset for this timezone
    abstract utcoffset: dt: obj -> timedelta
    /// Return the timezone name string for this timezone
    abstract tzname: dt: obj -> string

/// Static factory for timezone instances
[<Import("timezone", "datetime")>]
type timezoneStatic =
    /// Create a timezone with the given fixed UTC offset (as a timedelta)
    /// See https://docs.python.org/3/library/datetime.html#datetime.timezone
    [<Emit("$0($1)")>]
    abstract Create: offset: timedelta -> timezone
    /// Create a named timezone with the given fixed UTC offset
    [<Emit("$0($1, $2)")>]
    abstract Create: offset: timedelta * name: string -> timezone
    /// The UTC timezone singleton (offset zero, name "UTC")
    abstract utc: timezone

/// Factory for creating timezone values
[<Import("timezone", "datetime")>]
let timezone: timezoneStatic = nativeOnly
