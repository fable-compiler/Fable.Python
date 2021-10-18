# Fable Python on BBC micro:bit

Write your F# program in `src/App.fs`.

Note that the Fable Library is not supported on the micro:bit so it's
very limited what we can do. Parts of Fable Library needs to
be ported to work with MicroPython see `util.fs` as an example.

The micro:bit have a flat file-system so all files needs to be in the
same top-level directory. For more information see
https://microbit-micropython.readthedocs.io/en/latest/tutorials/storage.html

## Install Dependecies

To flash the microbit we use
[`uFlash`](https://uflash.readthedocs.io/en/latest/) to flash the app
and [`MicroFS`](https://microfs.readthedocs.io/en/latest/) to transfer
any Fable Libary (modified) files.

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

