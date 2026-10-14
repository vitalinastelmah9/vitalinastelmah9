using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// The display scene's root. It owns the session seed, drives spin -> resolve -> card,
// and is the only thing that moves money.
//
// There is no clock here, nothing drains, and the pouch refills itself for free the
// moment it empties - so a run that is never touched simply sits in gameplay for as
// long as anyone looks at it (rule C.5).
public sealed class _0x47a6d4c6 : MonoBehaviour
{
    private static readonly string AttemptKey = _0x2affeb67._0xc7c3f55f(new byte[11] { 160, 185, 168, 225, 174, 187, 187, 170, 162, 191, 187 }, 207);
    public void _0xdc381e22()
    {
        if (this._pops != null)
        {
            this._pops._0xb36a92ea();
        }

        this._0xdc6ec8f2(this._0xcbf0216b == PhaseIdle);
    }

    private readonly bool[] _0xb368fd31 = new bool[_0x9f11ea4e.RelicsPerHall];
    private readonly _0xa624856f _0x9c072558 = new _0xa624856f();
    [SerializeField]
    private Sprite _hallBadgeSprite;
    public void _0x357e386b()
    {
        SceneManager.LoadScene(LobbySceneIndex);
    }

    private void Start()
    {
        this._0x58b5e9e0._0x4aa87c38();
        this._0x045552d6._0x59daee8e();
        this._0x9c072558._0x0b3bfeb3();
        this._0xd8a2ce05 = this._0x9c072558._0x6e8a9fd6;
        int _0x5d0f3cf7 = PlayerPrefs.GetInt(AttemptKey, 0) + 1;
        PlayerPrefs.SetInt(AttemptKey, _0x5d0f3cf7);
        PlayerPrefs.Save();
        int _0x18aa5693 = (this._0xd8a2ce05 * 7919) ^ (_0x5d0f3cf7 * 104729);
        // Braces on both sides of the directive: the obfuscator parses the region the
        // same whether the branch is compiled in or out. A bug here is reproducible
        // from the logged seed alone.
        {
#if B_LOGS
            {
                Debug.Log(_0x2affeb67._0xc7c3f55f(new byte[12] { 255, 204, 197, 200, 200, 249, 132, 215, 193, 193, 192, 153 }, 164) + _0x18aa5693 + _0x2affeb67._0xc7c3f55f(new byte[6] { 146, 218, 211, 222, 222, 143 }, 178) + this._0xd8a2ce05 + _0x2affeb67._0xc7c3f55f(new byte[9] { 89, 24, 13, 13, 28, 20, 9, 13, 68 }, 121) + _0x5d0f3cf7);
            }
#endif
        }

        this._0x3c33cfbb = new _0x1b53ce68()._0x97db330a(_0x18aa5693);
        this._0x080dc81b._0x44d7a961(this._0x3c33cfbb);
        for (int _0xb6496987 = 0; _0xb6496987 < _0x9f11ea4e.RelicsPerHall; _0xb6496987++)
        {
            this._0xb368fd31[_0xb6496987] = this._0x9c072558._0x80ff3499(this._0xd8a2ce05, _0xb6496987);
        }

        this._0x331077ee();
        this._0xb9c3496f();
        this._0xb68094a7();
        if (this._hud != null)
        {
            this._hud._0x52ab4e9d(this._0x58b5e9e0._0xed102b64);
            this._hud._0xe27d2490(this._0x045552d6._0x29c88373);
            this._hud._0x2af68356(this._0x045552d6._0xa8ad5b38);
            this._hud._0xc3958aeb(_0x9f11ea4e.Name(this._0xd8a2ce05), this._0xc5ede85d(), _0x9f11ea4e.RelicsPerHall);
            this._hud._0xd9f1b8a7();
        }

        if (this._pops != null)
        {
            this._pops._0xf8470dfb(this);
            this._pops._0xe959904f(this._hallBadgeSprite, _0xd7ad5941.Tint(_0x9f11ea4e.Tint(this._0xd8a2ce05)));
        }

        this._0xcbf0216b = PhaseIdle;
    }

    private void _0xcdbc9389()
    {
        bool _0x8c7a1947 = this._0x58b5e9e0._0xed102b64;
        for (int _0x5359b5ed = 0; _0x5359b5ed < this._columns.Length && _0x5359b5ed < this._0x0df7ca4a.Length; _0x5359b5ed++)
        {
            if (this._columns[_0x5359b5ed] == null)
            {
                continue;
            }

            float _0x2ac654ea = _0x10f6a5cc[_0x5359b5ed % _0x10f6a5cc.Length];
            int _0xfc481e60 = _0xc911c6f6[_0x5359b5ed % _0xc911c6f6.Length];
            this._columns[_0x5359b5ed]._0x65e32126(this._0x0df7ca4a[_0x5359b5ed], _0x2ac654ea, _0xfc481e60, _0x8c7a1947);
        }
    }

    private _0x1b53ce68 _0x3c33cfbb;
    private readonly RectTransform[] _0x4d06c9e7 = new RectTransform[_0xa026540f.Reels];
    private const float PunchSeconds = 0.36f;
    private int _0xd8a2ce05;
    private void _0x41f01839()
    {
        this._0xcbf0216b = PhaseIdle;
        if (this._0xc5ede85d() >= _0x9f11ea4e.RelicsPerHall && this._pops != null && !this._pops._0x23f3abfc)
        {
            this._0x045552d6._0x4514bb1e(_0xe3eb3e0e.HallCompleteReward);
            if (this._hud != null)
            {
                this._hud._0x75c80cc0(this._0x045552d6._0x29c88373);
            }

            this._pops._0x107a3508(_0x9f11ea4e.Name(this._0xd8a2ce05), _0xe3eb3e0e.HallCompleteReward);
            return;
        }

        if (!this._0x045552d6._0x12f934a0())
        {
            this._0xb31b858b();
            return;
        }

        this._0xdc6ec8f2(true);
    }

    private const int DisplaySceneIndex = 1;
    private void _0xb31b858b()
    {
        this._0xdc6ec8f2(false);
        if (this._pops != null && !this._pops._0x23f3abfc)
        {
            this._pops._0x355cbf82(_0xe3eb3e0e.DepletionGrant);
        }
    }

    private static readonly float[] _0x10f6a5cc =
    {
        0.55f,
        0.90f,
        1.25f
    };
    // -- spin machinery ------------------------------------------------------
    // Where the columns land. The layout, the relic set and the decor all come from
    // the seed; only the first couple of spins are nudged onto a stop that shows a
    // still-missing relic, so a review walkthrough sees progress rather than three
    // identical "NO MATCH" frames.
    private void _0x96438e54()
    {
        int len = this._0x080dc81b._0x67a2dff5;
        for (int _0x9fd84b58 = 0; _0x9fd84b58 < this._0x0df7ca4a.Length; _0x9fd84b58++)
        {
            this._0x0df7ca4a[_0x9fd84b58] = this._0x3c33cfbb._0x2429757e(len);
        }

        bool _0xfbf50f98 = this._0x0140bae1 || this._0xf842b9db < CourtesySpins;
        if (!_0xfbf50f98 || this._0xc5ede85d() >= _0x9f11ea4e.RelicsPerHall)
        {
            return;
        }

        for (int _0xc97c290b = 0; _0xc97c290b < 60; _0xc97c290b++)
        {
            if (this._0x90ed4817())
            {
                return;
            }

            for (int _0xa336e2f5 = 0; _0xa336e2f5 < this._0x0df7ca4a.Length; _0xa336e2f5++)
            {
                this._0x0df7ca4a[_0xa336e2f5] = this._0x3c33cfbb._0x2429757e(len);
            }
        }
    }

    private bool _0x1df1b00b()
    {
        if (this._columns == null)
        {
            return true;
        }

        for (int _0xb0adfdd0 = 0; _0xb0adfdd0 < this._columns.Length; _0xb0adfdd0++)
        {
            if (this._columns[_0xb0adfdd0] != null && this._columns[_0xb0adfdd0]._0x8f4fc550)
            {
                return false;
            }
        }

        return true;
    }

    public void _0x69a79c3d()
    {
        this._0x045552d6._0x12a2e659();
        if (this._hud != null)
        {
            this._hud._0x75c80cc0(this._0x045552d6._0x29c88373);
            this._hud._0x2af68356(this._0x045552d6._0xa8ad5b38);
            this._hud._0xd398d2a6(_0x2affeb67._0xc7c3f55f(new byte[28] { 241, 237, 224, 133, 230, 240, 247, 228, 241, 234, 247, 133, 246, 241, 228, 238, 224, 246, 133, 252, 234, 240, 133, 228, 226, 228, 236, 235 }, 165), _0xd7ad5941.Secondary);
        }

        if (this._pops != null)
        {
            this._pops._0xb36a92ea();
        }

        this._0xdc6ec8f2(true);
    }

    private float _0xed2e9b71;
    private bool _0x0140bae1;
    private int _0xcbf0216b;
    // -- winning symbols punch so a win is visible, not only counted ----------
    private void _0xc19c037b()
    {
        if (this._0x58b5e9e0._0xed102b64 || this._columns == null)
        {
            return;
        }

        for (int _0x23cc34ff = 0; _0x23cc34ff < this._0x4d06c9e7.Length; _0x23cc34ff++)
        {
            this._0x4d06c9e7[_0x23cc34ff] = _0x23cc34ff < this._columns.Length && this._columns[_0x23cc34ff] != null ? this._columns[_0x23cc34ff]._0x73f00f9a(1) : null;
        }

        this._0xed2e9b71 = 0f;
        this._0x753a46dc = true;
    }

    private readonly int[] _0xe35d5431 = new int[9];
    private int _0xf842b9db;
    [SerializeField]
    private Image _spinPlate;
    private readonly _0x061d8955 _0x58b5e9e0 = new _0x061d8955();
    private void _0x3f6447c5()
    {
        this._0xed2e9b71 += Time.deltaTime;
        float _0xb6cb4001 = this._0xed2e9b71 / PunchSeconds;
        bool _0xfa916645 = _0xb6cb4001 >= 1f;
        if (_0xfa916645)
        {
            _0xb6cb4001 = 1f;
        }

        for (int _0xf4435e93 = 0; _0xf4435e93 < this._0x4d06c9e7.Length; _0xf4435e93++)
        {
            RectTransform _0xa82c745d = this._0x4d06c9e7[_0xf4435e93];
            if (_0xa82c745d == null)
            {
                continue;
            }

            float _0xfea3161c = _0xb6cb4001 - (_0xf4435e93 * 0.12f);
            if (_0xfea3161c < 0f)
            {
                _0xfea3161c = 0f;
            }

            if (_0xfea3161c > 1f)
            {
                _0xfea3161c = 1f;
            }

            float _0xc4735827 = 1f + (0.14f * Mathf.Sin(_0xfea3161c * Mathf.PI));
            _0xa82c745d.localScale = new Vector3(_0xc4735827, _0xc4735827, 1f);
        }

        if (!_0xfa916645)
        {
            return;
        }

        this._0x753a46dc = false;
        for (int _0x5bfac3a8 = 0; _0x5bfac3a8 < this._0x4d06c9e7.Length; _0x5bfac3a8++)
        {
            if (this._0x4d06c9e7[_0x5bfac3a8] != null)
            {
                this._0x4d06c9e7[_0x5bfac3a8].localScale = Vector3.one;
                this._0x4d06c9e7[_0x5bfac3a8] = null;
            }
        }
    }

    private const int CourtesySpins = 2;
    private readonly _0xe3eb3e0e _0x045552d6 = new _0xe3eb3e0e();
    [SerializeField]
    private Button _spinButton;
    [SerializeField]
    private _0x65dcd6f6 _hud;
    [SerializeField]
    private Button _betUpButton;
    public void _0x9d0961cc(int _0x7e49ec45)
    {
        if (this._0xcbf0216b != PhaseIdle)
        {
            return;
        }

        if (this._0x045552d6._0xeaa74626(_0x7e49ec45) && this._hud != null)
        {
            this._hud._0x2af68356(this._0x045552d6._0xa8ad5b38);
        }
    }

    private const int LobbySceneIndex = 0;
    [SerializeField]
    private _0xac00ad0e[] _columns;
    private void _0xb68094a7()
    {
        if (this._spinButton != null)
        {
            this._spinButton.onClick.AddListener(() => this._0x4d115e11());
        }

        if (this._betDownButton != null)
        {
            this._betDownButton.onClick.AddListener(() => this._0x9d0961cc(-1));
        }

        if (this._betUpButton != null)
        {
            this._betUpButton.onClick.AddListener(() => this._0x9d0961cc(1));
        }

        if (this._backButton != null)
        {
            this._backButton.onClick.AddListener(() => this._0x357e386b());
        }

        if (this._pauseButton != null)
        {
            this._pauseButton.onClick.AddListener(() => this._0x324248f4());
        }
    }

    private const int PhaseAnticipating = 1;
    private const int PhaseSpinning = 2;
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Image[] _decorLamps;
    // -- player actions ------------------------------------------------------
    public void _0x4d115e11()
    {
        if (this._0xcbf0216b != PhaseIdle || (this._pops != null && this._pops._0x23f3abfc))
        {
            return;
        }

        if (!this._0x045552d6._0xb097c68e())
        {
            this._0xb31b858b();
            return;
        }

        // A seal is spent here and nowhere else: it buys the guarantee that THIS spin
        // uncovers a relic. Deciding it before the roll, rather than fixing the result
        // afterwards, is what keeps the reels honest.
        this._0x0140bae1 = this._0xc5ede85d() < _0x9f11ea4e.RelicsPerHall && this._0x9c072558._0x2091cfd4();
        this._0x96438e54();
        this._0xdc6ec8f2(false);
        if (this._hud != null)
        {
            this._hud._0x75c80cc0(this._0x045552d6._0x29c88373);
            this._hud._0xd9f1b8a7();
            this._hud._0xa12f361a();
        }

        this._0xcbf0216b = PhaseAnticipating;
        this._0x96ea87fd = 0f;
    }

    private const float AnticipationSeconds = 0.12f;
    private const int PhaseResolving = 3;
    private const int PhaseIdle = 0;
    private void _0xdc6ec8f2(bool _0xd0dc23d0)
    {
        if (this._spinButton != null)
        {
            this._spinButton.interactable = _0xd0dc23d0;
        }

        if (this._spinPlate != null)
        {
            this._spinPlate.color = _0xd0dc23d0 ? Color.white : _0xd7ad5941.AccentDeep;
        }

        _0x777dc4f8.Set(this._spinLabel, _0xd0dc23d0 ? _0x2affeb67._0xc7c3f55f(new byte[4] { 19, 16, 9, 14 }, 64) : _0x2affeb67._0xc7c3f55f(new byte[3] { 217, 217, 217 }, 247), _0xd7ad5941.Text);
    }

    [SerializeField]
    private Image[] _themedPanels;
    private void _0x331077ee()
    {
        bool _0x9b2d9544 = this._0x58b5e9e0._0x639ea781;
        if (this._themedPanels != null)
        {
            for (int _0x7cd8be18 = 0; _0x7cd8be18 < this._themedPanels.Length; _0x7cd8be18++)
            {
                if (this._themedPanels[_0x7cd8be18] != null)
                {
                    this._themedPanels[_0x7cd8be18].color = _0xd7ad5941.PanelFill(_0x9b2d9544);
                }
            }
        }

        // The backdrop decor differs per session: which three pedestals are lit, and
        // in which hue (rule C.11 - the change has to be visible, not just true).
        if (this._decorLamps == null)
        {
            return;
        }

        for (int _0x8042aea6 = 0; _0x8042aea6 < this._decorLamps.Length; _0x8042aea6++)
        {
            if (this._decorLamps[_0x8042aea6] != null)
            {
                this._decorLamps[_0x8042aea6].color = _0xd7ad5941.WithAlpha(_0xd7ad5941.Locked, 0.35f);
            }
        }

        for (int _0xf25a4993 = 0; _0xf25a4993 < 3; _0xf25a4993++)
        {
            int _0x57a3e440 = this._0x080dc81b._0x211a47d8(_0xf25a4993);
            if (_0x57a3e440 >= 0 && _0x57a3e440 < this._decorLamps.Length && this._decorLamps[_0x57a3e440] != null)
            {
                this._decorLamps[_0x57a3e440].color = _0xd7ad5941.WithAlpha(_0xd7ad5941.Tint(this._0x080dc81b._0x704e4dbc(_0xf25a4993)), 0.75f);
            }
        }
    }

    public void _0xd6ed8061()
    {
        this._0x9c072558._0x9cd27392();
        SceneManager.LoadScene(DisplaySceneIndex);
    }

    private void _0xb9c3496f()
    {
        if (this._columns == null)
        {
            return;
        }

        for (int _0xb210e4e6 = 0; _0xb210e4e6 < this._columns.Length; _0xb210e4e6++)
        {
            if (this._columns[_0xb210e4e6] == null)
            {
                continue;
            }

            this._columns[_0xb210e4e6]._0xaea599ed(this._0x080dc81b, _0xb210e4e6);
            this._0x0df7ca4a[_0xb210e4e6] = this._0x3c33cfbb._0x2429757e(this._0x080dc81b._0x67a2dff5);
            this._columns[_0xb210e4e6]._0x0327dd13(this._0x0df7ca4a[_0xb210e4e6]);
        }
    }

    private readonly _0x82a37252 _0x062558d8 = new _0x82a37252();
    [SerializeField]
    private Button _betDownButton;
    private bool _0x90ed4817()
    {
        for (int _0xa6e67ad0 = 0; _0xa6e67ad0 < _0x9f11ea4e.RelicsPerHall; _0xa6e67ad0++)
        {
            if (this._0xb368fd31[_0xa6e67ad0])
            {
                continue;
            }

            if (this._0x080dc81b._0x690bc7d2(this._0x080dc81b._0x17dec05b(_0xa6e67ad0), this._0x0df7ca4a[0], this._0x0df7ca4a[1], this._0x0df7ca4a[2]))
            {
                return true;
            }
        }

        return false;
    }

    [SerializeField]
    private Button _backButton;
    private void Update()
    {
        if (this._0x753a46dc)
        {
            this._0x3f6447c5();
        }

        if (this._0xcbf0216b == PhaseAnticipating)
        {
            this._0x96ea87fd += Time.deltaTime;
            if (this._0x96ea87fd >= AnticipationSeconds)
            {
                this._0xcdbc9389();
                this._0xcbf0216b = PhaseSpinning;
            }

            return;
        }

        if (this._0xcbf0216b == PhaseSpinning)
        {
            if (this._0x1df1b00b())
            {
                this._0xb7391cee();
                this._0xcbf0216b = PhaseResolving;
                this._0x96ea87fd = 0f;
            }

            return;
        }

        if (this._0xcbf0216b == PhaseResolving)
        {
            this._0x96ea87fd += Time.deltaTime;
            if (this._0x96ea87fd >= ResolveHoldSeconds)
            {
                this._0x41f01839();
            }
        }
    }

    [SerializeField]
    private _0xd6e10a48 _pops;
    private readonly int[] _0x0df7ca4a = new int[_0xa026540f.Reels];
    private bool _0x753a46dc;
    [SerializeField]
    private TextMeshProUGUI _spinLabel;
    private const float ResolveHoldSeconds = 0.65f;
    private float _0x96ea87fd;
    private void _0xb7391cee()
    {
        this._0xf842b9db++;
        for (int _0x864ff6f2 = 0; _0x864ff6f2 < _0xa026540f.Rows; _0x864ff6f2++)
        {
            for (int _0x7d17f5cb = 0; _0x7d17f5cb < _0xa026540f.Reels; _0x7d17f5cb++)
            {
                int _0x3a5f495b = this._columns != null && _0x7d17f5cb < this._columns.Length && this._columns[_0x7d17f5cb] != null ? this._columns[_0x7d17f5cb]._0x8166a267(_0x864ff6f2) : 0;
                this._0xe35d5431[(_0x864ff6f2 * _0xa026540f.Reels) + _0x7d17f5cb] = _0x3a5f495b;
            }
        }

        this._0x062558d8._0x0b4d4b72(this._0xe35d5431, this._0x045552d6._0xa8ad5b38, this._0x080dc81b, this._0xb368fd31);
        string _0x7e73369f = _0x2affeb67._0xc7c3f55f(new byte[8] { 15, 14, 97, 12, 0, 21, 2, 9 }, 65);
        Color _0xac2d5b6d = _0xd7ad5941.TextSoft;
        if (this._0x062558d8._0xe59ff1f7 > 0)
        {
            this._0x045552d6._0x4514bb1e(this._0x062558d8._0xe59ff1f7);
            _0x7e73369f = _0x2affeb67._0xc7c3f55f(new byte[1] { 7 }, 44) + this._0x062558d8._0xe59ff1f7 + _0x2affeb67._0xc7c3f55f(new byte[7] { 244, 128, 155, 159, 145, 154, 135 }, 212);
            _0xac2d5b6d = _0xd7ad5941.Accent;
            if (this._hud != null)
            {
                this._hud._0x4765ab53();
                this._hud._0x75c80cc0(this._0x045552d6._0x29c88373);
            }

            this._0xc19c037b();
        }

        if (this._0x062558d8._0x6f541eba >= 0)
        {
            int _0xa10b61bf = this._0x062558d8._0x6f541eba;
            this._0xb368fd31[_0xa10b61bf] = true;
            this._0x9c072558._0x56c3399d(this._0xd8a2ce05, _0xa10b61bf);
            _0x7e73369f = (this._0x0140bae1 ? _0x2affeb67._0xc7c3f55f(new byte[13] { 87, 65, 69, 72, 36, 87, 84, 65, 74, 80, 36, 41, 36 }, 4) : _0x2affeb67._0xc7c3f55f(new byte[14] { 167, 176, 185, 188, 182, 213, 179, 186, 160, 187, 177, 213, 216, 213 }, 245)) + _0x66896ed0.Name(this._0x080dc81b._0x17dec05b(_0xa10b61bf));
            _0xac2d5b6d = _0xd7ad5941.Secondary;
        }

        if (this._hud != null)
        {
            this._hud._0xd398d2a6(_0x7e73369f, _0xac2d5b6d);
            this._hud._0xc3958aeb(_0x9f11ea4e.Name(this._0xd8a2ce05), this._0xc5ede85d(), _0x9f11ea4e.RelicsPerHall);
        }
    }

    private static readonly int[] _0xc911c6f6 =
    {
        14,
        22,
        30
    };
    public void _0x324248f4()
    {
        if (this._pops == null || this._pops._0x23f3abfc)
        {
            return;
        }

        this._pops._0x7c676d3b(_0x9f11ea4e.Name(this._0xd8a2ce05), this._0xc5ede85d(), _0x9f11ea4e.RelicsPerHall, this._0x045552d6._0xa8ad5b38);
    }

    private int _0xc5ede85d()
    {
        int _0x6e1ab0cf = 0;
        for (int _0x609b220a = 0; _0x609b220a < this._0xb368fd31.Length; _0x609b220a++)
        {
            if (this._0xb368fd31[_0x609b220a])
            {
                _0x6e1ab0cf++;
            }
        }

        return _0x6e1ab0cf;
    }

    private readonly _0xa026540f _0x080dc81b = new _0xa026540f()._0x973e6a72();
}

internal static class _0x2affeb67
{
    internal static string _0xc7c3f55f(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}