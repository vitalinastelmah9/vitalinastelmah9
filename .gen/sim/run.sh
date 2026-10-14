#!/usr/bin/env bash
# Compile the SHIPPING rules files (no engine types outside #if B_LOGS) and RUN them,
# so the design document's C.5 / C.11 claims are measured rather than asserted.
#
# No shim and no restatement of the rules: a compile failure here is itself the signal
# that an engine type leaked into the rules layer.
#
# Run on the editor's own Mono rather than its dotnet: the bundled .NET host refuses to
# resolve its own shared framework when it is driven from outside the editor
# ("No frameworks were found" with a correct .NET location), and Mono needs no
# runtimeconfig at all.
set -eu
cd "$(dirname "$0")/../.."
PROJ="$(cygpath -m "$(pwd)")"
ED="C:/Program Files/Unity/Hub/Editor/6000.5.10f1/Editor/Data"
CSC="$ED/DotNetSdk/sdk/8.0.318/Roslyn/bincore/csc.dll"
DOTNET="$ED/DotNetSdk/dotnet.exe"
MONO="$ED/MonoBleedingEdge/bin/mono.exe"
API="$ED/MonoBleedingEdge/lib/mono/4.7.1-api"
OUT="$PROJ/.gen/sim"
mkdir -p .gen/sim

RSP=".gen/sim/args.rsp"
{
  echo '-nostdlib'
  echo '-nologo'
  echo '-preferreduilang:en-US'
  echo '-target:exe'
  echo '-langversion:9.0'
  echo '-nowarn:0649'
  printf -- '-out:"%s"\n' "$OUT/sim.exe"
  printf -- '-r:"%s"\n' "$API/mscorlib.dll"
  for f in "$API"/System*.dll; do printf -- '-r:"%s"\n' "$(cygpath -m "$f")"; done
  printf -- '-r:"%s"\n' "$API/Facades/netstandard.dll"
  for s in GalleryRng RelicCatalog HallCatalog ReelStripGenerator SpinResolver GalleryEconomy; do
    printf '"%s"\n' "$PROJ/Assets/Scripts/Generated/$s.cs"
  done
  printf '"%s"\n' "$PROJ/.gen/sim/Sim.cs"
  echo ''
} > "$RSP"

"$DOTNET" "$CSC" "@$(cygpath -m "$RSP")"
"$MONO" .gen/sim/sim.exe
