// The eight relics a display can turn up: strip weight, payout multiplier and the
// caption shown when one is found. Index IS the identity - the arrays are parallel and
// every other file addresses a relic by its index, never by a name, so obfuscation has
// nothing to break.
public static class _0x66896ed0
{
    private static readonly string[] _0xb144f0b9 =
    {
        _0xc805570a._0x3db7f528(new byte[9] { 230, 228, 236, 129, 242, 233, 224, 243, 229 }, 161),
        _0xc805570a._0x3db7f528(new byte[13] { 124, 119, 122, 97, 126, 127, 27, 109, 126, 104, 104, 126, 119 }, 59),
        _0xc805570a._0x3db7f528(new byte[6] { 228, 244, 229, 248, 251, 251 }, 183),
        _0xc805570a._0x3db7f528(new byte[10] { 97, 113, 98, 112, 112, 3, 111, 98, 110, 115 }, 35),
        _0xc805570a._0x3db7f528(new byte[6] { 198, 214, 212, 199, 212, 215 }, 149),
        _0xc805570a._0x3db7f528(new byte[10] { 131, 147, 128, 146, 146, 225, 141, 152, 147, 132 }, 193),
        _0xc805570a._0x3db7f528(new byte[15] { 174, 168, 191, 168, 160, 162, 163, 164, 172, 161, 205, 160, 172, 190, 166 }, 237),
        _0xc805570a._0x3db7f528(new byte[11] { 183, 186, 174, 169, 190, 183, 219, 168, 190, 186, 183 }, 251),
    };
    /// <summary>Stops a relic occupies when a strip of StripLength is drawn (sums to StripLength).</summary>
    private static readonly int[] _0xa6e2ae4c =
    {
        3,
        3,
        3,
        2,
        2,
        1,
        1,
        1
    };
    /// <summary>Payout multiplier applied to the bet on a middle-row triple.</summary>
    private static readonly int[] _0x6ec3a058 =
    {
        3,
        3,
        4,
        6,
        8,
        12,
        20,
        40
    };
    /// <summary>Number of distinct relics.</summary>
    public const int Count = 8;
    /// <summary>Total weight, i.e. the number of stops a freshly drawn strip holds.</summary>
    public const int StripLength = 16;
    public static int Multiplier(int _0xa8b24765)
    {
        return (_0xa8b24765 < 0 || _0xa8b24765 >= Count) ? 0 : _0x6ec3a058[_0xa8b24765];
    }

    /// <summary>
    /// Only the first five relics are ever asked for by a hall. The lyre, the mask and
    /// the seal are the paying end of the strip - a single stop each - and a hall whose
    /// set contained one would take hundreds of spins to light, measured at 89.
    /// </summary>
    public const int CollectibleCount = 5;
    public static string Name(int _0xe54a6098)
    {
        return (_0xe54a6098 < 0 || _0xe54a6098 >= Count) ? _0xc805570a._0x3db7f528(new byte[5] { 154, 141, 132, 129, 139 }, 200) : _0xb144f0b9[_0xe54a6098];
    }

    /// <summary>The laurel seal substitutes for any relic when completing a middle-row triple.</summary>
    public const int WildIndex = 7;
    public static int Weight(int _0xccf89c33)
    {
        return (_0xccf89c33 < 0 || _0xccf89c33 >= Count) ? 0 : _0xa6e2ae4c[_0xccf89c33];
    }

    /// <summary>Draw one relic index against the weight table.</summary>
    public static int DrawWeighted(_0x1b53ce68 _0x96414821)
    {
        int _0x319aaa1d = _0x96414821._0x2429757e(StripLength);
        int _0x84d94928 = 0;
        for (int _0xac6b3d46 = 0; _0xac6b3d46 < Count; _0xac6b3d46++)
        {
            _0x84d94928 += _0xa6e2ae4c[_0xac6b3d46];
            if (_0x319aaa1d < _0x84d94928)
            {
                return _0xac6b3d46;
            }
        }

        return Count - 1;
    }
}

internal static class _0xc805570a
{
    internal static string _0x3db7f528(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}