using UnityEngine;

// Which relics of which hall have been found, which halls are unlocked, and which hall
// the player is looking at. Local only: PlayerPrefs, no account, no server, no network.
public sealed class _0xa624856f
{
    public void _0x683a98f3()
    {
        char[] _0x85ce2be8 = new char[this._0xfd82b25d.Length];
        for (int _0x3007ccc7 = 0; _0x3007ccc7 < this._0xfd82b25d.Length; _0x3007ccc7++)
        {
            _0x85ce2be8[_0x3007ccc7] = this._0xfd82b25d[_0x3007ccc7] ? '1' : '0';
        }

        PlayerPrefs.SetString(AlbumKey, new string (_0x85ce2be8));
        PlayerPrefs.SetInt(HallKey, this._0xb2b2e3f4);
        PlayerPrefs.SetInt(SealKey, this._0x8629003a);
        PlayerPrefs.Save();
    }

    public bool _0xd83ab145(int _0xe033fa8f)
    {
        return this._0x15578ad5(_0xe033fa8f) >= _0x9f11ea4e.RelicsPerHall;
    }

    public int _0x15578ad5(int _0x64a7f122)
    {
        int _0x9bc3d3f0 = 0;
        for (int _0x885384e6 = 0; _0x885384e6 < _0x9f11ea4e.RelicsPerHall; _0x885384e6++)
        {
            if (this._0x80ff3499(_0x64a7f122, _0x885384e6))
            {
                _0x9bc3d3f0++;
            }
        }

        return _0x9bc3d3f0;
    }

    public void _0x3b757eb1(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        this._0x8629003a += amount;
        this._0x683a98f3();
    }

    private int _0xa75cd842(int _0x7c8d20ad, int _0x7e957cdc)
    {
        if (_0x7c8d20ad < 0 || _0x7c8d20ad >= _0x9f11ea4e.Count)
        {
            return -1;
        }

        if (_0x7e957cdc < 0 || _0x7e957cdc >= _0x9f11ea4e.RelicsPerHall)
        {
            return -1;
        }

        return (_0x7c8d20ad * _0x9f11ea4e.RelicsPerHall) + _0x7e957cdc;
    }

    public int _0x65462401()
    {
        int best = 0;
        for (int _0xb61f9e84 = 0; _0xb61f9e84 < _0x9f11ea4e.Count; _0xb61f9e84++)
        {
            if (this._0x1008a5a8(_0xb61f9e84))
            {
                best = _0xb61f9e84;
            }
        }

        return best;
    }

    /// <summary>Move to the next hall that still wants relics, if there is one.</summary>
    public bool _0x9cd27392()
    {
        for (int _0x6911527c = 0; _0x6911527c < _0x9f11ea4e.Count; _0x6911527c++)
        {
            if (this._0x1008a5a8(_0x6911527c) && !this._0xd83ab145(_0x6911527c))
            {
                this._0xb2b2e3f4 = _0x6911527c;
                this._0x683a98f3();
                return true;
            }
        }

        return false;
    }

    public int _0x6e8a9fd6
    {
        get
        {
            return this._0xb2b2e3f4;
        }
    }

    public bool _0x80ff3499(int _0x71f13a38, int _0x5686a43d)
    {
        int _0xdd39b093 = this._0xa75cd842(_0x71f13a38, _0x5686a43d);
        return _0xdd39b093 >= 0 && this._0xfd82b25d[_0xdd39b093];
    }

    /// <summary>The first hall is always open; each lit hall opens the next one.</summary>
    public bool _0x1008a5a8(int _0xa0b0df98)
    {
        if (_0xa0b0df98 <= 0)
        {
            return true;
        }

        if (_0xa0b0df98 >= _0x9f11ea4e.Count)
        {
            return false;
        }

        return this._0xd83ab145(_0xa0b0df98 - 1);
    }

    private static readonly string HallKey = _0x5e78baab._0xbf4b84f0(new byte[8] { 154, 131, 146, 219, 157, 148, 153, 153 }, 245);
    private static readonly string SealKey = _0x5e78baab._0xbf4b84f0(new byte[9] { 138, 147, 130, 203, 150, 128, 132, 137, 150 }, 229);
    private static readonly string AlbumKey = _0x5e78baab._0xbf4b84f0(new byte[9] { 88, 65, 80, 25, 86, 91, 85, 66, 90 }, 55);
    public void _0x5b08c3a1()
    {
        for (int _0xaca1c655 = 0; _0xaca1c655 < this._0xfd82b25d.Length; _0xaca1c655++)
        {
            this._0xfd82b25d[_0xaca1c655] = false;
        }

        this._0xb2b2e3f4 = 0;
        this._0x8629003a = 0;
        this._0x683a98f3();
    }

    private int _0xb2b2e3f4;
    private int _0x8629003a;
    /// <summary>
    /// Take one seal from the pouch. A seal does not hand out a relic by itself - it
    /// guarantees that the NEXT spin uncovers one, which keeps the reward a reason to
    /// spin rather than a reason not to.
    /// </summary>
    public bool _0x2091cfd4()
    {
        if (this._0x8629003a <= 0)
        {
            return false;
        }

        this._0x8629003a--;
        this._0x683a98f3();
        return true;
    }

    public void _0xda98307b(int _0xd44e84a6)
    {
        int _0xa8f32912 = _0x9f11ea4e.Clamp(_0xd44e84a6);
        if (!this._0x1008a5a8(_0xa8f32912))
        {
            return;
        }

        this._0xb2b2e3f4 = _0xa8f32912;
        this._0x683a98f3();
    }

    public int _0x12f86fc8()
    {
        int _0x0f99f56c = 0;
        for (int _0x0e308202 = 0; _0x0e308202 < _0x9f11ea4e.Count; _0x0e308202++)
        {
            if (this._0xd83ab145(_0x0e308202))
            {
                _0x0f99f56c++;
            }
        }

        return _0x0f99f56c;
    }

    // One slot per relic of every hall, flattened: hall * RelicsPerHall + index.
    private readonly bool[] _0xfd82b25d = new bool[_0x9f11ea4e.Count * _0x9f11ea4e.RelicsPerHall];
    public void _0x0b3bfeb3()
    {
        string _0x7af62c7c = PlayerPrefs.GetString(AlbumKey, string.Empty);
        for (int _0xf2cc01dc = 0; _0xf2cc01dc < this._0xfd82b25d.Length; _0xf2cc01dc++)
        {
            this._0xfd82b25d[_0xf2cc01dc] = _0xf2cc01dc < _0x7af62c7c.Length && _0x7af62c7c[_0xf2cc01dc] == '1';
        }

        this._0xb2b2e3f4 = _0x9f11ea4e.Clamp(PlayerPrefs.GetInt(HallKey, 0));
        this._0x8629003a = PlayerPrefs.GetInt(SealKey, 0);
        if (this._0x8629003a < 0)
        {
            this._0x8629003a = 0;
        }

        if (!this._0x1008a5a8(this._0xb2b2e3f4))
        {
            this._0xb2b2e3f4 = this._0x65462401();
        }
    }

    public int _0x40874962
    {
        get
        {
            return this._0x8629003a;
        }
    }

    public void _0x56c3399d(int _0x6cfb56d2, int _0x61c0df7e)
    {
        int _0x7aafa980 = this._0xa75cd842(_0x6cfb56d2, _0x61c0df7e);
        if (_0x7aafa980 >= 0)
        {
            this._0xfd82b25d[_0x7aafa980] = true;
            this._0x683a98f3();
        }
    }
}

internal static class _0x5e78baab
{
    internal static string _0xbf4b84f0(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}