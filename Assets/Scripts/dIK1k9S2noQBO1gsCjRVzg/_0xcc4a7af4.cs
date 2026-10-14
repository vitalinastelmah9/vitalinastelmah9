// Every number that moves tokens, in one engine-free place so the balance claims can be
// compiled and RUN outside the editor against the shipping values rather than a copy of
// them. TokenVault is the only thing that reads these; it adds persistence and nothing
// else.
//
// The tokens are fictional: nothing buys them, nothing redeems them, and DepletionGrant
// is handed out for free and without limit, which is why this game needs no timer and
// no fail state at all.
public static class _0xcc4a7af4
{
    /// <summary>
    /// Seconds one spin takes end to end: anticipation, the last column's stop, its
    /// settle, and the hold while the result is read. The passive-run figure above is
    /// derived from this, so the two cannot drift apart.
    /// </summary>
    public const float SpinCycleSeconds = 0.12f + 1.25f + 0.22f + 0.65f;
    /// <summary>The three stakes, cheapest first.</summary>
    private static readonly int[] _0x1b2e14f6 =
    {
        25,
        50,
        100
    };
    public const int DailyGrant = 250;
    public const int HallCompleteReward = 750;
    public const int DefaultBetStep = 1;
    public const int DepletionGrant = 500;
    public static int BetAt(int _0xddd24ec9)
    {
        if (_0xddd24ec9 < 0)
        {
            _0xddd24ec9 = 0;
        }

        if (_0xddd24ec9 >= _0x1b2e14f6.Length)
        {
            _0xddd24ec9 = _0x1b2e14f6.Length - 1;
        }

        return _0x1b2e14f6[_0xddd24ec9];
    }

    public static int _0x4fc226e8
    {
        get
        {
            return _0x1b2e14f6.Length;
        }
    }

    /// <summary>The cheapest stake, which the grant always keeps affordable.</summary>
    public static int _0x796a1b3e
    {
        get
        {
            return _0x1b2e14f6[0];
        }
    }

    // 3000, not 2500, for one measured reason: the worst a passive run can do is the
    // top bet with zero wins, and at a 2.24 s spin cycle that is 30 spins = 67 s. At
    // 2500 it measured 56 s, under the 60 s floor a review capture is sized against.
    public const int StartingBalance = 3000;
}