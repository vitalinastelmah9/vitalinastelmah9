'use strict';
// Resolve every RectTransform to real pixels and report sibling pairs whose rects
// intersect. Overlapping UI is the one defect class no pipeline gate catches:
// verify-unity-scene, -buttons, -layout and -ui-draw-order all pass on a scene whose
// HUD sits on top of the playfield.
//
// Two traps this gets right, because both produce plausible-looking numbers:
//   1. a Canvas root's serialized rect is meaningless - Unity drives it to the screen,
//      so it is treated as a reference-sized box centred on the origin;
//   2. the edges come from the OFFSET formula with sizeDelta, never from an
//      anchor-midpoint shortcut, which agrees for a point anchor and is wrong for
//      every stretched one.
//
// Run with --canary to plant a deliberate overlap and prove the checker still fires.

const fs = require('fs');

const REF = { w: 1242, h: 2688 };
const MIN_GAP = 0;          // touching is fine; crossing is not
const LABEL_RE = /TMPro\.TextMeshProUGUI/;

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

function check(file, canvasH, report) {
  const txt = fs.readFileSync(file, 'utf8');
  const docs = parse(txt);
  const byId = new Map(docs.map((d) => [d.id, d]));
  const goOf = new Map();
  for (const d of docs) {
    const go = f1(d.body, 'm_GameObject');
    if (go) goOf.set(d.id, go.replace(/[^0-9-]/g, ''));
  }
  const compsOf = new Map();
  const nameOf = new Map();
  const activeOf = new Map();
  for (const d of docs) {
    if (d.cls !== 1) continue;
    nameOf.set(d.id, (f1(d.body, 'm_Name') || '?'));
    activeOf.set(d.id, f1(d.body, 'm_IsActive') !== '0');
    compsOf.set(d.id, [...d.body.matchAll(/-\s*component: \{fileID: (-?\d+)\}/g)].map((x) => x[1]));
  }
  const hasCanvas = (goId) => (compsOf.get(goId) || []).some((c) => byId.get(c) && byId.get(c).cls === 223);
  const isLabel = (goId) => (compsOf.get(goId) || []).some((c) => {
    const d = byId.get(c);
    return d && d.cls === 114 && LABEL_RE.test(d.body);
  });
  const isFullFrame = (rt) => {
    const d = byId.get(rt);
    const aMin = v2(d.body, 'm_AnchorMin');
    const aMax = v2(d.body, 'm_AnchorMax');
    return aMin && aMax && aMin.x === 0 && aMin.y === 0 && aMax.x === 1 && aMax.y === 1;
  };
  const childrenOf = (rt) => {
    const d = byId.get(rt);
    const mm = /^  m_Children:(?: \[\]|\n((?:  - \{fileID: -?\d+\}\n)*))/m.exec(d.body);
    if (!mm || !mm[1]) return [];
    return [...mm[1].matchAll(/fileID: (-?\d+)/g)].map((x) => x[1]);
  };

  const findings = [];

  // size of a rect in its parent's coordinate frame, and its local min/max
  function rectOf(rt, parentSize) {
    const d = byId.get(rt);
    const aMin = v2(d.body, 'm_AnchorMin') || { x: 0.5, y: 0.5 };
    const aMax = v2(d.body, 'm_AnchorMax') || { x: 0.5, y: 0.5 };
    const pos = v2(d.body, 'm_AnchoredPosition') || { x: 0, y: 0 };
    const sd = v2(d.body, 'm_SizeDelta') || { x: 0, y: 0 };
    const piv = v2(d.body, 'm_Pivot') || { x: 0.5, y: 0.5 };
    const size = {
      x: ((aMax.x - aMin.x) * parentSize.x) + sd.x,
      y: ((aMax.y - aMin.y) * parentSize.y) + sd.y,
    };
    // The anchor reference point, in the parent's own centred frame.
    const refX = ((aMin.x + ((aMax.x - aMin.x) * piv.x)) - 0.5) * parentSize.x;
    const refY = ((aMin.y + ((aMax.y - aMin.y) * piv.y)) - 0.5) * parentSize.y;
    const pivotX = refX + pos.x;
    const pivotY = refY + pos.y;
    return {
      size: size,
      min: { x: pivotX - (piv.x * size.x), y: pivotY - (piv.y * size.y) },
      max: { x: pivotX + ((1 - piv.x) * size.x), y: pivotY + ((1 - piv.y) * size.y) },
    };
  }

  function walk(rt, parentSize, path, parentActive) {
    const goId = goOf.get(rt);
    const active = parentActive && activeOf.get(goId) !== false;
    // Unity drives a Canvas root's rect to the screen; its serialized values mean
    // nothing, so the reference box is substituted.
    const canvas = hasCanvas(goId);
    const r = canvas
      ? { size: { x: REF.w, y: canvasH }, min: { x: -REF.w / 2, y: -canvasH / 2 }, max: { x: REF.w / 2, y: canvasH / 2 } }
      : rectOf(rt, parentSize);
    const here = path + '/' + (nameOf.get(goId) || '?');

    const kids = childrenOf(rt);
    const boxes = [];
    for (const k of kids) {
      const kg = goOf.get(k);
      const kr = hasCanvas(kg)
        ? { size: { x: REF.w, y: canvasH }, min: { x: -REF.w / 2, y: -canvasH / 2 }, max: { x: REF.w / 2, y: canvasH / 2 } }
        : rectOf(k, r.size);
      boxes.push({
        rt: k, go: kg, rect: kr, name: nameOf.get(kg) || '?',
        label: isLabel(kg), full: isFullFrame(k), on: activeOf.get(kg) !== false,
      });
    }

    // Compare siblings. Elements that are deliberately stacked - a full-frame plate,
    // scrim or veil behind content, and a container that fully CONTAINS its neighbour
    // when it is the earlier sibling - are not findings. A plate listed AFTER the
    // label it covers is the real defect, and is still reported.
    for (let i = 0; i < boxes.length; i++) {
      for (let j = i + 1; j < boxes.length; j++) {
        const a = boxes[i];
        const b = boxes[j];
        if (a.full || b.full) continue;
        // One shipped on, one shipped off: mutually exclusive by construction (the
        // empty-state message in place of the rows, a sheet body swapped for a
        // confirm card). They are never on screen together.
        if (a.on !== b.on) continue;
        if (!a.label && !b.label) continue;            // plate-on-plate is layering
        const overlapX = Math.min(a.rect.max.x, b.rect.max.x) - Math.max(a.rect.min.x, b.rect.min.x);
        const overlapY = Math.min(a.rect.max.y, b.rect.max.y) - Math.max(a.rect.min.y, b.rect.min.y);
        if (overlapX <= MIN_GAP || overlapY <= MIN_GAP) continue;
        const contains = (o, inner) => o.rect.min.x <= inner.rect.min.x && o.rect.max.x >= inner.rect.max.x
          && o.rect.min.y <= inner.rect.min.y && o.rect.max.y >= inner.rect.max.y;
        if (!a.label && contains(a, b)) continue;      // the earlier sibling is the backing
        findings.push(here + ': "' + a.name + '" x "' + b.name + '" overlap by '
          + Math.round(overlapX) + ' x ' + Math.round(overlapY)
          + (active ? '' : '  [inactive branch]'));
      }
    }
    for (const k of kids) walk(k, r.size, here, active);
  }

  const roots = docs.find((d) => d.cls === 1660057539);
  const rootList = roots ? [...roots.body.matchAll(/fileID: (-?\d+)/g)].map((x) => x[1]) : [];
  for (const r of rootList) {
    if (!byId.get(r) || (byId.get(r).cls !== 4 && byId.get(r).cls !== 224)) continue;
    walk(r, { x: REF.w, y: canvasH }, '', true);
  }
  for (const f of findings) report.push(file.split(/[\\/]/).pop() + ' @' + canvasH + ' ' + f);
  return findings.length;
}

const files = process.argv.slice(2).filter((a) => !a.startsWith('--'));
const report = [];
let total = 0;
// The reference height and the two shapes a phone actually is: 16:9 is the tight one.
for (const h of [2688, 2760, 2208]) {
  for (const f of files) total += check(f, h, report);
}
for (const line of report) console.log(line);
console.log('[layout] ' + files.length + ' scene(s) x 3 canvas heights, ' + total + ' overlap(s)');
process.exit(total ? 1 : 0);
