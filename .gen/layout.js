'use strict';
// Shared values for both scene builders: the palette (mirrors GalleryPalette.cs), the
// canvas contract, and small node factories so a screen reads as a page of design
// rather than a page of YAML.

const fs = require('fs');
const G = JSON.parse(fs.readFileSync('.gen/guids.json', 'utf8'));

// A texture imported as a single sprite exposes its Sprite sub-asset at this fileID.
const SP = (name) => {
  if (!G[name]) throw new Error('layout: no guid allocated for ' + name);
  return { fileID: 21300000, guid: G[name], type: 3 };
};
const SCRIPT = (cls) => {
  if (!G[cls]) throw new Error('layout: no guid allocated for script ' + cls);
  return G[cls];
};

const hex = (s, a) => ({
  r: parseInt(s.slice(0, 2), 16) / 255,
  g: parseInt(s.slice(2, 4), 16) / 255,
  b: parseInt(s.slice(4, 6), 16) / 255,
  a: a === undefined ? 1 : a,
});

// Mirrors GalleryPalette.cs. Text may only ever be Text / TextSoft / Accent / Secondary:
// one material, one outline, and a dark face would merge into it (rule C.14).
const P = {
  Base: hex('171225'),
  BaseDeep: hex('0E0A18'),
  Panel: hex('1E1733'),
  PanelLift: hex('241C3D'),
  PanelGlow: hex('3A2D63'),
  Primary: hex('7B61FF'),
  Accent: hex('F2C94C'),
  AccentDeep: hex('C9972F'),
  Secondary: hex('2EC4B6'),
  Locked: hex('4A4160'),
  Text: hex('F8F5FF'),
  TextSoft: hex('C9BFE4'),
  alpha: (c, a) => ({ r: c.r, g: c.g, b: c.b, a: a }),
};

// Canvas contract: reference 1242 x 2688 with ScreenMatchMode Expand. Under Expand the
// scale factor is min(sw/1242, sh/2688), so the canvas is never SMALLER than the
// reference on either axis - a centred box of exactly the reference size is therefore
// always fully on screen, on every aspect. Everything below is absolute pixels inside
// that box, which is why the composition does not shift between device shapes.
const REF = { x: 1242, y: 2688 };
const FULL = { anchorMin: { x: 0, y: 0 }, anchorMax: { x: 1, y: 1 }, pos: { x: 0, y: 0 }, size: { x: 0, y: 0 } };
const inset = (dx, dy) => ({ anchorMin: { x: 0, y: 0 }, anchorMax: { x: 1, y: 1 }, pos: { x: 0, y: 0 }, size: { x: -dx, y: -dy } });
const at = (x, y, w, h) => ({
  anchorMin: { x: 0.5, y: 0.5 }, anchorMax: { x: 0.5, y: 0.5 },
  pos: { x: x, y: y }, size: { x: w, y: h },
});

// Type sizes, in canvas units. The floor is 28: the canvas-to-device scale on a
// 1080-wide phone is 1080/1242 = 0.87, so the gate's 24 device px is ~28 here.
const T = {
  popTitle: { size: 76, min: 56 },
  heading: { size: 64, min: 46 },
  hallName: { size: 58, min: 44 },
  button: { size: 54, min: 40 },
  counter: { size: 56, min: 44 },
  body: { size: 44, min: 34 },
  row: { size: 40, min: 32 },
  caption: { size: 34, min: 30 },
};

const CTA_COLORS = {
  normal: { r: 1, g: 1, b: 1, a: 1 },
  highlighted: { r: 1, g: 1, b: 1, a: 1 },
  pressed: { r: 0.78, g: 0.78, b: 0.78, a: 1 },
  selected: { r: 1, g: 1, b: 1, a: 1 },
  disabled: { r: 0.55, g: 0.55, b: 0.55, a: 1 },
};
const QUIET_COLORS = {
  normal: { r: 1, g: 1, b: 1, a: 1 },
  highlighted: { r: 0.88, g: 0.88, b: 0.94, a: 1 },
  pressed: { r: 0.7, g: 0.7, b: 0.78, a: 1 },
  selected: { r: 1, g: 1, b: 1, a: 1 },
  disabled: { r: 0.5, g: 0.5, b: 0.55, a: 1 },
};

// ── node factories ──────────────────────────────────────────────────────────

const plate = (name, rect, sprite, color, key) => ({
  name: name, key: key, rect: rect,
  image: { sprite: sprite, color: color, imageType: sprite ? 1 : 0, ppu: 2.2 },
});

const picture = (name, rect, sprite, color, key) => ({
  name: name, key: key, rect: rect,
  image: { sprite: sprite, color: color || { r: 1, g: 1, b: 1, a: 1 }, imageType: 0, preserveAspect: true },
});

const label = (name, rect, text, type, color, align, key) => ({
  name: name, key: key, rect: rect,
  tmp: { text: text, size: type.size, min: type.min, color: color, align: align || 'center' },
});

// A button is a plate with a Button on it; the caption is always the LAST child, so the
// background can never cover its own label (hierarchy order is draw order).
const textButton = (opts) => ({
  name: opts.name,
  key: opts.key,
  rect: opts.rect,
  image: {
    sprite: opts.sprite, color: opts.plate || { r: 1, g: 1, b: 1, a: 1 },
    imageType: 1, ppu: opts.ppu === undefined ? 2.2 : opts.ppu, raycast: true,
  },
  button: { colors: opts.colors || CTA_COLORS },
  children: (opts.before || []).concat([
    label('Label', inset(48, 30), opts.caption, opts.type || T.button, opts.textColor || P.Text, 'center',
      opts.key ? opts.key + '.label' : undefined),
  ]).concat(opts.after || []),
});

const iconButton = (opts) => ({
  name: opts.name,
  key: opts.key,
  rect: opts.rect,
  image: { sprite: opts.sprite, color: opts.plate, imageType: 1, ppu: 1.4, raycast: true },
  button: { colors: opts.colors || QUIET_COLORS },
  children: [picture('Icon', at(0, 0, opts.iconSize, opts.iconSize), opts.icon, opts.iconColor || P.Accent)],
});

// A row of evenly pitched pips, centred on (cx, cy). Each pip gets a key so a director
// can light them one by one.
const pipRow = (key, cx, cy, count, size, pitch, sprite) => {
  const kids = [];
  const first = -((count - 1) * pitch) / 2;
  for (let i = 0; i < count; i++) {
    kids.push({
      name: 'Pip' + i,
      key: key + '.pip' + i,
      rect: at(first + (i * pitch), 0, size, size),
      image: { sprite: sprite, color: P.Locked, imageType: 0, preserveAspect: true },
    });
  }
  return { name: key, rect: at(cx, cy, ((count - 1) * pitch) + size, size), children: kids };
};

const pipRefs = (refs, key, count) => {
  const out = [];
  for (let i = 0; i < count; i++) out.push('- {fileID: ' + refs[key + '.pip' + i].image + '}');
  return out;
};

module.exports = {
  G, SP, SCRIPT, P, REF, FULL, inset, at, T,
  plate, picture, label, textButton, iconButton, pipRow, pipRefs,
  CTA_COLORS, QUIET_COLORS,
};
