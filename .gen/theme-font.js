'use strict';
// Theme the one TMP material the whole app renders through.
//
// The outline is not optional and it is not just a width: without OUTLINE_ON in the
// keyword list TMP draws no outline at all, whatever _OutlineWidth says. The ceilings
// are the ones the atlas can carry - dilate + outline <= 0.45, bold <= 0.5, underlay
// dilate <= 0.25 - because the SDF this pipeline builds is 256x256 at padding 5, about
// 22 pt glyphs, and heavier values fill in the holes of P, R, 6 and A until RETRY is a
// blob and PLAY reads FLAY.
//
// The glyphs and the atlas belong to the pipeline's own font stage; nothing here
// touches _GradientScale, the texture sizes or m_AtlasPadding.

const fs = require('fs');

const PATH = 'Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset';
let s = fs.readFileSync(PATH, 'utf8');

const FLOATS = {
  _FaceDilate: '0.2',
  _OutlineWidth: '0.24',
  _OutlineSoftness: '0',
  _WeightBold: '0.45',
  _UnderlayDilate: '0.15',
  _UnderlayOffsetX: '0.6',
  _UnderlayOffsetY: '-0.6',
  _UnderlaySoftness: '0.25',
};
// #F8F5FF face on a #0E0A18 outline: WCAG contrast about 18:1, far above the 7:1 the
// contrast pass aims for, and every label colour in the palette is light for the same
// reason (there is one material, so there is one outline).
const COLORS = {
  _FaceColor: '{r: 0.972549, g: 0.960784, b: 1, a: 1}',
  _OutlineColor: '{r: 0.054902, g: 0.039216, b: 0.094118, a: 1}',
  _UnderlayColor: '{r: 0.054902, g: 0.039216, b: 0.094118, a: 0.6}',
};

for (const [k, v] of Object.entries(FLOATS)) {
  const re = new RegExp('^    - ' + k + ': .*$', 'm');
  if (!re.test(s)) throw new Error('font material has no float ' + k);
  s = s.replace(re, '    - ' + k + ': ' + v);
}
for (const [k, v] of Object.entries(COLORS)) {
  const re = new RegExp('^    - ' + k + ': .*$', 'm');
  if (!re.test(s)) throw new Error('font material has no colour ' + k);
  s = s.replace(re, '    - ' + k + ': ' + v);
}

// Both keyword lists: this asset serialises m_ShaderKeywords, newer TMP reads
// m_ValidKeywords, and the outline is drawn only when the keyword is present.
const KEYWORDS = ['OUTLINE_ON', 'UNDERLAY_ON'];
s = s.replace(/^  m_ShaderKeywords: ?.*$/m, '  m_ShaderKeywords: ' + KEYWORDS.join(' '));
if (!/^  m_ValidKeywords:/m.test(s)) {
  s = s.replace(/^  m_ShaderKeywords: .*$/m,
    (line) => line + '\n  m_ValidKeywords:\n' + KEYWORDS.map((k) => '  - ' + k).join('\n') + '\n  m_InvalidKeywords: []');
}

fs.writeFileSync(PATH, s);

// Prove the ceilings hold, rather than trusting the numbers above.
const num = (k) => parseFloat(new RegExp('^    - ' + k + ': ([-\\d.]+)$', 'm').exec(s)[1]);
const dilate = num('_FaceDilate');
const outline = num('_OutlineWidth');
const bold = num('_WeightBold');
const under = num('_UnderlayDilate');
const fail = [];
if (!(outline >= 0.2 && outline <= 0.3)) fail.push('_OutlineWidth ' + outline + ' outside 0.20..0.30');
if (!(dilate >= 0.15 && dilate <= 0.25)) fail.push('_FaceDilate ' + dilate + ' outside 0.15..0.25');
if (dilate + outline > 0.45) fail.push('dilate+outline ' + (dilate + outline) + ' > 0.45');
if (bold > 0.5) fail.push('_WeightBold ' + bold + ' > 0.5');
if (under > 0.25) fail.push('_UnderlayDilate ' + under + ' > 0.25');
if (!/OUTLINE_ON/.test(s)) fail.push('OUTLINE_ON keyword missing - TMP would draw no outline at all');
if (fail.length) { for (const f of fail) console.error('[font] ' + f); process.exit(1); }
console.log('[font] themed: face #F8F5FF, outline #0E0A18 at ' + outline
  + ', dilate ' + dilate + ' (sum ' + (dilate + outline).toFixed(2) + '), bold ' + bold
  + ', underlay dilate ' + under + ', OUTLINE_ON set');
