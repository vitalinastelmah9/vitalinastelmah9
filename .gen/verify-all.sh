#!/usr/bin/env bash
# Everything, in one run: the pipeline's own gates plus the four checks it does not
# make (reference resolution, rect overlap, a real compile, and the rules engine
# actually played). Each of the four has a canary proving it still fires.
set -u
cd "$(dirname "$0")/.."
PROJ="$(pwd)"
WS=/c/AndroidWorkspace
fails=0
run() {
  local name="$1"; shift
  local out rc
  out="$("$@" 2>&1)"; rc=$?
  if [ "$rc" = "0" ]; then
    printf '  ok    %-28s %s\n' "$name" "$(printf '%s' "$out" | grep -E '^\[|^OK|^\{' | tail -1 | cut -c1-96)"
  else
    printf '  FAIL  %-28s rc=%s\n' "$name" "$rc"
    printf '%s\n' "$out" | tail -12 | sed 's/^/        /'
    fails=$((fails + 1))
  fi
}

echo "== pipeline gates"
for v in verify-unity-scene verify-unity-buttons verify-unity-layout verify-ui-draw-order \
         verify-game-scene verify-cs-ambiguity verify-text-size verify-canvas-order \
         verify-unity-splash-slider verify-sprite-order verify-unity-aspect; do
  run "$v" bash "$WS/scripts/$v.sh" "$PROJ"
done
run verify-preprocessor-balance bash "$WS/scripts/verify-preprocessor-balance.sh" "$PROJ/Assets/Scripts"

echo "== checks the pipeline does not make"
run template      node .gen/template-intact.js
run audit         node .gen/audit.js Assets/Scenes/Loader.unity Assets/Scenes/Games/2_GameScene.unity
run layout        node .gen/layout-check.js Assets/Scenes/Loader.unity Assets/Scenes/Games/2_GameScene.unity
run final-scan    node .gen/final-scan.js
run typecheck     bash .gen/typecheck.sh
run rules-engine  bash .gen/sim/run.sh

echo
if [ "$fails" = "0" ]; then echo "ALL GREEN"; else echo "$fails check(s) failed"; fi
exit "$fails"
