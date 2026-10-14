using UnityEngine;

// Two accessibility switches, both of which change something a screenshot can show
// (rule C.7). There is deliberately no sound control of any kind: this app ships no
// audio at all, so a speaker icon would be a button that confirms nothing (rule C.20).
public sealed class _0x061d8955
{
    private int _0x86417a48;
    private const int ReducedMotionBit = 1;
    public bool _0x639ea781
    {
        get
        {
            return (this._0x86417a48 & HighContrastBit) != 0;
        }
    }

    public bool _0x672913e5()
    {
        this._0x86417a48 ^= ReducedMotionBit;
        this._0xa7f8211f();
        return this._0xed102b64;
    }

    public void _0xa7f8211f()
    {
        PlayerPrefs.SetInt(FlagsKey, this._0x86417a48);
        PlayerPrefs.Save();
    }

    private static readonly string FlagsKey = _0x263ad869._0x3323d916(new byte[9] { 122, 99, 114, 59, 115, 121, 116, 114, 102 }, 21);
    public void _0x4aa87c38()
    {
        this._0x86417a48 = PlayerPrefs.GetInt(FlagsKey, 0);
    }

    public bool _0xc3fdd186()
    {
        this._0x86417a48 ^= HighContrastBit;
        this._0xa7f8211f();
        return this._0x639ea781;
    }

    public bool _0xed102b64
    {
        get
        {
            return (this._0x86417a48 & ReducedMotionBit) != 0;
        }
    }

    private const int HighContrastBit = 2;
    public void _0xdc481723()
    {
        this._0x86417a48 = 0;
        this._0xa7f8211f();
    }
}

internal static class _0x263ad869
{
    internal static string _0x3323d916(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}