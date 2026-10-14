// Reads one 3x3 window and says what the player won: a middle-row triple (the laurel
// seal substitutes for any relic), and at most one relic shard - the first of the
// hall's set that is still missing.
//
// A relic is uncovered when THREE or more of it share the window, wherever they land -
// a scatter, not a line. It must be found as itself: the wild pays lines, it does not
// stand in for a relic. The same test lives in ReelStripGenerator.WindowScatters, which
// is what proves a generated hall can be finished; the two must agree.
//
// Engine-free, so the paytable can be exercised outside the editor.
public sealed class _0x82a37252
{
    /// <summary>
    /// window is laid out row-major: window[(row * 3) + reel], rows top to bottom.
    /// found[i] says whether relic i of the hall's set is already collected.
    /// </summary>
    public void _0x0b4d4b72(int[] _0x386905cc, int _0x0aecb964, _0xa026540f _0xd0f83571, bool[] _0x66e743f7)
    {
        this._0xfb910d5f();
        if (_0x386905cc == null || _0x386905cc.Length < 9 || _0xd0f83571 == null)
        {
            return;
        }

        int _0x419866c3 = _0x386905cc[3];
        int _0x97afa3d1 = _0x386905cc[4];
        int _0xd99e6997 = _0x386905cc[5];
        int _0xafe32d16 = this._0xfd54b421(_0x419866c3, _0x97afa3d1, _0xd99e6997);
        if (_0xafe32d16 >= 0)
        {
            this._0xf89370b4 = _0xafe32d16;
            this._0xe59ff1f7 = _0x0aecb964 * _0x66896ed0.Multiplier(_0xafe32d16);
        }

        if (_0x66e743f7 == null)
        {
            return;
        }

        for (int _0x40239782 = 0; _0x40239782 < _0x9f11ea4e.RelicsPerHall && _0x40239782 < _0x66e743f7.Length; _0x40239782++)
        {
            if (_0x66e743f7[_0x40239782])
            {
                continue;
            }

            int _0xe596420e = _0xd0f83571._0x17dec05b(_0x40239782);
            int _0x25254ca5 = 0;
            for (int _0x2b7022b4 = 0; _0x2b7022b4 < _0x386905cc.Length; _0x2b7022b4++)
            {
                if (_0x386905cc[_0x2b7022b4] == _0xe596420e)
                {
                    _0x25254ca5++;
                }
            }

            if (_0x25254ca5 >= ScatterNeeded)
            {
                this._0x6f541eba = _0x40239782;
                return;
            }
        }
    }

    /// <summary>Index 0..2 into the hall's relic set that this spin completed, or -1.</summary>
    public int _0x6f541eba { get; private set; }

    /// <summary>Copies of a relic that must share the window to uncover it.</summary>
    private const int ScatterNeeded = 3;
    /// <summary>Relic index of the middle-row triple, or -1 when there is none.</summary>
    public int _0xf89370b4 { get; private set; }
    /// <summary>Tokens won on this spin.</summary>
    public int _0xe59ff1f7 { get; private set; }

    /// <summary>The paying relic of a three-of-a-kind with wild substitution, else -1.</summary>
    private int _0xfd54b421(int _0xb09a9ee2, int _0x89b756c2, int _0xf6b8a195)
    {
        int _0x4d0066c7 = _0x66896ed0.WildIndex;
        int _0xeb4ec95d = -1;
        if (_0xb09a9ee2 != _0x4d0066c7)
        {
            _0xeb4ec95d = _0xb09a9ee2;
        }
        else if (_0x89b756c2 != _0x4d0066c7)
        {
            _0xeb4ec95d = _0x89b756c2;
        }
        else if (_0xf6b8a195 != _0x4d0066c7)
        {
            _0xeb4ec95d = _0xf6b8a195;
        }

        if (_0xeb4ec95d < 0)
        {
            // Three seals: the richest line in the game.
            return _0x4d0066c7;
        }

        bool _0x8f220d83 = (_0xb09a9ee2 == _0xeb4ec95d || _0xb09a9ee2 == _0x4d0066c7) && (_0x89b756c2 == _0xeb4ec95d || _0x89b756c2 == _0x4d0066c7) && (_0xf6b8a195 == _0xeb4ec95d || _0xf6b8a195 == _0x4d0066c7);
        return _0x8f220d83 ? _0xeb4ec95d : -1;
    }

    public void _0xfb910d5f()
    {
        this._0xf89370b4 = -1;
        this._0xe59ff1f7 = 0;
        this._0x6f541eba = -1;
    }
}