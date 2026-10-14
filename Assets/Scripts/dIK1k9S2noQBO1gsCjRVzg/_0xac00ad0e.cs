using UnityEngine;
using UnityEngine.UI;

// One column of the display: four cells on a content rect that slides down by exactly
// one cell at a time, so the strip can be recycled forever through four Images. The
// fourth cell lives above the window and is only ever seen mid-slide.
//
// The travel is computed from the START and TARGET stop, so the column always lands on
// the stop the session decided - the animation never picks the result.
public sealed class _0xac00ad0e : MonoBehaviour
{
    private void _0xff0a1ea0()
    {
        if (this._cells == null || this._0xa0e049e7 == null || this._relicSprites == null)
        {
            return;
        }

        for (int _0xc4f16bf1 = 0; _0xc4f16bf1 < this._cells.Length; _0xc4f16bf1++)
        {
            Image _0x75c481bf = this._cells[_0xc4f16bf1];
            if (_0x75c481bf == null)
            {
                continue;
            }

            int _0xe2c8bc49 = this._0xa0e049e7._0x80a9f59e(this._0xf60d6add, _0xc4f16bf1 - 1, this._0xc33a5a89);
            if (_0xe2c8bc49 >= 0 && _0xe2c8bc49 < this._relicSprites.Length)
            {
                _0x75c481bf.sprite = this._relicSprites[_0xe2c8bc49];
                // Content art, so the PNG's own proportions win over the cell's square:
                // the relics come back from the generator at whatever aspect they are,
                // and a stretched vessel reads as a bug on the first review frame.
                _0x75c481bf.type = Image.Type.Simple;
                _0x75c481bf.preserveAspect = true;
            }

            _0x75c481bf.color = Color.white;
        }
    }

    private bool _0x2eb164f8;
    private void _0xef7e7769()
    {
        this._0x7b1271c8 += Time.deltaTime;
        float _0xc805a22d = this._0x7b1271c8 / SettleSeconds;
        if (_0xc805a22d >= 1f)
        {
            this._0x2eb164f8 = false;
            if (this._content != null)
            {
                this._content.anchoredPosition = Vector2.zero;
            }

            return;
        }

        float amount = this._cellStep * SettleOvershoot;
        float _0xa853fe80 = -amount * Mathf.Sin(_0xc805a22d * Mathf.PI * 2f) * (1f - _0xc805a22d);
        if (this._content != null)
        {
            this._content.anchoredPosition = new Vector2(0f, _0xa853fe80);
        }
    }

    private int _0x7ec6be84;
    private const float SettleOvershoot = 0.07f;
    private int _0xf60d6add;
    private void Update()
    {
        if (this._0x48405023)
        {
            this._0x2cd5620b();
            return;
        }

        if (this._0x2eb164f8)
        {
            this._0xef7e7769();
        }
    }

    private _0xa026540f _0xa0e049e7;
    private float _0x60bda8ea;
    private void _0x2cd5620b()
    {
        this._0x1eb55f91 += Time.deltaTime;
        float _0xd270ba25 = this._0x1eb55f91 / this._0x60bda8ea;
        if (_0xd270ba25 > 1f)
        {
            _0xd270ba25 = 1f;
        }

        float _0x0b170ec8 = 1f - _0xd270ba25;
        float _0x4a7f60f1 = 1f - (_0x0b170ec8 * _0x0b170ec8 * _0x0b170ec8);
        float _0x37e59093 = this._0xffd2140a * _0x4a7f60f1;
        int _0x05e40f17 = Mathf.FloorToInt(_0x37e59093 / this._cellStep);
        int _0xd841fe6f = this._0x9af5b16f(this._0xd9e7789f - _0x05e40f17);
        if (_0xd841fe6f != this._0xc33a5a89)
        {
            this._0xc33a5a89 = _0xd841fe6f;
            this._0xff0a1ea0();
        }

        float _0x9fa54af8 = _0x37e59093 - (_0x05e40f17 * this._cellStep);
        if (this._content != null)
        {
            this._content.anchoredPosition = new Vector2(0f, -_0x9fa54af8);
        }

        if (_0xd270ba25 >= 1f)
        {
            this._0x48405023 = false;
            this._0xc33a5a89 = this._0x7ec6be84;
            this._0xff0a1ea0();
            if (this._content != null)
            {
                this._content.anchoredPosition = Vector2.zero;
            }

            this._0x2eb164f8 = true;
            this._0x7b1271c8 = 0f;
        }
    }

    /// <summary>The relic currently on a visible row, 0 top to 2 bottom.</summary>
    public int _0x8166a267(int _0x445abb19)
    {
        return this._0xa0e049e7 == null ? 0 : this._0xa0e049e7._0x80a9f59e(this._0xf60d6add, _0x445abb19, this._0xc33a5a89);
    }

    private float _0x7b1271c8;
    private int _0x9af5b16f(int _0xa44b2429)
    {
        int len = this._0xa0e049e7 == null ? _0x66896ed0.StripLength : this._0xa0e049e7._0x67a2dff5;
        int _0xeb79bbfa = _0xa44b2429 % len;
        return _0xeb79bbfa < 0 ? _0xeb79bbfa + len : _0xeb79bbfa;
    }

    /// <summary>True until the column has come to rest on its target stop.</summary>
    public bool _0x8f4fc550
    {
        get
        {
            return this._0x48405023 || this._0x2eb164f8;
        }
    }

    [SerializeField]
    private float _cellStep = 350f;
    [SerializeField]
    private Image[] _cells;
    [SerializeField]
    private RectTransform _content;
    private float _0xffd2140a;
    /// <summary>The Image that draws a visible row, so a win can punch it.</summary>
    public RectTransform _0x73f00f9a(int _0xf713ad5e)
    {
        int _0xc10dd308 = _0xf713ad5e + 1;
        if (this._cells == null || _0xc10dd308 < 0 || _0xc10dd308 >= this._cells.Length || this._cells[_0xc10dd308] == null)
        {
            return null;
        }

        return this._cells[_0xc10dd308].rectTransform;
    }

    [SerializeField]
    private Sprite[] _relicSprites;
    private bool _0x48405023;
    /// <summary>Place the column on a stop with no animation at all.</summary>
    public void _0x0327dd13(int _0x78eda175)
    {
        this._0x48405023 = false;
        this._0x2eb164f8 = false;
        this._0xc33a5a89 = this._0x9af5b16f(_0x78eda175);
        this._0xff0a1ea0();
        if (this._content != null)
        {
            this._content.anchoredPosition = Vector2.zero;
        }
    }

    private float _0x1eb55f91;
    public void _0xaea599ed(_0xa026540f _0xd70d2e25, int _0x5acf5af0)
    {
        this._0xa0e049e7 = _0xd70d2e25;
        this._0xf60d6add = _0x5acf5af0;
    }

    private int _0xc33a5a89;
    private int _0xd9e7789f;
    private const float SettleSeconds = 0.22f;
    /// <summary>
    /// Start the roll. travelCells only sets how far it LOOKS like the column turned;
    /// where it stops is targetCursor and nothing else.
    /// </summary>
    public void _0x65e32126(int _0x1e01c0d3, float _0x160cbe70, int _0x91745e5d, bool _0xc135c018)
    {
        if (this._0xa0e049e7 == null)
        {
            return;
        }

        // REDUCED MOTION means less of it, not a shorter burst of the same amount:
        // squeezing a full strip into 0.18 s would scroll several times FASTER than the
        // normal spin. The column simply arrives.
        if (_0xc135c018)
        {
            this._0x0327dd13(_0x1e01c0d3);
            return;
        }

        int len = this._0xa0e049e7._0x67a2dff5;
        this._0xd9e7789f = this._0xc33a5a89;
        this._0x7ec6be84 = this._0x9af5b16f(_0x1e01c0d3);
        int _0xc761ce63 = ((this._0xd9e7789f - this._0x7ec6be84) % len + len) % len;
        int _0x764a37be = (_0x91745e5d / len) + 1;
        _0xc761ce63 += _0x764a37be * len;
        this._0xffd2140a = _0xc761ce63 * this._cellStep;
        this._0x60bda8ea = _0x160cbe70 <= 0.01f ? 0.01f : _0x160cbe70;
        this._0x1eb55f91 = 0f;
        this._0x7b1271c8 = 0f;
        this._0x48405023 = true;
        this._0x2eb164f8 = false;
    }
}