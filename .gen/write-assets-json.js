'use strict';
// assets.json - the art manifest.
//
// One art style for the whole project, named in EVERY prompt: the generator only
// appends its own style when the prompt names none, and two competing directives give
// mush. The icon is composited from these same sprites, so a style mismatch would show
// up on the storefront too.
//
// No background colour is ever specified for a sprite: the background is removed by AI
// matting, and naming a colour makes the two halves of the pipeline fight and leaves a
// halo. Orientation IS declared for every sprite, because codegen runs before the art
// exists and the direction check has nothing to compare against otherwise.

const fs = require('fs');

const STYLE = 'stylized 3D render, soft glossy gem-like surfaces, chunky rounded forms, '
  + 'clean cel shading, deep violet and brass gold and mint museum palette, no text, no lettering';

const SPRITES = 'Assets/Art/Sprites/';
const BG = 'Assets/Art/Textures/_BASE/Backgrounds/';

// Every relic is a static museum object seen head on: nothing here moves or points, so
// none of them may be flipped or rotated by the orientation pass.
const STATIC = 'front view, upright, vertically symmetric, facing the VIEWER, centred, isolated, one single object';

const sprite = (name, core, orientation) => ({
  name: name,
  type: 'sprite',
  transparent: true,
  target: SPRITES + name + '.png',
  prompt: core + ', ' + (orientation || STATIC) + ', ' + STYLE,
});

const background = (name, core) => ({
  name: name,
  type: 'bg',
  transparent: false,
  target: BG + name + '.png',
  prompt: 'vertical 19.5:9 portrait mobile game background, ' + core
    + ', no text, no UI elements, nothing busy under the centre of the frame, ' + STYLE,
});

const assets = [
  // ── backgrounds ──────────────────────────────────────────────────────────
  background('BG_loader',
    'a closed museum corridor at night seen end on, one tall violet arch far down it, '
    + 'a single brass lamp pooling warm light on the floor, deep shadow everywhere else, '
    + 'much darker and emptier than a lobby, the centre band kept very low contrast'),
  background('BG_main',
    'a private museum atrium after closing, tall violet arches, brass handrails, '
    + 'soft mint light spilling from hidden coves, lit and welcoming, '
    + 'the centre band kept calm and low contrast for UI'),
  background('BG_game',
    'a dim exhibition hall interior, a row of unlit pedestals receding into violet shadow, '
    + 'one warm brass spotlight pool low in the frame, '
    + 'very low contrast across the whole middle band for a UI overlay'),

  // ── the eight relics ─────────────────────────────────────────────────────
  sprite('sprite_sym_shard', 'one single rough opalescent gem shard with violet and mint fire inside'),
  sprite('sprite_sym_vessel', 'one single glazed ceramic vessel with a brass rim and two small handles'),
  sprite('sprite_sym_scroll', 'one single rolled parchment scroll with an ornate brass clasp'),
  sprite('sprite_sym_lamp', 'one single brass gallery lamp with a warm amber glass shade'),
  sprite('sprite_sym_scarab', 'one single gilded scarab beetle figurine on a small stone plinth'),
  sprite('sprite_sym_lyre', 'one single small brass lyre with mint-tinted strings'),
  sprite('sprite_sym_mask', 'one single ceremonial face mask inlaid with violet gemstone'),
  sprite('sprite_sym_seal', 'one single brass laurel-wreath seal medallion'),

  // ── UI plates, all 9-slice: a flat even surface, no object inside ────────
  sprite('sprite_frame_reel',
    'an ornate rectangular brass display frame, BORDER ONLY, completely hollow and empty '
    + 'through the middle, violet enamel inlay along the rails, nothing at all in the centre'),
  sprite('sprite_plate_panel',
    'a plain rounded rectangular violet panel plate with a thin brass edge, '
    + 'flat even surface, nothing drawn on it'),
  sprite('sprite_cta_plate',
    'a rounded rectangular brass button plate with a warm gold gradient and a soft bevel, '
    + 'flat even surface, nothing drawn on it'),
  sprite('sprite_chip',
    'a small rounded rectangular violet chip plate with a thin mint edge, '
    + 'flat even surface, nothing drawn on it'),

  // ── UI marks and icons ───────────────────────────────────────────────────
  sprite('sprite_pip_shard', 'one single small faceted round gem pip'),
  sprite('sprite_icon_token',
    'one single round brass curator token with an engraved facet pattern and no lettering'),
  sprite('sprite_icon_close', 'one single thick rounded brass cross mark'),
  sprite('sprite_icon_back', 'one single thick rounded brass chevron arrow head',
    'side view, pointing LEFT, facing LEFT, centred, isolated, one single object'),
  sprite('sprite_icon_pause', 'one single pair of thick rounded vertical brass bars side by side'),
  sprite('sprite_icon_gear', 'one single rounded brass gear wheel'),
  sprite('sprite_mark_rosette',
    'one single abstract opal-cut hexagonal rosette emblem, brass rim with a violet gem core, '
    + 'an abstract symbol with no letters and no words of any kind'),
  sprite('sprite_hall_badge',
    'one single arched gallery doorway medallion with a violet gem keystone'),
  sprite('sprite_gift_case', 'one single closed brass relic case with a violet gem clasp'),
  sprite('sprite_gift_open', 'one single open brass relic case with mint light spilling out of it'),
];

fs.writeFileSync('assets.json', JSON.stringify({ assets: assets }, null, 2) + '\n');
// Pin the style so the generator's own deterministic pick never competes with it.
fs.writeFileSync('.art-style', STYLE + '\n');

// Every sprite the scenes reference must be in the manifest, and vice versa.
const guids = JSON.parse(fs.readFileSync('.gen/guids.json', 'utf8'));
const names = JSON.parse(fs.readFileSync('.gen/names.json', 'utf8'));
const declared = new Set(assets.map((a) => a.name));
const problems = [];
for (const n of names.sprites.concat(['BG_main', 'BG_game', 'BG_loader'])) {
  if (!declared.has(n)) problems.push('allocated a guid for ' + n + ' but the manifest does not generate it');
}
for (const a of assets) {
  if (!guids[a.name]) problems.push(a.name + ' is in the manifest but has no .meta guid');
}
// The icon is built from these sprites afterwards; its texture only needs to exist as
// a .meta so the icon stage has somewhere to put the bytes.
if (!fs.existsSync('Assets/Art/Textures/game_icon.png.meta')) problems.push('game_icon.png.meta is missing - the icon stage copies only onto an existing target');
if (problems.length) { for (const p of problems) console.error('[assets] ' + p); process.exit(1); }
console.log('[assets] ' + assets.length + ' entries (' + assets.filter((a) => a.type === 'bg').length
  + ' backgrounds, ' + assets.filter((a) => a.type === 'sprite').length + ' sprites), all matched to metas');
