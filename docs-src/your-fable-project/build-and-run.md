# Build & run

Your project is ready? Then it's time to build and run it.

## Setup environment

```console
> dotnet new tool-manifest
> dotnet tool install fable --prerelease
```

## Build

If you are buliding an F# script:

```console
> dotnet fable --lang py project.fsx
```

If you are buliding an F# project:

```console
> dotnet fable --lang py project.fsproj
```

## Run

```console
> python project.py
```
