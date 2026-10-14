using UnityEngine;

// The pouch of curator's tokens. They are fictional: nothing buys them, nothing
// redeems them, and the vault hands out more for free the moment it runs dry - so the
// run can never dead-end and never needs a timer (rule C.5).
//
// Keys are explicit string literals, not nameof: obfuscation renames members, and a
// persisted identifier that moves with a member name loses every player's progress.
public sealed class _0xe3eb3e0e
{
    /// <summary>Take the stake. Returns false when the pouch cannot cover the cheapest bet.</summary>
    public bool _0xb097c68e()
    {
        this._0x6282c238();
        int cost = _0xcc4a7af4.BetAt(this._0x64443acf);
        if (this._0xdb691b7d < cost)
        {
            return false;
        }

        this._0xdb691b7d -= cost;
        this._0xf4954f74();
        return true;
    }

    public void _0x59daee8e()
    {
        this._0xdb691b7d = PlayerPrefs.GetInt(BalanceKey, StartingBalance);
        this._0x64443acf = PlayerPrefs.GetInt(BetKey, _0xcc4a7af4.DefaultBetStep);
        if (this._0x64443acf < 0 || this._0x64443acf >= _0xcc4a7af4._0x4fc226e8)
        {
            this._0x64443acf = _0xcc4a7af4.DefaultBetStep;
        }

        if (this._0xdb691b7d < 0)
        {
            this._0xdb691b7d = 0;
        }

        this._0x6282c238();
    }

    private static readonly string BalanceKey = _0x7bfdbfb8._0xb5b6145c(new byte[10] { 100, 125, 108, 37, 127, 100, 96, 110, 101, 120 }, 11);
    public void _0x4514bb1e(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        this._0xdb691b7d += amount;
        this._0xf4954f74();
    }

    /// <summary>The curator stakes the player again. No cost, no purchase, no limit.</summary>
    public void _0x12a2e659()
    {
        this._0xdb691b7d += DepletionGrant;
        this._0x6282c238();
        this._0xf4954f74();
    }

    public static int _0x2e6944db
    {
        get
        {
            return _0xcc4a7af4._0x4fc226e8;
        }
    }

    /// <summary>The cheapest bet is always affordable, because the grant guarantees it.</summary>
    private void _0x6282c238()
    {
        while (this._0x64443acf > 0 && _0xcc4a7af4.BetAt(this._0x64443acf) > this._0xdb691b7d)
        {
            this._0x64443acf--;
        }
    }

    public const int DailyGrant = _0xcc4a7af4.DailyGrant;
    public int _0x29c88373
    {
        get
        {
            return this._0xdb691b7d;
        }
    }

    public int _0xb80cb0ef
    {
        get
        {
            return this._0x64443acf;
        }
    }

    public const int HallCompleteReward = _0xcc4a7af4.HallCompleteReward;
    /// <summary>Step the bet up or down, never past what the pouch can cover.</summary>
    public bool _0xeaa74626(int _0x5d3ee933)
    {
        int _0x67ca59f4 = this._0x64443acf;
        int _0x819b8c47 = this._0x64443acf + _0x5d3ee933;
        if (_0x819b8c47 < 0)
        {
            _0x819b8c47 = 0;
        }

        if (_0x819b8c47 >= _0xcc4a7af4._0x4fc226e8)
        {
            _0x819b8c47 = _0xcc4a7af4._0x4fc226e8 - 1;
        }

        this._0x64443acf = _0x819b8c47;
        this._0x6282c238();
        if (this._0x64443acf == _0x67ca59f4)
        {
            return false;
        }

        this._0xf4954f74();
        return true;
    }

    private static readonly string BetKey = _0x7bfdbfb8._0xb5b6145c(new byte[7] { 138, 147, 130, 203, 135, 128, 145 }, 229);
    private int _0x64443acf;
    private int _0xdb691b7d;
    public bool _0x12f934a0()
    {
        return this._0xdb691b7d >= _0xcc4a7af4._0x796a1b3e;
    }

    public int _0xa8ad5b38
    {
        get
        {
            return _0xcc4a7af4.BetAt(this._0x64443acf);
        }
    }

    public void _0x347530ab()
    {
        this._0xdb691b7d = StartingBalance;
        this._0x64443acf = _0xcc4a7af4.DefaultBetStep;
        this._0xf4954f74();
    }

    public const int DepletionGrant = _0xcc4a7af4.DepletionGrant;
    public const int StartingBalance = _0xcc4a7af4.StartingBalance;
    public void _0xf4954f74()
    {
        PlayerPrefs.SetInt(BalanceKey, this._0xdb691b7d);
        PlayerPrefs.SetInt(BetKey, this._0x64443acf);
        PlayerPrefs.Save();
    }
}

internal static class _0x7bfdbfb8
{
    internal static string _0xb5b6145c(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}