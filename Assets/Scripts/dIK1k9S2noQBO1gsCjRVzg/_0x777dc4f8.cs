using TMPro;
using UnityEngine;

// Every label this app writes goes through here.
//
// Two rules it enforces, both of which a scene-file pass cannot (rule C.10): word wrap
// is OFF with the line breaks placed by hand, autosize is ALWAYS on with the minimum
// at the readability floor, and each label gets its OWN material instance whose outline
// contrasts with the face colour. enforce-text-contrast.sh only sees m_fontColor
// serialised in a scene, so a label written from code would otherwise ship with the
// shared dark outline under a dark face.
public static class _0x777dc4f8
{
    /// <summary>
    /// Bring a label built in the scene, or created at runtime, to the house rules.
    /// </summary>
    public static void Apply(TextMeshProUGUI _0x83bebd63, float _0xddf3a8b6, float _0xf35143d8, Color _0x5e8fd13b)
    {
        if (_0x83bebd63 == null)
        {
            return;
        }

        _0x83bebd63.enableWordWrapping = false;
        _0x83bebd63.overflowMode = TextOverflowModes.Overflow;
        _0x83bebd63.enableAutoSizing = true;
        _0x83bebd63.fontSizeMin = _0xf35143d8 < FloorSize ? FloorSize : _0xf35143d8;
        _0x83bebd63.fontSizeMax = _0xddf3a8b6;
        _0x83bebd63.fontSize = _0xddf3a8b6;
        _0x83bebd63.color = _0x5e8fd13b;
        ApplyOutline(_0x83bebd63, _0x5e8fd13b);
    }

    /// <summary>Set the text and the colour in one go; the colour must be a light one.</summary>
    public static void Set(TextMeshProUGUI _0x92406278, string _0xf9b46832, Color _0xd19c97f1)
    {
        if (_0x92406278 == null)
        {
            return;
        }

        _0x92406278.text = _0xf9b46832 == null ? string.Empty : _0xf9b46832;
        _0x92406278.color = _0xd19c97f1;
    }

    private static readonly int _0x94542bfc = Shader.PropertyToID(_0x07e86f6e._0xb0c29e60(new byte[13] { 24, 8, 50, 51, 43, 46, 41, 34, 16, 46, 35, 51, 47 }, 71));
    private static readonly int _0xd0673956 = Shader.PropertyToID(_0x07e86f6e._0xb0c29e60(new byte[13] { 133, 149, 175, 174, 182, 179, 180, 191, 153, 181, 182, 181, 168 }, 218));
    /// <summary>
    /// Give a label its own outline, chosen against its own face colour. Reading
    /// fontMaterial creates an INSTANCE - the shared asset is left alone.
    /// </summary>
    public static void ApplyOutline(TextMeshProUGUI _0xcc1e046a, Color _0xc1f22910)
    {
        if (_0xcc1e046a == null)
        {
            return;
        }

        Material _0xfc6fb87f = _0xcc1e046a.fontMaterial;
        if (_0xfc6fb87f == null)
        {
            return;
        }

        float _0xde1701d7 = (0.299f * _0xc1f22910.r) + (0.587f * _0xc1f22910.g) + (0.114f * _0xc1f22910.b);
        Color _0x4d1708f4 = _0xde1701d7 < 0.5f ? _0xd7ad5941.Text : _0xd7ad5941.BaseDeep;
        _0xfc6fb87f.SetColor(_0xd0673956, _0x4d1708f4);
        _0xfc6fb87f.SetFloat(_0x94542bfc, 0.18f);
    }

    /// <summary>Set a caption's text. Line breaks are explicit, never left to the engine.</summary>
    public static void Set(TextMeshProUGUI _0xfc2a06eb, string _0x7d5e6d33)
    {
        if (_0xfc2a06eb == null)
        {
            return;
        }

        _0xfc2a06eb.text = _0x7d5e6d33 == null ? string.Empty : _0x7d5e6d33;
    }

    /// <summary>Readability floor on the 1242-wide reference canvas (24 px of 1080).</summary>
    public const float FloorSize = 28f;
}

internal static class _0x07e86f6e
{
    internal static string _0xb0c29e60(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}