using TMPro;
using UnityEngine;
using UnityEngine.UI;

// One hall's line in the collection sheet.
//
// The pip column and the text column occupy bands that never share x-space (rule
// C.25): TextAlignmentOptions.Left aligns text INSIDE its rectangle and does not move
// the rectangle, so the separation has to be arithmetic, done where the row is laid
// out - this view only fills what is already two columns.
public sealed class _0xa62056e6 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _status;
    [SerializeField]
    private Image _plate;
    [SerializeField]
    private Image[] _pips;
    public void _0x16fed6c7(int _0xea0c4324, int _0xa72a739d, bool _0x2a55f430, bool _0x0490d794)
    {
        _0x777dc4f8.Set(this._title, _0x9f11ea4e.Name(_0xea0c4324), _0x2a55f430 ? _0xd7ad5941.Text : _0xd7ad5941.TextSoft);
        if (!_0x2a55f430)
        {
            _0x777dc4f8.Set(this._status, _0xf2cb839d._0xcbfa7943(new byte[6] { 41, 42, 38, 46, 32, 33 }, 101), _0xd7ad5941.TextSoft);
        }
        else if (_0x0490d794)
        {
            _0x777dc4f8.Set(this._status, _0xf2cb839d._0xcbfa7943(new byte[3] { 140, 137, 148 }, 192), _0xd7ad5941.Accent);
        }
        else
        {
            _0x777dc4f8.Set(this._status, _0x1e75d7de.RelicProgress(_0xa72a739d, _0x9f11ea4e.RelicsPerHall), _0xd7ad5941.TextSoft);
        }

        if (this._plate != null)
        {
            this._plate.color = _0x2a55f430 ? _0xd7ad5941.PanelLift : _0xd7ad5941.WithAlpha(_0xd7ad5941.Locked, 0.55f);
        }

        if (this._pips == null)
        {
            return;
        }

        for (int _0xffaf7721 = 0; _0xffaf7721 < this._pips.Length; _0xffaf7721++)
        {
            if (this._pips[_0xffaf7721] == null)
            {
                continue;
            }

            this._pips[_0xffaf7721].color = _0xffaf7721 < _0xa72a739d ? _0xd7ad5941.Accent : _0xd7ad5941.Locked;
        }
    }
}

internal static class _0xf2cb839d
{
    internal static string _0xcbfa7943(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}