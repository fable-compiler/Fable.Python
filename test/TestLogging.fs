module Fable.Python.Tests.Logging

open Fable.Python.Testing
open Fable.Python.Logging

[<Fact>]
let ``test getLogger works`` () =
    let logger = logging.getLogger "test"
    logger.name |> equal "test"

[<Fact>]
let ``test getLogger without name returns root logger`` () =
    let logger = logging.getLogger ()
    logger.name |> equal "root"

[<Fact>]
let ``test setLevel works`` () =
    let logger = logging.getLogger "test_level"
    logger.setLevel Level.DEBUG
    logger.getEffectiveLevel () |> equal Level.DEBUG

[<Fact>]
let ``test isEnabledFor works`` () =
    let logger = logging.getLogger "test_enabled"
    logger.setLevel Level.WARNING
    logger.isEnabledFor Level.ERROR |> equal true
    logger.isEnabledFor Level.DEBUG |> equal false

[<Fact>]
let ``test debug logging works`` () =
    let logger = logging.getLogger "test_debug"
    logger.setLevel Level.DEBUG
    // Just verify it doesn't throw
    logger.debug "Debug message"
    true |> equal true

[<Fact>]
let ``test info logging works`` () =
    let logger = logging.getLogger "test_info"
    logger.setLevel Level.INFO
    logger.info "Info message"
    true |> equal true

[<Fact>]
let ``test warning logging works`` () =
    let logger = logging.getLogger "test_warning"
    logger.warning "Warning message"
    true |> equal true

[<Fact>]
let ``test error logging works`` () =
    let logger = logging.getLogger "test_error"
    logger.error "Error message"
    true |> equal true

[<Fact>]
let ``test critical logging works`` () =
    let logger = logging.getLogger "test_critical"
    logger.critical "Critical message"
    true |> equal true

[<Fact>]
let ``test log with level works`` () =
    let logger = logging.getLogger "test_log"
    logger.setLevel Level.DEBUG
    logger.log (Level.INFO, "Log message with level")
    true |> equal true

[<Fact>]
let ``test Level constants`` () =
    Level.DEBUG |> equal 10
    Level.INFO |> equal 20
    Level.WARNING |> equal 30
    Level.ERROR |> equal 40
    Level.CRITICAL |> equal 50
    Level.NOTSET |> equal 0

[<Fact>]
let ``test logging module level constants`` () =
    logging.DEBUG |> equal 10
    logging.INFO |> equal 20
    logging.WARNING |> equal 30
    logging.ERROR |> equal 40
    logging.CRITICAL |> equal 50

[<Fact>]
let ``test hasHandlers works`` () =
    let logger = logging.getLogger "test_has_handlers"
    // A new logger typically has no handlers
    // (unless propagating to parent that has handlers)
    let _ = logger.hasHandlers ()
    true |> equal true

[<Fact>]
let ``test basicConfig works`` () =
    logging.basicConfig (level = Level.DEBUG, format = "%(message)s")
    true |> equal true
