using TMPro;
using UnityEngine;
using UnityEngine.UI;

// A settings switch. It slides, recolours AND relabels, so what it did is legible in a
// still frame - a control that only flips a boolean would be indistinguishable from a
// dead button (rule C.7).
public sealed class _0x408694e7 : MonoBehaviour
{
    private const float KnobTravel = 52f;
    [SerializeField]
    private Image _track;
    public void _0x6ea7aaa3(bool _0x765f688d)
    {
        if (this._track != null)
        {
            this._track.color = _0x765f688d ? _0xd7ad5941.Secondary : _0xd7ad5941.Locked;
        }

        if (this._knob != null)
        {
            this._knob.anchoredPosition = new Vector2(_0x765f688d ? KnobTravel : -KnobTravel, 0f);
        }

        _0x777dc4f8.Set(this._stateLabel, _0x765f688d ? _0xfa7a176c._0x488a3b4c(new byte[2] { 184, 185 }, 247) : _0xfa7a176c._0x488a3b4c(new byte[3] { 116, 125, 125 }, 59), _0x765f688d ? _0xd7ad5941.Text : _0xd7ad5941.TextSoft);
    }

    [SerializeField]
    private Button _pillButton;
    [SerializeField]
    private RectTransform _knob;
    [SerializeField]
    private TextMeshProUGUI _stateLabel;
    public void _0x5e484fcf(System.Action _0x532770ae)
    {
        if (this._pillButton == null || _0x532770ae == null)
        {
            return;
        }

        this._pillButton.onClick.AddListener(() => _0x532770ae());
    }
}

internal static class _0xfa7a176c
{
    internal static string _0x488a3b4c(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}