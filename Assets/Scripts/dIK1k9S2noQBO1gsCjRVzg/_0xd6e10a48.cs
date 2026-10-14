using TMPro;
using UnityEngine;
using UnityEngine.UI;

// The three cards the display scene can raise: the set is complete, the pouch is
// empty, the player paused. They are this game's own objects, themed from this game's
// palette and captioned in this game's words - every button carries its OWN caption,
// and the close cross carries none at all.
//
// Parallel arrays indexed by the three Pop* constants: one shape, three fills.
public sealed class _0xd6e10a48 : MonoBehaviour
{
    /// <summary>True while any card is up, so the display ignores taps behind it.</summary>
    public bool _0x23f3abfc
    {
        get
        {
            return this._0xd7a65bf1 >= 0;
        }
    }

    public void _0x107a3508(string _0xadba0c1d, int _0x9a3a173f)
    {
        this._0x7c6f2657(PopSetComplete, _0x115067c1._0x413f5eab(new byte[8] { 45, 36, 41, 41, 69, 41, 44, 49 }, 101), _0xadba0c1d + _0x115067c1._0x413f5eab(new byte[19] { 16, 91, 86, 86, 58, 41, 58, 72, 95, 86, 83, 89, 73, 58, 92, 85, 79, 84, 94 }, 26), _0x115067c1._0x413f5eab(new byte[1] { 167 }, 140) + _0x9a3a173f + _0x115067c1._0x413f5eab(new byte[7] { 52, 64, 91, 95, 81, 90, 71 }, 20));
        this._0xfb1eb82a(PopSetComplete);
    }

    [SerializeField]
    private TextMeshProUGUI[] _popBodies;
    [SerializeField]
    private Image[] _popBadges;
    public const int PopPouchEmpty = 1;
    [SerializeField]
    private Button[] _popCloseButtons;
    private void _0x5ff40adc(int _0x11241f1e, string _0x4924f1b3)
    {
        if (this._popSecondaryLabels != null && _0x11241f1e < this._popSecondaryLabels.Length)
        {
            _0x777dc4f8.Set(this._popSecondaryLabels[_0x11241f1e], _0x4924f1b3, _0xd7ad5941.Text);
        }
    }

    [SerializeField]
    private GameObject[] _popRoots;
    private void _0xd43145cb()
    {
        this._0x049758b5(PopSetComplete, _0x115067c1._0x413f5eab(new byte[9] { 240, 251, 230, 234, 158, 246, 255, 242, 242 }, 190));
        this._0x049758b5(PopPouchEmpty, _0x115067c1._0x413f5eab(new byte[15] { 155, 142, 132, 138, 239, 250, 255, 255, 239, 155, 128, 132, 138, 129, 156 }, 207));
        this._0x049758b5(PopPaused, _0x115067c1._0x413f5eab(new byte[6] { 107, 124, 106, 108, 116, 124 }, 57));
        for (int _0x69f3d235 = 0; _0x69f3d235 < PopCount; _0x69f3d235++)
        {
            this._0x5ff40adc(_0x69f3d235, _0x115067c1._0x413f5eab(new byte[9] { 229, 232, 232, 132, 236, 229, 232, 232, 247 }, 164));
            if (this._popCards != null && _0x69f3d235 < this._popCards.Length && this._popCards[_0x69f3d235] != null)
            {
                this._popCards[_0x69f3d235].color = _0xd7ad5941.Panel;
            }
        }
    }

    private void _0x7c6f2657(int _0xdec49300, string _0xa3a8d0ee, string _0xb5520d17, string _0xdfe0b8b2)
    {
        if (this._popHeaders != null && _0xdec49300 < this._popHeaders.Length)
        {
            _0x777dc4f8.Set(this._popHeaders[_0xdec49300], _0xa3a8d0ee, _0xd7ad5941.Accent);
            _0x777dc4f8.ApplyOutline(this._popHeaders[_0xdec49300], _0xd7ad5941.Accent);
        }

        if (this._popBodies != null && _0xdec49300 < this._popBodies.Length)
        {
            _0x777dc4f8.Set(this._popBodies[_0xdec49300], _0xb5520d17, _0xd7ad5941.Text);
        }

        if (this._popExtras != null && _0xdec49300 < this._popExtras.Length)
        {
            _0x777dc4f8.Set(this._popExtras[_0xdec49300], _0xdfe0b8b2, _0xd7ad5941.Secondary);
            _0x777dc4f8.ApplyOutline(this._popExtras[_0xdec49300], _0xd7ad5941.Secondary);
        }
    }

    [SerializeField]
    private GameObject _veil;
    [SerializeField]
    private TextMeshProUGUI[] _popHeaders;
    // The cross has no caption (rule C.17) and never strands the player: closing the
    // pouch-empty card takes the free stake anyway, because a run must not dead-end.
    private void _0xabcc474e(int _0xe5b4352c)
    {
        if (this._0x7286b7a5 == null)
        {
            return;
        }

        if (_0xe5b4352c == PopPouchEmpty)
        {
            this._0x7286b7a5._0x69a79c3d();
            return;
        }

        this._0x7286b7a5._0xdc381e22();
    }

    private void _0x049758b5(int _0xa5895561, string _0x92d6bc1a)
    {
        if (this._popPrimaryLabels != null && _0xa5895561 < this._popPrimaryLabels.Length)
        {
            _0x777dc4f8.Set(this._popPrimaryLabels[_0xa5895561], _0x92d6bc1a, _0xd7ad5941.Text);
        }
    }

    private _0x47a6d4c6 _0x7286b7a5;
    private void _0x25033f1e(int _0xc9c021fa)
    {
        if (this._0x7286b7a5 == null)
        {
            return;
        }

        if (_0xc9c021fa == PopSetComplete)
        {
            this._0x7286b7a5._0xd6ed8061();
            return;
        }

        if (_0xc9c021fa == PopPouchEmpty)
        {
            this._0x7286b7a5._0x69a79c3d();
            return;
        }

        this._0x7286b7a5._0xdc381e22();
    }

    [SerializeField]
    private Button[] _popPrimaryButtons;
    [SerializeField]
    private TextMeshProUGUI[] _popSecondaryLabels;
    [SerializeField]
    private TextMeshProUGUI[] _popPrimaryLabels;
    public const int PopSetComplete = 0;
    /// <summary>Wired by the director, which owns the actions these buttons perform.</summary>
    public void _0xf8470dfb(_0x47a6d4c6 _0x8269cef5)
    {
        this._0x7286b7a5 = _0x8269cef5;
        this._0x1846ff65();
        this._0xd43145cb();
        this._0xb36a92ea();
    }

    public void _0xb36a92ea()
    {
        this._0xd7a65bf1 = -1;
        if (this._veil != null)
        {
            this._veil.SetActive(false);
        }

        if (this._popRoots == null)
        {
            return;
        }

        for (int _0x38fb38d3 = 0; _0x38fb38d3 < this._popRoots.Length; _0x38fb38d3++)
        {
            if (this._popRoots[_0x38fb38d3] != null)
            {
                this._popRoots[_0x38fb38d3].SetActive(false);
            }
        }
    }

    [SerializeField]
    private TextMeshProUGUI[] _popExtras;
    public void _0xe959904f(Sprite _0x04da31af, Color _0x9da6b7fa)
    {
        if (this._popBadges == null)
        {
            return;
        }

        for (int _0xe7ff0873 = 0; _0xe7ff0873 < this._popBadges.Length; _0xe7ff0873++)
        {
            if (this._popBadges[_0xe7ff0873] == null)
            {
                continue;
            }

            if (_0x04da31af != null)
            {
                this._popBadges[_0xe7ff0873].sprite = _0x04da31af;
            }

            this._popBadges[_0xe7ff0873].color = _0x9da6b7fa;
        }
    }

    [SerializeField]
    private Image[] _popCards;
    private const int PopCount = 3;
    public const int PopPaused = 2;
    private void _0x161dcca4()
    {
        if (this._0x7286b7a5 != null)
        {
            this._0x7286b7a5._0x357e386b();
        }
    }

    private int _0xd7a65bf1 = -1;
    public void _0x355cbf82(int _0x652c3a5d)
    {
        this._0x7c6f2657(PopPouchEmpty, _0x115067c1._0x413f5eab(new byte[11] { 66, 93, 71, 81, 90, 50, 87, 95, 66, 70, 75 }, 18), _0x115067c1._0x413f5eab(new byte[23] { 111, 115, 126, 27, 120, 110, 105, 122, 111, 116, 105, 27, 104, 111, 122, 112, 126, 104, 27, 98, 116, 110, 49 }, 59) + _0x652c3a5d + _0x115067c1._0x413f5eab(new byte[7] { 116, 0, 27, 31, 17, 26, 7 }, 84), _0x115067c1._0x413f5eab(new byte[21] { 92, 93, 50, 66, 71, 64, 81, 90, 83, 65, 87, 50, 63, 50, 92, 93, 50, 81, 93, 65, 70 }, 18));
        this._0xfb1eb82a(PopPouchEmpty);
    }

    public void _0x7c676d3b(string _0xfa00d6b4, int _0x61179d64, int _0xf2e53f59, int _0xdfefa63d)
    {
        this._0x7c6f2657(PopPaused, _0x115067c1._0x413f5eab(new byte[6] { 205, 220, 200, 206, 216, 217 }, 157), _0xfa00d6b4 + _0x115067c1._0x413f5eab(new byte[1] { 190 }, 180) + _0x1e75d7de.RelicProgress(_0x61179d64, _0xf2e53f59), _0x115067c1._0x413f5eab(new byte[4] { 36, 35, 50, 70 }, 102) + _0xdfefa63d);
        this._0xfb1eb82a(PopPaused);
    }

    private void _0xfb1eb82a(int _0xf0c9363a)
    {
        this._0xb36a92ea();
        this._0xd7a65bf1 = _0xf0c9363a;
        if (this._veil != null)
        {
            this._veil.SetActive(true);
        }

        if (this._popRoots != null && _0xf0c9363a < this._popRoots.Length && this._popRoots[_0xf0c9363a] != null)
        {
            this._popRoots[_0xf0c9363a].SetActive(true);
        }
    }

    private void _0x1846ff65()
    {
        // A lambda, never a method group: obfuscation renames the declaration and
        // leaves a bare reference behind, and the cloud build dies on it.
        if (this._popPrimaryButtons != null)
        {
            for (int _0xb9e3397b = 0; _0xb9e3397b < this._popPrimaryButtons.Length && _0xb9e3397b < PopCount; _0xb9e3397b++)
            {
                int _0xd65400cd = _0xb9e3397b;
                Button _0x6d9ad97b = this._popPrimaryButtons[_0xb9e3397b];
                if (_0x6d9ad97b != null)
                {
                    _0x6d9ad97b.onClick.AddListener(() => this._0x25033f1e(_0xd65400cd));
                }
            }
        }

        if (this._popSecondaryButtons != null)
        {
            for (int _0x0291082a = 0; _0x0291082a < this._popSecondaryButtons.Length && _0x0291082a < PopCount; _0x0291082a++)
            {
                Button _0xb850b529 = this._popSecondaryButtons[_0x0291082a];
                if (_0xb850b529 != null)
                {
                    _0xb850b529.onClick.AddListener(() => this._0x161dcca4());
                }
            }
        }

        if (this._popCloseButtons != null)
        {
            for (int _0x454f15fa = 0; _0x454f15fa < this._popCloseButtons.Length && _0x454f15fa < PopCount; _0x454f15fa++)
            {
                int _0x509bf61b = _0x454f15fa;
                Button _0xbbca9f50 = this._popCloseButtons[_0x454f15fa];
                if (_0xbbca9f50 != null)
                {
                    _0xbbca9f50.onClick.AddListener(() => this._0xabcc474e(_0x509bf61b));
                }
            }
        }
    }

    [SerializeField]
    private Button[] _popSecondaryButtons;
}

internal static class _0x115067c1
{
    internal static string _0x413f5eab(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}