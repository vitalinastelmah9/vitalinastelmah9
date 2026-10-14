using TMPro;
using UnityEngine;
using UnityEngine.UI;

// One hall chip in the lobby's row. Tapping it swaps the hero card above, which is the
// visible confirmation a state-changing button owes the player (rule C.7).
public sealed class _0xdff7421a : MonoBehaviour
{
    [SerializeField]
    private Button _chipButton;
    [SerializeField]
    private TextMeshProUGUI _label;
    /// <summary>Subscribe the chip. A lambda, never a method group (rule C.1).</summary>
    public void _0x092c4ce1(System.Action _0xd587ca94)
    {
        if (this._chipButton == null || _0xd587ca94 == null)
        {
            return;
        }

        this._chipButton.onClick.AddListener(() => _0xd587ca94());
    }

    [SerializeField]
    private Image _edge;
    public void _0x6ced7778(int _0xc0d5135c, bool _0xd2be705b, bool _0x4624623e, bool _0xf4b2dcc2)
    {
        _0x777dc4f8.Set(this._label, _0x34dbf22b._0x79b56efe(new byte[5] { 178, 187, 182, 182, 218 }, 250) + (_0xc0d5135c + 1), _0xd2be705b ? _0xd7ad5941.Text : _0xd7ad5941.TextSoft);
        if (this._chipButton != null)
        {
            this._chipButton.interactable = _0xd2be705b;
        }

        if (this._plate != null)
        {
            Color _0xb4ae7294 = _0xd2be705b ? _0xd7ad5941.PanelLift : _0xd7ad5941.Locked;
            this._plate.color = _0xf4b2dcc2 ? _0xd7ad5941.PanelGlow : _0xb4ae7294;
        }

        if (this._edge == null)
        {
            return;
        }

        if (_0xf4b2dcc2)
        {
            this._edge.color = _0xd7ad5941.Accent;
            return;
        }

        if (_0x4624623e)
        {
            this._edge.color = _0xd7ad5941.Secondary;
            return;
        }

        this._edge.color = _0xd2be705b ? _0xd7ad5941.WithAlpha(_0xd7ad5941.Primary, 0.6f) : _0xd7ad5941.WithAlpha(_0xd7ad5941.Locked, 0.8f);
    }

    [SerializeField]
    private Image _plate;
}

internal static class _0x34dbf22b
{
    internal static string _0x79b56efe(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}