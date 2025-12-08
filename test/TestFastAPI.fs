module Fable.Python.Tests.TestFastAPI

open Fable.Python.Tests.Util.Testing

#if FABLE_COMPILER
open Fable.Core
open Fable.Core.PyInterop
open Fable.Python.FastAPI
open Fable.Python.Pydantic

// Helper to check if object exists (not null in Python)
let inline notNull (x: obj) : bool = not (isNull x)

// ============================================================================
// FastAPI Application Tests
// ============================================================================

[<Fact>]
let ``test FastAPI app can be created`` () =
    let app = FastAPI()
    notNull app |> equal true

[<Fact>]
let ``test FastAPI app can be created with title`` () =
    let app = FastAPI(title = "My API")
    app.title |> equal "My API"

[<Fact>]
let ``test FastAPI app can be created with title and version`` () =
    let app = FastAPI(title = "My API", version = "1.0.0")
    app.title |> equal "My API"
    app.version |> equal "1.0.0"

[<Fact>]
let ``test FastAPI app can be created with description`` () =
    let app = FastAPI(title = "My API", description = "A test API")
    app.description |> equal "A test API"

// ============================================================================
// APIRouter Tests
// ============================================================================

[<Fact>]
let ``test APIRouter can be created`` () =
    let router = APIRouter()
    notNull router |> equal true

[<Fact>]
let ``test APIRouter can be created with prefix`` () =
    let router = APIRouter(prefix = "/api")
    notNull router |> equal true

[<Fact>]
let ``test APIRouter can be created with tags`` () =
    let router = APIRouter(tags = ResizeArray ["users"; "admin"])
    notNull router |> equal true

[<Fact>]
let ``test FastAPI app can include router`` () =
    let app = FastAPI()
    let router = APIRouter(prefix = "/api")
    app.include_router(router)
    // If we get here without error, the test passes
    true |> equal true

// ============================================================================
// HTTPException Tests
// ============================================================================

[<Fact>]
let ``test HTTPException can be created with status code`` () =
    let exc = HTTPException(404)
    notNull exc |> equal true

[<Fact>]
let ``test HTTPException can be created with status code and detail`` () =
    let exc = HTTPException(404, detail = "Not found")
    notNull exc |> equal true

// ============================================================================
// Response Classes Tests
// ============================================================================

[<Fact>]
let ``test JSONResponse can be created`` () =
    let response = JSONResponse({| message = "Hello" |})
    notNull response |> equal true

[<Fact>]
let ``test JSONResponse can be created with status code`` () =
    let response = JSONResponse({| message = "Created" |}, status_code = 201)
    notNull response |> equal true

[<Fact>]
let ``test HTMLResponse can be created`` () =
    let response = HTMLResponse("<h1>Hello</h1>")
    notNull response |> equal true

[<Fact>]
let ``test PlainTextResponse can be created`` () =
    let response = PlainTextResponse("Hello, World!")
    notNull response |> equal true

[<Fact>]
let ``test RedirectResponse can be created`` () =
    let response = RedirectResponse("/new-url")
    notNull response |> equal true

// ============================================================================
// Status Codes Tests
// ============================================================================

[<Fact>]
let ``test status codes are correct`` () =
    status.HTTP_200_OK |> equal 200
    status.HTTP_201_CREATED |> equal 201
    status.HTTP_204_NO_CONTENT |> equal 204
    status.HTTP_400_BAD_REQUEST |> equal 400
    status.HTTP_401_UNAUTHORIZED |> equal 401
    status.HTTP_403_FORBIDDEN |> equal 403
    status.HTTP_404_NOT_FOUND |> equal 404
    status.HTTP_500_INTERNAL_SERVER_ERROR |> equal 500

// ============================================================================
// BackgroundTasks Tests
// ============================================================================

[<Fact>]
let ``test BackgroundTasks can be created`` () =
    let tasks = BackgroundTasks()
    notNull tasks |> equal true

// ============================================================================
// Security Tests
// ============================================================================

[<Fact>]
let ``test OAuth2PasswordBearer can be created`` () =
    let oauth2_scheme = OAuth2PasswordBearer.Create("token")
    notNull oauth2_scheme |> equal true

[<Fact>]
let ``test HTTPBasic can be created`` () =
    let basic = HTTPBasic()
    notNull basic |> equal true

[<Fact>]
let ``test HTTPBearer can be created`` () =
    let bearer = HTTPBearer()
    notNull bearer |> equal true

[<Fact>]
let ``test APIKeyHeader can be created`` () =
    let api_key = APIKeyHeader(name = "X-API-Key")
    notNull api_key |> equal true

[<Fact>]
let ``test APIKeyQuery can be created`` () =
    let api_key = APIKeyQuery(name = "api_key")
    notNull api_key |> equal true

[<Fact>]
let ``test APIKeyCookie can be created`` () =
    let api_key = APIKeyCookie(name = "api_key")
    notNull api_key |> equal true

// ============================================================================
// Pydantic Model with FastAPI (integration)
// ============================================================================

[<Py.ClassAttributes(style = Py.ClassAttributeStyle.Attributes, init = false)>]
type Item(Name: string, Price: float, InStock: bool) =
    inherit BaseModel()
    member val Name: string = Name with get, set
    member val Price: float = Price with get, set
    member val InStock: bool = InStock with get, set

[<Fact>]
let ``test Pydantic model works with FastAPI patterns`` () =
    // This test verifies that Pydantic models can be used in FastAPI endpoints
    let item = Item(Name = "Widget", Price = 9.99, InStock = true)
    item.Name |> equal "Widget"
    item.Price |> equal 9.99
    item.InStock |> equal true

[<Fact>]
let ``test Pydantic model serialization for FastAPI`` () =
    let item = Item(Name = "Gadget", Price = 19.99, InStock = false)
    let json = item.model_dump_json()
    json.Contains("Gadget") |> equal true
    json.Contains("19.99") |> equal true

// ============================================================================
// Class-based API with decorators (the main pattern)
// ============================================================================

// Note: These tests verify the decorator pattern compiles correctly.
// Full endpoint testing requires running the server.

let app = FastAPI(title = "Test API", version = "1.0.0")

[<APIClass>]
type API() =
    [<Get("/")>]
    static member root() : obj =
        {| message = "Hello World" |}

    [<Get("/items/{item_id}")>]
    static member get_item(item_id: int) : obj =
        {| item_id = item_id; name = "Test Item" |}

    [<Post("/items")>]
    static member create_item(item: Item) : obj =
        {| status = "created"; item = item |}

    [<Put("/items/{item_id}")>]
    static member update_item(item_id: int, item: Item) : obj =
        {| item_id = item_id; item = item |}

    [<Delete("/items/{item_id}")>]
    static member delete_item(item_id: int) : obj =
        {| deleted = item_id |}

[<Fact>]
let ``test class-based API methods can be called`` () =
    let result = API.root()
    notNull result |> equal true

[<Fact>]
let ``test class-based API with path parameter`` () =
    let result = API.get_item(42)
    notNull result |> equal true

[<Fact>]
let ``test class-based API POST method`` () =
    let item = Item(Name = "Test", Price = 10.0, InStock = true)
    let result = API.create_item(item)
    notNull result |> equal true

[<Fact>]
let ``test class-based API PUT method`` () =
    let item = Item(Name = "Updated", Price = 20.0, InStock = false)
    let result = API.update_item(1, item)
    notNull result |> equal true

[<Fact>]
let ``test class-based API DELETE method`` () =
    let result = API.delete_item(1)
    notNull result |> equal true

// ============================================================================
// TestClient Tests
// ============================================================================

[<Fact>]
let ``test TestClient can be created`` () =
    let testApp = FastAPI()
    let client = TestClient(testApp)
    notNull client |> equal true

// ============================================================================
// Router-based API pattern
// ============================================================================

let router = APIRouter(prefix = "/users", tags = ResizeArray ["users"])

[<APIClass>]
type UsersAPI() =
    [<RouterGet("/")>]
    static member list_users() : obj =
        {| users = [| "Alice"; "Bob" |] |}

    [<RouterGet("/{user_id}")>]
    static member get_user(user_id: int) : obj =
        {| user_id = user_id |}

    [<RouterPost("/")>]
    static member create_user(name: string) : obj =
        {| name = name; id = 1 |}

    [<RouterPut("/{user_id}")>]
    static member update_user(user_id: int, name: string) : obj =
        {| user_id = user_id; name = name |}

    [<RouterDelete("/{user_id}")>]
    static member delete_user(user_id: int) : obj =
        {| deleted = user_id |}

[<Fact>]
let ``test router-based API methods work`` () =
    let users = UsersAPI.list_users()
    notNull users |> equal true

[<Fact>]
let ``test router can be included in app`` () =
    let mainApp = FastAPI()
    let usersRouter = APIRouter(prefix = "/api/v1")
    mainApp.include_router(usersRouter)
    true |> equal true

[<Fact>]
let ``test router can be included with prefix and tags`` () =
    let mainApp = FastAPI()
    let usersRouter = APIRouter(prefix = "/users", tags = ResizeArray ["users"])
    mainApp.include_router_with_prefix_and_tags(usersRouter, "/api/v1", ResizeArray ["api"])
    true |> equal true

#endif
