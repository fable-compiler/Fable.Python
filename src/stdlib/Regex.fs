/// Type bindings for Python re (regular expressions) module: https://docs.python.org/3/library/re.html
module Fable.Python.Regex

open System.Collections.Generic
open Fable.Core

// fsharplint:disable MemberNames

// ============================================================================
// Flags
// ============================================================================

/// Compile flags for re functions. Combine with bitwise OR (|||).
/// See https://docs.python.org/3/library/re.html#flags
module Flags =
    /// No flags.
    [<Literal>]
    let NOFLAG = 0

    /// Case-insensitive matching. Short alias: I.
    /// See https://docs.python.org/3/library/re.html#re.IGNORECASE
    [<Literal>]
    let IGNORECASE = 2

    /// Case-insensitive matching. Alias for IGNORECASE.
    [<Literal>]
    let I = 2

    /// Make ^ match at the beginning and $ at the end of each line. Short alias: M.
    /// See https://docs.python.org/3/library/re.html#re.MULTILINE
    [<Literal>]
    let MULTILINE = 8

    /// Make ^ match at the beginning and $ at the end of each line. Alias for MULTILINE.
    [<Literal>]
    let M = 8

    /// Make . match any character including newline. Short alias: S.
    /// See https://docs.python.org/3/library/re.html#re.DOTALL
    [<Literal>]
    let DOTALL = 16

    /// Make . match any character including newline. Alias for DOTALL.
    [<Literal>]
    let S = 16

    /// Restrict \w, \d, \s etc. to ASCII characters only. Short alias: A.
    /// See https://docs.python.org/3/library/re.html#re.ASCII
    [<Literal>]
    let ASCII = 256

    /// Restrict \w, \d, \s etc. to ASCII characters only. Alias for ASCII.
    [<Literal>]
    let A = 256

    /// Make \w, \W etc. locale-dependent (rarely needed). Short alias: L.
    /// See https://docs.python.org/3/library/re.html#re.LOCALE
    [<Literal>]
    let LOCALE = 4

    /// Make \w, \W etc. locale-dependent. Alias for LOCALE.
    [<Literal>]
    let L = 4

    /// Unicode character matching for \w, \W etc. (default in Python 3). Short alias: U.
    /// See https://docs.python.org/3/library/re.html#re.UNICODE
    [<Literal>]
    let UNICODE = 32

    /// Unicode character matching for \w, \W etc. Alias for UNICODE.
    [<Literal>]
    let U = 32

    /// Allow whitespace and comments in the pattern. Short alias: X.
    /// See https://docs.python.org/3/library/re.html#re.VERBOSE
    [<Literal>]
    let VERBOSE = 64

    /// Allow whitespace and comments in the pattern. Alias for VERBOSE.
    [<Literal>]
    let X = 64

// ============================================================================
// Match object
// ============================================================================

/// A match object returned by re.match(), re.search(), re.fullmatch(), and Pattern methods.
/// See https://docs.python.org/3/library/re.html#match-objects
[<Import("Match", "re")>]
type Match() =
    /// The string passed to match() or search().
    member _.string: string = nativeOnly

    /// The value of pos passed to match() or search().
    member _.pos: int = nativeOnly

    /// The value of endpos passed to match() or search().
    member _.endpos: int = nativeOnly

    /// The integer index of the last matched capturing group, or None if no group was matched.
    member _.lastindex: int option = nativeOnly

    /// The name of the last matched capturing group, or None if no named group was matched.
    member _.lastgroup: string option = nativeOnly

    /// Return the string matched by the whole expression (group 0).
    /// See https://docs.python.org/3/library/re.html#re.Match.group
    member _.group() : string = nativeOnly

    /// Return the string matched by a numbered capturing group (1-based).
    /// Returns None if the group exists but did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.group
    [<Emit("$0.group(int($1))")>]
    member _.group(group: int) : string option = nativeOnly

    /// Return the string matched by a named capturing group.
    /// Returns None if the group exists but did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.group
    [<Emit("$0.group($1)")>]
    member _.group(group: string) : string option = nativeOnly

    /// Return a tuple of all subgroup strings (groups 1..N).
    /// Groups that did not participate in the match appear as None.
    /// See https://docs.python.org/3/library/re.html#re.Match.groups
    member _.groups() : string option[] = nativeOnly

    /// Return a tuple of all subgroup strings, substituting defaultValue for groups
    /// that did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.groups
    [<Emit("$0.groups(default=$1)")>]
    member _.groups(defaultValue: string) : string[] = nativeOnly

    /// Return a dictionary mapping group names to matched strings.
    /// Groups that did not participate in the match map to None.
    /// See https://docs.python.org/3/library/re.html#re.Match.groupdict
    member _.groupdict() : Dictionary<string, string option> = nativeOnly

    /// Return a dictionary mapping group names to matched strings,
    /// substituting defaultValue for groups that did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.groupdict
    [<Emit("$0.groupdict(default=$1)")>]
    member _.groupdict(defaultValue: string) : Dictionary<string, string> = nativeOnly

    /// Return the start position of the whole match in the original string.
    /// See https://docs.python.org/3/library/re.html#re.Match.start
    member _.start() : int = nativeOnly

    /// Return the start position of a numbered capturing group.
    /// Returns -1 if the group exists but did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.start
    [<Emit("$0.start(int($1))")>]
    member _.start(group: int) : int = nativeOnly

    /// Return the start position of a named capturing group.
    /// Returns -1 if the group exists but did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.start
    [<Emit("$0.start($1)")>]
    member _.start(group: string) : int = nativeOnly

    /// Return the end position (exclusive) of the whole match in the original string.
    /// See https://docs.python.org/3/library/re.html#re.Match.end
    [<Emit("$0.end()")>]
    member _.``end``() : int = nativeOnly

    /// Return the end position (exclusive) of a numbered capturing group.
    /// Returns -1 if the group exists but did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.end
    [<Emit("$0.end(int($1))")>]
    member _.``end``(group: int) : int = nativeOnly

    /// Return the end position (exclusive) of a named capturing group.
    /// Returns -1 if the group exists but did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.end
    [<Emit("$0.end($1)")>]
    member _.``end``(group: string) : int = nativeOnly

    /// Return the (start, end) span of the whole match as a tuple.
    /// See https://docs.python.org/3/library/re.html#re.Match.span
    member _.span() : int * int = nativeOnly

    /// Return the (start, end) span of a numbered capturing group.
    /// Both values are -1 if the group did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.span
    [<Emit("$0.span(int($1))")>]
    member _.span(group: int) : int * int = nativeOnly

    /// Return the (start, end) span of a named capturing group.
    /// Both values are -1 if the group did not participate in the match.
    /// See https://docs.python.org/3/library/re.html#re.Match.span
    [<Emit("$0.span($1)")>]
    member _.span(group: string) : int * int = nativeOnly

    /// Return the string obtained by doing backslash substitution on the template string.
    /// See https://docs.python.org/3/library/re.html#re.Match.expand
    member _.expand(template: string) : string = nativeOnly

// ============================================================================
// Pattern object
// ============================================================================

/// A compiled regular expression object returned by re.compile().
/// See https://docs.python.org/3/library/re.html#regular-expression-objects
[<Import("Pattern", "re")>]
type Pattern() =
    /// The pattern string from which this pattern object was compiled.
    member _.pattern: string = nativeOnly

    /// The flags used when the pattern was compiled (an integer).
    member _.flags: int = nativeOnly

    /// The number of capturing groups in the pattern.
    member _.groups: int = nativeOnly

    /// A dictionary mapping group names to their group number.
    member _.groupindex: Dictionary<string, int> = nativeOnly

    /// Try to match the pattern at the beginning of string.
    /// Returns None if the pattern does not match.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.match
    member _.``match``(string: string) : Match option = nativeOnly

    /// Try to match the pattern at the beginning of string, starting at pos.
    /// Returns None if the pattern does not match.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.match
    [<Emit("$0.match($1, int($2))")>]
    member _.``match``(string: string, pos: int) : Match option = nativeOnly

    /// Scan through string looking for the first location where the pattern produces a match.
    /// Returns None if no position in the string matches.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.search
    member _.search(string: string) : Match option = nativeOnly

    /// Scan through string looking for the first location where the pattern produces a match,
    /// starting at pos.
    /// Returns None if no position in the string matches.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.search
    [<Emit("$0.search($1, int($2))")>]
    member _.search(string: string, pos: int) : Match option = nativeOnly

    /// Try to match the pattern against all of the string.
    /// Returns None if the pattern does not match.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.fullmatch
    member _.fullmatch(string: string) : Match option = nativeOnly

    /// Return all non-overlapping matches of the pattern in string as a list of strings.
    /// If the pattern has groups, return a list of groups; if multiple groups, a list of tuples.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.findall
    member _.findall(string: string) : string[] = nativeOnly

    /// Return an iterator yielding Match objects for all non-overlapping matches of the pattern.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.finditer
    member _.finditer(string: string) : Match seq = nativeOnly

    /// Return the string obtained by replacing the leftmost (or all, if count=0) non-overlapping
    /// occurrences of the pattern in string with repl.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.sub
    member _.sub(repl: string, string: string) : string = nativeOnly

    /// Return the string obtained by replacing up to count occurrences of the pattern.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.sub
    [<Emit("$0.sub($1, $2, count=int($3))")>]
    member _.sub(repl: string, string: string, count: int) : string = nativeOnly

    /// Like sub(), but return a tuple (new_string, number_of_subs_made).
    /// See https://docs.python.org/3/library/re.html#re.Pattern.subn
    member _.subn(repl: string, string: string) : string * int = nativeOnly

    /// Like sub(), but return a tuple (new_string, number_of_subs_made), up to count substitutions.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.subn
    [<Emit("$0.subn($1, $2, count=int($3))")>]
    member _.subn(repl: string, string: string, count: int) : string * int = nativeOnly

    /// Split string by occurrences of the pattern.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.split
    member _.split(string: string) : string[] = nativeOnly

    /// Split string by occurrences of the pattern, with at most maxsplit splits.
    /// See https://docs.python.org/3/library/re.html#re.Pattern.split
    [<Emit("$0.split($1, maxsplit=int($2))")>]
    member _.split(string: string, maxsplit: int) : string[] = nativeOnly

// ============================================================================
// Module-level functions
// ============================================================================

[<Erase>]
type IExports =
    // ========================================================================
    // Compile
    // ========================================================================

    /// Compile a regular expression pattern into a Pattern object.
    /// See https://docs.python.org/3/library/re.html#re.compile
    abstract compile: pattern: string -> Pattern

    /// Compile a regular expression pattern with flags into a Pattern object.
    /// See https://docs.python.org/3/library/re.html#re.compile
    [<Emit("$0.compile($1, $2)")>]
    abstract compile: pattern: string * flags: int -> Pattern

    // ========================================================================
    // Matching
    // ========================================================================

    /// Try to match the pattern at the beginning of string.
    /// Returns None if the pattern does not match.
    /// See https://docs.python.org/3/library/re.html#re.match
    abstract ``match``: pattern: string * string: string -> Match option

    /// Try to match the pattern at the beginning of string, using the given flags.
    /// Returns None if the pattern does not match.
    /// See https://docs.python.org/3/library/re.html#re.match
    [<Emit("$0.match($1, $2, $3)")>]
    abstract ``match``: pattern: string * string: string * flags: int -> Match option

    /// Scan through string looking for the first location where the pattern produces a match.
    /// Returns None if no position in the string matches.
    /// See https://docs.python.org/3/library/re.html#re.search
    abstract search: pattern: string * string: string -> Match option

    /// Scan through string looking for the first location where the pattern produces a match,
    /// using the given flags.
    /// Returns None if no position in the string matches.
    /// See https://docs.python.org/3/library/re.html#re.search
    [<Emit("$0.search($1, $2, $3)")>]
    abstract search: pattern: string * string: string * flags: int -> Match option

    /// Try to match the pattern against all of the string.
    /// Returns None if the pattern does not match the entire string.
    /// See https://docs.python.org/3/library/re.html#re.fullmatch
    abstract fullmatch: pattern: string * string: string -> Match option

    /// Try to match the pattern against all of the string, using the given flags.
    /// Returns None if the pattern does not match the entire string.
    /// See https://docs.python.org/3/library/re.html#re.fullmatch
    [<Emit("$0.fullmatch($1, $2, $3)")>]
    abstract fullmatch: pattern: string * string: string * flags: int -> Match option

    // ========================================================================
    // Finding all matches
    // ========================================================================

    /// Return all non-overlapping matches of pattern in string as a list of strings.
    /// If the pattern has groups, return a list of groups; if multiple groups, a list of tuples.
    /// See https://docs.python.org/3/library/re.html#re.findall
    abstract findall: pattern: string * string: string -> string[]

    /// Return all non-overlapping matches of pattern in string as a list of strings, with flags.
    /// See https://docs.python.org/3/library/re.html#re.findall
    [<Emit("$0.findall($1, $2, $3)")>]
    abstract findall: pattern: string * string: string * flags: int -> string[]

    /// Return an iterator yielding Match objects for all non-overlapping matches of pattern in string.
    /// See https://docs.python.org/3/library/re.html#re.finditer
    abstract finditer: pattern: string * string: string -> Match seq

    /// Return an iterator yielding Match objects for all non-overlapping matches of pattern in string,
    /// with flags.
    /// See https://docs.python.org/3/library/re.html#re.finditer
    [<Emit("$0.finditer($1, $2, $3)")>]
    abstract finditer: pattern: string * string: string * flags: int -> Match seq

    // ========================================================================
    // Substitution
    // ========================================================================

    /// Return the string obtained by replacing the leftmost non-overlapping occurrences of pattern
    /// in string with repl. repl can be a string or a callable.
    /// See https://docs.python.org/3/library/re.html#re.sub
    abstract sub: pattern: string * repl: string * string: string -> string

    /// Return the string obtained by replacing up to count non-overlapping occurrences of pattern
    /// in string with repl.
    /// See https://docs.python.org/3/library/re.html#re.sub
    [<Emit("$0.sub($1, $2, $3, count=int($4))")>]
    abstract sub: pattern: string * repl: string * string: string * count: int -> string

    /// Like sub(), but return a tuple (new_string, number_of_subs_made).
    /// See https://docs.python.org/3/library/re.html#re.subn
    abstract subn: pattern: string * repl: string * string: string -> string * int

    /// Like sub(), but return a tuple (new_string, number_of_subs_made), up to count substitutions.
    /// See https://docs.python.org/3/library/re.html#re.subn
    [<Emit("$0.subn($1, $2, $3, count=int($4))")>]
    abstract subn: pattern: string * repl: string * string: string * count: int -> string * int

    // ========================================================================
    // Splitting
    // ========================================================================

    /// Split string by the occurrences of pattern.
    /// If pattern contains capturing groups, the text of all groups are also returned.
    /// See https://docs.python.org/3/library/re.html#re.split
    abstract split: pattern: string * string: string -> string[]

    /// Split string by the occurrences of pattern, with at most maxsplit splits.
    /// See https://docs.python.org/3/library/re.html#re.split
    [<Emit("$0.split($1, $2, maxsplit=int($3))")>]
    abstract split: pattern: string * string: string * maxsplit: int -> string[]

    // ========================================================================
    // Utilities
    // ========================================================================

    /// Return string with all non-alphanumeric characters backslash-escaped.
    /// This is useful to match a literal string that may contain special regex characters.
    /// See https://docs.python.org/3/library/re.html#re.escape
    abstract escape: pattern: string -> string

    /// Clear the regular expression cache. Rarely needed.
    /// See https://docs.python.org/3/library/re.html#re.purge
    abstract purge: unit -> unit

/// Python's re module: regular expression operations.
/// See https://docs.python.org/3/library/re.html
[<ImportAll("re")>]
let re: IExports = nativeOnly
