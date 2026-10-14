using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Everything the display scene says in words: the pouch, the hall and its set, the
// one-line control hint, the last result, and the bet. All of it is the game's OWN
// scene objects - nothing is slipped into a slot the template drew for another game.
public sealed class _0x65dcd6f6 : MonoBehaviour
{
    [SerializeField]
    private Image _payLine;
    public void _0xa12f361a()
    {
        this._0xbbdffd0b = false;
        if (this._payLine != null)
        {
            this._payLine.color = _0xd7ad5941.WithAlpha(_0xd7ad5941.Accent, 0f);
        }
    }

    private void _0xc95a2ae2()
    {
        this._0x02c6b9f6 += Time.deltaTime;
        float _0x2cf82b89;
        if (this._0x02c6b9f6 < 0.12f)
        {
            _0x2cf82b89 = (this._0x02c6b9f6 / 0.12f) * 0.9f;
        }
        else if (this._0x02c6b9f6 < 0.52f)
        {
            _0x2cf82b89 = 0.9f;
        }
        else if (this._0x02c6b9f6 < 0.72f)
        {
            _0x2cf82b89 = 0.9f * (1f - ((this._0x02c6b9f6 - 0.52f) / 0.2f));
        }
        else
        {
            _0x2cf82b89 = 0f;
            this._0xbbdffd0b = false;
        }

        this._payLine.color = _0xd7ad5941.WithAlpha(_0xd7ad5941.Accent, _0x2cf82b89);
    }

    [SerializeField]
    private Image[] _setPips;
    public void _0xd9f1b8a7()
    {
        _0x777dc4f8.Set(this._result, string.Empty, _0xd7ad5941.TextSoft);
    }

    [SerializeField]
    private TextMeshProUGUI _setStatus;
    private int _0x3e31f781;
    private bool _0x5de7cfe7;
    private void Update()
    {
        if (this._0x67023796)
        {
            this._0x7f7eeda7();
        }

        if (this._0xbbdffd0b)
        {
            this._0xc95a2ae2();
        }
    }

    private void Start()
    {
        if (this._payLine != null)
        {
            this._payLine.color = _0xd7ad5941.WithAlpha(_0xd7ad5941.Accent, 0f);
        }

        _0x777dc4f8.Set(this._hint, _0xb42fc04c._0xd38a60ca(new byte[44] { 248, 237, 252, 140, 255, 252, 229, 226, 140, 129, 140, 248, 228, 254, 233, 233, 140, 227, 234, 140, 237, 140, 228, 237, 224, 224, 140, 254, 233, 224, 229, 239, 140, 249, 226, 239, 227, 250, 233, 254, 255, 140, 229, 248 }, 172), _0xd7ad5941.TextSoft);
    }

    public void _0xd398d2a6(string _0x728601c7, Color _0x5c5e3591)
    {
        _0x777dc4f8.Set(this._result, _0x728601c7, _0x5c5e3591);
        _0x777dc4f8.ApplyOutline(this._result, _0x5c5e3591);
    }

    private bool _0xbbdffd0b;
    private const float CounterSeconds = 0.45f;
    /// <summary>Run the counter up to a new balance so a win is visible, not just true.</summary>
    public void _0x75c80cc0(int _0xfe2eab27)
    {
        if (this._0x5de7cfe7 || _0xfe2eab27 == this._0xfa9431f4)
        {
            this._0xe27d2490(_0xfe2eab27);
            return;
        }

        this._0x3e31f781 = _0xfe2eab27;
        this._0x149a81c0 = 0f;
        this._0x67023796 = true;
    }

    private bool _0x67023796;
    private float _0x149a81c0;
    public void _0x52ab4e9d(bool _0xfc8ec835)
    {
        this._0x5de7cfe7 = _0xfc8ec835;
    }

    /// <summary>Flash the pay line across the middle row.</summary>
    public void _0x4765ab53()
    {
        if (this._payLine == null)
        {
            return;
        }

        if (this._0x5de7cfe7)
        {
            this._payLine.color = _0xd7ad5941.WithAlpha(_0xd7ad5941.Accent, 0.9f);
            this._0xbbdffd0b = false;
            return;
        }

        this._0x02c6b9f6 = 0f;
        this._0xbbdffd0b = true;
    }

    [SerializeField]
    private TextMeshProUGUI _setTitle;
    [SerializeField]
    private TextMeshProUGUI _hint;
    private int _0xfa9431f4;
    [SerializeField]
    private TextMeshProUGUI _tokenValue;
    public void _0x2af68356(int _0x37c46ca0)
    {
        _0x777dc4f8.Set(this._betValue, _0xb42fc04c._0xd38a60ca(new byte[4] { 22, 17, 0, 116 }, 84) + _0x37c46ca0, _0xd7ad5941.Text);
    }

    public void _0xc3958aeb(string _0x33d34235, int _0xb0f065da, int _0x019fb5f5)
    {
        _0x777dc4f8.Set(this._setTitle, _0x33d34235, _0xd7ad5941.Text);
        _0x777dc4f8.Set(this._setStatus, _0x1e75d7de.RelicProgress(_0xb0f065da, _0x019fb5f5), _0xd7ad5941.TextSoft);
        if (this._setPips == null)
        {
            return;
        }

        for (int _0x35db4f36 = 0; _0x35db4f36 < this._setPips.Length; _0x35db4f36++)
        {
            if (this._setPips[_0x35db4f36] == null)
            {
                continue;
            }

            this._setPips[_0x35db4f36].color = _0x35db4f36 < _0xb0f065da ? _0xd7ad5941.Accent : _0xd7ad5941.Locked;
        }
    }

    [SerializeField]
    private TextMeshProUGUI _result;
    private float _0x02c6b9f6;
    public void _0xe27d2490(int _0x7b3044a2)
    {
        this._0xfa9431f4 = _0x7b3044a2;
        this._0x3e31f781 = _0x7b3044a2;
        this._0x67023796 = false;
        _0x777dc4f8.Set(this._tokenValue, _0x1e75d7de.Tokens(_0x7b3044a2));
    }

    private void _0x7f7eeda7()
    {
        this._0x149a81c0 += Time.deltaTime;
        float _0x6258c6d2 = this._0x149a81c0 / CounterSeconds;
        if (_0x6258c6d2 >= 1f)
        {
            _0x6258c6d2 = 1f;
            this._0x67023796 = false;
        }

        float _0x32d16d65 = 1f - _0x6258c6d2;
        float _0x7f5987e8 = 1f - (_0x32d16d65 * _0x32d16d65);
        int _0x550e85ef = Mathf.RoundToInt(Mathf.Lerp(this._0xfa9431f4, this._0x3e31f781, _0x7f5987e8));
        _0x777dc4f8.Set(this._tokenValue, _0x1e75d7de.Tokens(_0x550e85ef));
        if (this._tokenBox != null)
        {
            float _0x7981060e = 1f + (0.08f * Mathf.Sin(_0x6258c6d2 * Mathf.PI));
            this._tokenBox.localScale = new Vector3(_0x7981060e, _0x7981060e, 1f);
        }

        if (!this._0x67023796)
        {
            this._0xfa9431f4 = this._0x3e31f781;
            if (this._tokenBox != null)
            {
                this._tokenBox.localScale = Vector3.one;
            }
        }
    }

    [SerializeField]
    private RectTransform _tokenBox;
    [SerializeField]
    private TextMeshProUGUI _betValue;
}

internal static class _0xb42fc04c
{
    internal static string _0xd38a60ca(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}