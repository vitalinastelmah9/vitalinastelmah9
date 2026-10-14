#!/usr/bin/env bash
# A real Roslyn compile of the generated C# against the Hub editor's own reference
# assemblies. The Editor is never opened - only its dlls and csc are used.
#
# This editor (6000.5.10f1) ships no ProjectTemplates/libcache, so UGUI / TMP are built
# from the BuiltInPackages SOURCE as three assemblies with their REAL names, in order:
# TextCore's internals are exposed with InternalsVisibleTo("Unity.TextMeshPro"), so
# folding them into one assembly produces ~30 CS0122 in vendor code.
#
# Every path handed to csc goes through `cygpath -m` first: csc is a native Windows
# program and resolves a POSIX /c/... path against the current drive root (C:\c\...).
set -u
cd "$(dirname "$0")/.."
PROJ="$(cygpath -m "$(pwd)")"
ED="C:/Program Files/Unity/Hub/Editor/6000.5.10f1/Editor/Data"
CSC="$ED/DotNetSdk/sdk/8.0.318/Roslyn/bincore/csc.dll"
DOTNET="$ED/DotNetSdk/dotnet.exe"
UGUI="$ED/Resources/PackageManager/BuiltInPackages/com.unity.ugui/Runtime"
OUT="$PROJ/.gen/check"
mkdir -p "$(pwd)/.gen/check"
w() { cygpath -m "$1"; }

# netstandard2.1 facades, not the 4.7.1-api profile: UGUI's InputField.cs calls
# string.Contains(char), which only exists from netstandard2.1 onward.
refs_corlib() {
  printf -- '-r:"%s"\n' "$ED/NetStandard/ref/2.1.0/netstandard.dll"
}
refs_engine() {
  for f in "$ED/Managed/UnityEngine"/UnityEngine*.dll; do
    case "$(basename "$f")" in UnityEngine.dll) continue;; esac
    printf -- '-r:"%s"\n' "$(w "$f")"
  done
  printf -- '-r:"%s"\n' "$ED/Managed/UnityEngine/Unity.Scripting.dll"
}

build_vendor() {
  local name="$1"; shift
  local rsp="$(pwd)/.gen/check/$name.rsp"
  {
    echo '-nostdlib'
    echo '-nologo'
    echo '-preferreduilang:en-US'
    echo '-target:library'
    echo '-langversion:9.0'
    echo '-unsafe+'
    echo '-nowarn:0169,0649,0414,0618,0108,0162,0219,0067,0672,0809,0282,0436'
    printf -- '-out:"%s"\n' "$OUT/$name.dll"
    refs_corlib
    refs_engine
    for extra in "$@"; do
      case "$extra" in SRC:*) :;; *) printf -- '-r:"%s"\n' "$OUT/$extra.dll";; esac
    done
    for extra in "$@"; do
      case "$extra" in
        SRC:*) find "${extra#SRC:}" -name '*.cs' -not -name '._*' | while read -r s; do printf '"%s"\n' "$(w "$s")"; done;;
      esac
    done
  } > "$rsp"
  "$DOTNET" "$CSC" "@$(w "$rsp")" > ".gen/check/$name.log" 2>&1
  local rc=$?
  echo "[vendor] $name rc=$rc errors=$(grep -ci 'error' ".gen/check/$name.log" || true)"
  [ "$rc" = "0" ] || { grep -i error ".gen/check/$name.log" | head -15; return 1; }
}

if [ ! -f ".gen/check/Unity.TextMeshPro.dll" ] || [ "${REBUILD_VENDOR:-0}" = "1" ]; then
  build_vendor Unity.InternalAPIEngineBridge.004 "SRC:$UGUI/InternalBridge" || exit 1
  build_vendor UnityEngine.UI Unity.InternalAPIEngineBridge.004 "SRC:$UGUI/UGUI" || exit 1
  build_vendor Unity.TextMeshPro Unity.InternalAPIEngineBridge.004 UnityEngine.UI "SRC:$UGUI/TMP" || exit 1
fi

# ── the project itself ──────────────────────────────────────────────────────
RSP="$(pwd)/.gen/check/args.rsp"
{
  echo '-nostdlib'
  echo '-nologo'
  echo '-preferreduilang:en-US'
  echo '-target:library'
  echo '-langversion:9.0'
  echo '-nowarn:0649,0169,0414'
  printf -- '-out:"%s"\n' "$OUT/Generated.dll"
  refs_corlib
  refs_engine
  printf -- '-r:"%s"\n' "$OUT/Unity.InternalAPIEngineBridge.004.dll"
  printf -- '-r:"%s"\n' "$OUT/UnityEngine.UI.dll"
  printf -- '-r:"%s"\n' "$OUT/Unity.TextMeshPro.dll"
  for s in Assets/Scripts/Generated/*.cs; do printf '"%s"\n' "$PROJ/$s"; done
  printf '"%s"\n' "$PROJ/Assets/Scripts/PanelsFlow.cs"
  for extra in ${EXTRA_SOURCES:-}; do printf '"%s"\n' "$extra"; done
  echo ''
} > "$RSP"

"$DOTNET" "$CSC" "@$(w "$RSP")" > .gen/check/project.log 2>&1
RC=$?
echo "[project] rc=$RC  'error' occurrences=$(grep -ci error .gen/check/project.log || true)"
grep -i error .gen/check/project.log | head -40
ls -la .gen/check/Generated.dll 2>/dev/null
exit $RC
