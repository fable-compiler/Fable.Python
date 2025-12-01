# Fable.Python build commands
# Install just: https://github.com/casey/just

set dotenv-load

build_path := "build"
src_path := "src"
test_path := "test"

# Default recipe - show available commands
default:
    @just --list

# Clean build artifacts
clean:
    rm -rf {{build_path}}
    rm -rf {{src_path}}/obj {{test_path}}/obj
    rm -rf {{src_path}}/bin {{test_path}}/bin

# Build F# source to Python using Fable
build: clean
    mkdir -p {{build_path}}
    dotnet fable {{src_path}} --lang Python --outDir {{build_path}}

# Compile F# source using dotnet (without Fable transpilation)
run: clean
    dotnet build {{src_path}}

# Run all tests (native .NET and Python)
test: build
    dotnet build {{test_path}}
    @echo "Running native .NET tests..."
    dotnet run --project {{test_path}}
    @echo "Compiling and running Python tests..."
    dotnet fable {{test_path}} --lang Python --outDir {{build_path}}/tests
    uv run pytest {{build_path}}/tests

# Run only native .NET tests
test-native:
    dotnet build {{test_path}}
    dotnet run --project {{test_path}}

# Run only Python tests (requires build first)
test-python: build
    dotnet fable {{test_path}} --lang Python --outDir {{build_path}}/tests
    uv run pytest {{build_path}}/tests

# Create NuGet package
pack: build
    dotnet pack {{src_path}} -c Release

# Create NuGet package with specific version (used in CI)
pack-version version:
    dotnet pack {{src_path}} -c Release -p:PackageVersion={{version}} -p:FileVersion={{version}} -p:InformationalVersion={{version}}

# Format code with Fantomas
format:
    dotnet fantomas {{src_path}} -r
    dotnet fantomas {{test_path}} -r

# Check code formatting without making changes
format-check:
    dotnet fantomas {{src_path}} -r --check
    dotnet fantomas {{test_path}} -r --check

# Restore all dependencies
restore:
    dotnet tool restore
    dotnet restore {{src_path}}
    dotnet restore {{test_path}}

# Install Python dependencies with uv
install-python:
    uv sync

# Full setup: restore .NET and Python dependencies
setup: restore install-python

# Watch for changes and rebuild (useful during development)
watch:
    dotnet fable watch {{src_path}} --lang Python --outDir {{build_path}}
