/// Type bindings for Python base64 module: https://docs.python.org/3/library/base64.html
module Fable.Python.Base64

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    /// Encode bytes-like object using Base64
    /// See https://docs.python.org/3/library/base64.html#base64.b64encode
    abstract b64encode: s: byte[] -> byte[]
    /// Encode bytes-like object using Base64 with alternative characters
    /// See https://docs.python.org/3/library/base64.html#base64.b64encode
    abstract b64encode: s: byte[] * altchars: byte[] -> byte[]

    /// Decode Base64 encoded bytes
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: byte[] -> byte[]
    /// Decode Base64 encoded string
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: string -> byte[]
    /// Decode Base64 encoded bytes with alternative characters
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: byte[] * altchars: byte[] -> byte[]
    /// Decode Base64 encoded string with alternative characters
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: string * altchars: byte[] -> byte[]
    /// Decode Base64 encoded bytes with validation
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: byte[] * altchars: byte[] * validate: bool -> byte[]
    /// Decode Base64 encoded string with validation
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: string * altchars: byte[] * validate: bool -> byte[]
    /// Decode Base64 encoded bytes with validation
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: byte[] * validate: bool -> byte[]
    /// Decode Base64 encoded string with validation
    /// See https://docs.python.org/3/library/base64.html#base64.b64decode
    abstract b64decode: s: string * validate: bool -> byte[]

    /// Encode bytes using standard Base64 alphabet
    /// See https://docs.python.org/3/library/base64.html#base64.standard_b64encode
    abstract standard_b64encode: s: byte[] -> byte[]
    /// Decode bytes using standard Base64 alphabet
    /// See https://docs.python.org/3/library/base64.html#base64.standard_b64decode
    abstract standard_b64decode: s: string -> byte[]
    /// Decode bytes using standard Base64 alphabet
    /// See https://docs.python.org/3/library/base64.html#base64.standard_b64decode
    abstract standard_b64decode: s: byte[] -> byte[]
    /// Encode bytes using URL-safe Base64 alphabet
    /// See https://docs.python.org/3/library/base64.html#base64.urlsafe_b64encode
    abstract urlsafe_b64encode: s: byte[] -> byte[]
    /// Decode string using URL-safe Base64 alphabet
    /// See https://docs.python.org/3/library/base64.html#base64.urlsafe_b64decode
    abstract urlsafe_b64decode: s: string -> byte[]
    /// Decode bytes using URL-safe Base64 alphabet
    /// See https://docs.python.org/3/library/base64.html#base64.urlsafe_b64decode
    abstract urlsafe_b64decode: s: byte[] -> byte[]
    /// Encode bytes using Base32
    /// See https://docs.python.org/3/library/base64.html#base64.b32encode
    abstract b32encode: s: byte[] -> byte[]
    /// Encode bytes using Base16
    /// See https://docs.python.org/3/library/base64.html#base64.b16encode
    abstract b16encode: s: byte[] -> byte[]

/// Base16, Base32, Base64, Base85 Data Encodings
[<ImportAll("base64")>]
let base64: IExports = nativeOnly
