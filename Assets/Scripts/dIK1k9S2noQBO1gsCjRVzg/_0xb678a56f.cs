using TMPro;
using UnityEngine;
using UnityEngine.UI;

// The collection sheet: one row per hall.
//
// The list itself can never be empty - there are always six halls - so the six rows
// always render and the "nothing yet" case is explained in the sub-heading, rather
// than replacing the page with a message. A blank panel is never acceptable; neither
// is hiding the thing the player opened the sheet to look at.
public sealed class _0xb678a56f : MonoBehaviour
{
    [SerializeField]
    private Button _closeButton;
    private void _0x12cdfd9c()
    {
        if (this._0x92fd6710 != null)
        {
            this._0x92fd6710._0x1803d95b();
        }
    }

    [SerializeField]
    private _0xa62056e6[] _rows;
    [SerializeField]
    private TextMeshProUGUI _subHeading;
    private void _0xe301e4ae()
    {
        if (this._0x92fd6710 != null)
        {
            this._0x92fd6710._0x6134100b();
        }
    }

    private _0xa624856f _0x0883ff0e;
    public void _0xa0d07a31(_0x72a57d44 _0xb90b5f26, _0xa624856f _0x6ac62576)
    {
        this._0x92fd6710 = _0xb90b5f26;
        this._0x0883ff0e = _0x6ac62576;
        if (this._playHallButton != null)
        {
            this._playHallButton.onClick.AddListener(() => this._0xe301e4ae());
        }

        if (this._closeButton != null)
        {
            this._closeButton.onClick.AddListener(() => this._0x12cdfd9c());
        }

        _0x777dc4f8.Set(this._heading, _0xa8303bf9._0xd3a02894(new byte[10] { 186, 182, 181, 181, 188, 186, 173, 176, 182, 183 }, 249), _0xd7ad5941.Accent);
        _0x777dc4f8.ApplyOutline(this._heading, _0xd7ad5941.Accent);
    }

    public void _0xeb261666()
    {
        if (this._0x0883ff0e == null)
        {
            return;
        }

        int _0x5dc85234 = this._0x0883ff0e._0x12f86fc8();
        int _0x4ef36aa5 = 0;
        for (int _0xdbb9397f = 0; _0xdbb9397f < _0x9f11ea4e.Count; _0xdbb9397f++)
        {
            _0x4ef36aa5 += this._0x0883ff0e._0x15578ad5(_0xdbb9397f);
        }

        if (_0x4ef36aa5 == 0)
        {
            _0x777dc4f8.Set(this._subHeading, _0xa8303bf9._0xd3a02894(new byte[38] { 20, 21, 14, 18, 19, 20, 29, 122, 22, 19, 14, 122, 3, 31, 14, 122, 119, 122, 10, 22, 27, 3, 122, 27, 122, 18, 27, 22, 22, 122, 14, 21, 122, 9, 14, 27, 8, 14 }, 90), _0xd7ad5941.TextSoft);
            _0x777dc4f8.ApplyOutline(this._subHeading, _0xd7ad5941.TextSoft);
        }
        else
        {
            _0x777dc4f8.Set(this._subHeading, _0x5dc85234 + _0xa8303bf9._0xd3a02894(new byte[4] { 239, 128, 137, 239 }, 207) + _0x9f11ea4e.Count + _0xa8303bf9._0xd3a02894(new byte[10] { 24, 112, 121, 116, 116, 107, 24, 116, 113, 108 }, 56), _0xd7ad5941.Secondary);
            _0x777dc4f8.ApplyOutline(this._subHeading, _0xd7ad5941.Secondary);
        }

        _0x777dc4f8.Set(this._playHallLabel, _0xa8303bf9._0xd3a02894(new byte[5] { 182, 170, 167, 191, 198 }, 230) + _0x9f11ea4e.Name(this._0x0883ff0e._0x6e8a9fd6), _0xd7ad5941.Text);
        if (this._rows == null)
        {
            return;
        }

        for (int _0x22e617ad = 0; _0x22e617ad < this._rows.Length && _0x22e617ad < _0x9f11ea4e.Count; _0x22e617ad++)
        {
            if (this._rows[_0x22e617ad] == null)
            {
                continue;
            }

            this._rows[_0x22e617ad]._0x16fed6c7(_0x22e617ad, this._0x0883ff0e._0x15578ad5(_0x22e617ad), this._0x0883ff0e._0x1008a5a8(_0x22e617ad), this._0x0883ff0e._0xd83ab145(_0x22e617ad));
        }
    }

    [SerializeField]
    private TextMeshProUGUI _heading;
    [SerializeField]
    private TextMeshProUGUI _playHallLabel;
    private _0x72a57d44 _0x92fd6710;
    [SerializeField]
    private Button _playHallButton;
}

internal static class _0xa8303bf9
{
    internal static string _0xd3a02894(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}