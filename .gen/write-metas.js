'use strict';
// Allocate guids that are provably unused anywhere in the project (rule B.2) and write
// every .meta codegen owns: sprite textures, background textures, scripts, prefabs.
// The sprite meta is taken BYTE FOR BYTE from a project that shipped green
// (PlinkoBeacon) with only the guid and spriteBorder substituted - a hand-written
// importer block imports silently with defaults and no gate reads it.

const fs = require('fs');
const path = require('path');
const crypto = require('crypto');

const ROOT = process.cwd();
const NAMES = JSON.parse(fs.readFileSync('.gen/names.json', 'utf8'));
const OUT = '.gen/guids.json';
const REFERENCE_META = 'C:/AndroidWorkspace/projects/PlinkoBeacon/Assets/Art/Sprites/sprite_beacon_dark.png.meta';

// ── every 32-hex token already present anywhere in the project ──────────────
const taken = new Set();
function scan(dir) {
  let entries = [];
  try { entries = fs.readdirSync(dir, { withFileTypes: true }); } catch (e) { return; }
  for (const e of entries) {
    const p = path.join(dir, e.name);
    if (e.isDirectory()) { if (e.name === 'node_modules' || e.name === '.git') continue; scan(p); continue; }
    let t = '';
    try { t = fs.readFileSync(p, 'latin1'); } catch (e2) { continue; }
    for (const m of t.matchAll(/[0-9a-f]{32}/g)) taken.add(m[0]);
  }
}
for (const r of ['Assets', 'ProjectSettings', 'Packages']) scan(r);
// Guids the pipeline itself will introduce later; never hand them out.
taken.add('7c21f04faa643f373af07e1e551f4a7d'); // TmpContrastGuard.cs, staged at 5c3b
console.error('[metas] guid-ish tokens already present: ' + taken.size);

// Reuse any guid already written: a re-run must never reissue an id a scene already
// references, which is exactly how a repointed asset turns into a white rectangle.
const guids = {};
try {
  const prior = JSON.parse(fs.readFileSync(OUT, 'utf8'));
  for (const k of Object.keys(prior)) { guids[k] = prior[k]; }
  console.error('[metas] reusing ' + Object.keys(guids).length + ' guid(s) already allocated');
} catch (e) { /* first run */ }

function allocate(key) {
  if (guids[key]) { return guids[key]; }
  let g;
  do { g = crypto.randomBytes(16).toString('hex'); } while (taken.has(g) || /^0{8}/.test(g));
  taken.add(g);
  guids[key] = g;
  return g;
}

// ── sprite + texture metas ──────────────────────────────────────────────────
const refMeta = fs.readFileSync(REFERENCE_META, 'utf8');

function textureMeta(guid, border, maxSize) {
  let t = refMeta;
  t = t.replace(/^guid: [0-9a-f]{32}$/m, 'guid: ' + guid);
  const b = border || [0, 0, 0, 0];
  t = t.replace(/^  spriteBorder: \{x: .*\}$/m,
    '  spriteBorder: {x: ' + b[0] + ', y: ' + b[1] + ', z: ' + b[2] + ', w: ' + b[3] + '}');
  if (maxSize) t = t.replace(/maxTextureSize: 1024/g, 'maxTextureSize: ' + maxSize);
  return t;
}

fs.mkdirSync('Assets/Art/Sprites', { recursive: true });
fs.mkdirSync('Assets/Art/Textures/_BASE/Backgrounds', { recursive: true });

for (const name of NAMES.sprites) {
  const g = allocate(name);
  fs.writeFileSync('Assets/Art/Sprites/' + name + '.png.meta',
    textureMeta(g, NAMES.borders[name], 512));
}
const TEX_TARGET = {
  BG_main: 'Assets/Art/Textures/_BASE/Backgrounds/BG_main.png',
  BG_game: 'Assets/Art/Textures/_BASE/Backgrounds/BG_game.png',
  BG_loader: 'Assets/Art/Textures/_BASE/Backgrounds/BG_loader.png',
  game_icon: 'Assets/Art/Textures/game_icon.png',
};
for (const name of NAMES.textures) {
  const g = allocate(name);
  // Backgrounds are full-screen art: no 9-slice, a larger budget than a symbol.
  fs.writeFileSync(TEX_TARGET[name] + '.meta', textureMeta(g, null, name === 'game_icon' ? 512 : 2048));
}

// ── script metas ────────────────────────────────────────────────────────────
fs.mkdirSync('Assets/Scripts/Generated', { recursive: true });
const CS_META = (g) => [
  'fileFormatVersion: 2',
  'guid: ' + g,
  'MonoImporter:',
  '  externalObjects: {}',
  '  serializedVersion: 2',
  '  defaultReferences: []',
  '  executionOrder: 0',
  '  icon: {instanceID: 0}',
  '  userData: ',
  '  assetBundleName: ',
  '  assetBundleVariant: ',
  '',
].join('\n');
for (const cls of NAMES.scripts) {
  const g = allocate(cls);
  fs.writeFileSync('Assets/Scripts/Generated/' + cls + '.cs.meta', CS_META(g));
}

fs.writeFileSync(OUT, JSON.stringify(guids, null, 2));
console.error('[metas] wrote ' + Object.keys(guids).length + ' metas -> ' + OUT);
