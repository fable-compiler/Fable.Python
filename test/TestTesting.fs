module Fable.Python.Tests.Testing

open Fable.Python.Testing

// ============================================================================
// Test that equal works correctly
// ============================================================================

[<Fact>]
let ``test equal passes for equal values`` () =
    equal 1 1
    equal "hello" "hello"
    equal true true
    equal [1; 2; 3] [1; 2; 3]

[<Fact>]
let ``test equal fails for unequal values`` () =
    // This tests that equal actually fails when values differ
    // We use throwsAnyError to verify the assertion throws
    throwsAnyError (fun () -> equal 1 2)

[<Fact>]
let ``test equal fails for unequal strings`` () =
    throwsAnyError (fun () -> equal "hello" "world")

[<Fact>]
let ``test equal fails for unequal booleans`` () =
    throwsAnyError (fun () -> equal true false)

// ============================================================================
// Test that notEqual works correctly
// ============================================================================

[<Fact>]
let ``test notEqual passes for unequal values`` () =
    notEqual 1 2
    notEqual "hello" "world"
    notEqual true false

[<Fact>]
let ``test notEqual fails for equal values`` () =
    throwsAnyError (fun () -> notEqual 1 1)

[<Fact>]
let ``test notEqual fails for equal strings`` () =
    throwsAnyError (fun () -> notEqual "hello" "hello")

// ============================================================================
// Test throwsAnyError
// ============================================================================

[<Fact>]
let ``test throwsAnyError passes when function throws`` () =
    throwsAnyError (fun () -> failwith "boom")

[<Fact>]
let ``test throwsAnyError fails when function does not throw`` () =
    // Meta-test: throwsAnyError should fail if the function doesn't throw
    throwsAnyError (fun () ->
        throwsAnyError (fun () -> 42)
    )

// ============================================================================
// Test doesntThrow
// ============================================================================

[<Fact>]
let ``test doesntThrow passes when function succeeds`` () =
    doesntThrow (fun () -> 1 + 1)

[<Fact>]
let ``test doesntThrow fails when function throws`` () =
    throwsAnyError (fun () ->
        doesntThrow (fun () -> failwith "boom")
    )

// ============================================================================
// Test throwsError with exact message
// ============================================================================

[<Fact>]
let ``test throwsError passes with matching message`` () =
    throwsError "exact error" (fun () -> failwith "exact error")

[<Fact>]
let ``test throwsError fails with wrong message`` () =
    throwsAnyError (fun () ->
        throwsError "expected message" (fun () -> failwith "different message")
    )

[<Fact>]
let ``test throwsError fails when no error thrown`` () =
    throwsAnyError (fun () ->
        throwsError "expected error" (fun () -> 42)
    )

// ============================================================================
// Test throwsErrorContaining
// ============================================================================

[<Fact>]
let ``test throwsErrorContaining passes when message contains substring`` () =
    throwsErrorContaining "partial" (fun () -> failwith "this is a partial match error")

[<Fact>]
let ``test throwsErrorContaining fails when message does not contain substring`` () =
    throwsAnyError (fun () ->
        throwsErrorContaining "notfound" (fun () -> failwith "different error message")
    )

[<Fact>]
let ``test throwsErrorContaining fails when no error thrown`` () =
    throwsAnyError (fun () ->
        throwsErrorContaining "error" (fun () -> 42)
    )
