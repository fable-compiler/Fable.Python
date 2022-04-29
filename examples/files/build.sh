#!/usr/bin/env sh
set -eux
dotnet fable-py src/Files.fsproj --outDir ./src
chmod +x src/program.py
python3 src/program.py
