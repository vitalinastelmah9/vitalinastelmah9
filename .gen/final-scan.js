'use strict';
// The checks the pipeline's own gates do not make, in one place.
//
// Every one of these is a defect that compiles, builds, passes every verifier and only
// shows up on a review screenshot or in a store listing.

const fs = require('fs');
const path = require('path');

const GEN = 'Assets/Scripts/Generated';
const SCENES = ['Assets/Scenes/Loader.unity', 'Assets/Scenes/Games/2_GameScene.unity'];
const problems = [];
const notes = [];

const read = (p) => fs.readFileSync(p, 'utf8');
const genFiles = fs.readdirSync(GEN).filter((f) => f.endsWith('.cs'));
// TmpContrastGuard is the pipeline's, staged at build time; it is not ours to judge.
const ours = genFiles.filter((f) => f !== 'TmpContrastGuard.cs');

// ── 1. the title may not appear anywhere the player can see ────────────────
// The app is "Golden Empire Slots" and the package is com.opalsocial.gallery; neither
// the name nor its concept words may reach a label, and no sprite is ever asked to
// carry lettering.
const BANNED = ['GOLDEN', 'EMPIRE', 'SLOTS', 'SLOT', 'OPAL', 'SOCIAL', 'GALLERY'];
const literals = [];
for (const f of ours) {
  const src = read(path.join(GEN, f));
  for (const m of src.matchAll(/"((?:[^"\\]|\\.)*)"/g)) literals.push({ file: f, text: m[1] });
}
for (const s of SCENES) {
  for (const m of read(s).matchAll(/^  m_text: (.*)$/gm)) {
    if (m[1].trim()) literals.push({ file: path.basename(s), text: m[1] });
  }
}
for (const lit of literals) {
  const upper = lit.text.toUpperCase();
  for (const word of BANNED) {
    if (new RegExp('\\b' + word + '\\b').test(upper)) {
      problems.push('the title word "' + word + '" reaches the screen: ' + lit.file + ' -> "' + lit.text + '"');
    }
  }
}

// ── 2. the TMP atlas this pipeline builds is ASCII 32..126 and has no fallback ──
for (const f of ours) {
  const src = read(path.join(GEN, f));
  src.split('\n').forEach((line, i) => {
    for (const ch of line) {
      const cp = ch.codePointAt(0);
      if (cp > 126 || (cp < 32 && cp !== 9)) {
        problems.push(f + ':' + (i + 1) + ' has codepoint ' + cp + ' - the atlas is ASCII only, it would ship as a missing glyph');
        return;
      }
    }
  });
}
for (const s of SCENES) {
  for (const m of read(s).matchAll(/^  m_text: (.*)$/gm)) {
    for (const ch of m[1]) {
      if (ch.codePointAt(0) > 126) problems.push(path.basename(s) + ' m_text has a non-ASCII character: "' + m[1] + '"');
    }
  }
}

// ── 3. forbidden APIs ──────────────────────────────────────────────────────
// No audio of any kind (nothing ships to play), no physics (nothing drives a body),
// and nothing that resolves by a name the obfuscator renames.
const FORBIDDEN = [
  [/\bAudioSource\b|\bAudioClip\b|\bAudioListener\b|PlayOneShot/, 'audio API in a game that ships no sound'],
  [/\bRigidbody2?D\b|\bCollider2?D\b|\bPhysics2?D\b|AddForce|OnCollision|OnTrigger|gravityScale/, 'physics API in a game that drives no bodies'],
  [/GameObject\.Find|transform\.Find|\.Find\s*\(\s*"/, 'lookup by name'],
  [/Resources\.Load/, 'Resources.Load by string'],
  [/\bnameof\s*\(/, 'nameof for an identifier'],
  [/UnityEngine\.Random|(?<!System\.)\bRandom\.(?:Range|value|insideUnit)/, 'UnityEngine.Random carries global state'],
  [/SendMessage|\bInvoke\s*\(\s*"|Type\.GetType/, 'reflection or a string method name'],
  [/DOTween|\.SetLink\(/, 'DOTween, which this template does not ship'],
  [/Screen\.orientation|Screen\.autorotateTo/, 'orientation set from code instead of ProjectSettings'],
];
for (const f of ours) {
  const src = read(path.join(GEN, f));
  src.split('\n').forEach((line, i) => {
    if (/^\s*(\/\/|\*|\/\*)/.test(line)) return;        // a comment is not an API call
    for (const [re, why] of FORBIDDEN) {
      if (re.test(line)) problems.push(f + ':' + (i + 1) + ' ' + why + ': ' + line.trim().slice(0, 80));
    }
  });
}

// ── 4. shape: one public type per file, named after the file, and sealed ───
for (const f of ours) {
  const src = read(path.join(GEN, f));
  const decls = [...src.matchAll(/^public\s+(?:sealed\s+class|static\s+class|readonly\s+struct|enum)\s+(\w+)/gm)];
  const loose = [...src.matchAll(/^public\s+(?:partial\s+)?class\s+(\w+)/gm)];
  if (loose.length) problems.push(f + ' declares a public class that is neither sealed nor static: ' + loose.map((m) => m[1]).join(', '));
  if (decls.length !== 1) problems.push(f + ' declares ' + decls.length + ' public type(s); one per file');
  else if (decls[0][1] !== f.slice(0, -3)) problems.push(f + ' declares ' + decls[0][1] + ', which does not match the file name');
  if (/\bstatic\s+\w+\s+s_instance|\bpublic\s+static\s+\w+\s+Instance\b/.test(src)) {
    problems.push(f + ' introduces a singleton; the template owns the only ones');
  }
}

// ── 5. every button must have a caption or an icon, never neither ──────────
// A Button whose object has no TMP child and no icon child is a blank slab.
for (const s of SCENES) {
  const txt = read(s);
  const docs = [];
  const re = /^--- !u!(\d+) &(-?\d+)(.*)$/gm;
  let m, prev = null;
  while ((m = re.exec(txt))) {
    if (prev) prev.body = txt.slice(prev.start, m.index);
    prev = { cls: +m[1], id: m[2], start: re.lastIndex };
    docs.push(prev);
  }
  if (prev) prev.body = txt.slice(prev.start);
  const byId = new Map(docs.map((d) => [d.id, d]));
  const goOf = (d) => (/m_GameObject: \{fileID: (-?\d+)\}/.exec(d.body) || [])[1];
  const rtOfGo = new Map();
  for (const d of docs) {
    if (d.cls === 4 || d.cls === 224) rtOfGo.set(goOf(d), d.id);
  }
  const kidsOf = (rt) => {
    const mm = /^  m_Children:(?: \[\]|\n((?:  - \{fileID: -?\d+\}\n)*))/m.exec(byId.get(rt).body);
    return (mm && mm[1]) ? [...mm[1].matchAll(/fileID: (-?\d+)/g)].map((x) => x[1]) : [];
  };
  const hasVisual = (rt, depth) => {
    if (depth > 3) return false;
    const go = goOf(byId.get(rt));
    const comps = [...(byId.get(go) || { body: '' }).body.matchAll(/-\s*component: \{fileID: (-?\d+)\}/g)].map((x) => x[1]);
    for (const c of comps) {
      const d = byId.get(c);
      if (!d) continue;
      if (/TMPro\.TextMeshProUGUI/.test(d.body)) return true;
      if (depth > 0 && /UnityEngine\.UI\.Image/.test(d.body) && /m_Sprite: \{fileID: \d+, guid:/.test(d.body)) return true;
    }
    return kidsOf(rt).some((k) => hasVisual(k, depth + 1));
  };
  for (const d of docs) {
    if (d.cls !== 114 || !/^\s*m_OnClick:/m.test(d.body)) continue;
    const go = goOf(d);
    const rt = rtOfGo.get(go);
    const name = (/^\s*m_Name: (.*)$/m.exec((byId.get(go) || { body: '' }).body) || [])[1] || '?';
    if (!rt || !hasVisual(rt, 0)) {
      problems.push(path.basename(s) + ': the button on "' + name.trim() + '" has neither a caption nor an icon');
    }
  }
}

// ── 6. nothing the pipeline needs may be missing ───────────────────────────
for (const need of ['assets.json', 'screenshot-scenario.json', '.art-style',
  'Assets/Art/Textures/game_icon.png.meta']) {
  if (!fs.existsSync(need)) problems.push('missing ' + need);
}
if (fs.readdirSync('Assets', { recursive: true }).some((f) => path.basename(f).startsWith('._'))) {
  problems.push('AppleDouble files are back; the font stage dies reading one as UTF-8');
}

// Scenario sanity: the shots must be distinct and every tap must carry a label.
const scen = JSON.parse(read('screenshot-scenario.json'));
const shots = scen.steps.map((s) => s.shot);
if (new Set(shots).size !== shots.length) problems.push('screenshot-scenario has duplicate shot names');
for (const s of scen.steps) {
  if (s.tapFrac && !s.label) problems.push('a scenario tap has no label');
}

for (const n of notes) console.log('note: ' + n);
if (problems.length) {
  for (const p of problems) console.log('PROBLEM ' + p);
  console.log('[final-scan] ' + problems.length + ' problem(s) across ' + ours.length + ' generated file(s)');
  process.exit(1);
}
console.log('[final-scan] clean: ' + ours.length + ' generated files, ' + literals.length
  + ' visible strings, ' + scen.steps.length + ' scenario steps');
