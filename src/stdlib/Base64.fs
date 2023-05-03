module Fable.Python.Base64

open Fable.Core

// fsharplint:disable MemberNames

[<Erase>]
type IExports =
    abstract b64encode: byte[] -> byte[]
    abstract b64encode: s: byte[] * altchars: byte[] -> byte[]

    abstract b64decode: byte[] -> byte[]
    abstract b64decode: string -> byte[]
    abstract b64decode: s: byte[] * altchars: byte[] -> byte[]
    abstract b64decode: s: string * altchars: byte[] -> byte[]
    abstract b64decode: s: byte[] * altchars: byte[] * validate: bool -> byte[]
    abstract b64decode: s: string * altchars: byte[] * validate: bool -> byte[]
    abstract b64decode: s: byte[] * validate: bool -> byte[]
    abstract b64decode: s: string * validate: bool -> byte[]

    abstract standard_b64encode: byte[] -> byte[]
    abstract standard_b64decode: string -> byte[]
    abstract standard_b64decode: byte[] -> byte[]
    abstract urlsafe_b64encode: byte[] -> byte[]
    abstract urlsafe_b64decode: string -> byte[]
    abstract urlsafe_b64decode: byte[] -> byte[]
    abstract b32encode: byte[] -> byte[]
    abstract b16encode: byte[] -> byte

/// Base16, Base32, Base64, Base85 Data Encodings
[<ImportAll("base64")>]
let base64: IExports = nativeOnly
