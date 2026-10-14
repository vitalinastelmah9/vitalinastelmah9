using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// The lobby. It binds the pouch counter to the template's own menu panel, dresses the
// hero card for the active hall, drives the chip row and the three sheets, and owns
// the one button that leads into the display scene.
//
// The template's play button ships with a reference held but no listener ever added -
// so it is this script's, and it is wired with a lambda rather than a method group,
// because obfuscation renames the declaration and leaves a bare reference behind.
public sealed class _0x72a57d44 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _hallName;
    [SerializeField]
    private Image _hallBadge;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private TextMeshProUGUI _hallObjective;
    [SerializeField]
    private Image[] _hallPips;
    [SerializeField]
    private _0x2d01d6be _giftBoard;
    [SerializeField]
    private TextMeshProUGUI _sectionCaption;
    [SerializeField]
    private Image[] _themedPanels;
    // -- sheets --------------------------------------------------------------
    public void _0xaf037df8()
    {
        if (this._giftBoard != null)
        {
            this._giftBoard._0x3956baf4();
        }

        if (this._overlays != null)
        {
            this._overlays._0x91cef7b7(_0xf9b7f38e.SheetGift);
        }
    }

    [SerializeField]
    private Image[] _collectionPips;
    [SerializeField]
    private Button _giftButton;
    [SerializeField]
    private _0xf9b7f38e _overlays;
    [SerializeField]
    private Image _giftDot;
    public void _0xfdb88642(int _0xe233dea6, int _0x35b63340)
    {
        this._0x35ce284e._0x4514bb1e(_0xe233dea6);
        this._0x7dcd82b7._0x3b757eb1(_0x35b63340);
        this._0xaceb69b4();
    }

    private readonly _0xa624856f _0x7dcd82b7 = new _0xa624856f();
    [SerializeField]
    private Button _playButton;
    public void _0x6134100b()
    {
        SceneManager.LoadScene(DisplaySceneIndex);
    }

    [SerializeField]
    private _0xdff7421a[] _chips;
    private void Start()
    {
        this._0x96eeed33._0x4aa87c38();
        this._0x35ce284e._0x59daee8e();
        this._0x7dcd82b7._0x0b3bfeb3();
        this._0x42c3d89e._0x16fba8ea();
        this._0xf4c25ae2();
        if (this._giftBoard != null)
        {
            this._giftBoard._0x5b3ec547(this, this._0x42c3d89e);
        }

        if (this._albumBoard != null)
        {
            this._albumBoard._0xa0d07a31(this, this._0x7dcd82b7);
        }

        if (this._settingsBoard != null)
        {
            this._settingsBoard._0xf49e754e(this, this._0x96eeed33);
        }

        this._0xeb6865d8();
        this._0x46ea1352(this._0x96eeed33._0x639ea781);
        this._0x60660b4a(this._0x96eeed33._0xed102b64);
        this._0xaceb69b4();
    }

    [SerializeField]
    private TextMeshProUGUI _playLabel;
    public void _0x6aaab8ac()
    {
        if (this._settingsBoard != null)
        {
            this._settingsBoard._0x6e83ad66();
        }

        if (this._overlays != null)
        {
            this._overlays._0x91cef7b7(_0xf9b7f38e.SheetSettings);
        }
    }

    [SerializeField]
    private _0xb678a56f _albumBoard;
    [SerializeField]
    private Button _albumButton;
    [SerializeField]
    private TextMeshProUGUI _collectionLabel;
    [SerializeField]
    private TextMeshProUGUI _hallStatus;
    public void _0x46ea1352(bool _0x06a5d415)
    {
        if (this._themedPanels != null)
        {
            for (int _0x35c2a7bc = 0; _0x35c2a7bc < this._themedPanels.Length; _0x35c2a7bc++)
            {
                if (this._themedPanels[_0x35c2a7bc] != null)
                {
                    this._themedPanels[_0x35c2a7bc].color = _0xd7ad5941.PanelFill(_0x06a5d415);
                }
            }
        }

        if (this._playPlate != null)
        {
            this._playPlate.color = _0x06a5d415 ? _0xd7ad5941.Accent : Color.white;
        }

        if (this._privacyPlate != null)
        {
            this._privacyPlate.color = _0x06a5d415 ? _0xd7ad5941.PanelGlow : _0xd7ad5941.PanelLift;
        }
    }

    [SerializeField]
    private _0xc19c2b8c _settingsBoard;
    public void _0x293a976b()
    {
        if (this._albumBoard != null)
        {
            this._albumBoard._0xeb261666();
        }

        if (this._overlays != null)
        {
            this._overlays._0x91cef7b7(_0xf9b7f38e.SheetAlbum);
        }
    }

    [SerializeField]
    private TextMeshProUGUI _albumLabel;
    private readonly _0x061d8955 _0x96eeed33 = new _0x061d8955();
    [SerializeField]
    private TextMeshProUGUI _giftLabel;
    public void _0x60660b4a(bool _0x91e682fc)
    {
        if (this._badgeRocker != null)
        {
            this._badgeRocker._0x7b4ea113(!_0x91e682fc);
        }
    }

    [SerializeField]
    private Image _playPlate;
    private readonly _0x938e0968 _0x42c3d89e = new _0x938e0968();
    private void _0xf4c25ae2()
    {
        if (this._playButton != null)
        {
            this._playButton.onClick.AddListener(() => this._0x6134100b());
        }

        if (this._giftButton != null)
        {
            this._giftButton.onClick.AddListener(() => this._0xaf037df8());
        }

        if (this._albumButton != null)
        {
            this._albumButton.onClick.AddListener(() => this._0x293a976b());
        }

        if (this._settingsButton != null)
        {
            this._settingsButton.onClick.AddListener(() => this._0x6aaab8ac());
        }

        if (this._chips == null)
        {
            return;
        }

        for (int _0xe21b0f7c = 0; _0xe21b0f7c < this._chips.Length; _0xe21b0f7c++)
        {
            if (this._chips[_0xe21b0f7c] == null)
            {
                continue;
            }

            int _0x0516f5b5 = _0xe21b0f7c;
            this._chips[_0xe21b0f7c]._0x092c4ce1(() => this._0x0022ba1d(_0x0516f5b5));
        }
    }

    [SerializeField]
    private TextMeshProUGUI _footer;
    // -- actions the sheets delegate back here -------------------------------
    public void _0x0022ba1d(int _0x8ff12bca)
    {
        this._0x7dcd82b7._0xda98307b(_0x8ff12bca);
        this._0xaceb69b4();
    }

    /// <summary>Redraw everything the stores can have changed.</summary>
    public void _0xaceb69b4()
    {
        int _0x7664c821 = this._0x7dcd82b7._0x6e8a9fd6;
        _0x777dc4f8.Set(this._tokenValue, _0x1e75d7de.Tokens(this._0x35ce284e._0x29c88373), _0xd7ad5941.Secondary);
        _0x777dc4f8.ApplyOutline(this._tokenValue, _0xd7ad5941.Secondary);
        _0x777dc4f8.Set(this._hallName, _0x9f11ea4e.Name(_0x7664c821), _0xd7ad5941.Text);
        _0x777dc4f8.Set(this._playLabel, _0xd5d7fb59._0x0847fe8d(new byte[5] { 185, 165, 168, 176, 227 }, 233) + _0x9f11ea4e.Name(_0x7664c821), _0xd7ad5941.Text);
        Color _0x48fc68f4 = _0xd7ad5941.Tint(_0x9f11ea4e.Tint(_0x7664c821));
        if (this._hallBadge != null)
        {
            this._hallBadge.color = _0x48fc68f4;
        }

        int _0x20f4df61 = this._0x7dcd82b7._0x15578ad5(_0x7664c821);
        if (this._hallPips != null)
        {
            for (int _0xd2aec6f0 = 0; _0xd2aec6f0 < this._hallPips.Length; _0xd2aec6f0++)
            {
                if (this._hallPips[_0xd2aec6f0] != null)
                {
                    this._hallPips[_0xd2aec6f0].color = _0xd2aec6f0 < _0x20f4df61 ? _0xd7ad5941.Accent : _0xd7ad5941.Locked;
                }
            }
        }

        if (this._0x7dcd82b7._0xd83ab145(_0x7664c821))
        {
            _0x777dc4f8.Set(this._hallStatus, _0xd5d7fb59._0x0847fe8d(new byte[3] { 243, 246, 235 }, 191), _0xd7ad5941.Accent);
        }
        else
        {
            _0x777dc4f8.Set(this._hallStatus, _0x1e75d7de.RelicProgress(_0x20f4df61, _0x9f11ea4e.RelicsPerHall), _0xd7ad5941.TextSoft);
        }

        if (this._chips != null)
        {
            for (int _0x3682813d = 0; _0x3682813d < this._chips.Length && _0x3682813d < _0x9f11ea4e.Count; _0x3682813d++)
            {
                if (this._chips[_0x3682813d] != null)
                {
                    this._chips[_0x3682813d]._0x6ced7778(_0x3682813d, this._0x7dcd82b7._0x1008a5a8(_0x3682813d), this._0x7dcd82b7._0xd83ab145(_0x3682813d), _0x3682813d == _0x7664c821);
                }
            }
        }

        int _0x3de70bbc = this._0x7dcd82b7._0x12f86fc8();
        if (this._collectionPips != null)
        {
            for (int _0x732f4102 = 0; _0x732f4102 < this._collectionPips.Length; _0x732f4102++)
            {
                if (this._collectionPips[_0x732f4102] != null)
                {
                    this._collectionPips[_0x732f4102].color = this._0x7dcd82b7._0xd83ab145(_0x732f4102) ? _0xd7ad5941.Accent : _0xd7ad5941.Locked;
                }
            }
        }

        _0x777dc4f8.Set(this._collectionLabel, _0xd5d7fb59._0x0847fe8d(new byte[13] { 23, 27, 24, 24, 17, 23, 0, 29, 27, 26, 116, 121, 116 }, 84) + _0x3de70bbc + _0xd5d7fb59._0x0847fe8d(new byte[4] { 123, 20, 29, 123 }, 91) + _0x9f11ea4e.Count + _0xd5d7fb59._0x0847fe8d(new byte[10] { 18, 122, 115, 126, 126, 97, 18, 126, 123, 102 }, 50), _0xd7ad5941.Text);
        if (this._giftDot != null)
        {
            this._giftDot.color = this._0x42c3d89e._0x28e98a9e() ? _0xd7ad5941.Secondary : _0xd7ad5941.WithAlpha(_0xd7ad5941.Locked, 0f);
        }
    }

    private readonly _0xe3eb3e0e _0x35ce284e = new _0xe3eb3e0e();
    private void _0xeb6865d8()
    {
        _0x777dc4f8.Set(this._tokenUnit, _0xd5d7fb59._0x0847fe8d(new byte[6] { 1, 26, 30, 16, 27, 6 }, 85), _0xd7ad5941.TextSoft);
        _0x777dc4f8.Set(this._privacyLabel, _0xd5d7fb59._0x0847fe8d(new byte[14] { 221, 223, 196, 219, 204, 206, 212, 173, 221, 194, 193, 196, 206, 212 }, 141), _0xd7ad5941.Text);
        _0x777dc4f8.Set(this._sectionCaption, _0xd5d7fb59._0x0847fe8d(new byte[13] { 149, 131, 138, 131, 133, 146, 230, 135, 230, 142, 135, 138, 138 }, 198), _0xd7ad5941.TextSoft);
        _0x777dc4f8.Set(this._giftLabel, _0xd5d7fb59._0x0847fe8d(new byte[10] { 0, 5, 13, 8, 29, 100, 3, 13, 2, 16 }, 68), _0xd7ad5941.Text);
        _0x777dc4f8.Set(this._albumLabel, _0xd5d7fb59._0x0847fe8d(new byte[5] { 44, 33, 47, 56, 32 }, 109), _0xd7ad5941.Text);
        _0x777dc4f8.Set(this._hallObjective, _0xd5d7fb59._0x0847fe8d(new byte[35] { 196, 223, 210, 222, 199, 212, 195, 177, 162, 177, 195, 212, 221, 216, 210, 194, 177, 197, 222, 177, 221, 216, 214, 217, 197, 177, 197, 217, 216, 194, 177, 217, 208, 221, 221 }, 145), _0xd7ad5941.TextSoft);
        _0x777dc4f8.Set(this._footer, _0xd5d7fb59._0x0847fe8d(new byte[53] { 5, 17, 6, 6, 99, 23, 12, 8, 6, 13, 16, 99, 12, 13, 15, 26, 99, 110, 99, 13, 12, 23, 11, 10, 13, 4, 99, 23, 12, 99, 1, 22, 26, 73, 13, 12, 23, 11, 10, 13, 4, 99, 23, 12, 99, 0, 2, 16, 11, 99, 12, 22, 23 }, 67), _0xd7ad5941.TextSoft);
    }

    [SerializeField]
    private Image _privacyPlate;
    private const int DisplaySceneIndex = 1;
    [SerializeField]
    private TextMeshProUGUI _tokenValue;
    [SerializeField]
    private TextMeshProUGUI _tokenUnit;
    [SerializeField]
    private _0xd8f7dc87 _badgeRocker;
    [SerializeField]
    private TextMeshProUGUI _privacyLabel;
    public void _0x7337ae7b()
    {
        this._0x7dcd82b7._0x5b08c3a1();
        this._0x35ce284e._0x347530ab();
        this._0x42c3d89e._0xf65d8b4c();
        this._0xaceb69b4();
        if (this._albumBoard != null)
        {
            this._albumBoard._0xeb261666();
        }
    }

    public void _0x1803d95b()
    {
        if (this._overlays != null)
        {
            this._overlays._0x4ef4a85f();
        }

        this._0xaceb69b4();
    }
}

internal static class _0xd5d7fb59
{
    internal static string _0x0847fe8d(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}