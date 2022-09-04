module Giraffe.Python.FormatExpressions

open System
open System.Text.RegularExpressions
open Microsoft.FSharp.Reflection
open FSharp.Core

// ---------------------------
// String matching functions
// ---------------------------

let private formatStringMap =
    let decodeSlashes (str: string) =
        // Kestrel has made the weird decision to
        // partially decode a route argument, which
        // means that a given route argument would get
        // entirely URL decoded except for '%2F' (/).
        // Hence decoding %2F must happen separately as
        // part of the string parsing function.
        //
        // For more information please check:
        // https://github.com/aspnet/Mvc/issues/4599
        str.Replace("%2F", "/").Replace("%2f", "/")

    let parseGuid (str: string) =
        match str.Length with
        | 22 -> ShortGuid.toGuid str
        | _ -> Guid str

    let guidPattern =
        "([0-9A-Fa-f]{8}\-[0-9A-Fa-f]{4}\-[0-9A-Fa-f]{4}\-[0-9A-Fa-f]{4}\-[0-9A-Fa-f]{12}|[0-9A-Fa-f]{32}|[-_0-9A-Za-z]{22})"

    let shortIdPattern = "([-_0-9A-Za-z]{10}[048AEIMQUYcgkosw])"

    dict
        [
          // Char    Regex                    Parser
          // -------------------------------------------------------------
          'b', ("(?i:(true|false)){1}", (fun (s: string) -> bool.Parse s) >> box) // bool
          'c', ("([^/]{1})", char >> box) // char
          's', ("([^/]+)", decodeSlashes >> box) // string
          'i', ("(-?\d+)", int32 >> box) // int
          'd', ("(-?\d+)", int64 >> box) // int64
          'f', ("(-?\d+\.{1}\d+)", float >> box) // float
          'O', (guidPattern, parseGuid >> box) // Guid
          'u', (shortIdPattern, ShortId.toUInt64 >> box) ] // uint64

type MatchMode =
    | Exact // Will try to match entire string from start to end.
    | StartsWith // Will try to match a substring. Subject string should start with test case.
    | EndsWith // Will try to match a substring. Subject string should end with test case.
    | Contains // Will try to match a substring. Subject string should contain test case.

type MatchOptions =
    { IgnoreCase: bool
      MatchMode: MatchMode }

    static member Exact =
        { IgnoreCase = false
          MatchMode = Exact }

    static member IgnoreCaseExact = { IgnoreCase = true; MatchMode = Exact }

let private convertToRegexPatternAndFormatChars (mode: MatchMode) (formatString: string) =
    let rec convert (chars: char list) =
        match chars with
        | '%' :: '%' :: tail ->
            let pattern, formatChars = convert tail
            "%" + pattern, formatChars
        | '%' :: c :: tail ->
            let pattern, formatChars = convert tail
            let regex, _ = formatStringMap.[c]
            regex + pattern, c :: formatChars
        | c :: tail ->
            let pattern, formatChars = convert tail
            c.ToString() + pattern, formatChars
        | [] -> "", []

    let inline formatRegex mode pattern =
        match mode with
        | Exact -> "^" + pattern + "$"
        | StartsWith -> "^" + pattern
        | EndsWith -> pattern + "$"
        | Contains -> pattern

    formatString
    |> List.ofSeq
    |> convert
    |> (fun (pattern, formatChars) -> formatRegex mode pattern, formatChars)

/// <summary>
/// Tries to parse an input string based on a given format string and return a tuple of all parsed arguments.
/// </summary>
/// <param name="format">The format string which shall be used for parsing.</param>
/// <param name="options">The options record with specifications on how the matching should behave.</param>
/// <param name="input">The input string from which the parsed arguments shall be extracted.</param>
/// <returns>Matched value as an option of 'T</returns>
let tryMatchInput (format: PrintfFormat<_, _, _, _, 'T>) (options: MatchOptions) (input: string) =
    try
        let pattern, formatChars =
            format.Value
            |> Regex.Escape
            |> convertToRegexPatternAndFormatChars options.MatchMode

        let options =
            match options.IgnoreCase with
            | true -> RegexOptions.IgnoreCase
            | false -> RegexOptions.None

        let result = Regex.Match(input, pattern, options)

        if result.Groups.Count <= 1 then
            None
        else
            let groups = result.Groups |> Seq.cast<Group> |> Seq.skip 1

            let values =
                (groups, formatChars)
                ||> Seq.map2 (fun g c ->
                    let _, parser = formatStringMap.[c]
                    let value = parser g.Value
                    value)
                |> Seq.toArray

            let result =
                match values.Length with
                | 1 -> values.[0]
                | _ ->
                    let types = values |> Array.map (fun v -> v.GetType())
                    let tupleType = FSharpType.MakeTupleType types
                    FSharpValue.MakeTuple(values, tupleType)

            result :?> 'T |> Some
    with _ ->
        None

/// <summary>
/// Tries to parse an input string based on a given format string and return a tuple of all parsed arguments.
/// </summary>
/// <param name="format">The format string which shall be used for parsing.</param>
/// <param name="ignoreCase">The flag to make matching case insensitive.</param>
/// <param name="input">The input string from which the parsed arguments shall be extracted.</param>
/// <returns>Matched value as an option of 'T</returns>
let tryMatchInputExact (format: PrintfFormat<_, _, _, _, 'T>) (ignoreCase: bool) (input: string) =
    let options =
        match ignoreCase with
        | true -> MatchOptions.IgnoreCaseExact
        | false -> MatchOptions.Exact

    tryMatchInput format options input


// ---------------------------
// Validation helper functions
// ---------------------------

/// **Description**
///
/// Validates if a given format string can be matched with a given tuple.
///
/// **Parameters**
///
/// `format`: The format string which shall be used for parsing.
///
/// **Output**
///
/// Returns `unit` if validation was successful otherwise will throw an `Exception`.
/// Returns `unit` if validation was successful otherwise will throw an `Exception`.
