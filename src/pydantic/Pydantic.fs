/// Pydantic bindings for F# to Python data validation and serialization
///
/// Usage: Create Pydantic models by inheriting from BaseModel and using
/// the ClassAttributes attribute:
///
/// ```fsharp
/// [<Py.ClassAttributes(style=Py.ClassAttributeStyle.Attributes, init=false)>]
/// type User(name: string, age: int, email: string option) =
///     inherit BaseModel()
///     member val Name: string = name with get, set
///     member val Age: int = age with get, set
///     member val Email: string option = email with get, set
/// ```
module Fable.Python.Pydantic

open Fable.Core

// fsharplint:disable MemberNames,InterfaceNames

// ============================================================================
// Field Configuration
// ============================================================================

/// Erased type for Pydantic Field values
[<Erase>]
type Field<'T> = 'T

/// Field helper functions for creating Pydantic field configurations
module Field =
    /// Create a field marked as frozen (immutable)
    [<Import("Field", "pydantic")>]
    [<Emit("$0(frozen=$1)")>]
    let Frozen (_frozen: bool) : Field<'T> = nativeOnly

    /// Create a field with a default value
    [<Import("Field", "pydantic")>]
    [<Emit("$0(default=$1)")>]
    let Default (_value: 'T) : Field<'T> = nativeOnly

    /// Create a field with a default factory function
    [<Import("Field", "pydantic")>]
    [<Emit("$0(default_factory=$1)")>]
    let DefaultFactory (_factory: unit -> 'T) : Field<'T> = nativeOnly

    /// Create a field with an alias
    [<Import("Field", "pydantic")>]
    [<Emit("$0(alias=$1)")>]
    let Alias (_alias: string) : Field<'T> = nativeOnly

    /// Create a field with a title
    [<Import("Field", "pydantic")>]
    [<Emit("$0(title=$1)")>]
    let Title (_title: string) : Field<'T> = nativeOnly

    /// Create a field with a description
    [<Import("Field", "pydantic")>]
    [<Emit("$0(description=$1)")>]
    let Description (_description: string) : Field<'T> = nativeOnly

    /// Create a field with gt (greater than) constraint
    [<Import("Field", "pydantic")>]
    [<Emit("$0(gt=$1)")>]
    let Gt (_value: float) : Field<'T> = nativeOnly

    /// Create a field with ge (greater than or equal) constraint
    [<Import("Field", "pydantic")>]
    [<Emit("$0(ge=$1)")>]
    let Ge (_value: float) : Field<'T> = nativeOnly

    /// Create a field with lt (less than) constraint
    [<Import("Field", "pydantic")>]
    [<Emit("$0(lt=$1)")>]
    let Lt (_value: float) : Field<'T> = nativeOnly

    /// Create a field with le (less than or equal) constraint
    [<Import("Field", "pydantic")>]
    [<Emit("$0(le=$1)")>]
    let Le (_value: float) : Field<'T> = nativeOnly

    /// Create a field with min_length constraint
    [<Import("Field", "pydantic")>]
    [<Emit("$0(min_length=$1)")>]
    let MinLength (_length: int) : Field<'T> = nativeOnly

    /// Create a field with max_length constraint
    [<Import("Field", "pydantic")>]
    [<Emit("$0(max_length=$1)")>]
    let MaxLength (_length: int) : Field<'T> = nativeOnly

    /// Create a field with pattern (regex) constraint
    [<Import("Field", "pydantic")>]
    [<Emit("$0(pattern=$1)")>]
    let Pattern (_pattern: string) : Field<'T> = nativeOnly

    /// Create a field with multiple constraints
    [<Import("Field", "pydantic")>]
    [<Emit("$0(default=$1, ge=$2, le=$3)")>]
    let WithRange (_defaultValue: 'T) (_ge: float) (_le: float) : Field<'T> = nativeOnly

    /// Create a field with default and description
    [<Import("Field", "pydantic")>]
    [<Emit("$0(default=$1, description=$2)")>]
    let WithDescription (_defaultValue: 'T) (_description: string) : Field<'T> = nativeOnly

// ============================================================================
// BaseModel Class
// ============================================================================

/// Pydantic BaseModel base class - inherit from this for Pydantic models
[<Import("BaseModel", "pydantic")>]
type BaseModel() =
    /// Convert model to dictionary
    [<Emit("$0.model_dump()")>]
    member _.model_dump() : obj = nativeOnly

    /// Convert model to JSON string
    [<Emit("$0.model_dump_json()")>]
    member _.model_dump_json() : string = nativeOnly

    /// Convert model to JSON string with indentation
    [<Emit("$0.model_dump_json(indent=$1)")>]
    member _.model_dump_json_indented(_indent: int) : string = nativeOnly

    /// Create a copy of the model
    [<Emit("$0.model_copy()")>]
    member _.model_copy() : BaseModel = nativeOnly

    /// Create a copy of the model with updated values
    [<Emit("$0.model_copy(update=$1)")>]
    member _.model_copy_with(_update: obj) : BaseModel = nativeOnly

    /// Get model fields info (class property)
    [<Emit("type($0).model_fields")>]
    member _.model_fields: obj = nativeOnly

    /// Generate JSON schema
    [<Emit("type($0).model_json_schema()")>]
    member _.model_json_schema() : obj = nativeOnly

    /// Get model configuration
    [<Emit("type($0).model_config")>]
    member _.model_config: obj = nativeOnly

    /// Validate data and create a model instance (class method called via instance)
    [<Emit("type($0).model_validate($1)")>]
    member _.model_validate(_data: obj) : BaseModel = nativeOnly

    /// Validate JSON string and create a model instance (class method called via instance)
    [<Emit("type($0).model_validate_json($1)")>]
    member _.model_validate_json(_json: string) : BaseModel = nativeOnly

    /// Create a model instance without validation (for performance)
    [<Emit("type($0).model_construct(**$1)")>]
    member _.model_construct(_data: obj) : BaseModel = nativeOnly

// ============================================================================
// ConfigDict - Model Configuration
// ============================================================================

/// Options for extra field handling
[<StringEnum>]
type ExtraFieldsMode =
    | [<CompiledName("forbid")>] Forbid
    | [<CompiledName("allow")>] Allow
    | [<CompiledName("ignore")>] Ignore

/// ConfigDict helper type for model configuration
type ConfigDictBuilder =
    /// Create a ConfigDict with extra fields mode
    [<Import("ConfigDict", "pydantic")>]
    [<Emit("$0(extra=$1)")>]
    static member WithExtra(_extra: string) : obj = nativeOnly

    /// Create a ConfigDict with strict mode
    [<Import("ConfigDict", "pydantic")>]
    [<Emit("$0(strict=$1)")>]
    static member WithStrict(_strict: bool) : obj = nativeOnly

    /// Create a ConfigDict with frozen mode
    [<Import("ConfigDict", "pydantic")>]
    [<Emit("$0(frozen=$1)")>]
    static member WithFrozen(_frozen: bool) : obj = nativeOnly

    /// Create a ConfigDict with validate_assignment
    [<Import("ConfigDict", "pydantic")>]
    [<Emit("$0(validate_assignment=$1)")>]
    static member WithValidateAssignment(_validate: bool) : obj = nativeOnly

    /// Create a ConfigDict with arbitrary_types_allowed
    [<Import("ConfigDict", "pydantic")>]
    [<Emit("$0(arbitrary_types_allowed=$1)")>]
    static member WithArbitraryTypesAllowed(_allowed: bool) : obj = nativeOnly

    /// Create a ConfigDict with from_attributes
    [<Import("ConfigDict", "pydantic")>]
    [<Emit("$0(from_attributes=$1)")>]
    static member WithFromAttributes(_fromAttrs: bool) : obj = nativeOnly

// ============================================================================
// Validation Error Handling
// ============================================================================

/// Pydantic ValidationError
[<Import("ValidationError", "pydantic")>]
type ValidationError() =
    /// Get error count
    [<Emit("$0.error_count()")>]
    member _.error_count() : int = nativeOnly

    /// Get errors as list
    [<Emit("$0.errors()")>]
    member _.errors() : obj array = nativeOnly

    /// Get errors as JSON string
    [<Emit("$0.json()")>]
    member _.json() : string = nativeOnly

// ============================================================================
// Functional Validators (for use with Annotated types)
// ============================================================================

/// Field validator decorator - use this to generate the import for @field_validator
/// This is a placeholder to ensure the import is generated.
/// Use with: [<Py.Decorate("""field_validator('FieldName')""")>]
[<Import("field_validator", "pydantic")>]
let pydantic_field_validator: obj = nativeOnly

/// BeforeValidator - runs before Pydantic's own validation
/// Usage with Python's Annotated: Annotated[str, BeforeValidator(my_func)]
[<Import("BeforeValidator", "pydantic")>]
[<Emit("$0($1)")>]
let BeforeValidator (_validator: 'T -> 'T) : obj = nativeOnly

/// AfterValidator - runs after Pydantic's own validation
/// Usage with Python's Annotated: Annotated[str, AfterValidator(my_func)]
[<Import("AfterValidator", "pydantic")>]
[<Emit("$0($1)")>]
let AfterValidator (_validator: 'T -> 'T) : obj = nativeOnly

/// PlainValidator - replaces Pydantic's validation entirely
[<Import("PlainValidator", "pydantic")>]
[<Emit("$0($1)")>]
let PlainValidator (_validator: obj -> 'T) : obj = nativeOnly

/// WrapValidator - wraps Pydantic's validation with custom logic
[<Import("WrapValidator", "pydantic")>]
[<Emit("$0($1)")>]
let WrapValidator (_validator: obj -> (obj -> 'T) -> 'T) : obj = nativeOnly

// ============================================================================
// TypeAdapter - for validating non-model types
// ============================================================================

/// TypeAdapter for validating arbitrary types without a full model
/// Can be used with Annotated types and validators
[<Import("TypeAdapter", "pydantic")>]
type TypeAdapter<'T>(_type: obj) =
    /// Validate data and return typed result
    [<Emit("$0.validate_python($1)")>]
    member _.validate_python(_data: obj) : 'T = nativeOnly

    /// Validate JSON string and return typed result
    [<Emit("$0.validate_json($1)")>]
    member _.validate_json(_json: string) : 'T = nativeOnly

    /// Dump to Python object
    [<Emit("$0.dump_python($1)")>]
    member _.dump_python(_value: 'T) : obj = nativeOnly

    /// Dump to JSON string
    [<Emit("$0.dump_json($1)")>]
    member _.dump_json(_value: 'T) : string = nativeOnly

    /// Get JSON schema
    [<Emit("$0.json_schema()")>]
    member _.json_schema() : obj = nativeOnly

// ============================================================================
// Field Information Types
// ============================================================================

/// Field information type
type FieldInfo =
    abstract annotation: obj
    abstract ``default``: obj option
    abstract is_required: unit -> bool

// ============================================================================
// Pydantic V2 Types
// ============================================================================

// --- String Types ---

/// Email string type with validation
[<Import("EmailStr", "pydantic")>]
type EmailStr = string

/// Name email string (e.g., "John Doe <john@example.com>")
[<Import("NameEmail", "pydantic")>]
type NameEmail = string

// --- URL Types ---

/// HTTP URL type with validation
[<Import("HttpUrl", "pydantic")>]
type HttpUrl = string

/// Any URL type with validation
[<Import("AnyUrl", "pydantic")>]
type AnyUrl = string

/// Any HTTP URL type
[<Import("AnyHttpUrl", "pydantic")>]
type AnyHttpUrl = string

/// File URL type (file://)
[<Import("FileUrl", "pydantic")>]
type FileUrl = string

/// FTP URL type
[<Import("FtpUrl", "pydantic")>]
type FtpUrl = string

/// WebSocket URL type
[<Import("WebsocketUrl", "pydantic")>]
type WebsocketUrl = string

// --- Numeric Types ---

/// Positive integer type (> 0)
[<Import("PositiveInt", "pydantic")>]
type PositiveInt = int

/// Negative integer type (< 0)
[<Import("NegativeInt", "pydantic")>]
type NegativeInt = int

/// Non-negative integer type (>= 0)
[<Import("NonNegativeInt", "pydantic")>]
type NonNegativeInt = int

/// Non-positive integer type (<= 0)
[<Import("NonPositiveInt", "pydantic")>]
type NonPositiveInt = int

/// Positive float type (> 0)
[<Import("PositiveFloat", "pydantic")>]
type PositiveFloat = float

/// Negative float type (< 0)
[<Import("NegativeFloat", "pydantic")>]
type NegativeFloat = float

/// Non-negative float type (>= 0)
[<Import("NonNegativeFloat", "pydantic")>]
type NonNegativeFloat = float

/// Non-positive float type (<= 0)
[<Import("NonPositiveFloat", "pydantic")>]
type NonPositiveFloat = float

/// Finite float type (not inf or -inf)
[<Import("FiniteFloat", "pydantic")>]
type FiniteFloat = float

// --- Strict Types (no coercion) ---

/// Strict integer - only accepts int, not bool or float
[<Import("StrictInt", "pydantic")>]
type StrictInt = int

/// Strict float - only accepts float, not int
[<Import("StrictFloat", "pydantic")>]
type StrictFloat = float

/// Strict string - only accepts str
[<Import("StrictStr", "pydantic")>]
type StrictStr = string

/// Strict bool - only accepts bool, not int
[<Import("StrictBool", "pydantic")>]
type StrictBool = bool

/// Strict bytes - only accepts bytes
[<Import("StrictBytes", "pydantic")>]
type StrictBytes = byte array

// --- Secret Types ---

/// Secret string - hidden in logs/repr, use get_secret_value() to access
[<Import("SecretStr", "pydantic")>]
type SecretStr() =
    [<Emit("$0.get_secret_value()")>]
    member _.get_secret_value() : string = nativeOnly

/// Secret bytes - hidden in logs/repr, use get_secret_value() to access
[<Import("SecretBytes", "pydantic")>]
type SecretBytes() =
    [<Emit("$0.get_secret_value()")>]
    member _.get_secret_value() : byte array = nativeOnly

// --- UUID Types ---

/// UUID version 1
[<Import("UUID1", "pydantic")>]
type UUID1 = string

/// UUID version 3
[<Import("UUID3", "pydantic")>]
type UUID3 = string

/// UUID version 4 (random)
[<Import("UUID4", "pydantic")>]
type UUID4 = string

/// UUID version 5
[<Import("UUID5", "pydantic")>]
type UUID5 = string

// --- Path Types ---

/// File path that must exist
[<Import("FilePath", "pydantic")>]
type FilePath = string

/// Directory path that must exist
[<Import("DirectoryPath", "pydantic")>]
type DirectoryPath = string

/// Path that must not exist (for creating new files)
[<Import("NewPath", "pydantic")>]
type NewPath = string

// --- Network Types ---

/// IP address (v4 or v6)
[<Import("IPvAnyAddress", "pydantic")>]
type IPvAnyAddress = string

/// IP interface (v4 or v6)
[<Import("IPvAnyInterface", "pydantic")>]
type IPvAnyInterface = string

/// IP network (v4 or v6)
[<Import("IPvAnyNetwork", "pydantic")>]
type IPvAnyNetwork = string

// --- Other Types ---

/// Base64 encoded bytes
[<Import("Base64Bytes", "pydantic")>]
type Base64Bytes = byte array

/// Base64 encoded string
[<Import("Base64Str", "pydantic")>]
type Base64Str = string

/// JSON type - validates JSON string and parses it
[<Import("Json", "pydantic")>]
type JsonValue = obj

