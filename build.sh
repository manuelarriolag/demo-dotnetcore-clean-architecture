#!/usr/bin/env bash
set -euo pipefail

# Build and publish artifacts expected by GreetingsApp/Dockerfile.
ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

if ! command -v dotnet >/dev/null 2>&1; then
  echo "dotnet CLI not found. Install .NET SDK before running this script." >&2
  exit 1
fi

cd "$ROOT_DIR"

dotnet restore
dotnet build

pushd "$ROOT_DIR/GreetingsApp" >/dev/null
rm -rf out
dotnet publish -c Release -o out
popd >/dev/null

echo "Build completed. Published artifacts are in GreetingsApp/out"