// The six halls of the collection. A hall is only a name, an unlock order and a
// badge tint slot - everything that makes a session (its relic set, its strips, its
// decor) is generated per session from the seed, so two visits to the same hall are
// not the same game (C.11).
public static class _0x9f11ea4e
{
    /// <summary>Relics a hall asks for before its page in the album is lit.</summary>
    public const int RelicsPerHall = 3;
    /// <summary>Which of the three accent tints a hall's badge and chip take.</summary>
    private static readonly int[] _0x0d6f3b4b =
    {
        0,
        1,
        2,
        1,
        2,
        0
    };
    public static int Tint(int _0x5622cf2f)
    {
        return (_0x5622cf2f < 0 || _0x5622cf2f >= Count) ? 0 : _0x0d6f3b4b[_0x5622cf2f];
    }

    private static readonly string[] _0xfc86e7b2 =
    {
        _0x82186c48._0x5c46e297(new byte[12] { 233, 229, 234, 237, 250, 136, 233, 252, 250, 225, 253, 229 }, 168),
        _0x82186c48._0x5c46e297(new byte[15] { 172, 179, 181, 182, 191, 174, 218, 185, 182, 181, 179, 169, 174, 191, 168 }, 250),
        _0x82186c48._0x5c46e297(new byte[13] { 241, 225, 242, 224, 224, 147, 225, 252, 231, 230, 253, 247, 242 }, 179),
        _0x82186c48._0x5c46e297(new byte[13] { 70, 68, 68, 69, 71, 66, 95, 43, 93, 74, 94, 71, 95 }, 11),
        _0x82186c48._0x5c46e297(new byte[14] { 75, 71, 90, 73, 68, 40, 88, 73, 94, 65, 68, 65, 71, 70 }, 8),
        _0x82186c48._0x5c46e297(new byte[13] { 59, 51, 60, 59, 44, 94, 63, 44, 61, 54, 55, 40, 59 }, 126),
    };
    public static int Clamp(int _0xd8e6f506)
    {
        if (_0xd8e6f506 < 0)
        {
            return 0;
        }

        return _0xd8e6f506 >= Count ? Count - 1 : _0xd8e6f506;
    }

    public const int Count = 6;
    public static string Name(int _0x32c2a610)
    {
        return (_0x32c2a610 < 0 || _0x32c2a610 >= Count) ? _0x82186c48._0x5c46e297(new byte[4] { 208, 217, 212, 212 }, 152) : _0xfc86e7b2[_0x32c2a610];
    }
}

internal static class _0x82186c48
{
    internal static string _0x5c46e297(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}