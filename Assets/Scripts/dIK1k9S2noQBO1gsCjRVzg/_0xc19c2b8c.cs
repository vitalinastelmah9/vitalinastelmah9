using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Four rows, and every one of them changes something a screenshot can show.
//
// There is deliberately no sound row of any kind: this app ships no audio, so a volume
// slider or a speaker icon would be a control that confirms nothing (rule C.20).
public sealed class _0xc19c2b8c : MonoBehaviour
{
    [SerializeField]
    private Button _howToButton;
    private void _0x137ec33c()
    {
        this._0x63147bc8(false, true, false);
    }

    [SerializeField]
    private Button _howToBackButton;
    [SerializeField]
    private TextMeshProUGUI _heading;
    private void _0xa5e06000()
    {
        if (this._0xb221dcc2 == null)
        {
            return;
        }

        bool _0x0947d368 = this._0xb221dcc2._0x672913e5();
        if (this._motionToggle != null)
        {
            this._motionToggle._0x6ea7aaa3(_0x0947d368);
        }

        if (this._0x1cc37ff3 != null)
        {
            this._0x1cc37ff3._0x60660b4a(_0x0947d368);
        }
    }

    public void _0x6e83ad66()
    {
        this._0x6b49adad();
        if (this._0xb221dcc2 == null)
        {
            return;
        }

        if (this._motionToggle != null)
        {
            this._motionToggle._0x6ea7aaa3(this._0xb221dcc2._0xed102b64);
        }

        if (this._contrastToggle != null)
        {
            this._contrastToggle._0x6ea7aaa3(this._0xb221dcc2._0x639ea781);
        }
    }

    [SerializeField]
    private GameObject _rowsRoot;
    private void _0x63147bc8(bool _0x3dfafd8a, bool _0x4c9234cc, bool _0x350a09bd)
    {
        if (this._rowsRoot != null)
        {
            this._rowsRoot.SetActive(_0x3dfafd8a);
        }

        if (this._howToCard != null)
        {
            this._howToCard.SetActive(_0x4c9234cc);
        }

        if (this._confirmCard != null)
        {
            this._confirmCard.SetActive(_0x350a09bd);
        }
    }

    private void _0xa7c222fb()
    {
        if (this._0xb221dcc2 == null)
        {
            return;
        }

        bool _0x1dac4b23 = this._0xb221dcc2._0xc3fdd186();
        if (this._contrastToggle != null)
        {
            this._contrastToggle._0x6ea7aaa3(_0x1dac4b23);
        }

        if (this._0x1cc37ff3 != null)
        {
            this._0x1cc37ff3._0x46ea1352(_0x1dac4b23);
        }
    }

    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private Button _resetButton;
    public void _0xf49e754e(_0x72a57d44 _0xefbbcad9, _0x061d8955 _0xd501b08f)
    {
        this._0x1cc37ff3 = _0xefbbcad9;
        this._0xb221dcc2 = _0xd501b08f;
        if (this._motionToggle != null)
        {
            this._motionToggle._0x5e484fcf(() => this._0xa5e06000());
        }

        if (this._contrastToggle != null)
        {
            this._contrastToggle._0x5e484fcf(() => this._0xa7c222fb());
        }

        if (this._howToButton != null)
        {
            this._howToButton.onClick.AddListener(() => this._0x137ec33c());
        }

        if (this._howToBackButton != null)
        {
            this._howToBackButton.onClick.AddListener(() => this._0x6b49adad());
        }

        if (this._resetButton != null)
        {
            this._resetButton.onClick.AddListener(() => this._0xc98651a8());
        }

        if (this._confirmYesButton != null)
        {
            this._confirmYesButton.onClick.AddListener(() => this._0xea94a2ee());
        }

        if (this._confirmNoButton != null)
        {
            this._confirmNoButton.onClick.AddListener(() => this._0x6b49adad());
        }

        if (this._closeButton != null)
        {
            this._closeButton.onClick.AddListener(() => this._0x52b49f8a());
        }

        _0x777dc4f8.Set(this._heading, _0x90df325c._0x3031390f(new byte[8] { 209, 199, 214, 214, 203, 204, 197, 209 }, 130), _0xd7ad5941.Accent);
        _0x777dc4f8.ApplyOutline(this._heading, _0xd7ad5941.Accent);
        _0x777dc4f8.Set(this._howToBody, _0x90df325c._0x3031390f(new byte[142] { 74, 83, 89, 81, 58, 91, 58, 82, 91, 86, 86, 58, 91, 84, 94, 58, 78, 91, 74, 58, 73, 74, 83, 84, 52, 16, 78, 82, 72, 95, 95, 58, 85, 92, 58, 91, 58, 81, 83, 84, 94, 58, 85, 84, 58, 78, 82, 95, 58, 87, 83, 94, 94, 86, 95, 58, 72, 85, 77, 58, 74, 91, 67, 73, 52, 16, 78, 82, 72, 95, 95, 58, 85, 92, 58, 91, 58, 82, 91, 86, 86, 58, 72, 95, 86, 83, 89, 58, 91, 84, 67, 77, 82, 95, 72, 95, 58, 79, 84, 89, 85, 76, 95, 72, 73, 58, 83, 78, 52, 16, 79, 84, 89, 85, 76, 95, 72, 58, 91, 86, 86, 58, 41, 58, 78, 85, 58, 86, 83, 93, 82, 78, 58, 78, 82, 95, 58, 82, 91, 86, 86, 52 }, 26), _0xd7ad5941.TextSoft);
        _0x777dc4f8.Set(this._confirmBody, _0x90df325c._0x3031390f(new byte[53] { 97, 105, 116, 112, 125, 4, 112, 108, 97, 4, 101, 104, 102, 113, 105, 4, 101, 106, 96, 4, 118, 97, 112, 113, 118, 106, 46, 112, 108, 97, 4, 116, 107, 113, 103, 108, 4, 112, 107, 4, 23, 4, 20, 20, 20, 4, 112, 107, 111, 97, 106, 119, 27 }, 36), _0xd7ad5941.Text);
        _0x777dc4f8.Set(this._footer, _0x90df325c._0x3031390f(new byte[63] { 245, 238, 234, 228, 239, 242, 129, 224, 243, 228, 129, 231, 232, 226, 245, 232, 238, 239, 224, 237, 129, 140, 129, 239, 238, 129, 241, 244, 243, 226, 233, 224, 242, 228, 242, 171, 239, 238, 129, 226, 224, 242, 233, 129, 247, 224, 237, 244, 228, 129, 140, 129, 239, 238, 129, 246, 224, 230, 228, 243, 232, 239, 230 }, 161), _0xd7ad5941.TextSoft);
    }

    private void _0xea94a2ee()
    {
        if (this._0x1cc37ff3 != null)
        {
            this._0x1cc37ff3._0x7337ae7b();
        }

        this._0x6b49adad();
    }

    private _0x72a57d44 _0x1cc37ff3;
    private _0x061d8955 _0xb221dcc2;
    [SerializeField]
    private GameObject _howToCard;
    [SerializeField]
    private TextMeshProUGUI _howToBody;
    [SerializeField]
    private _0x408694e7 _contrastToggle;
    [SerializeField]
    private TextMeshProUGUI _footer;
    [SerializeField]
    private Button _confirmNoButton;
    [SerializeField]
    private GameObject _confirmCard;
    private void _0x52b49f8a()
    {
        if (this._0x1cc37ff3 != null)
        {
            this._0x1cc37ff3._0x1803d95b();
        }
    }

    private void _0xc98651a8()
    {
        this._0x63147bc8(false, false, true);
    }

    [SerializeField]
    private Button _confirmYesButton;
    [SerializeField]
    private _0x408694e7 _motionToggle;
    [SerializeField]
    private TextMeshProUGUI _confirmBody;
    private void _0x6b49adad()
    {
        this._0x63147bc8(true, false, false);
    }
}

internal static class _0x90df325c
{
    internal static string _0x3031390f(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}