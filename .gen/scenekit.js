'use strict';
// Document-level surgery on an existing Unity scene/prefab text, plus a declarative
// tree builder. Everything works on the raw text so nothing is reformatted.

const fs = require('fs');
const K = require('./yamlkit');

function parse(text) {
  const docs = [];
  const re = /^--- !u!(\d+) &(-?\d+)(.*)$/gm;
  let m, prev = null;
  while ((m = re.exec(text)) !== null) {
    if (prev) prev.body = text.slice(prev.start, m.index);
    prev = { cls: +m[1], id: m[2], tail: m[3] || '', header: m[0], start: re.lastIndex + 1 };
    docs.push(prev);
  }
  if (prev) prev.body = text.slice(prev.start);
  const head = docs.length ? text.slice(0, docs[0].start - docs[0].header.length - 1) : text;
  return { head, docs };
}

function render(scene) {
  return scene.head + scene.docs.map((d) => d.header + '\n' + d.body).join('');
}

function byId(scene, id) {
  const d = scene.docs.find((x) => x.id === String(id));
  if (!d) throw new Error('scenekit: no document &' + id);
  return d;
}

// Replace a top-level (two-space indented) field's value in one document.
function setField(scene, id, key, value) {
  const d = byId(scene, id);
  const re = new RegExp('^(  ' + key.replace(/[.*+?^${}()|[\]\\]/g, '\\$&') + ': ).*$', 'm');
  if (!re.test(d.body)) throw new Error('scenekit: &' + id + ' has no field ' + key);
  d.body = d.body.replace(re, '$1' + value);
  return scene;
}

// Replace a nested field (any indentation) inside one document, first match only.
function setNested(scene, id, key, value, indent) {
  const d = byId(scene, id);
  const pad = indent === undefined ? '\\s+' : ' '.repeat(indent);
  const re = new RegExp('^(' + pad + key.replace(/[.*+?^${}()|[\]\\]/g, '\\$&') + ': ).*$', 'm');
  if (!re.test(d.body)) throw new Error('scenekit: &' + id + ' has no nested field ' + key);
  d.body = d.body.replace(re, '$1' + value);
  return scene;
}

// Rewrite a RectTransform's m_Children list.
function setChildren(scene, rtId, kids) {
  const d = byId(scene, rtId);
  const list = kids.length ? '\n' + kids.map((c) => '  - {fileID: ' + c + '}').join('\n') : ' []';
  const re = /^  m_Children:(?: \[\]|\n(?:  - \{fileID: -?\d+\}\n)*)/m;
  if (!re.test(d.body)) throw new Error('scenekit: &' + rtId + ' has no m_Children');
  d.body = d.body.replace(re, '  m_Children:' + list + '\n');
  return scene;
}

function getChildren(scene, rtId) {
  const d = byId(scene, rtId);
  const m = /^  m_Children:(?: \[\]|\n((?:  - \{fileID: -?\d+\}\n)*))/m.exec(d.body);
  if (!m || !m[1]) return [];
  return [...m[1].matchAll(/fileID: (-?\d+)/g)].map((x) => x[1]);
}

// Append a component fileID to a GameObject's m_Component list.
function addComponent(scene, goId, compId) {
  const d = byId(scene, goId);
  const re = /^(  m_Component:\n(?:  - component: \{fileID: -?\d+\}\n)*)/m;
  if (!re.test(d.body)) throw new Error('scenekit: &' + goId + ' has no m_Component');
  d.body = d.body.replace(re, '$1  - component: {fileID: ' + compId + '}\n');
  return scene;
}

// Append whole documents (strings). Unity keeps SceneRoots last, so insert before it.
function appendDocs(scene, texts) {
  const blob = texts.join('');
  const parsed = parse('%YAML\n' + blob);
  const fresh = parsed.docs;
  const rootsIdx = scene.docs.findIndex((d) => d.cls === 1660057539);
  if (rootsIdx < 0) scene.docs.push(...fresh);
  else scene.docs.splice(rootsIdx, 0, ...fresh);
  return scene;
}

// Ensure a SceneRoots document exists and lists exactly these root transform fileIDs,
// and that it is the LAST document in the file (Unity always writes it last).
function setSceneRoots(scene, roots) {
  const list = roots.map((r) => '  - {fileID: ' + r + '}').join('\n');
  const body = ['  m_ObjectHideFlags: 0', '  m_Roots:', list, ''].join('\n');
  const idx = scene.docs.findIndex((d) => d.cls === 1660057539);
  let doc;
  if (idx >= 0) { doc = scene.docs.splice(idx, 1)[0]; doc.body = body; }
  else doc = { cls: 1660057539, id: '9223372036854775807', header: '--- !u!1660057539 &9223372036854775807\nSceneRoots:', body: body };
  doc.header = '--- !u!1660057539 &' + doc.id + '\nSceneRoots:';
  scene.docs.push(doc);
  return scene;
}

function sceneRoots(scene) {
  const d = scene.docs.find((x) => x.cls === 1660057539);
  if (!d) return [];
  return [...d.body.matchAll(/fileID: (-?\d+)/g)].map((x) => x[1]);
}

// ── declarative tree ────────────────────────────────────────────────────────
// node = { name, key?, active?, rect:{anchorMin,anchorMax,pos,size,pivot,scale},
//          image?:{...}, tmp?:{...}, button?:{...}, mask?:true, group?:{...},
//          canvas?:{order}, scaler?:{...}, raycaster?:true,
//          monos?: [ {cls, guid, fields: (refs)=>[...] } ],
//          children?: [node] }
// Allocates ids, pushes documents into `out` (mono bodies deferred as functions),
// and records refs[key] = { go, rt, image, tmp, button, group, mask }.
function buildTree(node, fatherRtId, alloc, out, refs) {
  const go = alloc();
  const rt = alloc();
  const comps = [rt];
  const rec = { go: go, rt: rt };

  let crId = null;
  const needsCanvasRenderer = !!(node.image || node.tmp);
  if (node.canvas) { const c = alloc(); comps.push(c); out.push(K.canvas(c, { go: go, order: node.canvas.order })); rec.canvas = c; }
  if (node.scaler) { const c = alloc(); comps.push(c); out.push(K.canvasScaler(c, Object.assign({ go: go }, node.scaler))); rec.scaler = c; }
  if (node.raycaster) { const c = alloc(); comps.push(c); out.push(K.graphicRaycaster(c, { go: go })); rec.raycaster = c; }
  if (needsCanvasRenderer) { crId = alloc(); comps.push(crId); rec.cr = crId; }
  if (node.image) { const c = alloc(); comps.push(c); out.push(K.image(c, Object.assign({ go: go }, node.image))); rec.image = c; }
  if (node.tmp) { const c = alloc(); comps.push(c); out.push(K.tmpText(c, Object.assign({ go: go }, node.tmp))); rec.tmp = c; }
  if (node.mask) { const c = alloc(); comps.push(c); out.push(K.rectMask2D(c, { go: go })); rec.mask = c; }
  if (node.group) { const c = alloc(); comps.push(c); out.push(K.canvasGroup(c, Object.assign({ go: go }, node.group))); rec.group = c; }
  if (node.button) {
    const c = alloc(); comps.push(c);
    out.push(K.button(c, Object.assign({ go: go, target: rec.image || 0 }, node.button)));
    rec.button = c;
  }
  if (node.monos) {
    rec.monos = {};
    for (const m of node.monos) {
      const c = alloc(); comps.push(c);
      rec.monos[m.cls] = c;
      out.push(() => K.mono(c, { go: go, guid: m.guid, cls: m.cls, fields: m.fields ? m.fields(refs) : [] }));
    }
  }

  const kids = [];
  const kidNodes = node.children || [];
  // Reserve this node's documents first so ids read in hierarchy order.
  out.push(K.gameObject(go, { name: node.name, comps: comps, active: node.active, layer: node.layer }));
  const rtPlaceholder = { i: out.length };
  out.push('');
  if (crId !== null) out.push(K.canvasRenderer(crId, go));

  if (node.key) refs[node.key] = rec;

  for (const kid of kidNodes) {
    const kr = buildTree(kid, rt, alloc, out, refs);
    kids.push(kr.rt);
  }
  const r = node.rect || {};
  out[rtPlaceholder.i] = K.rectTransform(rt, {
    go: go, father: fatherRtId, children: kids,
    anchorMin: r.anchorMin, anchorMax: r.anchorMax, pos: r.pos, size: r.size,
    pivot: r.pivot, scale: r.scale,
  });
  return rec;
}

function flatten(out) {
  return out.map((x) => (typeof x === 'function' ? x() : x));
}

// A fileID allocator that never collides with ids already in the given texts.
function allocator(texts, start) {
  const used = new Set();
  for (const t of texts) for (const m of t.matchAll(/&(-?\d+)/g)) used.add(m[1]);
  let next = start;
  return () => { while (used.has(String(next))) next++; used.add(String(next)); return next++; };
}

module.exports = {
  parse, render, byId, setField, setNested, setChildren, getChildren,
  addComponent, appendDocs, setSceneRoots, sceneRoots, buildTree, flatten, allocator,
  read: (p) => fs.readFileSync(p, 'utf8'),
  write: (p, s) => fs.writeFileSync(p, s),
};
