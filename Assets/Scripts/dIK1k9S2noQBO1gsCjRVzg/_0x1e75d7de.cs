// Number formatting for the token counters.
//
// Grouped by hand rather than through a culture format: Hermes-style locale data is not
// something to depend on here, and the group separator must be a plain ASCII space -
// the TMP atlas this app ships carries ASCII 32..126 and nothing else, so a thin space
// or a middle dot would render as a missing glyph.
public static class _0x1e75d7de
{
    /// <summary>1234567 becomes "1 234 567".</summary>
    public static string Tokens(int _0x013b2385)
    {
        bool _0x0259cd36 = _0x013b2385 < 0;
        int _0x702178bb = _0x0259cd36 ? -_0x013b2385 : _0x013b2385;
        string _0x3664d88d = _0x702178bb.ToString();
        if (_0x3664d88d.Length <= 3)
        {
            return _0x0259cd36 ? _0x254b8048._0x269f094a(new byte[1] { 197 }, 232) + _0x3664d88d : _0x3664d88d;
        }

        string _0x70301eb5 = string.Empty;
        int _0x02cb1c6e = 0;
        for (int _0x4b87f702 = _0x3664d88d.Length - 1; _0x4b87f702 >= 0; _0x4b87f702--)
        {
            _0x70301eb5 = _0x3664d88d[_0x4b87f702] + _0x70301eb5;
            _0x02cb1c6e++;
            if (_0x02cb1c6e % 3 == 0 && _0x4b87f702 > 0)
            {
                _0x70301eb5 = _0x254b8048._0x269f094a(new byte[1] { 89 }, 121) + _0x70301eb5;
            }
        }

        return _0x0259cd36 ? _0x254b8048._0x269f094a(new byte[1] { 167 }, 138) + _0x70301eb5 : _0x70301eb5;
    }

    /// <summary>"2 OF 3 RELICS", the wording used on the strip, the album and the pause pop.</summary>
    public static string RelicProgress(int _0x01167e11, int _0x40c64cd3)
    {
        return _0x01167e11 + _0x254b8048._0x269f094a(new byte[4] { 162, 205, 196, 162 }, 130) + _0x40c64cd3 + _0x254b8048._0x269f094a(new byte[7] { 96, 18, 5, 12, 9, 3, 19 }, 64);
    }
}

internal static class _0x254b8048
{
    internal static string _0x269f094a(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}