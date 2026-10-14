'use strict';
// Loader.unity: the splash panel and the lobby.
//
// Both roots are the template's own (PanelLoader / PanelMenu) and neither is deleted,
// renamed, moved or reordered. What changes: values on existing objects (the canvas
// scalers, the slider's geometry and colours, the two template buttons' rects and
// captions), the six dangling sprite references and the dead font the template ships
// with, and new children added INTO the existing containers.

const S = require('./scenekit');
const K = require('./yamlkit');
const L = require('./layout');
const {
  SP, SCRIPT, P, FULL, inset, at, T,
  plate, picture, label, textButton, iconButton, pipRow, pipRefs,
  CTA_COLORS, QUIET_COLORS,
} = L;

const PATH = 'Assets/Scenes/Loader.unity';
// Always build from the pristine template copy, so a re-run is a rebuild and not an
// edit layered on an edit. The copy is taken once, before the first build.
const ORIG = '.gen/Loader.unity.orig';
if (!require('fs').existsSync(ORIG)) { require('fs').copyFileSync(PATH, ORIG); }
let text = S.read(ORIG);

// ── template fileIDs, confirmed against this copy with .gen/dump.js ─────────
const ID = {
  loaderUiRt: '473367939', loaderScaler: '473367937', loaderBgImage: '2052145229',
  sliderRt: '397194676', sliderTrackImage: '1320501298', sliderBgRt: '1320501297',
  fillAreaRt: '1699811543', fillRt: '954641864', fillImage: '954641865',
  controllerLoader: '1836528986',
  menuUiRt: '1008058962', menuScaler: '1008058964', menuBgImage: '855584338',
  scoreLabelTmp: '565135048',
  playRt: '713131003', playImage: '713131005', playButton: '713131004',
  playCaptionGo: '2085339827', playCaptionRt: '2085339828', playCaptionTmp: '2085339829',
  privacyRt: '481126437', privacyImage: '481126439', privacyButton: '481126438',
  privacyCaptionGo: '144922631', privacyCaptionRt: '144922632', privacyCaptionTmp: '144922633',
};

// ── 1. template debt: dangling sprites, the dead font, the dead input actions ──
// None of this is caught by any gate - verify-unity-scene does not read Loader.unity at
// all, and verify-unity-layout only validates `type: 2` sprite refs while every one of
// these is `type: 3`. They ship as white rectangles unless codegen repoints them.
const DEAD_SPRITES = {
  '5269d62e75e5d4780ae8b06af1d115e0': 'BG_loader',
  'ec9637376801343788adccf4b57f601a': 'BG_main',
  'f3baf8ea6fd2c48ad807793215d704e6': 'sprite_cta_plate',
  '70c3f0d2c159c447ba1f1dbd247f714e': 'sprite_plate_panel',
  '73c72bc72ccdc486f808a528d24ba64c': 'sprite_plate_panel',
  'c4864ff8032884d2794285a7a9de92dd': 'sprite_cta_plate',
};
for (const dead of Object.keys(DEAD_SPRITES)) {
  // Each dangling reference carries its OWN sub-sprite id, not the usual 21300000, so
  // matching the literal {fileID: 21300000, ...} would replace nothing and report
  // success while the white rectangles shipped.
  const re = new RegExp('\\{fileID: -?\\d+, guid: ' + dead + ', type: 3\\}', 'g');
  const before = text;
  text = text.replace(re, K.ref(SP(DEAD_SPRITES[dead])));
  if (before === text) throw new Error('dangling sprite ' + dead + ' matched nothing');
}
// The dead TMP font is two tokens, not one guid swap: a plain guid replace would leave
// m_sharedMaterial pointing at a sub-asset that does not exist inside LiberationSans.
text = text.replace(/\{fileID: -1097357345681707745, guid: 791792d007be241d6bcb8c25219b324b, type: 2\}/g,
  '{fileID: 2180264, guid: 8f586378b4e144a9851e7b34d9b748ee, type: 2}');
text = text.replace(/guid: 791792d007be241d6bcb8c25219b324b/g,
  'guid: 8f586378b4e144a9851e7b34d9b748ee');
// The EventSystem's input actions asset: that guid is in no .meta, so uGUI raycasting
// is dead on arrival and nothing receives a pointer event at all. The project's real
// asset carries the SAME internal sub-asset fileIDs, so a plain guid swap repairs it.
text = text.replace(/guid: ca9f5fa95ffab41fb9a615ab714db018/g,
  'guid: 2bcd2660ca9b64942af0de543d8d7100');

const scene = S.parse(text);

// ── 2. the canvas contract, on both of the template's canvases ──────────────
for (const scaler of [ID.loaderScaler, ID.menuScaler]) {
  S.setField(scene, scaler, 'm_ReferenceResolution', '{x: 1242, y: 2688}');
  S.setField(scene, scaler, 'm_ScreenMatchMode', '1');
  S.setField(scene, scaler, 'm_MatchWidthOrHeight', '0');
}

// ── 3. the loading bar: geometry and colours ────────────────────────────────
// The template lays it out for a 2400x1120 landscape canvas, so every number has to be
// restated for the portrait contract above. Fill Area insets itself on x only on this
// template, so the "bar collapses to zero height" trap does not apply - but Fill ships
// a 10-unit sizeDelta that Slider.UpdateVisuals never clears, which would leave the
// filled part permanently 10 units wider than its value.
S.setField(scene, ID.sliderRt, 'm_AnchoredPosition', '{x: 0, y: -900}');
S.setField(scene, ID.sliderRt, 'm_SizeDelta', '{x: 880, y: 56}');
S.setField(scene, ID.sliderBgRt, 'm_AnchorMin', '{x: 0, y: 0}');
S.setField(scene, ID.sliderBgRt, 'm_AnchorMax', '{x: 1, y: 1}');
S.setField(scene, ID.sliderBgRt, 'm_SizeDelta', '{x: 0, y: 0}');
S.setField(scene, ID.fillAreaRt, 'm_AnchorMin', '{x: 0, y: 0}');
S.setField(scene, ID.fillAreaRt, 'm_AnchorMax', '{x: 1, y: 1}');
S.setField(scene, ID.fillAreaRt, 'm_AnchoredPosition', '{x: 0, y: 0}');
S.setField(scene, ID.fillAreaRt, 'm_SizeDelta', '{x: -16, y: -16}');
S.setField(scene, ID.fillRt, 'm_SizeDelta', '{x: 0, y: 0}');
S.setField(scene, ID.sliderTrackImage, 'm_Color', K.col(P.alpha(P.PanelLift, 0.85)));
S.setField(scene, ID.sliderTrackImage, 'm_PixelsPerUnitMultiplier', '2.2');
S.setField(scene, ID.fillImage, 'm_Color', K.col(P.Accent));
S.setField(scene, ID.fillImage, 'm_PixelsPerUnitMultiplier', '2.2');

// The loader waits 8 seconds, not the template's 30. Nothing else drives this scene -
// the BootFlow / PageHost components the template puts on SceneFlow point at scripts
// that are not in the project at all - so ControllerLoader is the only hand on the
// panel swap, and at 30 s a review capture settles on the splash and photographs it.
S.setField(scene, ID.controllerLoader, 'slider_wait_time', '8');

// ── 4. the two template buttons, restyled in place ──────────────────────────
// Reusing the slots the template already has rather than adding a parallel set beside
// them, and giving PLAY an explicit size: the template ships it stretched 0..0.93 of
// the panel with sizeDelta 0x0, which is the full-screen slab rule G.3 bans.
S.setField(scene, ID.playRt, 'm_AnchorMin', '{x: 0.5, y: 0.5}');
S.setField(scene, ID.playRt, 'm_AnchorMax', '{x: 0.5, y: 0.5}');
S.setField(scene, ID.playRt, 'm_AnchoredPosition', '{x: 0, y: -430}');
S.setField(scene, ID.playRt, 'm_SizeDelta', '{x: 770, y: 168}');
S.setField(scene, ID.playImage, 'm_Sprite', K.ref(SP('sprite_cta_plate')));
S.setField(scene, ID.playImage, 'm_Color', K.col({ r: 1, g: 1, b: 1, a: 1 }));
S.setField(scene, ID.playImage, 'm_Type', '1');
S.setField(scene, ID.playImage, 'm_PixelsPerUnitMultiplier', '2.2');
for (const [k, c] of Object.entries(CTA_COLORS)) {
  S.setNested(scene, ID.playButton, 'm_' + k[0].toUpperCase() + k.slice(1) + 'Color', K.col(c), 4);
}

S.setField(scene, ID.privacyRt, 'm_AnchoredPosition', '{x: 0, y: -1105}');
S.setField(scene, ID.privacyRt, 'm_SizeDelta', '{x: 620, y: 120}');
S.setField(scene, ID.privacyImage, 'm_Sprite', K.ref(SP('sprite_plate_panel')));
S.setField(scene, ID.privacyImage, 'm_Color', K.col(P.PanelLift));
S.setField(scene, ID.privacyImage, 'm_Type', '1');
S.setField(scene, ID.privacyImage, 'm_PixelsPerUnitMultiplier', '2.2');
for (const [k, c] of Object.entries(QUIET_COLORS)) {
  S.setNested(scene, ID.privacyButton, 'm_' + k[0].toUpperCase() + k.slice(1) + 'Color', K.col(c), 4);
}

// Both captions ship m_IsActive: 0, i.e. blank slabs, and all three of the template's
// TMP labels ship fontSize 24 / fontSizeMin 18 with autosize OFF - under the readable
// floor, and judged by the SERIALISED value, so setting them from code is not enough.
// The play caption also ships a near-black face that would merge into the dark outline.
for (const [goId, tmpId, type] of [
  [ID.playCaptionGo, ID.playCaptionTmp, T.button],
  [ID.privacyCaptionGo, ID.privacyCaptionTmp, { size: 44, min: 34 }],
]) {
  S.setField(scene, goId, 'm_IsActive', '1');
  S.setField(scene, tmpId, 'm_fontSize', String(type.size));
  S.setField(scene, tmpId, 'm_fontSizeBase', String(type.size));
  S.setField(scene, tmpId, 'm_enableAutoSizing', '1');
  S.setField(scene, tmpId, 'm_fontSizeMin', String(type.min));
  S.setField(scene, tmpId, 'm_fontSizeMax', String(type.size));
  S.setField(scene, tmpId, 'm_TextWrappingMode', '0');
  S.setField(scene, tmpId, 'm_fontColor', K.col(P.Text));
  S.setNested(scene, tmpId, 'rgba', String(K.rgba32(P.Text)), 4);
  S.setField(scene, tmpId, 'm_HorizontalAlignment', '2');
  S.setField(scene, tmpId, 'm_VerticalAlignment', '512');
  S.setField(scene, tmpId, 'm_RaycastTarget', '0');
}
S.setField(scene, ID.playCaptionTmp, 'm_text', 'PLAY');
S.setField(scene, ID.privacyCaptionTmp, 'm_text', 'PRIVACY POLICY');
S.setField(scene, ID.playCaptionRt, 'm_SizeDelta', '{x: -48, y: -30}');
S.setField(scene, ID.privacyCaptionRt, 'm_SizeDelta', '{x: -48, y: -26}');

// The loose score label is rewritten with `game_score` by the template EVERY frame, so
// it can never hold anything of ours. It stays off, and its placeholder text goes, so
// no stray string ships even if something switches it on.
S.setField(scene, ID.scoreLabelTmp, 'm_text', '');
S.setField(scene, ID.scoreLabelTmp, 'm_fontSize', '40');
S.setField(scene, ID.scoreLabelTmp, 'm_fontSizeBase', '40');
S.setField(scene, ID.scoreLabelTmp, 'm_fontSizeMin', '32');
S.setField(scene, ID.scoreLabelTmp, 'm_fontSizeMax', '40');

// Backgrounds, now that the dangling guids point at real assets.
S.setField(scene, ID.loaderBgImage, 'm_Sprite', K.ref(SP('BG_loader')));
S.setField(scene, ID.loaderBgImage, 'm_Color', K.col({ r: 1, g: 1, b: 1, a: 1 }));
S.setField(scene, ID.menuBgImage, 'm_Sprite', K.ref(SP('BG_main')));
S.setField(scene, ID.menuBgImage, 'm_Color', K.col({ r: 1, g: 1, b: 1, a: 1 }));

// ── 5. splash content ───────────────────────────────────────────────────────
// An abstract mark and functional captions. No title, nothing that spells it, and
// NOTHING that moves: a looping idle on the loading screen keeps the capture's "has
// the screen settled" probe from ever settling, and the walkthrough comes back with a
// single frame.
const alloc = S.allocator([text], 410000000);
const out = [];
const refs = {};

const splashRec = S.buildTree({
  name: 'SplashMark',
  rect: FULL,
  children: [
    plate('Vignette', FULL, null, P.alpha(P.Base, 0.35)),
    picture('Rosette', at(0, 240, 420, 420), SP('sprite_mark_rosette')),
    label('MarkCaption', at(0, -40, 900, 60), "CURATOR'S DISPLAY", T.caption, P.TextSoft),
    label('LoadingCaption', at(0, -790, 900, 56), 'PREPARING THE DISPLAY', T.caption, P.TextSoft),
  ],
}, ID.loaderUiRt, alloc, out, refs);

// ── 6. the lobby ────────────────────────────────────────────────────────────
const chip = (i) => ({
  name: 'Chip' + i,
  key: 'chip' + i,
  rect: at(-425 + (i * 170), 0, 150, 90),
  children: [
    {
      name: 'Edge', key: 'chip' + i + '.edge', rect: FULL,
      image: { sprite: SP('sprite_chip'), color: P.Primary, imageType: 1, ppu: 2.4 },
    },
    {
      name: 'Plate', key: 'chip' + i + '.plate', rect: inset(10, 10),
      image: { sprite: SP('sprite_chip'), color: P.PanelLift, imageType: 1, ppu: 2.4, raycast: true },
      button: { colors: QUIET_COLORS },
      children: [label('Label', inset(16, 12), 'HALL ' + (i + 1), { size: 40, min: 32 }, P.Text, 'center', 'chip' + i + '.label')],
    },
  ],
  monos: [{
    cls: 'HallChipView', guid: SCRIPT('HallChipView'),
    fields: (r) => [
      '_chipButton: {fileID: ' + r['chip' + i + '.plate'].button + '}',
      '_plate: {fileID: ' + r['chip' + i + '.plate'].image + '}',
      '_edge: {fileID: ' + r['chip' + i + '.edge'].image + '}',
      '_label: {fileID: ' + r['chip' + i + '.label'].tmp + '}',
    ],
  }],
});

const lobby = {
  name: 'LobbyRoot',
  key: 'lobby',
  rect: FULL,
  children: [
    // Pouch bar. The icon, the counter and the unit occupy three bands that never
    // overlap: left alignment moves glyphs inside a rectangle, not the rectangle.
    {
      name: 'TokenBar', key: 'tokenBar', rect: at(-200, 1180, 760, 120),
      image: { sprite: SP('sprite_plate_panel'), color: P.Panel, imageType: 1, ppu: 2.2 },
      children: [
        picture('TokenIcon', at(-310, 0, 72, 72), SP('sprite_icon_token')),
        label('TokenValue', at(-70, 0, 360, 80), '0', T.counter, P.Secondary, 'left', 'tokenValue'),
        label('TokenUnit', at(230, 0, 180, 56), 'TOKENS', T.caption, P.TextSoft, 'left', 'tokenUnit'),
      ],
    },
    iconButton({
      name: 'GearButton', key: 'gear', rect: at(530, 1180, 120, 120),
      sprite: SP('sprite_chip'), plate: P.PanelLift, icon: SP('sprite_icon_gear'), iconSize: 70,
    }),

    // Hero card for the hall in focus.
    {
      name: 'HallCard', key: 'hallCard', rect: at(0, 480, 1020, 1000),
      image: { sprite: SP('sprite_plate_panel'), color: P.Panel, imageType: 1, ppu: 2.2 },
      children: [
        plate('CardGlow', { anchorMin: { x: 0, y: 0.52 }, anchorMax: { x: 1, y: 1 }, pos: { x: 0, y: 0 }, size: { x: -24, y: -24 } },
          SP('sprite_plate_panel'), P.alpha(P.PanelGlow, 0.5)),
        {
          name: 'HallBadge', key: 'hallBadge', rect: at(0, 210, 400, 400),
          image: { sprite: SP('sprite_hall_badge'), color: P.Accent, imageType: 0, preserveAspect: true },
          monos: [{
            cls: 'BadgeRocker', guid: SCRIPT('BadgeRocker'),
            fields: (r) => ['_target: {fileID: ' + r.hallBadge.rt + '}'],
          }],
        },
        label('HallName', at(0, -110, 880, 80), 'HALL', T.hallName, P.Text, 'center', 'hallName'),
        label('HallObjective', at(0, -200, 880, 60), 'OBJECTIVE', T.row, P.TextSoft, 'center', 'hallObjective'),
        pipRow('HallPips', -300, -300, 3, 64, 96, SP('sprite_pip_shard')),
        label('HallStatus', at(176, -300, 600, 60), 'STATUS', T.caption, P.TextSoft, 'left', 'hallStatus'),
      ],
    },

    label('SectionCaption', at(0, -90, 1020, 50), 'SELECT A HALL', { size: 36, min: 30 }, P.TextSoft, 'center', 'sectionCaption'),
    { name: 'ChipsRow', rect: at(0, -215, 1020, 90), children: [0, 1, 2, 3, 4, 5].map(chip) },

    // Button_play is the template's own object and sits between these in the panel.

    textButton({
      name: 'GiftButton', key: 'gift', rect: at(-290, -660, 540, 150),
      sprite: SP('sprite_plate_panel'), plate: P.PanelLift, colors: QUIET_COLORS,
      caption: 'DAILY GIFT', type: { size: 48, min: 36 },
      after: [{
        name: 'Dot', key: 'giftDot', rect: at(215, 46, 28, 28),
        image: { sprite: SP('sprite_pip_shard'), color: P.Secondary, imageType: 0, preserveAspect: true },
      }],
    }),
    textButton({
      name: 'AlbumButton', key: 'album', rect: at(290, -660, 540, 150),
      sprite: SP('sprite_plate_panel'), plate: P.PanelLift, colors: QUIET_COLORS,
      caption: 'ALBUM', type: { size: 48, min: 36 },
    }),

    // Collection strip: a pip gutter on the left, the text column on the right, and
    // the two never share x-space (rule C.25). Pips span -474..-126; the text box
    // starts at -100, which clears the gutter by 26.
    {
      name: 'CollectionStrip', key: 'collection', rect: at(0, -880, 1020, 150),
      image: { sprite: SP('sprite_plate_panel'), color: P.Panel, imageType: 1, ppu: 2.2 },
      children: [
        pipRow('CollectionPips', -300, 0, 6, 48, 60, SP('sprite_pip_shard')),
        label('CollectionLabel', at(190, 0, 580, 60), 'COLLECTION', T.row, P.Text, 'left', 'collectionLabel'),
      ],
    },

    label('Footer', at(0, -1255, 1140, 130), 'FREE TOKENS ONLY', T.caption, P.TextSoft, 'center', 'footer'),
  ],
  monos: [{
    cls: 'LobbyDirector', guid: SCRIPT('LobbyDirector'),
    fields: (r) => [
      '_playButton: {fileID: ' + ID.playButton + '}',
      '_playLabel: {fileID: ' + ID.playCaptionTmp + '}',
      '_playPlate: {fileID: ' + ID.playImage + '}',
      '_privacyLabel: {fileID: ' + ID.privacyCaptionTmp + '}',
      '_privacyPlate: {fileID: ' + ID.privacyImage + '}',
      '_tokenValue: {fileID: ' + r.tokenValue.tmp + '}',
      '_tokenUnit: {fileID: ' + r.tokenUnit.tmp + '}',
      '_hallBadge: {fileID: ' + r.hallBadge.image + '}',
      '_badgeRocker: {fileID: ' + r.hallBadge.monos.BadgeRocker + '}',
      '_hallName: {fileID: ' + r.hallName.tmp + '}',
      '_hallObjective: {fileID: ' + r.hallObjective.tmp + '}',
      '_hallPips:',
      ...pipRefs(r, 'HallPips', 3),
      '_hallStatus: {fileID: ' + r.hallStatus.tmp + '}',
      '_sectionCaption: {fileID: ' + r.sectionCaption.tmp + '}',
      '_chips:',
      ...[0, 1, 2, 3, 4, 5].map((i) => '- {fileID: ' + r['chip' + i].monos.HallChipView + '}'),
      '_giftButton: {fileID: ' + r.gift.button + '}',
      '_giftLabel: {fileID: ' + r['gift.label'].tmp + '}',
      '_giftDot: {fileID: ' + r.giftDot.image + '}',
      '_albumButton: {fileID: ' + r.album.button + '}',
      '_albumLabel: {fileID: ' + r['album.label'].tmp + '}',
      '_settingsButton: {fileID: ' + r.gear.button + '}',
      '_collectionPips:',
      ...pipRefs(r, 'CollectionPips', 6),
      '_collectionLabel: {fileID: ' + r.collectionLabel.tmp + '}',
      '_footer: {fileID: ' + r.footer.tmp + '}',
      '_overlays: {fileID: ' + r.overlays.monos.LobbyOverlayStack + '}',
      '_giftBoard: {fileID: ' + r.giftSheet.monos.DailyBonusBoard + '}',
      '_albumBoard: {fileID: ' + r.albumSheet.monos.AlbumBoard + '}',
      '_settingsBoard: {fileID: ' + r.settingsSheet.monos.SettingsBoard + '}',
      '_themedPanels:',
      '- {fileID: ' + r.tokenBar.image + '}',
      '- {fileID: ' + r.hallCard.image + '}',
      '- {fileID: ' + r.collection.image + '}',
    ],
  }],
};

// ── 7. the three sheets ─────────────────────────────────────────────────────
// Own objects, not template panels: a scene-level m_IsActive modification on a
// template object is silently discarded by Unity, so codegen only switches what it
// created itself. Child order per sheet is scrim -> plate -> content -> cross LAST.
const sheet = (name, key, rect, kids, monos) => ({
  name: name, key: key, active: 0, rect: FULL,
  group: { alpha: 1 },
  monos: [{
    cls: 'UiAppear', guid: SCRIPT('UiAppear'),
    fields: (r) => ['_group: {fileID: ' + r[key].group + '}', '_target: {fileID: ' + r[key + '.card'].rt + '}', '_scaleIn: 1'],
  }].concat(monos || []),
  children: [{
    name: 'Card', key: key + '.card', rect: rect,
    image: { sprite: SP('sprite_plate_panel'), color: P.Panel, imageType: 1, ppu: 2.2, raycast: true },
    children: kids,
  }],
});

const closeCross = (key, x, y) => iconButton({
  name: 'CloseCross', key: key, rect: at(x, y, 110, 110),
  sprite: SP('sprite_chip'), plate: P.PanelLift, icon: SP('sprite_icon_close'), iconSize: 60,
});

const giftSheet = sheet('GiftSheet', 'giftSheet', at(0, 40, 1060, 1560), [
  plate('Inner', inset(40, 40), SP('sprite_plate_panel'), P.alpha(P.PanelGlow, 0.35)),
  label('Heading', at(0, 640, 760, 90), "TODAY'S GIFT", T.heading, P.Accent, 'center', 'gift.heading'),
  picture('GiftArt', at(0, 300, 440, 440), SP('sprite_gift_case'), { r: 1, g: 1, b: 1, a: 1 }, 'gift.art'),
  {
    name: 'RewardChips', key: 'gift.chips', rect: at(0, 10, 640, 110), active: 0,
    children: [
      textButtonless('ChipTokens', at(-170, 0, 300, 110), '+250 TOKENS'),
      textButtonless('ChipSeal', at(170, 0, 300, 110), '+1 SEAL'),
    ],
  },
  pipRow('StreakPips', 0, -170, 7, 56, 86, SP('sprite_pip_shard')),
  label('Explainer', at(0, -300, 900, 110), 'A SEAL COMPLETES ONE RELIC', T.caption, P.TextSoft, 'center', 'gift.explainer'),
  textButton({
    name: 'ClaimButton', key: 'gift.claim', rect: at(0, -460, 640, 160),
    sprite: SP('sprite_cta_plate'), plate: { r: 1, g: 1, b: 1, a: 1 }, caption: 'CLAIM',
  }),
  textButton({
    name: 'ToAlbumButton', key: 'gift.toAlbum', rect: at(0, -650, 540, 130),
    sprite: SP('sprite_plate_panel'), plate: P.PanelLift, colors: QUIET_COLORS,
    caption: 'TO ALBUM', type: { size: 46, min: 36 },
  }),
  closeCross('gift.close', 460, 700),
], [{
  cls: 'DailyBonusBoard', guid: SCRIPT('DailyBonusBoard'),
  fields: (r) => [
    '_claimButton: {fileID: ' + r['gift.claim'].button + '}',
    '_claimLabel: {fileID: ' + r['gift.claim.label'].tmp + '}',
    '_claimPlate: {fileID: ' + r['gift.claim'].image + '}',
    '_toAlbumButton: {fileID: ' + r['gift.toAlbum'].button + '}',
    '_closeButton: {fileID: ' + r['gift.close'].button + '}',
    '_giftImage: {fileID: ' + r['gift.art'].image + '}',
    '_caseClosed: ' + K.ref(SP('sprite_gift_case')),
    '_caseOpen: ' + K.ref(SP('sprite_gift_open')),
    '_rewardChips: {fileID: ' + r['gift.chips'].go + '}',
    '_streakPips:',
    ...pipRefs(r, 'StreakPips', 7),
    '_heading: {fileID: ' + r['gift.heading'].tmp + '}',
    '_explainer: {fileID: ' + r['gift.explainer'].tmp + '}',
  ],
}]);

function textButtonless(name, rect, caption) {
  return {
    name: name, rect: rect,
    image: { sprite: SP('sprite_chip'), color: P.PanelLift, imageType: 1, ppu: 2.4 },
    children: [label('Label', inset(20, 16), caption, { size: 40, min: 32 }, P.Text)],
  };
}

const albumRow = (i) => ({
  name: 'Row' + i,
  key: 'row' + i,
  rect: at(0, 540 - (i * 220), 1000, 190),
  image: { sprite: SP('sprite_plate_panel'), color: P.PanelLift, imageType: 1, ppu: 2.2 },
  children: [
    pipRow('row' + i + 'Pips', -370, 0, 3, 64, 96, SP('sprite_pip_shard')),
    label('Title', at(126, 34, 640, 64), 'HALL', T.row, P.Text, 'left', 'row' + i + '.title'),
    label('Status', at(126, -34, 640, 56), 'STATUS', T.caption, P.TextSoft, 'left', 'row' + i + '.status'),
  ],
  monos: [{
    cls: 'AlbumRowView', guid: SCRIPT('AlbumRowView'),
    fields: (r) => [
      '_plate: {fileID: ' + r['row' + i].image + '}',
      '_pips:',
      ...pipRefs(r, 'row' + i + 'Pips', 3),
      '_title: {fileID: ' + r['row' + i + '.title'].tmp + '}',
      '_status: {fileID: ' + r['row' + i + '.status'].tmp + '}',
    ],
  }],
});

const albumSheet = sheet('AlbumSheet', 'albumSheet', at(0, 0, 1100, 1900), [
  plate('Inner', inset(40, 40), SP('sprite_plate_panel'), P.alpha(P.PanelGlow, 0.3)),
  label('Heading', at(0, 820, 760, 90), 'COLLECTION', T.heading, P.Accent, 'center', 'album.heading'),
  label('SubHeading', at(0, 720, 900, 70), 'HALLS LIT', T.row, P.Secondary, 'center', 'album.sub'),
  { name: 'Rows', key: 'album.rows', rect: at(0, -10, 1020, 1310), children: [0, 1, 2, 3, 4, 5].map(albumRow) },
  textButton({
    name: 'PlayHallButton', key: 'album.play', rect: at(0, -790, 770, 168),
    sprite: SP('sprite_cta_plate'), plate: { r: 1, g: 1, b: 1, a: 1 }, caption: 'PLAY THIS HALL',
  }),
  closeCross('album.close', 470, 820),
], [{
  cls: 'AlbumBoard', guid: SCRIPT('AlbumBoard'),
  fields: (r) => [
    '_rows:',
    ...[0, 1, 2, 3, 4, 5].map((i) => '- {fileID: ' + r['row' + i].monos.AlbumRowView + '}'),
    '_heading: {fileID: ' + r['album.heading'].tmp + '}',
    '_subHeading: {fileID: ' + r['album.sub'].tmp + '}',
    '_playHallButton: {fileID: ' + r['album.play'].button + '}',
    '_playHallLabel: {fileID: ' + r['album.play.label'].tmp + '}',
    '_closeButton: {fileID: ' + r['album.close'].button + '}',
  ],
}]);

// A settings row: the caption, the state word and the pill occupy three bands that do
// not touch. The pill is a button whose knob slides and whose track recolours, so what
// the tap did is legible in a still frame.
const togglePill = (key, y, caption) => ({
  name: key + 'Row', rect: at(0, y, 960, 150),
  children: [
    label('Caption', at(-230, 0, 440, 70), caption, T.row, P.Text, 'left'),
    label('State', at(110, 0, 140, 60), 'OFF', T.caption, P.TextSoft, 'center', key + '.state'),
    {
      name: 'Pill', key: key, rect: at(350, 0, 200, 80),
      image: { sprite: SP('sprite_chip'), color: P.Locked, imageType: 1, ppu: 0.6, raycast: true },
      button: { colors: QUIET_COLORS },
      children: [{
        name: 'Knob', key: key + '.knob', rect: at(-52, 0, 72, 72),
        image: { sprite: SP('sprite_pip_shard'), color: P.Text, imageType: 0, preserveAspect: true },
      }],
      monos: [{
        cls: 'TogglePillView', guid: SCRIPT('TogglePillView'),
        fields: (r) => [
          '_pillButton: {fileID: ' + r[key].button + '}',
          '_track: {fileID: ' + r[key].image + '}',
          '_knob: {fileID: ' + r[key + '.knob'].rt + '}',
          '_stateLabel: {fileID: ' + r[key + '.state'].tmp + '}',
        ],
      }],
    },
  ],
});

const settingsSheet = sheet('SettingsSheet', 'settingsSheet', at(0, 0, 1060, 1500), [
  plate('Inner', inset(40, 40), SP('sprite_plate_panel'), P.alpha(P.PanelGlow, 0.3)),
  label('Heading', at(0, 630, 700, 90), 'SETTINGS', T.heading, P.Accent, 'center', 'settings.heading'),
  {
    name: 'Rows', key: 'settings.rows', rect: at(0, 40, 960, 800),
    children: [
      togglePill('motionToggle', 340, 'REDUCED MOTION'),
      togglePill('contrastToggle', 150, 'HIGH CONTRAST'),
      textButton({
        name: 'HowToButton', key: 'settings.howTo', rect: at(0, -60, 900, 140),
        sprite: SP('sprite_plate_panel'), plate: P.PanelLift, colors: QUIET_COLORS,
        caption: 'HOW TO PLAY', type: { size: 46, min: 36 },
      }),
      textButton({
        name: 'ResetButton', key: 'settings.reset', rect: at(0, -250, 900, 140),
        sprite: SP('sprite_plate_panel'), plate: P.PanelLift, colors: QUIET_COLORS,
        caption: 'RESET COLLECTION', type: { size: 46, min: 36 },
      }),
    ],
  },
  {
    name: 'HowToCard', key: 'settings.howToCard', active: 0, rect: at(0, 40, 960, 760),
    image: { sprite: SP('sprite_plate_panel'), color: P.PanelLift, imageType: 1, ppu: 2.2 },
    children: [
      label('Body', at(0, 90, 880, 420), 'HOW TO PLAY', T.caption, P.TextSoft, 'center', 'settings.howToBody'),
      textButton({
        name: 'BackButton', key: 'settings.howToBack', rect: at(0, -260, 460, 130),
        sprite: SP('sprite_cta_plate'), plate: { r: 1, g: 1, b: 1, a: 1 },
        caption: 'BACK', type: { size: 46, min: 36 },
      }),
    ],
  },
  {
    name: 'ConfirmCard', key: 'settings.confirmCard', active: 0, rect: at(0, 40, 960, 620),
    image: { sprite: SP('sprite_plate_panel'), color: P.PanelLift, imageType: 1, ppu: 2.2 },
    children: [
      label('Body', at(0, 140, 880, 180), 'RESET?', T.body, P.Text, 'center', 'settings.confirmBody'),
      textButton({
        name: 'YesButton', key: 'settings.yes', rect: at(-230, -140, 400, 130),
        sprite: SP('sprite_cta_plate'), plate: { r: 1, g: 1, b: 1, a: 1 },
        caption: 'RESET', type: { size: 46, min: 36 },
      }),
      textButton({
        name: 'NoButton', key: 'settings.no', rect: at(230, -140, 400, 130),
        sprite: SP('sprite_plate_panel'), plate: P.Panel, colors: QUIET_COLORS,
        caption: 'KEEP', type: { size: 46, min: 36 },
      }),
    ],
  },
  label('Footer', at(0, -620, 980, 150), 'TOKENS ARE FICTIONAL', T.caption, P.TextSoft, 'center', 'settings.footer'),
  closeCross('settings.close', 440, 630),
], [{
  cls: 'SettingsBoard', guid: SCRIPT('SettingsBoard'),
  fields: (r) => [
    '_motionToggle: {fileID: ' + r.motionToggle.monos.TogglePillView + '}',
    '_contrastToggle: {fileID: ' + r.contrastToggle.monos.TogglePillView + '}',
    '_howToButton: {fileID: ' + r['settings.howTo'].button + '}',
    '_resetButton: {fileID: ' + r['settings.reset'].button + '}',
    '_closeButton: {fileID: ' + r['settings.close'].button + '}',
    '_rowsRoot: {fileID: ' + r['settings.rows'].go + '}',
    '_howToCard: {fileID: ' + r['settings.howToCard'].go + '}',
    '_howToBackButton: {fileID: ' + r['settings.howToBack'].button + '}',
    '_howToBody: {fileID: ' + r['settings.howToBody'].tmp + '}',
    '_confirmCard: {fileID: ' + r['settings.confirmCard'].go + '}',
    '_confirmYesButton: {fileID: ' + r['settings.yes'].button + '}',
    '_confirmNoButton: {fileID: ' + r['settings.no'].button + '}',
    '_confirmBody: {fileID: ' + r['settings.confirmBody'].tmp + '}',
    '_heading: {fileID: ' + r['settings.heading'].tmp + '}',
    '_footer: {fileID: ' + r['settings.footer'].tmp + '}',
  ],
}]);

const overlays = {
  name: 'LobbyOverlays',
  key: 'overlays',
  rect: FULL,
  children: [
    {
      name: 'Scrim', key: 'scrim', active: 0, rect: FULL,
      image: { color: P.alpha(P.BaseDeep, 0.74), imageType: 0, raycast: true },
    },
    giftSheet,
    albumSheet,
    settingsSheet,
  ],
  monos: [{
    cls: 'LobbyOverlayStack', guid: SCRIPT('LobbyOverlayStack'),
    fields: (r) => [
      '_scrim: {fileID: ' + r.scrim.go + '}',
      '_giftRoot: {fileID: ' + r.giftSheet.go + '}',
      '_albumRoot: {fileID: ' + r.albumSheet.go + '}',
      '_settingsRoot: {fileID: ' + r.settingsSheet.go + '}',
    ],
  }],
};

const lobbyRec = S.buildTree(lobby, ID.menuUiRt, alloc, out, refs);
const overlayRec = S.buildTree(overlays, ID.menuUiRt, alloc, out, refs);

// ── 8. hook the new roots into the template's own containers ────────────────
// The lobby goes BEFORE the template's two buttons so they draw on top of it, and the
// overlays go last so a raised sheet covers everything. The template's own children
// keep their relative order; nothing is moved or renamed.
const loaderKids = S.getChildren(scene, ID.loaderUiRt);
S.setChildren(scene, ID.loaderUiRt, loaderKids.concat([splashRec.rt]));

const menuKids = S.getChildren(scene, ID.menuUiRt);
const playIndex = menuKids.indexOf(ID.playRt);
if (playIndex < 0) throw new Error('Button_play is not a child of the menu canvas');
const newMenuKids = menuKids.slice(0, playIndex)
  .concat([lobbyRec.rt])
  .concat(menuKids.slice(playIndex))
  .concat([overlayRec.rt]);
S.setChildren(scene, ID.menuUiRt, newMenuKids);

S.appendDocs(scene, S.flatten(out));
S.setSceneRoots(scene, S.sceneRoots(scene));
S.write(PATH, S.render(scene));
console.log('[loader] ' + out.length + ' new documents; menu children ' + menuKids.length + ' -> ' + newMenuKids.length);
