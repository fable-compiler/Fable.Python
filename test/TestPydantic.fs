module Fable.Python.Tests.TestPydantic

open Fable.Python.Testing

#if FABLE_COMPILER
open Fable.Core
open Fable.Core.PyInterop
open Fable.Python.Pydantic

// Note: Constructor parameter names must be PascalCase to match Pydantic field names.
// Fable converts camelCase to snake_case, but Pydantic expects exact field name match.

// ============================================================================
// Basic Pydantic Model - Simple types
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type SimpleUser(Name: string, Age: int) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Age: int = Age with get, set

[<Fact>]
let ``test Simple Pydantic model can be created`` () =
    let user = SimpleUser(Name = "Alice", Age = 30)
    user.Name |> equal "Alice"
    user.Age |> equal 30

[<Fact>]
let ``test Simple Pydantic model can be modified`` () =
    let user = SimpleUser(Name = "Alice", Age = 30)
    user.Name <- "Bob"
    user.Age <- 25
    user.Name |> equal "Bob"
    user.Age |> equal 25

// ============================================================================
// Pydantic Model with optional fields
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type UserWithOptional(Name: string, Email: string option) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Email: string option = Email with get, set

[<Fact>]
let ``test Pydantic model with Some optional field`` () =
    let user = UserWithOptional(Name = "Alice", Email = Some "alice@example.com")
    user.Name |> equal "Alice"
    user.Email |> equal (Some "alice@example.com")

[<Fact>]
let ``test Pydantic model with None optional field`` () =
    let user = UserWithOptional(Name = "Bob", Email = None)
    user.Name |> equal "Bob"
    user.Email |> equal None

// ============================================================================
// Pydantic Model with Field defaults
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type UserWithDefaults(Name: string, Active: bool) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Active: bool = Field.Default(true) with get, set

[<Fact>]
let ``test Pydantic model with Field.Default`` () =
    let user = UserWithDefaults(Name = "Charlie", Active = false)
    user.Name |> equal "Charlie"

// ============================================================================
// Pydantic Model with frozen field
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type UserWithFrozenField(Id: string, Name: string) =
    inherit BaseModel()
    member val Id: string = Field.Frozen(true) with get, set
    member val Name: string = Name with get, set

[<Fact>]
let ``test Pydantic model with frozen field can be created`` () =
    let user = UserWithFrozenField(Id = "user-123", Name = "Dave")
    user.Name |> equal "Dave"

// ============================================================================
// Pydantic Model with numeric types (now with Pydantic schema support)
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type NumericModel(IntVal: int, FloatVal: float, Int64Val: int64) =
    inherit BaseModel()
    member val IntVal: int = IntVal with get, set
    member val FloatVal: float = FloatVal with get, set
    member val Int64Val: int64 = Int64Val with get, set

[<Fact>]
let ``test Pydantic model with int type`` () =
    let model = NumericModel(IntVal = 42, FloatVal = 3.14, Int64Val = 9999999999L)
    model.IntVal |> equal 42

[<Fact>]
let ``test Pydantic model with float type`` () =
    let model = NumericModel(IntVal = 42, FloatVal = 3.14, Int64Val = 9999999999L)
    model.FloatVal |> equal 3.14

[<Fact>]
let ``test Pydantic model with int64 type`` () =
    let model = NumericModel(IntVal = 42, FloatVal = 3.14, Int64Val = 9999999999L)
    model.Int64Val |> equal 9999999999L

// ============================================================================
// Pydantic Model with more numeric types
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type MoreNumericModel(ByteVal: byte, Int16Val: int16, DecimalVal: decimal) =
    inherit BaseModel()
    member val ByteVal: byte = ByteVal with get, set
    member val Int16Val: int16 = Int16Val with get, set
    member val DecimalVal: decimal = DecimalVal with get, set

[<Fact>]
let ``test Pydantic model with byte type`` () =
    let model = MoreNumericModel(ByteVal = 255uy, Int16Val = 1000s, DecimalVal = 99.99M)
    model.ByteVal |> equal 255uy

[<Fact>]
let ``test Pydantic model with int16 type`` () =
    let model = MoreNumericModel(ByteVal = 255uy, Int16Val = 1000s, DecimalVal = 99.99M)
    model.Int16Val |> equal 1000s

[<Fact>]
let ``test Pydantic model with decimal type`` () =
    let model = MoreNumericModel(ByteVal = 255uy, Int16Val = 1000s, DecimalVal = 99.99M)
    model.DecimalVal |> equal 99.99M

// ============================================================================
// Pydantic Model with list fields
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type ModelWithList(Name: string, Tags: string list) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Tags: string list = Tags with get, set

[<Fact>]
let ``test Pydantic model with list field`` () =
    let model = ModelWithList(Name = "Test", Tags = [ "tag1"; "tag2"; "tag3" ])
    model.Name |> equal "Test"
    model.Tags |> List.length |> equal 3
    model.Tags |> List.head |> equal "tag1"

[<Fact>]
let ``test Pydantic model with empty list field`` () =
    let model = ModelWithList(Name = "Test", Tags = [])
    model.Tags |> List.length |> equal 0

// ============================================================================
// Pydantic Model with nested models
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type Address(Street: string, City: string) =
    inherit BaseModel()
    member val Street: string = Street with get, set
    member val City: string = City with get, set

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type Person(Name: string, Address: Address) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Address: Address = Address with get, set

[<Fact>]
let ``test Pydantic model with nested model`` () =
    let address = Address(Street = "123 Main St", City = "Seattle")
    let person = Person(Name = "Eve", Address = address)
    person.Name |> equal "Eve"
    person.Address.Street |> equal "123 Main St"
    person.Address.City |> equal "Seattle"

// ============================================================================
// Pydantic Model serialization (model_dump_json)
// ============================================================================

[<Fact>]
let ``test Pydantic model_dump_json returns valid JSON`` () =
    let user = SimpleUser(Name = "Frank", Age = 40)
    let json = user.model_dump_json ()
    // Check that it contains expected fields
    json.Contains("Frank") |> equal true
    json.Contains("40") |> equal true

[<Fact>]
let ``test Pydantic model_dump returns object`` () =
    let user = SimpleUser(Name = "Grace", Age = 35)
    let dict = user.model_dump ()
    // The dump should be an object (Python dict)
    dict |> isNull |> equal false

// ============================================================================
// Pydantic Model validation (model_validate_json)
// ============================================================================

[<Fact>]
let ``test Pydantic model_validate_json parses JSON`` () =
    let template = SimpleUser(Name = "", Age = 0)

    let parsed =
        template.model_validate_json ("""{"Name": "Henry", "Age": 45}""") :?> SimpleUser

    parsed.Name |> equal "Henry"
    parsed.Age |> equal 45

[<Fact>]
let ``test Pydantic roundtrip dump and validate`` () =
    let original = SimpleUser(Name = "Ivy", Age = 28)
    let json = original.model_dump_json ()
    let restored = original.model_validate_json (json) :?> SimpleUser
    restored.Name |> equal "Ivy"
    restored.Age |> equal 28

// ============================================================================
// Pydantic Model with bool fields
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type SettingsModel(Enabled: bool, Visible: bool, Archived: bool) =
    inherit BaseModel()
    member val Enabled: bool = Enabled with get, set
    member val Visible: bool = Visible with get, set
    member val Archived: bool = Archived with get, set

[<Fact>]
let ``test Pydantic model with bool fields`` () =
    let settings = SettingsModel(Enabled = true, Visible = false, Archived = true)
    settings.Enabled |> equal true
    settings.Visible |> equal false
    settings.Archived |> equal true

// ============================================================================
// Pydantic Model inheriting from another Pydantic model
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type BaseEntity(Id: int) =
    inherit BaseModel()
    member val Id: int = Id with get, set

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type ExtendedEntity(Id: int, Name: string) =
    inherit BaseEntity(Id)
    member val Name: string = Name with get, set

[<Fact>]
let ``test Pydantic model inheritance`` () =
    let entity = ExtendedEntity(Id = 1, Name = "Entity1")
    entity.Id |> equal 1
    entity.Name |> equal "Entity1"

// ============================================================================
// Pydantic Model - Complex scenario with multiple field types
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type ComplexModel(Id: int, Name: string, Score: float, Email: string option, Tags: string list, Active: bool) =
    inherit BaseModel()
    member val Id: int = Id with get, set
    member val Name: string = Name with get, set
    member val Score: float = Score with get, set
    member val Email: string option = Email with get, set
    member val Tags: string list = Tags with get, set
    member val Active: bool = Active with get, set

[<Fact>]
let ``test Complex Pydantic model with multiple field types`` () =
    let model =
        ComplexModel(
            Id = 42,
            Name = "Complex",
            Score = 95.5,
            Email = Some "complex@example.com",
            Tags = [ "a"; "b"; "c" ],
            Active = true
        )

    model.Id |> equal 42
    model.Name |> equal "Complex"
    model.Score |> equal 95.5
    model.Email |> equal (Some "complex@example.com")
    model.Tags |> List.length |> equal 3
    model.Active |> equal true

// ============================================================================
// Test model_fields property
// ============================================================================

[<Fact>]
let ``test Pydantic model_fields property exists`` () =
    let user = SimpleUser(Name = "Test", Age = 25)
    let fields = user.model_fields
    fields |> isNull |> equal false

// ============================================================================
// Test model_json_schema method
// ============================================================================

[<Fact>]
let ``test Pydantic model_json_schema returns schema`` () =
    let user = SimpleUser(Name = "Test", Age = 25)
    let schema = user.model_json_schema ()
    schema |> isNull |> equal false

// ============================================================================
// Test model_config property
// ============================================================================

[<Fact>]
let ``test Pydantic model_config property exists`` () =
    let user = SimpleUser(Name = "Test", Age = 25)
    let config = user.model_config
    // Config should exist (may be empty dict by default)
    config |> isNull |> equal false

// ============================================================================
// Pydantic Model with field validator using Py.Decorate
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type UserWithValidator(Name: string, Age: int) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Age: int = Age with get, set

    [<Py.Decorate("""field_validator('Name')""")>]
    [<Py.ClassMethod>]
    static member validate_name(_cls: obj, v: string) : string = v.ToUpper()

// Reference to ensure field_validator gets imported
let private _fieldValidatorImport = pydantic_field_validator

[<Fact>]
let ``test Pydantic model with field validator`` () =
    let user = UserWithValidator(Name = "alice", Age = 30)
    // The validator should have upper-cased the name
    user.Name |> equal "ALICE"
    user.Age |> equal 30

#endif
