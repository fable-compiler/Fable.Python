# Giraffe.Python

## Build

To build giraffe.python, run:

```console
> poetry install
> dotnet fable --lang python
```

Building Giraffe.Python may require the very latest Fable compiler. You
can build against the latest version of Fable by running e.g:

```console
> dotnet run --project ..\..\..\Fable\src\Fable.Cli --lang Python
```

Remember to build fable library first if needed e.g run in Fable
directory:

```console
> dotnet fsi build.fsx library-py
```

## Running

```console
> poetry run uvicorn program:app  --port "8080" --workers 20

```
