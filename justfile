# Fable.Python build commands
# Install just: https://github.com/casey/just

set dotenv-load

build_path := "build"
src_path := "src"
test_path := "test"

# Development mode: use local Fable repo instead of dotnet tool
# Usage: just dev=true test-python
dev := "false"
fable_repo := justfile_directory() / "../Fable"
fable := if dev == "true" { "dotnet run --project " + fable_repo / "src/Fable.Cli" + " --" } else { "dotnet fable" }

# Default recipe - show available commands
default:
    @just --list

# Clean Fable build output (preserves dotnet obj/bin directories)
clean:
    rm -rf {{build_path}}
    rm -rf {{src_path}}/obj {{src_path}}/bin
    rm -rf {{test_path}}/obj {{test_path}}/bin
    rm -rf examples/*/build examples/*/obj examples/*/bin
    rm -rf examples/*/src/obj examples/*/src/bin
    rm -rf examples/*/.fable examples/*/src/.fable
    rm -rf .fable

# Deep clean - removes everything including dotnet obj/bin directories
clean-all: clean
    rm -rf {{src_path}}/obj {{test_path}}/obj
    rm -rf {{src_path}}/bin {{test_path}}/bin

# Build F# source to Python using Fable
build: clean
    mkdir -p {{build_path}}
    dotnet build {{src_path}}
    {{fable}} {{src_path}} --lang Python --outDir {{build_path}}

# Compile F# source using dotnet (without Fable transpilation)
run: clean
    dotnet build {{src_path}}

# Run all tests (native .NET and Python)
test: build
    dotnet build {{test_path}}
    @echo "Running native .NET tests..."
    dotnet run --project {{test_path}}
    @echo "Compiling and running Python tests..."
    {{fable}} {{test_path}} --lang Python --outDir {{build_path}}/tests
    uv run pytest {{build_path}}/tests

# Run only native .NET tests
test-native:
    dotnet build {{test_path}}
    dotnet run --project {{test_path}}

# Run only Python tests (requires build first)
test-python: build
    {{fable}} {{test_path}} --lang Python --outDir {{build_path}}/tests
    uv run pytest {{build_path}}/tests

# Create NuGet package
pack: build
    dotnet pack {{src_path}} -c Release

# Create NuGet package with specific version (used in CI)
# Note: FileVersion must be numeric-only (e.g., 5.0.0.0), so we don't set it for prerelease versions
pack-version version:
    dotnet pack {{src_path}} -c Release -p:PackageVersion={{version}} -p:InformationalVersion={{version}}

# Format code with Fantomas
format:
    dotnet fantomas {{src_path}} -r
    dotnet fantomas {{test_path}} -r

# Check code formatting without making changes
format-check:
    dotnet fantomas {{src_path}} -r --check
    dotnet fantomas {{test_path}} -r --check

# Install .NET tools (Fable, Fantomas) and Python dependencies
setup:
    dotnet tool restore
    uv sync

# Restore all dependencies
restore:
    dotnet paket install
    dotnet restore {{src_path}}
    dotnet restore {{test_path}}

# Install Python dependencies with uv
install-python:
    uv sync

# Watch for changes and rebuild (useful during development)
watch:
    {{fable}} watch {{src_path}} --lang Python --outDir {{build_path}}

# --- Examples ---

# Run Flask example
example-flask:
    cd examples/flask/src && {{fable}} . --lang Python --outDir ../build
    cd examples/flask/build && uv run --group flask flask --app app run

# Run TimeFlies example
example-timeflies:
    cd examples/timeflies && {{fable}} . --lang Python --outDir build
    cd examples/timeflies/build && uv run --group examples python program.py

# Run Django example
example-django:
    cd examples/django && {{fable}} . --lang Python --outDir build
    cd examples/django/build && uv run --group django python manage.py runserver

# Run Django minimal example
example-django-minimal:
    cd examples/django-minimal && {{fable}} . --lang Python --outDir build
    cd examples/django-minimal/build && uv run --group django python program.py

# Run FastAPI example
example-fastapi:
    cd examples/fastapi && {{fable}} . --lang Python --outDir build
    cd examples/fastapi/build && uv run --group fastapi uvicorn app:app --reload

# Run FastAPI example in dev mode (watch for F# changes and auto-reload)
dev-fastapi:
    cd examples/fastapi && {{fable}} . --lang Python --outDir build --watch & cd examples/fastapi/build && sleep 3 && uv run --group fastapi uvicorn app:app --reload

# Run Pydantic example (importing Python Pydantic models from F#)
example-pydantic:
    cd examples/pydantic && {{fable}} . --lang Python --outDir build
    cd examples/pydantic/build && uv run python app.py
