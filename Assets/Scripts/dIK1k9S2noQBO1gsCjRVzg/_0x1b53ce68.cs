// Deterministic random source for a hall session.
//
// Not UnityEngine.Random: that carries global state, so two systems drawing from it
// start affecting each other and "the same seed" stops meaning the same thing (C.11).
// An explicit LCG is used rather than System.Random so the exact sequence can be
// replayed outside the engine, which is how the balance and solvability claims in the
// design document were measured instead of asserted.
public sealed class _0x1b53ce68
{
    public void _0x564a8070(int[] _0x62cd0d10)
    {
        if (_0x62cd0d10 == null)
        {
            return;
        }

        for (int _0xe526b656 = _0x62cd0d10.Length - 1; _0xe526b656 > 0; _0xe526b656--)
        {
            int _0x5a8f916f = this._0x2429757e(_0xe526b656 + 1);
            int _0x6067c128 = _0x62cd0d10[_0xe526b656];
            _0x62cd0d10[_0xe526b656] = _0x62cd0d10[_0x5a8f916f];
            _0x62cd0d10[_0x5a8f916f] = _0x6067c128;
        }
    }

    /// <summary>Uniform value in [low, high), low must be below high.</summary>
    public int _0x6286915b(int _0xe6c79eb2, int _0x79c81050)
    {
        if (_0x79c81050 <= _0xe6c79eb2)
        {
            return _0xe6c79eb2;
        }

        return _0xe6c79eb2 + this._0x2429757e(_0x79c81050 - _0xe6c79eb2);
    }

    /// <summary>Uniform value in [0, 1).</summary>
    public float _0x44b14721()
    {
        return (this._0xc639d9f3() >> 8) / 16777216f;
    }

    /// <summary>An independent copy, so a simulation never disturbs the live stream.</summary>
    public _0x1b53ce68 _0xfecd6adc()
    {
        _0x1b53ce68 _0xab0e3f84 = new _0x1b53ce68()._0x97db330a(1);
        _0xab0e3f84._0x023fcf21 = this._0x023fcf21;
        return _0xab0e3f84;
    }

    private uint _0xc639d9f3()
    {
        unchecked
        {
            this._0x023fcf21 = (this._0x023fcf21 * 1664525u) + 1013904223u;
        }

        return this._0x023fcf21;
    }

    /// <summary>Uniform value in [0, bound), bound must be positive.</summary>
    public int _0x2429757e(int _0x35e496c8)
    {
        if (_0x35e496c8 <= 1)
        {
            return 0;
        }

        // Top bits of an LCG are the well-mixed ones.
        uint _0x86f1aafd = this._0xc639d9f3() >> 8;
        return (int)(_0x86f1aafd % (uint)_0x35e496c8);
    }

    private uint _0x023fcf21;
    public _0x1b53ce68 _0x97db330a(int _0xa2f412e9)
    {
        this._0x023fcf21 = (uint)_0xa2f412e9;
        if (this._0x023fcf21 == 0u)
        {
            this._0x023fcf21 = 2463534242u;
        }

        // Discard the first few steps: a small seed otherwise yields a visibly
        // ascending first draw on an LCG.
        for (int _0xd4e634a5 = 0; _0xd4e634a5 < 4; _0xd4e634a5++)
        {
            this._0xc639d9f3();
        }

        return this;
    }
}