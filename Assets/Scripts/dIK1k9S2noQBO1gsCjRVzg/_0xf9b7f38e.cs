using UnityEngine;

// The three sheets the lobby can raise over itself - today's gift, the collection, the
// settings - and the scrim behind them. One at a time, and every one of them is an
// object this game created: a scene-level m_IsActive modification on a template object
// is silently discarded, so codegen only ever switches what it owns.
public sealed class _0xf9b7f38e : MonoBehaviour
{
    [SerializeField]
    private GameObject _giftRoot;
    private int _0xab4e8389 = SheetNone;
    private void _0x7e83a452(GameObject _0x7ece1ae7, bool _0x4c5a307d)
    {
        if (_0x7ece1ae7 != null && _0x7ece1ae7.activeSelf != _0x4c5a307d)
        {
            _0x7ece1ae7.SetActive(_0x4c5a307d);
        }
    }

    private void _0x89fd240a(GameObject _0x687711b9)
    {
        this._0x7e83a452(this._scrim, _0x687711b9 != null);
        this._0x7e83a452(this._giftRoot, _0x687711b9 == this._giftRoot && _0x687711b9 != null);
        this._0x7e83a452(this._albumRoot, _0x687711b9 == this._albumRoot && _0x687711b9 != null);
        this._0x7e83a452(this._settingsRoot, _0x687711b9 == this._settingsRoot && _0x687711b9 != null);
    }

    public const int SheetAlbum = 1;
    public const int SheetSettings = 2;
    [SerializeField]
    private GameObject _albumRoot;
    [SerializeField]
    private GameObject _settingsRoot;
    [SerializeField]
    private GameObject _scrim;
    public const int SheetGift = 0;
    public void _0x4ef4a85f()
    {
        this._0xab4e8389 = SheetNone;
        this._0x89fd240a(null);
    }

    public int _0xbf2859cc
    {
        get
        {
            return this._0xab4e8389;
        }
    }

    public void _0x91cef7b7(int _0x4bc7972e)
    {
        this._0xab4e8389 = _0x4bc7972e;
        if (_0x4bc7972e == SheetGift)
        {
            this._0x89fd240a(this._giftRoot);
            return;
        }

        if (_0x4bc7972e == SheetAlbum)
        {
            this._0x89fd240a(this._albumRoot);
            return;
        }

        if (_0x4bc7972e == SheetSettings)
        {
            this._0x89fd240a(this._settingsRoot);
            return;
        }

        this._0xab4e8389 = SheetNone;
        this._0x89fd240a(null);
    }

    public const int SheetNone = -1;
    public bool _0x22697a8a
    {
        get
        {
            return this._0xab4e8389 != SheetNone;
        }
    }

    private void Start()
    {
        this._0x4ef4a85f();
    }
}