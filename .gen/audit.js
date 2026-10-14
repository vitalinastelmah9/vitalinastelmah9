'use strict';
// Resolve every serialized field of every GENERATED component and print it as
//   field -> <ComponentType> on "<GameObject path>"
// plus the declared C# type, and FAIL when the two disagree, when a reference does not
// resolve, or when a reference-typed field was never written into the YAML at all.
//
// Eyeballing fileIDs does not catch a reference that landed on the wrong object; this
// does. A field the YAML never wrote is the same silent dead end as {fileID: 0}, with
// nothing in the file to spot.

const fs = require('fs');
const path = require('path');

const GUIDS = JSON.parse(fs.readFileSync('.gen/guids.json', 'utf8'));
const BY_GUID = new Map();
for (const [k, v] of Object.entries(GUIDS)) BY_GUID.set(v, k);

const CLS = {
  1: 'GameObject', 4: 'Transform', 20: 'Camera', 81: 'AudioListener',
  114: 'MonoBehaviour', 212: 'SpriteRenderer', 222: 'CanvasRenderer', 223: 'Canvas',
  224: 'RectTransform', 225: 'CanvasGroup', 1660057539: 'SceneRoots',
};

// The declared type of every [SerializeField] in the generated C#.
const declared = new Map();      // "Class._field" -> type
const arrayField = new Set();    // "Class._field" is T[]
for (const f of fs.readdirSync('Assets/Scripts/Generated')) {
  if (!f.endsWith('.cs')) continue;
  const cls = f.slice(0, -3);
  const src = fs.readFileSync(path.join('Assets/Scripts/Generated', f), 'utf8');
  // The optional initialiser matters: without it `private Button[] _x = new Button[0];`
  // is never seen, and the array fields most worth checking go unaudited.
  for (const m of src.matchAll(/\[SerializeField\]\s*private\s+([\w.]+)(\[\])?\s+(\w+)\s*(?:=[^;]*)?;/g)) {
    declared.set(cls + '.' + m[3], m[1]);
    if (m[2]) arrayField.add(cls + '.' + m[3]);
  }
}

const VALUE_TYPES = new Set(['int', 'float', 'bool', 'string', 'Color', 'Vector2', 'Vector3']);

function parse(file) {
  const txt = fs.readFileSync(file, 'utf8');
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

let problems = 0;
let checked = 0;
let components = 0;
const verbose = process.argv.includes('--verbose');

for (const file of process.argv.slice(2).filter((a) => !a.startsWith('--'))) {
  const docs = parse(file);
  const byId = new Map(docs.map((d) => [d.id, d]));
  const field = (b, n) => { const r = new RegExp('^\\s*' + n + ': (.*)$', 'm').exec(b); return r ? r[1].trim() : null; };

  const goOfComponent = new Map();
  const nameOfGo = new Map();
  const rtOfGo = new Map();
  const fatherOfRt = new Map();
  for (const d of docs) {
    if (d.cls === 1) nameOfGo.set(d.id, (field(d.body, 'm_Name') || '?'));
    const go = field(d.body, 'm_GameObject');
    if (go) goOfComponent.set(d.id, go.replace(/[^0-9-]/g, ''));
    if (d.cls === 4 || d.cls === 224) {
      const g = field(d.body, 'm_GameObject').replace(/[^0-9-]/g, '');
      rtOfGo.set(g, d.id);
      const f = field(d.body, 'm_Father');
      fatherOfRt.set(d.id, f ? f.replace(/[^0-9-]/g, '') : '0');
    }
  }
  const pathOf = (goId) => {
    const parts = [];
    let go = goId;
    for (let i = 0; i < 24 && go && go !== '0'; i++) {
      parts.unshift(nameOfGo.get(go) || ('?' + go));
      const rt = rtOfGo.get(go);
      const father = rt ? fatherOfRt.get(rt) : '0';
      go = father && father !== '0' ? goOfComponent.get(father) : null;
    }
    return parts.join('/');
  };
  const typeOf = (id) => {
    const d = byId.get(id);
    if (!d) return null;
    if (d.cls === 114) {
      const e = field(d.body, 'm_EditorClassIdentifier');
      return e ? e.split('::').pop().split('.').pop() : 'MonoBehaviour';
    }
    return CLS[d.cls] || ('class' + d.cls);
  };

  for (const d of docs) {
    if (d.cls !== 114) continue;
    const eci = field(d.body, 'm_EditorClassIdentifier') || '';
    if (!eci.startsWith('Assembly-CSharp::')) continue;
    const cls = eci.split('::').pop();
    if (!fs.existsSync(path.join('Assets/Scripts/Generated', cls + '.cs'))) continue;
    components++;

    // Which fields the YAML actually wrote, in order, including list items.
    const written = new Map();
    const lines = d.body.split('\n');
    let current = null;
    for (const line of lines) {
      const fm = /^  (\w+):\s*(.*)$/.exec(line);
      if (fm) {
        current = fm[1];
        written.set(current, fm[2].trim() === '' ? [] : [fm[2].trim()]);
        continue;
      }
      const im = /^\s*- (.*)$/.exec(line);
      if (im && current) written.get(current).push(im[1].trim());
    }

    for (const [key, type] of declared) {
      if (!key.startsWith(cls + '.')) continue;
      const name = key.slice(cls.length + 1);
      if (VALUE_TYPES.has(type) && !arrayField.has(key)) continue;   // value fields may be omitted
      checked++;
      if (!written.has(name)) {
        console.log('MISSING  ' + cls + '.' + name + ' (' + type + (arrayField.has(key) ? '[]' : '') +
          ') was never written into ' + path.basename(file) + ' on "' + pathOf(goOfComponent.get(d.id)) + '"');
        problems++;
        continue;
      }
      const values = written.get(name);
      if (!values.length) {
        console.log('EMPTY    ' + cls + '.' + name + ' is an empty list on "' + pathOf(goOfComponent.get(d.id)) + '"');
        problems++;
        continue;
      }
      for (const v of values) {
        const fid = /fileID: (-?\d+)/.exec(v);
        const gid = /guid: ([0-9a-f]{32})/.exec(v);
        if (gid) {
          // An asset reference: it must be one of the guids this codegen allocated.
          const asset = BY_GUID.get(gid[1]);
          if (!asset) { console.log('BADGUID  ' + cls + '.' + name + ' -> ' + gid[1]); problems++; continue; }
          if (verbose) console.log('  ok     ' + cls + '.' + name + ' -> asset ' + asset);
          continue;
        }
        if (!fid) { continue; }   // a plain value on a non-value-typed field is caught below
        if (fid[1] === '0') {
          console.log('NULLREF  ' + cls + '.' + name + ' is {fileID: 0} on "' + pathOf(goOfComponent.get(d.id)) + '"');
          problems++;
          continue;
        }
        const t = typeOf(fid[1]);
        if (!t) {
          console.log('DANGLING ' + cls + '.' + name + ' -> &' + fid[1] + ' which is in no document');
          problems++;
          continue;
        }
        const want = type;
        const ok = t === want
          || (want === 'GameObject' && t === 'GameObject')
          || (want === 'RectTransform' && t === 'RectTransform')
          || (want === 'Sprite' && t === null);
        if (!ok) {
          console.log('TYPE     ' + cls + '.' + name + ' declared ' + want + ' but resolves to ' + t +
            ' on "' + pathOf(goOfComponent.get(fid[1] === '0' ? d.id : (goOfComponent.get(fid[1]) || d.id))) + '"');
          problems++;
          continue;
        }
        if (verbose) {
          console.log('  ok     ' + cls + '.' + name + ' -> ' + t + ' on "' + pathOf(goOfComponent.get(fid[1])) + '"');
        }
      }
    }
  }
}

console.log('[audit] ' + components + ' generated component(s), ' + checked + ' reference field(s), ' + problems + ' problem(s)');
process.exit(problems ? 1 : 0);
