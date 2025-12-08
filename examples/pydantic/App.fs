/// Example showing how to use Pydantic models imported from Python.
///
/// This demonstrates:
/// 1. Importing Pydantic models defined in Python
/// 2. Creating instances of those models
/// 3. Accessing model properties
/// 4. Using Pydantic's validation and serialization
module App

open Models

// ============================================================================
// Using imported Pydantic models
// ============================================================================

// Create a user using the helper function
let user1 = User.create 1 "Alice" (Some "alice@example.com") (Some 30)
let user2 = User.create 2 "Bob" None None

// Create a product
let product1 =
    Product.create
        1
        "Laptop"
        "High-performance laptop"
        1299.99
        true
        [| "electronics"; "computers" |]

// Access properties (type-safe!)
printfn "User 1: %s (id=%d)" user1.name user1.id
printfn "User 1 email: %A" user1.email
printfn "User 1 age: %A" user1.age

printfn ""
printfn "User 2: %s (id=%d)" user2.name user2.id
printfn "User 2 email: %A" user2.email

printfn ""
printfn "Product: %s - $%.2f" product1.name product1.price
printfn "In stock: %b" product1.in_stock
printfn "Tags: %A" product1.tags

// Modify properties
user1.email <- Some "alice.updated@example.com"
printfn ""
printfn "Updated email: %A" user1.email

// Create a request model
let createRequest = CreateUserRequest.create "Charlie" (Some "charlie@example.com") (Some 25)
printfn ""
printfn "Create request for: %s" createRequest.name
