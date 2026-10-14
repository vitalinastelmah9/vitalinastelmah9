using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class _0x787a00d9 : MonoBehaviour
{
    private void _0xcf12e0c6()
    {
        EventSystem _0xc3333147 = null;
        EventSystem[] _0x3c7d654c = Resources.FindObjectsOfTypeAll<EventSystem>();
        for (int _0x66693027 = 0; _0x66693027 < _0x3c7d654c.Length; _0x66693027++)
        {
            EventSystem _0x6518bec1 = _0x3c7d654c[_0x66693027];
            if (_0x6518bec1 == null || !_0x6518bec1.isActiveAndEnabled)
                continue;
            if (_0x6518bec1.gameObject.hideFlags != HideFlags.None)
                continue;
            _0xc3333147 = _0x6518bec1;
            break;
        }

        if (_0xc3333147 == null)
        {
            var _0x9f732f47 = new GameObject(_0x9af6a2c6._0xfa2d418b(new byte[14] { 119, 85, 78, 81, 70, 68, 94, 119, 72, 78, 73, 83, 66, 85 }, 39));
            _0x9f732f47.transform.SetParent(transform, false);
            _0x9f732f47.AddComponent<EventSystem>();
            _0x9f732f47.AddComponent<InputSystemUIInputModule>();
            return;
        }

        if (_0xc3333147.GetComponent<InputSystemUIInputModule>() == null)
        {
            StandaloneInputModule _0x214cda81 = _0xc3333147.GetComponent<StandaloneInputModule>();
            if (_0x214cda81 != null)
                Destroy(_0x214cda81);
            _0xc3333147.gameObject.AddComponent<InputSystemUIInputModule>();
        }
    }

    private static int CloseBarPixels()
    {
        float _0xdcfefcd5 = Screen.dpi > 0f ? Screen.dpi : 160f;
        return Mathf.Max(48, Mathf.RoundToInt(48f * _0xdcfefcd5 / 160f));
    }

    private static bool BackReleased()
    {
        Keyboard _0x5d513ec6 = Keyboard.current;
        if (_0x5d513ec6 != null && _0x5d513ec6.escapeKey.wasReleasedThisFrame)
            return true;
#if ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyUp(KeyCode.Escape);
#else
        return false;
#endif
    }

    private void OnApplicationFocus(bool _0xbb7458ce)
    {
        if (_0xbb7458ce && _0x99c6a506)
            _0xfc025de3();
    }

    private void Update()
    {
        if (!_0x99c6a506 || _0x24ed3fd4 == null)
            return;
        if (_0x6e46b554 != Screen.safeArea || _0xce78d521.x != Screen.width || _0xce78d521.y != Screen.height)
        {
            _0xfc025de3();
        }

        if (BackReleased())
            _0xfafc898d();
    }

    public static bool _0x99c6a506 { get; private set; }

    private void _0xcebd0ac9(Rect _0x4ff5853b, int _0x119d0293)
    {
        if (_0x2338ed2d == null || Screen.width <= 0 || Screen.height <= 0)
            return;
        _0x2338ed2d.anchorMin = new Vector2(_0x4ff5853b.xMin / Screen.width, (_0x4ff5853b.yMax - _0x119d0293) / Screen.height);
        _0x2338ed2d.anchorMax = new Vector2(_0x4ff5853b.xMax / Screen.width, _0x4ff5853b.yMax / Screen.height);
        _0x2338ed2d.offsetMin = _0x2338ed2d.offsetMax = Vector2.zero;
    }

    private _0x3c98f17d _0x24ed3fd4;
    private void _0xd40eb861()
    {
        if (_0x9e864925 != null)
            return;
        _0xcf12e0c6();
        _0x9e864925 = new GameObject(_0x9af6a2c6._0xfa2d418b(new byte[13] { 245, 215, 204, 211, 196, 198, 220, 230, 205, 215, 202, 200, 192 }, 165), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        _0x9e864925.transform.SetParent(transform, false);
        Canvas _0x2b0d2c5e = _0x9e864925.GetComponent<Canvas>();
        _0x2b0d2c5e.renderMode = RenderMode.ScreenSpaceOverlay;
        _0x2b0d2c5e.sortingOrder = short.MaxValue;
        _0x2b0d2c5e.overrideSorting = true;
        var _0x777910db = new GameObject(_0x9af6a2c6._0xfa2d418b(new byte[8] { 38, 9, 10, 22, 0, 39, 4, 23 }, 101), typeof(RectTransform), typeof(Image));
        _0x2338ed2d = _0x777910db.GetComponent<RectTransform>();
        _0x2338ed2d.SetParent(_0x9e864925.transform, false);
        _0x777910db.GetComponent<Image>().color = new Color(0.08f, 0.08f, 0.1f, 1f);
        int _0x40186564 = CloseBarPixels();
        _0xcebd0ac9(Screen.safeArea, _0x40186564);
        Sprite _0xd1a53876 = Resources.GetBuiltinResource<Sprite>(_0x9af6a2c6._0xfa2d418b(new byte[20] { 134, 154, 252, 128, 184, 186, 189, 252, 134, 154, 128, 163, 161, 186, 167, 182, 253, 163, 160, 183 }, 211));
        var _0x37e6dd2d = new GameObject(_0x9af6a2c6._0xfa2d418b(new byte[5] { 133, 170, 169, 181, 163 }, 198), typeof(RectTransform), typeof(Image), typeof(Button));
        var _0x661359c7 = _0x37e6dd2d.GetComponent<RectTransform>();
        _0x661359c7.SetParent(_0x2338ed2d, false);
        _0x661359c7.anchorMin = new Vector2(1f, 0f);
        _0x661359c7.anchorMax = new Vector2(1f, 1f);
        _0x661359c7.pivot = new Vector2(1f, 0.5f);
        _0x661359c7.sizeDelta = new Vector2(Mathf.Max(_0x40186564 * 2.4f, 120f), 0f);
        _0x661359c7.anchoredPosition = new Vector2(-12f, 0f);
        Image _0x0f251857 = _0x37e6dd2d.GetComponent<Image>();
        _0x0f251857.sprite = _0xd1a53876;
        _0x0f251857.type = Image.Type.Sliced;
        _0x0f251857.color = new Color(0.95f, 0.96f, 0.98f, 1f);
        _0x37e6dd2d.GetComponent<Button>().onClick.AddListener(_0x003eb5f1);
        var _0xf6abc938 = new GameObject(_0x9af6a2c6._0xfa2d418b(new byte[5] { 145, 188, 191, 184, 177 }, 221), typeof(RectTransform), typeof(Text));
        var _0x9b04e26f = _0xf6abc938.GetComponent<RectTransform>();
        _0x9b04e26f.SetParent(_0x661359c7, false);
        _0x9b04e26f.anchorMin = Vector2.zero;
        _0x9b04e26f.anchorMax = Vector2.one;
        _0x9b04e26f.offsetMin = _0x9b04e26f.offsetMax = Vector2.zero;
        Text _0x7cb9ec9d = _0xf6abc938.GetComponent<Text>();
        _0x7cb9ec9d.text = _0x9af6a2c6._0xfa2d418b(new byte[5] { 255, 208, 211, 207, 217 }, 188);
        _0x7cb9ec9d.font = Resources.GetBuiltinResource<Font>(_0x9af6a2c6._0xfa2d418b(new byte[17] { 2, 43, 41, 47, 45, 55, 28, 59, 32, 58, 39, 35, 43, 96, 58, 58, 40 }, 78));
        _0x7cb9ec9d.fontSize = Mathf.Max(18, _0x40186564 / 3);
        _0x7cb9ec9d.alignment = TextAnchor.MiddleCenter;
        _0x7cb9ec9d.color = new Color(0.12f, 0.12f, 0.15f, 1f);
    }

    private Rect _0x6e46b554;
    private Vector2 _0xce78d521;
    private void OnDestroy()
    {
        _0x003eb5f1();
    }

    private void _0x003eb5f1()
    {
        _0x99c6a506 = false;
        if (_0x24ed3fd4 != null)
        {
            _0x24ed3fd4._0x27b20fda();
            _0x24ed3fd4._0x8ec780f4();
            _0x24ed3fd4 = null;
        }

        if (_0x9e864925 != null)
        {
            Destroy(_0x9e864925);
            _0x9e864925 = null;
            _0x2338ed2d = null;
        }
    }

    public void _0x3caf9aa1(string _0xb6d0d420)
    {
        if (string.IsNullOrWhiteSpace(_0xb6d0d420))
        {
            Debug.LogError(_0x9af6a2c6._0xfa2d418b(new byte[28] { 172, 142, 149, 138, 157, 159, 133, 220, 172, 147, 144, 149, 159, 133, 220, 169, 174, 176, 220, 149, 143, 220, 153, 145, 140, 136, 133, 210 }, 252), this);
            return;
        }

        _0x003eb5f1();
        _0x24ed3fd4 = new _0x3c98f17d();
        if (!_0x24ed3fd4._0x68a7de54)
        {
            _0x24ed3fd4._0x8ec780f4();
            _0x24ed3fd4 = null;
            Application.OpenURL(_0xb6d0d420);
            return;
        }

        _0x24ed3fd4._0x8bde8adf(true);
        _0x24ed3fd4.HandleRequest = (_0x0cf23c69, _0xa7ca370b, _0x968ee756) => true;
        _0xfc025de3();
        _0x24ed3fd4._0x6ddda77f(_0xb6d0d420);
        _0x24ed3fd4._0x43cd50a9();
        _0xd40eb861();
        _0x99c6a506 = true;
        Input.backButtonLeavesApp = false;
    }

    private void _0xfafc898d()
    {
        if (_0x24ed3fd4 == null)
        {
            _0x003eb5f1();
            return;
        }

        if (_0x24ed3fd4._0xeb20615f())
            return;
        if (_0x24ed3fd4._0x7ff00bb4)
        {
            _0x24ed3fd4._0x21bc97d6();
            return;
        }

        _0x003eb5f1();
    }

    private RectTransform _0x2338ed2d;
    private GameObject _0x9e864925;
    private void _0xfc025de3()
    {
        if (_0x24ed3fd4 == null || !_0x24ed3fd4._0x68a7de54 || Screen.width <= 0 || Screen.height <= 0)
            return;
        Rect _0x1312dcdb = Screen.safeArea;
        int _0x593cdcae = CloseBarPixels();
        int _0x117e128b = Mathf.Max(0, Mathf.RoundToInt(_0x1312dcdb.xMin));
        int _0xe2d2131c = Mathf.Max(0, Mathf.RoundToInt(Screen.width - _0x1312dcdb.xMax));
        int _0xe962d7cc = Mathf.Max(0, Mathf.RoundToInt(_0x1312dcdb.yMin));
        int _0x6d19f5ac = Mathf.Max(0, Mathf.RoundToInt(Screen.height - _0x1312dcdb.yMax)) + _0x593cdcae;
        _0x24ed3fd4._0x0c40a331(_0x117e128b, _0x6d19f5ac, _0xe2d2131c, _0xe962d7cc, Screen.width, Screen.height);
        _0xcebd0ac9(_0x1312dcdb, _0x593cdcae);
        _0x6e46b554 = Screen.safeArea;
        _0xce78d521 = new Vector2(Screen.width, Screen.height);
    }
}

internal static class _0x9af6a2c6
{
    internal static string _0xfa2d418b(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}