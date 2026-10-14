'use strict';
// Emitters for Unity 6 YAML documents, shaped from this template's own blocks.
// Every emitter takes a fileID + options and returns one complete document string.

const n = (v) => (Number.isFinite(v) ? (Math.round(v * 1e6) / 1e6) : 0);
const v2 = (o, d) => { const p = o || d; return '{x: ' + n(p.x) + ', y: ' + n(p.y) + '}'; };
const col = (c) => '{r: ' + n(c.r) + ', g: ' + n(c.g) + ', b: ' + n(c.b) + ', a: ' + n(c.a === undefined ? 1 : c.a) + '}';
const ref = (r) => {
  if (!r) return '{fileID: 0}';
  if (typeof r === 'number' || typeof r === 'string') return '{fileID: ' + r + '}';
  return '{fileID: ' + r.fileID + ', guid: ' + r.guid + ', type: ' + r.type + '}';
};
const PRE = (goId) => [
  '  m_ObjectHideFlags: 0',
  '  m_CorrespondingSourceObject: {fileID: 0}',
  '  m_PrefabInstance: {fileID: 0}',
  '  m_PrefabAsset: {fileID: 0}',
  '  m_GameObject: {fileID: ' + goId + '}',
  '  m_Enabled: 1',
  '  m_EditorHideFlags: 0',
].join('\n');

function gameObject(id, o) {
  const comps = (o.comps || []).map((c) => '  - component: {fileID: ' + c + '}').join('\n');
  return [
    '--- !u!1 &' + id,
    'GameObject:',
    '  m_ObjectHideFlags: 0',
    '  m_CorrespondingSourceObject: {fileID: 0}',
    '  m_PrefabInstance: {fileID: 0}',
    '  m_PrefabAsset: {fileID: 0}',
    '  serializedVersion: 6',
    '  m_Component:',
    comps,
    '  m_Layer: ' + (o.layer === undefined ? 5 : o.layer),
    '  m_Name: ' + o.name,
    '  m_TagString: Untagged',
    '  m_Icon: {fileID: 0}',
    '  m_NavMeshLayer: 0',
    '  m_StaticEditorFlags: 0',
    '  m_IsActive: ' + (o.active === 0 ? 0 : 1),
    '',
  ].join('\n');
}

function rectTransform(id, o) {
  const kids = (o.children || []).map((c) => '  - {fileID: ' + c + '}').join('\n');
  const s = o.scale || { x: 1, y: 1 };
  return [
    '--- !u!224 &' + id,
    'RectTransform:',
    '  m_ObjectHideFlags: 0',
    '  m_CorrespondingSourceObject: {fileID: 0}',
    '  m_PrefabInstance: {fileID: 0}',
    '  m_PrefabAsset: {fileID: 0}',
    '  m_GameObject: {fileID: ' + o.go + '}',
    '  m_LocalRotation: {x: -0, y: -0, z: -0, w: 1}',
    '  m_LocalPosition: {x: 0, y: 0, z: 0}',
    '  m_LocalScale: {x: ' + n(s.x) + ', y: ' + n(s.y) + ', z: 1}',
    '  m_ConstrainProportionsScale: 0',
    '  m_Children:' + (kids ? '\n' + kids : ' []'),
    '  m_Father: {fileID: ' + (o.father || 0) + '}',
    '  m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}',
    '  m_AnchorMin: ' + v2(o.anchorMin, { x: 0.5, y: 0.5 }),
    '  m_AnchorMax: ' + v2(o.anchorMax, { x: 0.5, y: 0.5 }),
    '  m_AnchoredPosition: ' + v2(o.pos, { x: 0, y: 0 }),
    '  m_SizeDelta: ' + v2(o.size, { x: 100, y: 100 }),
    '  m_Pivot: ' + v2(o.pivot, { x: 0.5, y: 0.5 }),
    '',
  ].join('\n');
}

function canvasRenderer(id, go) {
  return [
    '--- !u!222 &' + id,
    'CanvasRenderer:',
    '  m_ObjectHideFlags: 0',
    '  m_CorrespondingSourceObject: {fileID: 0}',
    '  m_PrefabInstance: {fileID: 0}',
    '  m_PrefabAsset: {fileID: 0}',
    '  m_GameObject: {fileID: ' + go + '}',
    '  m_CullTransparentMesh: 1',
    '',
  ].join('\n');
}

const G_IMAGE = 'fe87c0e1cc204ed48ad3b37840f39efc';
const G_BUTTON = '4e29b1a8efbd4b44bb3f3716e73f07ff';
const G_TMP = 'f4688fdb7df04437aeb418b961361dc5';
const G_SCALER = '0cd44c1031e13a943bb63640046fad76';
const G_RAYCASTER = 'dc42784cf147c0c48a680349fa168899';
const G_RECTMASK = '3312d7739989d2b4e91e6319e9a96d76';

function image(id, o) {
  return [
    '--- !u!114 &' + id,
    'MonoBehaviour:',
    PRE(o.go),
    '  m_Script: {fileID: 11500000, guid: ' + G_IMAGE + ', type: 3}',
    '  m_Name: ',
    '  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.Image',
    '  m_Material: {fileID: 0}',
    '  m_Color: ' + col(o.color || { r: 1, g: 1, b: 1, a: 1 }),
    '  m_RaycastTarget: ' + (o.raycast ? 1 : 0),
    '  m_RaycastPadding: {x: 0, y: 0, z: 0, w: 0}',
    '  m_Maskable: ' + (o.maskable === false ? 0 : 1),
    '  m_OnCullStateChanged:',
    '    m_PersistentCalls:',
    '      m_Calls: []',
    '  m_Sprite: ' + ref(o.sprite),
    '  m_Type: ' + (o.imageType || 0),
    '  m_PreserveAspect: ' + (o.preserveAspect ? 1 : 0),
    '  m_FillCenter: 1',
    '  m_FillMethod: 4',
    '  m_FillAmount: ' + (o.fillAmount === undefined ? 1 : n(o.fillAmount)),
    '  m_FillClockwise: 1',
    '  m_FillOrigin: 0',
    '  m_UseSpriteMesh: 0',
    '  m_PixelsPerUnitMultiplier: ' + (o.ppu === undefined ? 1 : n(o.ppu)),
    '',
  ].join('\n');
}

const FONT = { fileID: 11400000, guid: '8f586378b4e144a9851e7b34d9b748ee', type: 2 };
const FONT_MAT = { fileID: 2180264, guid: '8f586378b4e144a9851e7b34d9b748ee', type: 2 };
// little-endian r | g<<8 | b<<16 | a<<24
const rgba32 = (c) => {
  const b = (x) => Math.max(0, Math.min(255, Math.round((x === undefined ? 1 : x) * 255)));
  return (b(c.r) | (b(c.g) << 8) | (b(c.b) << 16) | (b(c.a) << 24)) >>> 0;
};
// Horizontal: 1 Left, 2 Center, 4 Right. Vertical: 256 Top, 512 Middle, 1024 Bottom.
const TMP_ALIGN = { left: 1, center: 2, right: 4 };
const TMP_VALIGN = { top: 256, middle: 512, bottom: 1024 };

function tmpText(id, o) {
  const text = o.text === undefined ? '' : String(o.text);
  if (/[\r\n]/.test(text)) throw new Error('tmpText &' + id + ': a real newline cannot be authored into m_text - set multi-line strings from code');
  for (const ch of text) {
    const cp = ch.codePointAt(0);
    if (cp < 32 || cp > 126) throw new Error('tmpText &' + id + ': non-ASCII codepoint ' + cp + ' in "' + text + '" (the atlas is ASCII 32..126)');
  }
  const c = o.color || { r: 1, g: 1, b: 1, a: 1 };
  const h = TMP_ALIGN[o.align || 'center'];
  const vv = TMP_VALIGN[o.valign || 'middle'];
  return [
    '--- !u!114 &' + id,
    'MonoBehaviour:',
    PRE(o.go),
    '  m_Script: {fileID: 11500000, guid: ' + G_TMP + ', type: 3}',
    '  m_Name: ',
    '  m_EditorClassIdentifier: Unity.TextMeshPro::TMPro.TextMeshProUGUI',
    '  m_Material: {fileID: 0}',
    '  m_Color: {r: 1, g: 1, b: 1, a: 1}',
    '  m_RaycastTarget: 0',
    '  m_RaycastPadding: {x: 0, y: 0, z: 0, w: 0}',
    '  m_Maskable: 1',
    '  m_OnCullStateChanged:',
    '    m_PersistentCalls:',
    '      m_Calls: []',
    '  m_text: ' + text,
    '  m_isRightToLeft: 0',
    '  m_fontAsset: ' + ref(FONT),
    '  m_sharedMaterial: ' + ref(FONT_MAT),
    '  m_fontSharedMaterials: []',
    '  m_fontMaterial: {fileID: 0}',
    '  m_fontMaterials: []',
    '  m_fontColor32:',
    '    serializedVersion: 2',
    '    rgba: ' + rgba32(c),
    '  m_fontColor: ' + col(c),
    '  m_enableVertexGradient: 0',
    '  m_colorMode: 3',
    '  m_fontColorGradient:',
    '    topLeft: {r: 1, g: 1, b: 1, a: 1}',
    '    topRight: {r: 1, g: 1, b: 1, a: 1}',
    '    bottomLeft: {r: 1, g: 1, b: 1, a: 1}',
    '    bottomRight: {r: 1, g: 1, b: 1, a: 1}',
    '  m_fontColorGradientPreset: {fileID: 0}',
    '  m_spriteAsset: {fileID: 0}',
    '  m_tintAllSprites: 0',
    '  m_StyleSheet: {fileID: 0}',
    '  m_TextStyleHashCode: -1183493901',
    '  m_overrideHtmlColors: 0',
    '  m_faceColor:',
    '    serializedVersion: 2',
    '    rgba: 4294967295',
    '  m_fontSize: ' + n(o.size),
    '  m_fontSizeBase: ' + n(o.size),
    '  m_fontWeight: 400',
    '  m_enableAutoSizing: 1',
    '  m_fontSizeMin: ' + n(o.min),
    '  m_fontSizeMax: ' + n(o.size),
    '  m_fontStyle: ' + (o.style || 0),
    '  m_HorizontalAlignment: ' + h,
    '  m_VerticalAlignment: ' + vv,
    '  m_textAlignment: 65535',
    '  m_characterSpacing: ' + (o.tracking === undefined ? 0 : n(o.tracking)),
    '  m_characterHorizontalScale: 1',
    '  m_wordSpacing: 0',
    '  m_lineSpacing: ' + (o.lineSpacing === undefined ? 0 : n(o.lineSpacing)),
    '  m_lineSpacingMax: 0',
    '  m_paragraphSpacing: 0',
    '  m_charWidthMaxAdj: 0',
    '  m_TextWrappingMode: 0',
    '  m_wordWrappingRatios: 0.4',
    '  m_overflowMode: 0',
    '  m_linkedTextComponent: {fileID: 0}',
    '  parentLinkedComponent: {fileID: 0}',
    '  m_enableKerning: 0',
    '  m_ActiveFontFeatures: 6e72656bu',
    '  m_enableExtraPadding: 0',
    '  checkPaddingRequired: 0',
    '  m_isRichText: 1',
    '  m_EmojiFallbackSupport: 1',
    '  m_parseCtrlCharacters: 1',
    '  m_isOrthographic: 1',
    '  m_isCullingEnabled: 0',
    '  m_horizontalMapping: 0',
    '  m_verticalMapping: 0',
    '  m_uvLineOffset: 0',
    '  m_geometrySortingOrder: 0',
    '  m_IsTextObjectScaleStatic: 0',
    '  m_VertexBufferAutoSizeReduction: 0',
    '  m_useMaxVisibleDescender: 1',
    '  m_pageToDisplay: 1',
    '  m_margin: {x: 0, y: 0, z: 0, w: 0}',
    '  m_isUsingLegacyAnimationComponent: 0',
    '  m_isVolumetricText: 0',
    '  m_hasFontAssetChanged: 0',
    '  m_baseMaterial: {fileID: 0}',
    '  m_maskOffset: {x: 0, y: 0, z: 0, w: 0}',
    '',
  ].join('\n');
}

function button(id, o) {
  const cl = o.colors || {};
  const C = (k, d) => col(cl[k] || d);
  return [
    '--- !u!114 &' + id,
    'MonoBehaviour:',
    PRE(o.go),
    '  m_Script: {fileID: 11500000, guid: ' + G_BUTTON + ', type: 3}',
    '  m_Name: ',
    '  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.Button',
    '  m_Navigation:',
    '    m_Mode: 3',
    '    m_WrapAround: 0',
    '    m_SelectOnUp: {fileID: 0}',
    '    m_SelectOnDown: {fileID: 0}',
    '    m_SelectOnLeft: {fileID: 0}',
    '    m_SelectOnRight: {fileID: 0}',
    '  m_Transition: 1',
    '  m_Colors:',
    '    m_NormalColor: ' + C('normal', { r: 1, g: 1, b: 1, a: 1 }),
    '    m_HighlightedColor: ' + C('highlighted', { r: 1, g: 1, b: 1, a: 1 }),
    '    m_PressedColor: ' + C('pressed', { r: 0.72, g: 0.72, b: 0.72, a: 1 }),
    '    m_SelectedColor: ' + C('selected', { r: 1, g: 1, b: 1, a: 1 }),
    '    m_DisabledColor: ' + C('disabled', { r: 0.5, g: 0.5, b: 0.5, a: 1 }),
    '    m_ColorMultiplier: 1',
    '    m_FadeDuration: 0.1',
    '  m_SpriteState:',
    '    m_HighlightedSprite: {fileID: 0}',
    '    m_PressedSprite: {fileID: 0}',
    '    m_SelectedSprite: {fileID: 0}',
    '    m_DisabledSprite: {fileID: 0}',
    '  m_AnimationTriggers:',
    '    m_NormalTrigger: Normal',
    '    m_HighlightedTrigger: Highlighted',
    '    m_PressedTrigger: Pressed',
    '    m_SelectedTrigger: Selected',
    '    m_DisabledTrigger: Disabled',
    '  m_Interactable: 1',
    '  m_TargetGraphic: {fileID: ' + (o.target || 0) + '}',
    '  m_OnClick:',
    '    m_PersistentCalls:',
    '      m_Calls: []',
    '',
  ].join('\n');
}

function canvas(id, o) {
  return [
    '--- !u!223 &' + id,
    'Canvas:',
    '  m_ObjectHideFlags: 0',
    '  m_CorrespondingSourceObject: {fileID: 0}',
    '  m_PrefabInstance: {fileID: 0}',
    '  m_PrefabAsset: {fileID: 0}',
    '  m_GameObject: {fileID: ' + o.go + '}',
    '  m_Enabled: 1',
    '  serializedVersion: 3',
    '  m_RenderMode: 0',
    '  m_Camera: {fileID: 0}',
    '  m_PlaneDistance: 100',
    '  m_PixelPerfect: 0',
    '  m_ReceivesEvents: 1',
    '  m_OverrideSorting: 0',
    '  m_OverridePixelPerfect: 0',
    '  m_SortingBucketNormalizedSize: 0',
    '  m_VertexColorAlwaysGammaSpace: 0',
    '  m_UseReflectionProbes: 0',
    '  m_AdditionalShaderChannelsFlag: 0',
    '  m_UpdateRectTransformForStandalone: 0',
    '  m_SortingLayerID: 0',
    '  m_SortingOrder: ' + (o.order === undefined ? 0 : o.order),
    '  m_TargetDisplay: 0',
    '',
  ].join('\n');
}

function canvasScaler(id, o) {
  return [
    '--- !u!114 &' + id,
    'MonoBehaviour:',
    PRE(o.go),
    '  m_Script: {fileID: 11500000, guid: ' + G_SCALER + ', type: 3}',
    '  m_Name: ',
    '  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler',
    '  m_UiScaleMode: 1',
    '  m_ReferencePixelsPerUnit: 100',
    '  m_ScaleFactor: 1',
    '  m_ReferenceResolution: ' + v2(o.reference, { x: 1242, y: 2688 }),
    '  m_ScreenMatchMode: ' + (o.matchMode === undefined ? 1 : o.matchMode),
    '  m_MatchWidthOrHeight: ' + (o.match === undefined ? 0 : n(o.match)),
    '  m_PhysicalUnit: 3',
    '  m_FallbackScreenDPI: 96',
    '  m_DefaultSpriteDPI: 96',
    '  m_DynamicPixelsPerUnit: 1',
    '  m_PresetInfoIsWorld: 0',
    '',
  ].join('\n');
}

function graphicRaycaster(id, o) {
  return [
    '--- !u!114 &' + id,
    'MonoBehaviour:',
    PRE(o.go),
    '  m_Script: {fileID: 11500000, guid: ' + G_RAYCASTER + ', type: 3}',
    '  m_Name: ',
    '  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.GraphicRaycaster',
    '  m_IgnoreReversedGraphics: 1',
    '  m_BlockingObjects: 0',
    '  m_BlockingMask:',
    '    serializedVersion: 2',
    '    m_Bits: 4294967295',
    '',
  ].join('\n');
}

function rectMask2D(id, o) {
  return [
    '--- !u!114 &' + id,
    'MonoBehaviour:',
    PRE(o.go),
    '  m_Script: {fileID: 11500000, guid: ' + G_RECTMASK + ', type: 3}',
    '  m_Name: ',
    '  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.RectMask2D',
    '  m_Padding: {x: 0, y: 0, z: 0, w: 0}',
    '  m_Softness: {x: 0, y: 0}',
    '',
  ].join('\n');
}

function canvasGroup(id, o) {
  return [
    '--- !u!225 &' + id,
    'CanvasGroup:',
    '  m_ObjectHideFlags: 0',
    '  m_CorrespondingSourceObject: {fileID: 0}',
    '  m_PrefabInstance: {fileID: 0}',
    '  m_PrefabAsset: {fileID: 0}',
    '  m_GameObject: {fileID: ' + o.go + '}',
    '  m_Enabled: 1',
    '  m_Alpha: ' + (o.alpha === undefined ? 1 : n(o.alpha)),
    '  m_Interactable: ' + (o.interactable === false ? 0 : 1),
    '  m_BlocksRaycasts: ' + (o.blocksRaycasts === false ? 0 : 1),
    '  m_IgnoreParentGroups: 0',
    '',
  ].join('\n');
}

// A generated MonoBehaviour. `fields` is an array of already-formatted lines
// (without the leading two spaces), so references can be composed by the caller.
function mono(id, o) {
  const body = (o.fields || []).map((l) => '  ' + l).join('\n');
  return [
    '--- !u!114 &' + id,
    'MonoBehaviour:',
    PRE(o.go),
    '  m_Script: {fileID: 11500000, guid: ' + o.guid + ', type: 3}',
    '  m_Name: ',
    '  m_EditorClassIdentifier: Assembly-CSharp::' + o.cls,
  ].join('\n') + '\n' + (body ? body + '\n' : '');
}

module.exports = {
  gameObject, rectTransform, canvasRenderer, image, tmpText, button,
  canvas, canvasScaler, graphicRaycaster, rectMask2D, canvasGroup, mono,
  ref, col, v2, rgba32, FONT, FONT_MAT,
};
