using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Today's gift. Purely local: the device's own UTC date against the last one stored.
// Nothing is bought, nothing is redeemed and nothing leaves the phone.
//
// Claiming swaps the case art, pops the two reward chips in and greys the button out -
// three changes in one frame, so the tap is legible on a screenshot (rule C.7).
public sealed class _0x2d01d6be : MonoBehaviour
{
    private void _0x793e37e0()
    {
        if (this._0x6dfe6bf7 == null || !this._0x6dfe6bf7._0xf7c87d76())
        {
            return;
        }

        this._0xbe81d030(true);
        this._0x3956baf4();
        if (this._0xab858d29 != null)
        {
            this._0xab858d29._0xfdb88642(_0xe3eb3e0e.DailyGrant, 1);
        }
    }

    [SerializeField]
    private Button _toAlbumButton;
    [SerializeField]
    private TextMeshProUGUI _claimLabel;
    [SerializeField]
    private Image[] _streakPips;
    [SerializeField]
    private Sprite _caseOpen;
    [SerializeField]
    private GameObject _rewardChips;
    [SerializeField]
    private TextMeshProUGUI _heading;
    [SerializeField]
    private Image _claimPlate;
    private _0x938e0968 _0x6dfe6bf7;
    /// <summary>Refresh the sheet for the state it is being opened in.</summary>
    public void _0x3956baf4()
    {
        if (this._0x6dfe6bf7 == null)
        {
            return;
        }

        bool _0x83d13cae = this._0x6dfe6bf7._0x28e98a9e();
        this._0xbe81d030(!_0x83d13cae);
        if (this._streakPips == null)
        {
            return;
        }

        int _0xe83c6449 = this._0x6dfe6bf7._0x9cbb30cf();
        int _0xfd9c746d = this._0x6dfe6bf7._0x8e3ccc0f;
        for (int _0x703e9767 = 0; _0x703e9767 < this._streakPips.Length; _0x703e9767++)
        {
            if (this._streakPips[_0x703e9767] == null)
            {
                continue;
            }

            if (!_0x83d13cae && _0x703e9767 == _0xe83c6449)
            {
                this._streakPips[_0x703e9767].color = _0xd7ad5941.Accent;
                continue;
            }

            if (_0x703e9767 < _0xfd9c746d)
            {
                this._streakPips[_0x703e9767].color = _0xd7ad5941.Secondary;
                continue;
            }

            this._streakPips[_0x703e9767].color = _0x703e9767 == _0xe83c6449 ? _0xd7ad5941.Accent : _0xd7ad5941.Locked;
        }
    }

    [SerializeField]
    private Sprite _caseClosed;
    private void _0xc6b26e49()
    {
        if (this._0xab858d29 != null)
        {
            this._0xab858d29._0x293a976b();
        }
    }

    [SerializeField]
    private Button _claimButton;
    private _0x72a57d44 _0xab858d29;
    [SerializeField]
    private TextMeshProUGUI _explainer;
    [SerializeField]
    private Button _closeButton;
    private void _0xbe81d030(bool _0xb5e27b09)
    {
        if (this._claimButton != null)
        {
            this._claimButton.interactable = !_0xb5e27b09;
        }

        if (this._claimPlate != null)
        {
            this._claimPlate.color = _0xb5e27b09 ? _0xd7ad5941.Locked : Color.white;
        }

        _0x777dc4f8.Set(this._claimLabel, _0xb5e27b09 ? _0x5f0d5204._0xc25f59db(new byte[13] { 215, 216, 213, 221, 217, 209, 208, 180, 192, 219, 208, 213, 205 }, 148) : _0x5f0d5204._0xc25f59db(new byte[5] { 218, 213, 216, 208, 212 }, 153), _0xd7ad5941.Text);
        if (this._giftImage != null)
        {
            Sprite _0xd606bc6d = _0xb5e27b09 ? this._caseOpen : this._caseClosed;
            if (_0xd606bc6d != null)
            {
                this._giftImage.sprite = _0xd606bc6d;
            }
        }

        if (this._rewardChips != null)
        {
            this._rewardChips.SetActive(_0xb5e27b09);
        }
    }

    public void _0x5b3ec547(_0x72a57d44 _0x7087a28c, _0x938e0968 _0x2c8001dc)
    {
        this._0xab858d29 = _0x7087a28c;
        this._0x6dfe6bf7 = _0x2c8001dc;
        if (this._claimButton != null)
        {
            this._claimButton.onClick.AddListener(() => this._0x793e37e0());
        }

        if (this._toAlbumButton != null)
        {
            this._toAlbumButton.onClick.AddListener(() => this._0xc6b26e49());
        }

        if (this._closeButton != null)
        {
            this._closeButton.onClick.AddListener(() => this._0x53bc1d15());
        }

        _0x777dc4f8.Set(this._heading, _0x5f0d5204._0xc25f59db(new byte[12] { 177, 170, 161, 164, 188, 194, 182, 197, 162, 172, 163, 177 }, 229), _0xd7ad5941.Accent);
        _0x777dc4f8.ApplyOutline(this._heading, _0xd7ad5941.Accent);
        _0x777dc4f8.Set(this._explainer, _0x5f0d5204._0xc25f59db(new byte[54] { 75, 42, 89, 79, 75, 70, 42, 77, 95, 75, 88, 75, 68, 94, 79, 79, 89, 42, 94, 66, 75, 94, 42, 83, 69, 95, 88, 42, 68, 79, 82, 94, 0, 89, 90, 67, 68, 42, 95, 68, 73, 69, 92, 79, 88, 89, 42, 75, 42, 88, 79, 70, 67, 73 }, 10), _0xd7ad5941.TextSoft);
    }

    [SerializeField]
    private Image _giftImage;
    private void _0x53bc1d15()
    {
        if (this._0xab858d29 != null)
        {
            this._0xab858d29._0x1803d95b();
        }
    }
}

internal static class _0x5f0d5204
{
    internal static string _0xc25f59db(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}