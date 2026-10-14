'use strict';
// 2_GameScene.unity: the display hall.
//
// The scene the pipeline seeds carries only a camera and an EventSystem - no Canvas,
// no Light, no SceneRoots document - so everything here is new. It is pure UGUI: there
// is not one SpriteRenderer in this project, which is why the sorting-corridor and
// draw-mode rules have nothing to apply to. Draw order is hierarchy order, full stop.

const S = require('./scenekit');
const K = require('./yamlkit');
const L = require('./layout');
const {
  SP, SCRIPT, P, FULL, inset, at, T,
  plate, picture, label, textButton, iconButton, pipRow, pipRefs,
  CTA_COLORS, QUIET_COLORS,
} = L;

const PATH = 'Assets/Scenes/Games/2_GameScene.unity';
// Always build from the pristine template copy, so a re-run is a rebuild and not an
// edit layered on an edit. The copy is taken once, before the first build.
const ORIG = '.gen/2_GameScene.unity.orig';
if (!require('fs').existsSync(ORIG)) { require('fs').copyFileSync(PATH, ORIG); }
let text = S.read(ORIG);

// The same dead input-actions guid as the loader scene, in both copies of the
// EventSystem. Without the swap uGUI raycasting is dead and no tap is delivered.
text = text.replace(/guid: ca9f5fa95ffab41fb9a615ab714db018/g,
  'guid: 2bcd2660ca9b64942af0de543d8d7100');
// The seeded scene copies the loader's camera verbatim, orthographic size 16. Nothing
// here is drawn in world space so it changes no geometry, but leaving a stray 16 is an
// invitation for the next person to derive a size from it.
text = text.replace(/^  orthographic size: 16$/m, '  orthographic size: 5');

const scene = S.parse(text);
const CAMERA_RT = '1413414529';
const EVENTSYSTEM_RT = '2119698235';

const alloc = S.allocator([text], 420000000);
const out = [];
const refs = {};

// ── reel geometry, derived from the canvas contract ─────────────────────────
const CELL = 350;              // one stop, square
const WINDOW = CELL * 3;       // the 3x3 the player reads
const REEL_Y = 180;

const cell = (col, i) => ({
  name: 'Cell' + i,
  key: 'col' + col + '.cell' + i,
  rect: at(0, (2 - i) * CELL, 270, 270),
  image: { sprite: SP('sprite_sym_shard'), color: { r: 1, g: 1, b: 1, a: 1 }, imageType: 0, preserveAspect: true },
});

const relicSprites = [
  'sprite_sym_shard', 'sprite_sym_vessel', 'sprite_sym_scroll', 'sprite_sym_lamp',
  'sprite_sym_scarab', 'sprite_sym_lyre', 'sprite_sym_mask', 'sprite_sym_seal',
];

const column = (c) => ({
  name: 'Column' + c,
  key: 'col' + c,
  rect: at((c - 1) * CELL, 0, CELL, WINDOW),
  children: [{
    name: 'Content',
    key: 'col' + c + '.content',
    rect: at(0, 0, CELL, WINDOW),
    children: [0, 1, 2, 3].map((i) => cell(c, i)),
  }],
  monos: [{
    cls: 'ReelColumnView', guid: SCRIPT('ReelColumnView'),
    fields: (r) => [
      '_content: {fileID: ' + r['col' + c + '.content'].rt + '}',
      '_cells:',
      ...[0, 1, 2, 3].map((i) => '- {fileID: ' + r['col' + c + '.cell' + i].image + '}'),
      '_relicSprites:',
      ...relicSprites.map((n) => '- ' + K.ref(SP(n))),
      '_cellStep: ' + CELL,
    ],
  }],
});

// ── the pops ────────────────────────────────────────────────────────────────
// Three cards of one shape. Captions, colours and the close icon are this game's,
// not the template's: an Image with no sprite is drawn as a solid white rectangle,
// which is exactly how a "customised" pop reaches review with a white blob for a cross.
const pop = (key, name) => ({
  name: name,
  key: key,
  active: 0,
  rect: FULL,
  group: { alpha: 1 },
  monos: [{
    cls: 'UiAppear', guid: SCRIPT('UiAppear'),
    fields: (r) => ['_group: {fileID: ' + r[key].group + '}', '_target: {fileID: ' + r[key + '.card'].rt + '}', '_scaleIn: 1'],
  }],
  children: [{
    name: 'Card', key: key + '.card', rect: at(0, 0, 1020, 1180),
    image: { sprite: SP('sprite_plate_panel'), color: P.Panel, imageType: 1, ppu: 2.2, raycast: true },
    children: [
      plate('Inner', inset(40, 40), SP('sprite_plate_panel'), P.alpha(P.PanelGlow, 0.35)),
      picture('Badge', at(0, 330, 300, 300), SP('sprite_hall_badge'), P.Accent, key + '.badge'),
      label('Header', at(0, 100, 860, 100), 'HEADER', T.popTitle, P.Accent, 'center', key + '.header'),
      label('Body', at(0, -50, 860, 140), 'BODY', T.body, P.Text, 'center', key + '.body'),
      label('Extra', at(0, -180, 860, 70), 'EXTRA', T.row, P.Secondary, 'center', key + '.extra'),
      textButton({
        name: 'PrimaryButton', key: key + '.primary', rect: at(0, -320, 700, 150),
        sprite: SP('sprite_cta_plate'), plate: { r: 1, g: 1, b: 1, a: 1 }, caption: 'PRIMARY',
      }),
      textButton({
        name: 'SecondaryButton', key: key + '.secondary', rect: at(0, -500, 560, 130),
        sprite: SP('sprite_plate_panel'), plate: P.PanelLift, colors: QUIET_COLORS,
        caption: 'SECONDARY', type: { size: 46, min: 36 },
      }),
      iconButton({
        name: 'CloseCross', key: key + '.close', rect: at(430, 470, 110, 110),
        sprite: SP('sprite_chip'), plate: P.PanelLift, icon: SP('sprite_icon_close'), iconSize: 60,
      }),
    ],
  }],
});

const POPS = ['popWin', 'popEmpty', 'popPause'];
const popNames = { popWin: 'SetCompletePop', popEmpty: 'PouchEmptyPop', popPause: 'PausePop' };

// ── the scene ───────────────────────────────────────────────────────────────
const root = {
  name: 'GameCanvas',
  key: 'canvas',
  // Unity drives a root Canvas's rect, pivot included; writing 0.5 keeps the file
  // honest about what every tool and every child rect assumes.
  rect: { anchorMin: { x: 0, y: 0 }, anchorMax: { x: 0, y: 0 }, pos: { x: 0, y: 0 }, size: { x: 0, y: 0 }, pivot: { x: 0.5, y: 0.5 } },
  canvas: { order: 0 },
  scaler: { reference: { x: 1242, y: 2688 }, matchMode: 1, match: 0 },
  raycaster: true,
  children: [
    picture('Background', FULL, SP('BG_game'), { r: 1, g: 1, b: 1, a: 1 }),

    // Back and Pause, one of each, with no second set beside them.
    iconButton({
      name: 'BackButton', key: 'back', rect: at(-500, 1180, 140, 140),
      sprite: SP('sprite_chip'), plate: P.PanelLift, icon: SP('sprite_icon_back'), iconSize: 70,
    }),
    iconButton({
      name: 'PauseButton', key: 'pause', rect: at(500, 1180, 140, 140),
      sprite: SP('sprite_chip'), plate: P.PanelLift, icon: SP('sprite_icon_pause'), iconSize: 62,
    }),

    {
      name: 'TokenReadout', key: 'tokenBox', rect: at(0, 1180, 620, 110),
      image: { sprite: SP('sprite_plate_panel'), color: P.Panel, imageType: 1, ppu: 2.2 },
      children: [
        picture('TokenIcon', at(-230, 0, 64, 64), SP('sprite_icon_token')),
        label('TokenValue', at(-25, 0, 290, 72), '0', T.counter, P.Secondary, 'left', 'tokenValue'),
        label('TokenUnit', at(222, 0, 150, 50), 'TOKENS', T.caption, P.TextSoft, 'left', 'tokenUnit'),
      ],
    },

    // Hall and set. Pips span -498..-242; the text column starts at -214, so the two
    // never share x-space (rule C.25) - left alignment moves glyphs inside a rectangle,
    // it does not move the rectangle.
    {
      name: 'SetStrip', key: 'setStrip', rect: at(0, 1000, 1020, 150),
      image: { sprite: SP('sprite_plate_panel'), color: P.Panel, imageType: 1, ppu: 2.2 },
      children: [
        pipRow('SetPips', -370, 0, 3, 64, 96, SP('sprite_pip_shard')),
        label('SetTitle', at(86, 34, 600, 60), 'HALL', T.row, P.Text, 'left', 'setTitle'),
        label('SetStatus', at(86, -34, 600, 52), 'RELICS', T.caption, P.TextSoft, 'left', 'setStatus'),
      ],
    },

    // The display. Order is plate -> frame -> window -> pay line: the frame goes
    // UNDER the symbols on purpose. A generated frame that comes back with an opaque
    // middle would otherwise cover the reels completely, and from below it is
    // harmless either way - the window is 1050 inside a 1180 frame, so its border
    // ring shows regardless.
    {
      name: 'Display', key: 'display', rect: at(0, REEL_Y, 1180, 1180),
      children: [
        plate('ReelPlate', at(0, 0, 1100, 1100), SP('sprite_plate_panel'), P.PanelLift, 'reelPlate'),
        plate('ReelFrame', at(0, 0, 1180, 1180), SP('sprite_frame_reel'), { r: 1, g: 1, b: 1, a: 1 }),
        {
          name: 'ReelWindow', rect: at(0, 0, WINDOW, WINDOW), mask: true,
          children: [0, 1, 2].map(column),
        },
        {
          name: 'PayLine', key: 'payLine', rect: at(0, 0, WINDOW, 10),
          image: { sprite: SP('sprite_chip'), color: P.alpha(P.Accent, 0), imageType: 1, ppu: 6 },
        },
      ],
    },

    label('Hint', at(0, -470, 980, 56), 'TAP SPIN', T.caption, P.TextSoft, 'center', 'hint'),
    label('ResultLine', at(0, -580, 980, 80), '', T.row, P.TextSoft, 'center', 'result'),

    // Which pedestals are lit, and in what hue, is drawn from the session seed - two
    // visits to a hall differ in the room, not only in a HUD number.
    {
      name: 'DecorRow', rect: at(0, -700, 1140, 40),
      children: [0, 1, 2, 3, 4, 5].map((i) => ({
        name: 'Lamp' + i, key: 'lamp' + i,
        rect: at(-475 + (i * 190), 0, 120, 36),
        image: { sprite: SP('sprite_chip'), color: P.alpha(P.Locked, 0.35), imageType: 1, ppu: 3.2 },
      })),
    },

    textButton({
      name: 'BetDownButton', key: 'betDown', rect: at(-440, -850, 150, 150),
      sprite: SP('sprite_chip'), plate: P.PanelLift, colors: QUIET_COLORS,
      caption: '-', type: { size: 64, min: 44 },
    }),
    textButton({
      name: 'BetUpButton', key: 'betUp', rect: at(440, -850, 150, 150),
      sprite: SP('sprite_chip'), plate: P.PanelLift, colors: QUIET_COLORS,
      caption: '+', type: { size: 64, min: 44 },
    }),
    label('BetValue', at(0, -850, 420, 80), 'BET 50', T.row, P.Text, 'center', 'betValue'),

    textButton({
      name: 'SpinButton', key: 'spin', rect: at(0, -1065, 740, 180),
      sprite: SP('sprite_cta_plate'), plate: { r: 1, g: 1, b: 1, a: 1 }, caption: 'SPIN',
    }),

    label('Footer', at(0, -1255, 1140, 110),
      'FREE TOKENS ONLY - NOTHING TO BUY', T.caption, P.TextSoft, 'center', 'footer'),

    // The HUD view sits on its own object so the director reaches it by reference.
    {
      name: 'Hud', key: 'hud', rect: at(0, 0, 10, 10),
      monos: [{
        cls: 'GameHudView', guid: SCRIPT('GameHudView'),
        fields: (r) => [
          '_tokenValue: {fileID: ' + r.tokenValue.tmp + '}',
          '_setTitle: {fileID: ' + r.setTitle.tmp + '}',
          '_setStatus: {fileID: ' + r.setStatus.tmp + '}',
          '_setPips:',
          ...pipRefs(r, 'SetPips', 3),
          '_hint: {fileID: ' + r.hint.tmp + '}',
          '_result: {fileID: ' + r.result.tmp + '}',
          '_betValue: {fileID: ' + r.betValue.tmp + '}',
          '_payLine: {fileID: ' + r.payLine.image + '}',
          '_tokenBox: {fileID: ' + r.tokenBox.rt + '}',
        ],
      }],
    },

    // Pops last, so a raised card covers the whole display.
    {
      name: 'PopLayer', key: 'popLayer', rect: FULL,
      children: [
        {
          name: 'Veil', key: 'veil', active: 0, rect: FULL,
          image: { color: P.alpha(P.BaseDeep, 0.74), imageType: 0, raycast: true },
        },
      ].concat(POPS.map((k) => pop(k, popNames[k]))),
      monos: [{
        cls: 'GamePopPresenter', guid: SCRIPT('GamePopPresenter'),
        fields: (r) => [
          '_veil: {fileID: ' + r.veil.go + '}',
          '_popRoots:', ...POPS.map((k) => '- {fileID: ' + r[k].go + '}'),
          '_popHeaders:', ...POPS.map((k) => '- {fileID: ' + r[k + '.header'].tmp + '}'),
          '_popBodies:', ...POPS.map((k) => '- {fileID: ' + r[k + '.body'].tmp + '}'),
          '_popExtras:', ...POPS.map((k) => '- {fileID: ' + r[k + '.extra'].tmp + '}'),
          '_popBadges:', ...POPS.map((k) => '- {fileID: ' + r[k + '.badge'].image + '}'),
          '_popCards:', ...POPS.map((k) => '- {fileID: ' + r[k + '.card'].image + '}'),
          '_popPrimaryButtons:', ...POPS.map((k) => '- {fileID: ' + r[k + '.primary'].button + '}'),
          '_popPrimaryLabels:', ...POPS.map((k) => '- {fileID: ' + r[k + '.primary.label'].tmp + '}'),
          '_popSecondaryButtons:', ...POPS.map((k) => '- {fileID: ' + r[k + '.secondary'].button + '}'),
          '_popSecondaryLabels:', ...POPS.map((k) => '- {fileID: ' + r[k + '.secondary.label'].tmp + '}'),
          '_popCloseButtons:', ...POPS.map((k) => '- {fileID: ' + r[k + '.close'].button + '}'),
        ],
      }],
    },
  ],
  monos: [{
    cls: 'HallSessionDirector', guid: SCRIPT('HallSessionDirector'),
    fields: (r) => [
      '_hud: {fileID: ' + r.hud.monos.GameHudView + '}',
      '_pops: {fileID: ' + r.popLayer.monos.GamePopPresenter + '}',
      '_columns:',
      ...[0, 1, 2].map((c) => '- {fileID: ' + r['col' + c].monos.ReelColumnView + '}'),
      '_spinButton: {fileID: ' + r.spin.button + '}',
      '_spinLabel: {fileID: ' + r['spin.label'].tmp + '}',
      '_spinPlate: {fileID: ' + r.spin.image + '}',
      '_betDownButton: {fileID: ' + r.betDown.button + '}',
      '_betUpButton: {fileID: ' + r.betUp.button + '}',
      '_backButton: {fileID: ' + r.back.button + '}',
      '_pauseButton: {fileID: ' + r.pause.button + '}',
      '_decorLamps:',
      ...[0, 1, 2, 3, 4, 5].map((i) => '- {fileID: ' + r['lamp' + i].image + '}'),
      '_themedPanels:',
      '- {fileID: ' + r.tokenBox.image + '}',
      '- {fileID: ' + r.setStrip.image + '}',
      '_hallBadgeSprite: ' + K.ref(SP('sprite_hall_badge')),
    ],
  }],
};

const rootRec = S.buildTree(root, 0, alloc, out, refs);
S.appendDocs(scene, S.flatten(out));
// The seeded scene has no SceneRoots document at all; Unity always writes one, last.
S.setSceneRoots(scene, [CAMERA_RT, EVENTSYSTEM_RT, rootRec.rt]);
S.write(PATH, S.render(scene));
console.log('[game] ' + out.length + ' new documents');
