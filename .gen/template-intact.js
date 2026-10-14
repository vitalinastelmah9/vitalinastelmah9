// Prove no template object was deleted, renamed, reparented or reordered: compare the
// ORIGINAL scene's objects with the built one, by fileID.
const fs = require('fs');
const parse = (p) => {
  const txt = fs.readFileSync(p, 'utf8');
  const docs = []; const re = /^--- !u!(\d+) &(-?\d+)(.*)$/gm; let m, prev = null;
  while ((m = re.exec(txt))) { if (prev) prev.body = txt.slice(prev.start, m.index); prev = { cls: +m[1], id: m[2], start: re.lastIndex }; docs.push(prev); }
  if (prev) prev.body = txt.slice(prev.start);
  return docs;
};
const f1 = (b, n) => { const r = new RegExp('^  ' + n + ': (.*)$', 'm').exec(b); return r ? r[1].trim() : null; };
const kids = (b) => { const mm = /^  m_Children:(?: \[\]|\n((?:  - \{fileID: -?\d+\}\n)*))/m.exec(b); return (mm && mm[1]) ? [...mm[1].matchAll(/fileID: (-?\d+)/g)].map(x => x[1]) : []; };
let bad = 0;
for (const [orig, built] of [['.gen/Loader.unity.orig', 'Assets/Scenes/Loader.unity'],
                            ['.gen/2_GameScene.unity.orig', 'Assets/Scenes/Games/2_GameScene.unity']]) {
  const A = new Map(parse(orig).map(d => [d.id, d]));
  const B = new Map(parse(built).map(d => [d.id, d]));
  for (const [id, a] of A) {
    const b = B.get(id);
    if (!b) { console.log('DELETED  &' + id + ' (cls' + a.cls + ') from ' + orig); bad++; continue; }
    if (a.cls === 1) {
      const an = f1(a.body, 'm_Name'), bn = f1(b.body, 'm_Name');
      if (an !== bn) { console.log('RENAMED  &' + id + ' "' + an + '" -> "' + bn + '"'); bad++; }
    }
    if (a.cls === 4 || a.cls === 224) {
      if (f1(a.body, 'm_Father') !== f1(b.body, 'm_Father')) { console.log('REPARENTED &' + id); bad++; }
      // Template children must keep their relative order; new siblings may be inserted.
      const ak = kids(a.body), bk = kids(b.body).filter(k => ak.includes(k));
      if (ak.join(',') !== bk.join(',')) { console.log('REORDERED &' + id + ': ' + ak.join(',') + ' -> ' + bk.join(',')); bad++; }
    }
  }
  console.log('  ' + orig.split('/').pop() + ': ' + A.size + ' template documents, all present');
}
console.log(bad ? ('[template] ' + bad + ' violation(s)') : '[template] intact: nothing deleted, renamed, reparented or reordered');
process.exit(bad ? 1 : 0);
