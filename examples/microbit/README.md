# Fable Python on BBC micro:bit

Write your F# program in `src/App.fs`.

Note that the Fable Library is not supported so it's very limited what
you can do. The needed parts of fable library need to be ported to work
with MicroPython see `util.fs` as an example.

The micro:bit have a flat file-system so all files needs to be in the
same top-level directory.

## Install

These tools are needed by the Build script:

```sh
pip install uflash
pip install microfs

dotnet tool restore
dotnet restore
```

## Build

```sh
dotnet run Build
```

## Flash to MicroBit

```sh
dotnet run Flash
```

## Copy Fable Library

Flashing will not copy the Fable Library (just `util.py` for now), so it
needs to be transferred separately:

```sh
dotnet run FableLibrary
```

