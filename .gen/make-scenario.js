'use strict';
// screenshot-scenario.json, computed from the RectTransforms that were actually built.
//
// Unity UI is invisible to Android's uiautomator - the whole app is one SurfaceView -
// so the review walkthrough taps fixed screen fractions, and every one of them has to
// be the real centre of the real button. A coordinate that misses leaves the step
// photographing the same screen as the step before it.
//
// Under ScreenMatchMode Expand the canvas scale is min(sw/1242, sh/2688). On the
// 1080x2400 device the capture runs on that is width-limited, so
//   xFrac = 0.5 + x/1242      yFracFromTop = 0.5 - y*(1080/1242)/2400 = 0.5 - y/2760
// Both forms are derived below from the device size rather than written out, so the
// numbers follow the device if it ever changes.

const fs = require('fs');

const REF = { w: 1242, h: 2688 };
const DEVICE = { w: 1080, h: 2400 };     // the AVD the capture runs on
const SCALE = Math.min(DEVICE.w / REF.w, DEVICE.h / REF.h);

function parse(txt) {
  const docs = [];
  const re = /^--- !u!(\d+) &(-?\d+)(.*)$/gm;
  let m, prev = null;
  while ((m = re.exec(txt))) {
    if (prev) prev.body = txt.slice(prev.start, m.index);
    prev = { cls: +m[1], id: m[2], start: re.lastIndex };
    docs.push(prev);
  }
  if (prev) prev.body = txt.slice(prev.start);
  return docs;
}
const f1 = (b, n) => { const r = new RegExp('^\\s*' + n + ': (.*)$', 'm').exec(b); return r ? r[1].trim() : null; };
const v2 = (b, n) => {
  const r = new RegExp('^\\s*' + n + ': \\{x: ([-\\d.eE+]+), y: ([-\\d.eE+]+)', 'm').exec(b);
  return r ? { x: parseFloat(r[1]), y: parseFloat(r[2]) } : null;
};

// Resolve every GameObject's centre in canvas coordinates, keyed by its full path.
function centres(file) {
  const docs = parse(fs.readFileSync(file, 'utf8'));
  const byId = new Map(docs.map((d) => [d.id, d]));
  const goOf = new Map();
  for (const d of docs) {
    const go = f1(d.body, 'm_GameObject');
    if (go) goOf.set(d.id, go.replace(/[^0-9-]/g, ''));
  }
  const nameOf = new Map();
  const compsOf = new Map();
  for (const d of docs) {
    if (d.cls !== 1) continue;
    nameOf.set(d.id, f1(d.body, 'm_Name') || '?');
    compsOf.set(d.id, [...d.body.matchAll(/-\s*component: \{fileID: (-?\d+)\}/g)].map((x) => x[1]));
  }
  const isCanvas = (goId) => (compsOf.get(goId) || []).some((c) => byId.get(c) && byId.get(c).cls === 223);
  const childrenOf = (rt) => {
    const mm = /^  m_Children:(?: \[\]|\n((?:  - \{fileID: -?\d+\}\n)*))/m.exec(byId.get(rt).body);
    return (mm && mm[1]) ? [...mm[1].matchAll(/fileID: (-?\d+)/g)].map((x) => x[1]) : [];
  };

  const out = new Map();
  function walk(rt, parentSize, parentCentre, path) {
    const goId = goOf.get(rt);
    const d = byId.get(rt);
    let size;
    let centre;
    if (isCanvas(goId)) {
      // Unity drives a root Canvas's rect to the screen, pivot included.
      size = { x: REF.w, y: REF.h };
      centre = { x: 0, y: 0 };
    } else {
      const aMin = v2(d.body, 'm_AnchorMin') || { x: 0.5, y: 0.5 };
      const aMax = v2(d.body, 'm_AnchorMax') || { x: 0.5, y: 0.5 };
      const pos = v2(d.body, 'm_AnchoredPosition') || { x: 0, y: 0 };
      const sd = v2(d.body, 'm_SizeDelta') || { x: 0, y: 0 };
      const piv = v2(d.body, 'm_Pivot') || { x: 0.5, y: 0.5 };
      size = {
        x: ((aMax.x - aMin.x) * parentSize.x) + sd.x,
        y: ((aMax.y - aMin.y) * parentSize.y) + sd.y,
      };
      const refX = ((aMin.x + ((aMax.x - aMin.x) * piv.x)) - 0.5) * parentSize.x;
      const refY = ((aMin.y + ((aMax.y - aMin.y) * piv.y)) - 0.5) * parentSize.y;
      centre = {
        x: parentCentre.x + refX + pos.x + ((0.5 - piv.x) * size.x),
        y: parentCentre.y + refY + pos.y + ((0.5 - piv.y) * size.y),
      };
    }
    const here = path + '/' + (nameOf.get(goId) || '?');
    out.set(here, { centre: centre, size: size });
    for (const k of childrenOf(rt)) walk(k, size, centre, here);
  }

  const roots = docs.find((x) => x.cls === 1660057539);
  for (const r of [...roots.body.matchAll(/fileID: (-?\d+)/g)].map((x) => x[1])) {
    const d = byId.get(r);
    if (d && (d.cls === 4 || d.cls === 224)) walk(r, { x: REF.w, y: REF.h }, { x: 0, y: 0 }, '');
  }
  return out;
}

const lobby = centres('Assets/Scenes/Loader.unity');
const game = centres('Assets/Scenes/Games/2_GameScene.unity');

const frac = (p) => [
  Math.round((0.5 + ((p.centre.x * SCALE) / DEVICE.w)) * 10000) / 10000,
  Math.round((0.5 - ((p.centre.y * SCALE) / DEVICE.h)) * 10000) / 10000,
];

const pick = (map, path) => {
  const hit = map.get(path);
  if (!hit) {
    console.error('[scenario] no object at ' + path);
    console.error('[scenario] candidates: ' + [...map.keys()].filter((k) => k.includes(path.split('/').pop())).join(', '));
    process.exit(1);
  }
  return hit;
};

const MENU = '/PanelMenu/UI';
const GAME = '/GameCanvas';

const steps = [
  // The loader runs 8 seconds and then swaps panels; waitLoader blocks until the
  // screen stops changing rather than guessing a number.
  { waitLoader: true, wait: 2.5, shot: '01_GalleryLobby' },
  {
    tapFrac: frac(pick(lobby, MENU + '/LobbyRoot/GiftButton')),
    label: 'DAILY GIFT', wait: 2.0, shot: '02_DailyGiftCard',
  },
  {
    // Claiming swaps the case art, pops the reward chips in and greys the button out:
    // the card stays open but the frame is visibly a different one.
    tapFrac: frac(pick(lobby, MENU + '/LobbyOverlays/GiftSheet/Card/ClaimButton')),
    label: 'CLAIM', wait: 2.0, shot: '03_GiftClaimed',
  },
  {
    // A DIFFERENT control from the one above, present since the card opened.
    tapFrac: frac(pick(lobby, MENU + '/LobbyOverlays/GiftSheet/Card/ToAlbumButton')),
    label: 'TO ALBUM', wait: 2.0, shot: '04_Collection',
  },
  {
    tapFrac: frac(pick(lobby, MENU + '/LobbyOverlays/AlbumSheet/Card/PlayHallButton')),
    label: 'PLAY THIS HALL', waitLoader: true, wait: 2.5, shot: '05_DisplayHall',
  },
  {
    // A spin cycle is 2.24 s end to end; 3.4 leaves the result line written and read.
    tapFrac: frac(pick(game, GAME + '/SpinButton')),
    label: 'SPIN', wait: 3.4, shot: '06_SpinResolved',
  },
  {
    // The first two spins of a session are nudged onto a stop that shows a missing
    // relic, so this frame carries a lit pip and RELIC FOUND where 06 shows a payout
    // or NO MATCH.
    tapFrac: frac(pick(game, GAME + '/SpinButton')),
    label: 'SPIN', wait: 3.4, shot: '07_RelicFound',
  },
  {
    tapFrac: frac(pick(game, GAME + '/PauseButton')),
    label: 'PAUSE', wait: 1.8, shot: '08_PausePop',
  },
];

// No two steps may tap the same spot for different reasons, and no two frames may be
// named as though they showed different screens when the tap did nothing.
const seen = new Map();
for (const s of steps) {
  if (!s.tapFrac) continue;
  const k = s.tapFrac.join(',');
  if (seen.has(k) && seen.get(k) !== s.label) {
    console.error('[scenario] ' + s.label + ' and ' + seen.get(k) + ' tap the same point ' + k);
    process.exit(1);
  }
  seen.set(k, s.label);
  if (s.tapFrac[0] <= 0.02 || s.tapFrac[0] >= 0.98 || s.tapFrac[1] <= 0.04 || s.tapFrac[1] >= 0.96) {
    console.error('[scenario] ' + s.label + ' at ' + k + ' is in the system gesture zone');
    process.exit(1);
  }
}

fs.writeFileSync('screenshot-scenario.json', JSON.stringify({ steps: steps }, null, 2) + '\n');
for (const s of steps) {
  console.log('  ' + (s.shot || '').padEnd(18) + (s.tapFrac ? s.tapFrac.join(', ') + '   ' + s.label : 'wait for the loader'));
}
console.log('[scenario] ' + steps.length + ' steps, canvas scale ' + SCALE.toFixed(5)
  + ' on ' + DEVICE.w + 'x' + DEVICE.h);
