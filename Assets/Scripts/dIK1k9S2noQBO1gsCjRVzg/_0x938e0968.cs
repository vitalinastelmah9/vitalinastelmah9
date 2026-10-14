using UnityEngine;

// The daily gift: a local day check and a streak, nothing more. No network, no server,
// no account - the "day" is the device's own UTC date, compared with the last one
// stored.
public sealed class _0x938e0968
{
    /// <summary>Claim today's gift. Returns false when it was already taken.</summary>
    public bool _0xf7c87d76()
    {
        if (!this._0x28e98a9e())
        {
            return false;
        }

        int _0x7b58c7f1 = Today();
        bool _0x7cb76b05 = this._0x647fec71 == _0x7b58c7f1 - 1;
        this._0x9b52eaf5 = _0x7cb76b05 ? this._0x9b52eaf5 + 1 : 1;
        if (this._0x9b52eaf5 > StreakLength)
        {
            this._0x9b52eaf5 = 1;
        }

        this._0x647fec71 = _0x7b58c7f1;
        this._0xb1b2b462();
        return true;
    }

    private int _0x9b52eaf5;
    private int _0x647fec71;
    public bool _0x28e98a9e()
    {
        return this._0x647fec71 != Today();
    }

    public int _0x8e3ccc0f
    {
        get
        {
            return this._0x9b52eaf5;
        }
    }

    public const int StreakLength = 7;
    /// <summary>Which pip of the streak row is today's, 0-based.</summary>
    public int _0x9cbb30cf()
    {
        int _0x6754805b = this._0x28e98a9e() ? this._0x9b52eaf5 : this._0x9b52eaf5 - 1;
        if (_0x6754805b < 0)
        {
            _0x6754805b = 0;
        }

        return _0x6754805b >= StreakLength ? StreakLength - 1 : _0x6754805b;
    }

    private static readonly string StreakKey = _0x395f0d71._0xbf3095dd(new byte[10] { 71, 94, 79, 6, 91, 92, 90, 77, 73, 67 }, 40);
    public void _0xb1b2b462()
    {
        PlayerPrefs.SetInt(DayKey, this._0x647fec71);
        PlayerPrefs.SetInt(StreakKey, this._0x9b52eaf5);
        PlayerPrefs.Save();
    }

    private static readonly string DayKey = _0x395f0d71._0xbf3095dd(new byte[12] { 115, 106, 123, 50, 126, 115, 114, 105, 111, 88, 125, 101 }, 28);
    public void _0xf65d8b4c()
    {
        this._0x647fec71 = -1;
        this._0x9b52eaf5 = 0;
        this._0xb1b2b462();
    }

    public void _0x16fba8ea()
    {
        this._0x647fec71 = PlayerPrefs.GetInt(DayKey, -1);
        this._0x9b52eaf5 = PlayerPrefs.GetInt(StreakKey, 0);
        if (this._0x9b52eaf5 < 0)
        {
            this._0x9b52eaf5 = 0;
        }

        if (this._0x9b52eaf5 > StreakLength)
        {
            this._0x9b52eaf5 = StreakLength;
        }
    }

    /// <summary>Days since the epoch, the device's own clock.</summary>
    private static int Today()
    {
        System.DateTime _0x044d606d = System.DateTime.UtcNow.Date;
        return (int)(_0x044d606d - new System.DateTime(2020, 1, 1)).TotalDays;
    }
}

internal static class _0x395f0d71
{
    internal static string _0xbf3095dd(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}