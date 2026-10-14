using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TmpContrastGuard.cs — staged into every Unity app by approve-pipeline-unity.sh
// (stage 5c3, rule C.14 in CLAUDE-unity.md). Do not edit the copy inside a project;
// edit scripts/lib/unity/TmpContrastGuard.cs.
//
// WHY: every TMP label gets an outline (C.10), and by default that outline is dark.
// A dark face colour on a dark outline merges into a smudge — the label is not
// readable on any backing (ANDROID-3627: PLAY drawn Deep #12151E on the #12151E
// outline read as a black blob). enforce-text-contrast.sh fixes colours SERIALISED
// in scenes/prefabs, but labels built at runtime from C# (UiKit.Cta, VaultUi.Caption,
// label.color = Palette.X ...) never reach a scene file, so that pass cannot see them.
//
// WHAT: after any TMP text is regenerated, compare its face colour with the outline
// colour of the material it actually renders with. Below WCAG 4.5:1 the face is
// blended toward white (dark outline) or black (light outline) until it reaches 7:1.
// Hue is kept; alpha is kept. A label whose outline was deliberately switched to a
// light colour (TextReadability-style per-label material) is measured against THAT
// outline, so intentionally dark text on a light rim is left alone. Labels without
// an outline are left alone too.
public sealed class _0xbedd2303 : MonoBehaviour
{
    private void OnDisable()
    {
        if (this._0x72127d39 != null)
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(this._0x72127d39);
    }

    private void LateUpdate()
    {
        if (this._0x8c6d15f3.Count == 0)
            return;
        this._0x4db8abf1.Clear();
        this._0x4db8abf1.AddRange(this._0x8c6d15f3);
        this._0x8c6d15f3.Clear();
        for (int _0x305c3d1e = 0; _0x305c3d1e < this._0x4db8abf1.Count; _0x305c3d1e++)
            Fix(this._0x4db8abf1[_0x305c3d1e]);
    }

    private static float Ratio(Color _0x18d25fa4, Color _0x082f5557)
    {
        float _0x9018debb = Luminance(_0x18d25fa4);
        float _0x27d946a5 = Luminance(_0x082f5557);
        return (Mathf.Max(_0x9018debb, _0x27d946a5) + 0.05f) / (Mathf.Min(_0x9018debb, _0x27d946a5) + 0.05f);
    }

    // The event fires from inside the canvas rebuild. Changing the colour right there
    // would re-dirty the graphic mid-rebuild, which Unity rejects — so queue it and
    // apply in LateUpdate, which runs before the next frame's rebuild.
    private void _0xb7a590d0(Object _0xf6f317f3)
    {
        TMP_Text _0x4a673b1c = _0xf6f317f3 as TMP_Text;
        if (_0x4a673b1c != null)
            this._0x8c6d15f3.Add(_0x4a673b1c);
    }

    private const float MinRatio = 4.5f;
    private const float TargetRatio = 7f;
    private static float Linear(float _0xa012dffd)
    {
        _0xa012dffd = Mathf.Clamp01(_0xa012dffd);
        return _0xa012dffd <= 0.03928f ? _0xa012dffd / 12.92f : Mathf.Pow((_0xa012dffd + 0.055f) / 1.055f, 2.4f);
    }

    // WCAG relative luminance of an sRGB colour, and the contrast ratio of two.
    private static float Luminance(Color _0xe57a7e58)
    {
        return 0.2126f * Linear(_0xe57a7e58.r) + 0.7152f * Linear(_0xe57a7e58.g) + 0.0722f * Linear(_0xe57a7e58.b);
    }

    private const float MinOutlineWidth = 0.01f;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Boot()
    {
        if (_0x809acc02 != null)
            return;
        GameObject _0x1d04765c = new GameObject(_0xb4a6f46d._0x90add6b4(new byte[16] { 158, 167, 186, 137, 165, 164, 190, 184, 171, 185, 190, 141, 191, 171, 184, 174 }, 202));
        _0x1d04765c.hideFlags = HideFlags.HideInHierarchy;
        DontDestroyOnLoad(_0x1d04765c);
        _0x809acc02 = _0x1d04765c.AddComponent<_0xbedd2303>();
    }

    private void OnEnable()
    {
        if (this._0x72127d39 == null)
            this._0x72127d39 = _0x7d43b8f0 => this._0xb7a590d0(_0x7d43b8f0);
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(this._0x72127d39);
    }

    private readonly List<TMP_Text> _0x4db8abf1 = new List<TMP_Text>();
    // A lambda held in a field, never the bare method group: Plana renames the method
    // declaration but not a method-group reference (verify-unity-buttons.sh, CS0103).
    // The field keeps Add and Remove on the same delegate instance.
    private System.Action<Object> _0x72127d39;
    private static void Fix(TMP_Text _0x78c5354a)
    {
        if (_0x78c5354a == null || !_0x78c5354a.isActiveAndEnabled)
            return;
        Material _0xaada1c49 = _0x78c5354a.fontSharedMaterial;
        if (_0xaada1c49 == null || !_0xaada1c49.HasProperty(ShaderUtilities.ID_OutlineColor) || !_0xaada1c49.HasProperty(ShaderUtilities.ID_OutlineWidth))
            return;
        if (_0xaada1c49.GetFloat(ShaderUtilities.ID_OutlineWidth) < MinOutlineWidth)
            return;
        Color _0x8a9bb16a = _0x78c5354a.color;
        if (_0x8a9bb16a.a <= 0f)
            return;
        Color _0xf8c25b6b = _0xaada1c49.GetColor(ShaderUtilities.ID_OutlineColor);
        if (Ratio(_0x8a9bb16a, _0xf8c25b6b) >= MinRatio)
            return;
        Color _0xdb6353a9 = Luminance(_0xf8c25b6b) < 0.5f ? Color.white : Color.black;
        Color _0x06726140;
        if (Ratio(_0xdb6353a9, _0xf8c25b6b) < TargetRatio)
        {
            _0x06726140 = _0xdb6353a9;
        }
        else
        {
            // Smallest blend that reaches the target: contrast grows monotonically
            // with t, so a short bisection keeps as much of the hue as possible.
            float _0x55e3f0bf = 0f;
            float _0xbcf53bd9 = 1f;
            for (int _0xde5f0bdf = 0; _0xde5f0bdf < 20; _0xde5f0bdf++)
            {
                float _0xaa4f4de8 = (_0x55e3f0bf + _0xbcf53bd9) * 0.5f;
                if (Ratio(Color.Lerp(_0x8a9bb16a, _0xdb6353a9, _0xaa4f4de8), _0xf8c25b6b) >= TargetRatio)
                    _0xbcf53bd9 = _0xaa4f4de8;
                else
                    _0x55e3f0bf = _0xaa4f4de8;
            }

            _0x06726140 = Color.Lerp(_0x8a9bb16a, _0xdb6353a9, _0xbcf53bd9);
        }

        _0x06726140.a = _0x8a9bb16a.a;
        _0x78c5354a.color = _0x06726140;
    }

    private readonly HashSet<TMP_Text> _0x8c6d15f3 = new HashSet<TMP_Text>();
    private static _0xbedd2303 _0x809acc02;
}

internal static class _0xb4a6f46d
{
    internal static string _0x90add6b4(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}