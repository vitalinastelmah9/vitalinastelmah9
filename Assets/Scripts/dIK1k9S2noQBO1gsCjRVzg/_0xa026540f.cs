// Builds one hall session from a seed: three independent relic strips, the three
// relics this hall asks for, and the backdrop decor. Nothing here is written out as a
// constant except the fallback, which exists so a pathological shuffle cannot strand
// the player - it is insurance, never the game (C.11).
//
// Engine-free on purpose: no UnityEngine type is named outside the B_LOGS block, so
// the same file can be compiled and run headlessly to measure what it really produces.
public sealed class _0xa026540f
{
    private const int SolvabilityAttempts = 20;
    public int _0x67a2dff5
    {
        get
        {
            return _0x66896ed0.StripLength;
        }
    }

    private const int MinScatterHits = 20;
    public const int Rows = 3;
    private void _0xabe1f28f()
    {
        for (int _0x3f899343 = 0; _0x3f899343 < Reels; _0x3f899343++)
        {
            int[] _0x16a84976 = this._0xee3cd7db[_0x3f899343];
            for (int _0x6fb93992 = 0; _0x6fb93992 < _0x16a84976.Length; _0x6fb93992++)
            {
                // A per-reel rotation keeps the three columns from showing one line.
                _0x16a84976[_0x6fb93992] = _0xabd8be99[(_0x6fb93992 + (_0x3f899343 * 5)) % _0xabd8be99.Length];
            }
        }

        for (int _0x10db4988 = 0; _0x10db4988 < _0x9f11ea4e.RelicsPerHall; _0x10db4988++)
        {
            this._0x52c2d15e[_0x10db4988] = _0xf640e7cd[_0x10db4988];
        }
    }

    /// <summary>
    /// The most of this relic that can ever share one window: per reel, the best count
    /// over any three consecutive stops, summed. An exact ceiling, not an estimate.
    /// </summary>
    public int _0xfa3faeff(int _0x1adfdb38)
    {
        int _0x556acad5 = 0;
        int _0x18a90927 = _0x66896ed0.StripLength;
        for (int _0xb8e70299 = 0; _0xb8e70299 < Reels; _0xb8e70299++)
        {
            int best = 0;
            for (int _0x4190d28f = 0; _0x4190d28f < _0x18a90927; _0x4190d28f++)
            {
                int _0x577ce091 = 0;
                for (int _0x11df852c = 0; _0x11df852c < Rows; _0x11df852c++)
                {
                    if (this._0x80a9f59e(_0xb8e70299, _0x11df852c, _0x4190d28f) == _0x1adfdb38)
                    {
                        _0x577ce091++;
                    }
                }

                if (_0x577ce091 > best)
                {
                    best = _0x577ce091;
                }
            }

            _0x556acad5 += best;
        }

        return _0x556acad5;
    }

    /// <summary>Which of the three accent hues a lit decor piece takes.</summary>
    public int _0x704e4dbc(int _0xed985149)
    {
        return this._0x94a86b04[((_0xed985149 % 3) + 3) % 3];
    }

    /// <summary>Which pedestal slot (0..5) each of the three lit decor pieces occupies.</summary>
    public int _0x211a47d8(int _0xf4abc6d8)
    {
        return this._0x5226ed61[((_0xf4abc6d8 % 3) + 3) % 3];
    }

    // Insurance only: one hand-written strip set that is reachable by construction.
    private static readonly int[] _0xabd8be99 =
    {
        0,
        3,
        1,
        6,
        2,
        0,
        4,
        1,
        7,
        2,
        5,
        0,
        3,
        1,
        4,
        2,
    };
    /// <summary>
    /// Two questions, both answered about the layout itself and nothing else.
    ///
    /// First, exactly: can three of this relic ever share one window? That is the sum
    /// over the reels of the best three-in-a-row count, and no amount of spinning beats
    /// it. Second, by simulation on a fixed stream: does it happen often enough that a
    /// hall finishes inside a sitting, rather than merely in principle.
    /// </summary>
    public bool _0x1f879b9d()
    {
        for (int _0x2d7a01cb = 0; _0x2d7a01cb < _0x9f11ea4e.RelicsPerHall; _0x2d7a01cb++)
        {
            if (this._0xfa3faeff(this._0x52c2d15e[_0x2d7a01cb]) < ScatterNeeded)
            {
                return false;
            }
        }

        _0x1b53ce68 _0xe0490964 = new _0x1b53ce68()._0x97db330a(VerifySeed);
        int[] _0x0f12f391 = new int[_0x9f11ea4e.RelicsPerHall];
        int _0x7b0da974 = 0;
        int _0x071dc9ca = _0x66896ed0.StripLength;
        for (int _0xde7ef812 = 0; _0xde7ef812 < SimulatedSpins; _0xde7ef812++)
        {
            int _0x2d133099 = _0xe0490964._0x2429757e(_0x071dc9ca);
            int _0xdfcfa7ae = _0xe0490964._0x2429757e(_0x071dc9ca);
            int _0x39331111 = _0xe0490964._0x2429757e(_0x071dc9ca);
            bool _0xe1db56c9 = false;
            for (int _0x38662eb9 = 0; _0x38662eb9 < _0x9f11ea4e.RelicsPerHall; _0x38662eb9++)
            {
                if (this._0x690bc7d2(this._0x52c2d15e[_0x38662eb9], _0x2d133099, _0xdfcfa7ae, _0x39331111))
                {
                    _0x0f12f391[_0x38662eb9]++;
                    _0xe1db56c9 = true;
                }
            }

            if (_0xe1db56c9)
            {
                _0x7b0da974++;
            }
        }

        for (int _0x71966791 = 0; _0x71966791 < _0x9f11ea4e.RelicsPerHall; _0x71966791++)
        {
            if (_0x0f12f391[_0x71966791] < MinScatterHits)
            {
                return false;
            }
        }

        float _0xe43fd8ab = _0x7b0da974 / (float)SimulatedSpins;
        return _0xe43fd8ab >= MinShardsPerSpin;
    }

    public const int Reels = 3;
    private void _0x47e9c1f5(_0x1b53ce68 _0x97cdc71e)
    {
        int[] _0x1042e7a8 =
        {
            0,
            1,
            2,
            3,
            4,
            5
        };
        _0x97cdc71e._0x564a8070(_0x1042e7a8);
        for (int _0x8e7f60f6 = 0; _0x8e7f60f6 < 3; _0x8e7f60f6++)
        {
            this._0x5226ed61[_0x8e7f60f6] = _0x1042e7a8[_0x8e7f60f6];
            this._0x94a86b04[_0x8e7f60f6] = _0x97cdc71e._0x2429757e(3);
        }
    }

    private const int SimulatedSpins = 400;
    /// <summary>Generate, prove the layout reachable, and only then keep it.</summary>
    public void _0x44d7a961(_0x1b53ce68 _0xcf2b082d)
    {
        this._0xdc248a62 = false;
        int _0x1700e97f = 0;
        this._0xd2216df5(_0xcf2b082d);
        while (_0x1700e97f < SolvabilityAttempts && !this._0x1f879b9d())
        {
            this._0xd2216df5(_0xcf2b082d);
            _0x1700e97f++;
        }

        if (!this._0x1f879b9d())
        {
            this._0xabe1f28f();
            this._0xdc248a62 = true;
        }

        this._0x47e9c1f5(_0xcf2b082d);
        // Braces on both sides of the directive: the obfuscator parses the region the
        // same whether the branch is compiled in or out, which is the whole point of
        // obfuscating with B_LOGS defined and cutting it afterwards.
        {
#if B_LOGS
            {
                UnityEngine.Debug.Log(_0x217d5d52._0x41521bf0(new byte[14] { 56, 11, 2, 15, 15, 62, 67, 17, 6, 15, 10, 0, 16, 94 }, 99) + this._0x52c2d15e[0] + _0x217d5d52._0x41521bf0(new byte[1] { 179 }, 156) + this._0x52c2d15e[1] + _0x217d5d52._0x41521bf0(new byte[1] { 232 }, 199) + this._0x52c2d15e[2] + _0x217d5d52._0x41521bf0(new byte[10] { 65, 0, 21, 21, 4, 12, 17, 21, 18, 92 }, 97) + _0x1700e97f + _0x217d5d52._0x41521bf0(new byte[10] { 108, 42, 45, 32, 32, 46, 45, 47, 39, 113 }, 76) + this._0xdc248a62);
            }
#endif
        }
    }

    private int[] _0x52c2d15e = new int[_0x9f11ea4e.RelicsPerHall];
    // The simulation runs on its OWN fixed stream, never on the live one: a solvability
    // verdict must be a property of the LAYOUT, not of how many numbers the game
    // happened to have drawn when it asked. Measured on the live stream, 11.7% of
    // layouts that passed at build time failed the identical check a moment later.
    private const int VerifySeed = 0x5EED;
    private const float MinShardsPerSpin = 0.15f;
    private int[] _0x5226ed61 = new int[3];
    /// <summary>True when the generated layout had to fall back to the constant set.</summary>
    public bool _0xdc248a62 { get; private set; }

    /// <summary>The relic shown on a reel at a row, for a given stop cursor.</summary>
    public int _0x80a9f59e(int _0x0f6070e8, int _0x9b4d2a60, int _0x424918e1)
    {
        int[] _0x769d0536 = this._0xee3cd7db[((_0x0f6070e8 % Reels) + Reels) % Reels];
        int _0x8b773fdc = _0x424918e1 + _0x9b4d2a60;
        int len = _0x769d0536.Length;
        _0x8b773fdc %= len;
        if (_0x8b773fdc < 0)
        {
            _0x8b773fdc += len;
        }

        return _0x769d0536[_0x8b773fdc];
    }

    private static readonly int[] _0xf640e7cd =
    {
        0,
        1,
        2
    };
    public _0xa026540f _0x973e6a72()
    {
        for (int _0x9cf7fa84 = 0; _0x9cf7fa84 < Reels; _0x9cf7fa84++)
        {
            this._0xee3cd7db[_0x9cf7fa84] = new int[_0x66896ed0.StripLength];
        }

        return this;
    }

    private const int ScatterNeeded = 3;
    /// <summary>
    /// The award rule, in one place: a relic is uncovered when THREE or more of it
    /// share the 3x3 window, wherever they land. SpinResolver applies the identical
    /// test to the resolved window - if the two ever disagree, a hall becomes
    /// unwinnable with nothing failing anywhere.
    /// </summary>
    public bool _0x690bc7d2(int _0xec5cbe40, int _0x83591540, int _0x277ac755, int _0x75927979)
    {
        int _0xe124b609 = 0;
        for (int _0x9e05ca3a = 0; _0x9e05ca3a < Rows; _0x9e05ca3a++)
        {
            if (this._0x80a9f59e(0, _0x9e05ca3a, _0x83591540) == _0xec5cbe40)
            {
                _0xe124b609++;
            }

            if (this._0x80a9f59e(1, _0x9e05ca3a, _0x277ac755) == _0xec5cbe40)
            {
                _0xe124b609++;
            }

            if (this._0x80a9f59e(2, _0x9e05ca3a, _0x75927979) == _0xec5cbe40)
            {
                _0xe124b609++;
            }
        }

        return _0xe124b609 >= ScatterNeeded;
    }

    private int[] _0x94a86b04 = new int[3];
    private int[][] _0xee3cd7db = new int[Reels][];
    private void _0xd2216df5(_0x1b53ce68 _0xbb460668)
    {
        for (int _0xff4e7c53 = 0; _0xff4e7c53 < Reels; _0xff4e7c53++)
        {
            int[] _0x171e6d45 = this._0xee3cd7db[_0xff4e7c53];
            for (int _0x69211d57 = 0; _0x69211d57 < _0x171e6d45.Length; _0x69211d57++)
            {
                _0x171e6d45[_0x69211d57] = _0x66896ed0.DrawWeighted(_0xbb460668);
            }

            _0xbb460668._0x564a8070(_0x171e6d45);
        }

        // Three distinct relics out of the five collectible ones.
        int _0x979dd584 = 0;
        while (_0x979dd584 < _0x9f11ea4e.RelicsPerHall)
        {
            int _0xb1b7d32b = _0xbb460668._0x2429757e(_0x66896ed0.CollectibleCount);
            bool _0x69a86b86 = false;
            for (int _0x0891dd1a = 0; _0x0891dd1a < _0x979dd584; _0x0891dd1a++)
            {
                if (this._0x52c2d15e[_0x0891dd1a] == _0xb1b7d32b)
                {
                    _0x69a86b86 = true;
                }
            }

            if (!_0x69a86b86)
            {
                this._0x52c2d15e[_0x979dd584] = _0xb1b7d32b;
                _0x979dd584++;
            }
        }
    }

    /// <summary>The relic at index 0..2 of this hall's set.</summary>
    public int _0x17dec05b(int _0x247c950e)
    {
        return this._0x52c2d15e[((_0x247c950e % _0x9f11ea4e.RelicsPerHall) + _0x9f11ea4e.RelicsPerHall) % _0x9f11ea4e.RelicsPerHall];
    }
}

internal static class _0x217d5d52
{
    internal static string _0x41521bf0(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}