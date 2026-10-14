using System;
using UnityEngine;

public class _0x3c98f17d
{
    private void _0x1ae5983a(string _0x6a043948)
    {
        _0x079ed5db(PageError, _0x6a043948 ?? string.Empty);
    }

    private bool _0xa2fc06fb(string _0xcbfccd61)
    {
        if (_0x26a090b3 == null || _0x0cf8139a)
            return false;
        try
        {
            return _0x26a090b3.Call<bool>(_0xcbfccd61);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[8] { 58, 8, 15, 59, 4, 8, 26, 77 }, 109) + _0xcbfccd61 + _0xa5e33a86._0x202635da(new byte[2] { 216, 194 }, 226) + exception.Message);
            return false;
        }
    }

    public void _0xe6053e13(string _0x08d58bce)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[4] { 0, 19, 4, 9 }, 101), _0x08d58bce ?? string.Empty);
    }

    private AndroidJavaObject _0x26a090b3;
    private _0x57711cb4 _0x07c00477;
    public bool _0x68a7de54
    {
        get
        {
            return _0x26a090b3 != null && !_0x0cf8139a;
        }
    }

    private void _0x3d482b6a(string _0xfadcafb3)
    {
        _0x079ed5db(PopupClosed, _0xfadcafb3 ?? string.Empty);
    }

    public Func<string, bool, bool, bool> HandleRequest;
    public event Action<string> PageStarted;
    private void _0x046ac412(string _0x84bc8c36)
    {
        _0x079ed5db(PageStarted, _0x84bc8c36 ?? string.Empty);
    }

    private bool _0x0cf8139a;
    public void _0x1746242e(string _0x257a09e1, string _0x3e4851b0)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[17] { 84, 66, 83, 119, 72, 87, 82, 87, 114, 84, 66, 85, 102, 64, 66, 73, 83 }, 39), _0x257a09e1 ?? string.Empty, _0x3e4851b0 ?? string.Empty);
    }

    public void _0x27b20fda()
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[4] { 215, 214, 219, 218 }, 191));
    }

    private void _0xa54126bf(string _0xdac40d73)
    {
        _0x079ed5db(PopupOpened, _0xdac40d73 ?? string.Empty);
    }

    public event Action<string> PageError;
    public void _0x3ce8bd7e()
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[4] { 129, 134, 157, 130 }, 242));
    }

    private string _0x5058de3a(string _0x8f640fbe)
    {
        if (_0x26a090b3 == null || _0x0cf8139a)
            return string.Empty;
        try
        {
            string _0x4c520c67 = _0x26a090b3.Call<string>(_0x8f640fbe);
            return _0x4c520c67 ?? string.Empty;
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[8] { 167, 149, 146, 166, 153, 149, 135, 208 }, 240) + _0x8f640fbe + _0xa5e33a86._0x202635da(new byte[2] { 73, 83 }, 115) + exception.Message);
            return string.Empty;
        }
    }

    public string _0xdf6e5f80()
    {
        return _0x5058de3a(_0xa5e33a86._0x202635da(new byte[7] { 14, 13, 15, 7, 57, 30, 0 }, 108));
    }

    private void _0x94b218d9(string _0x9366f7f4, string _0x199547d1)
    {
        _0x079ed5db(PopupPageFinished, _0x9366f7f4 ?? string.Empty, _0x199547d1 ?? string.Empty);
    }

    private void _0x84a6a5fe(string _0x3659b273, params object[] _0x75b734a7)
    {
        if (_0x26a090b3 == null || _0x0cf8139a)
            return;
        try
        {
            if (_0x75b734a7 == null || _0x75b734a7.Length == 0)
                _0x26a090b3.Call(_0x3659b273);
            else
                _0x26a090b3.Call(_0x3659b273, _0x75b734a7);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[8] { 77, 127, 120, 76, 115, 127, 109, 58 }, 26) + _0x3659b273 + _0xa5e33a86._0x202635da(new byte[2] { 50, 40 }, 8) + exception.Message);
        }
    }

    private void _0x079ed5db<T1, T2>(Action<T1, T2> _0x107074fb, T1 _0xf4b49b4e, T2 _0xd52f767e)
    {
        if (_0x0cf8139a || _0x107074fb == null)
            return;
        try
        {
            _0x107074fb(_0xf4b49b4e, _0xd52f767e);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[18] { 65, 115, 116, 64, 127, 115, 97, 54, 117, 119, 122, 122, 116, 119, 117, 125, 44, 54 }, 22) + exception.Message);
        }
    }

    private sealed class _0x57711cb4 : AndroidJavaProxy
    {
        private readonly _0x3c98f17d _0x39899e37;
        public _0x57711cb4(_0x3c98f17d _0x0bfb098e) : base(_0xa5e33a86._0x202635da(new byte[34] { 241, 252, 255, 231, 224, 189, 210, 248, 171, 248, 254, 251, 170, 171, 240, 255, 242, 254, 250, 253, 227, 233, 252, 227, 227, 223, 250, 253, 248, 219, 252, 252, 248, 224 }, 147))
        {
            this._0x39899e37 = _0x0bfb098e;
        }

        [UnityEngine.Scripting.Preserve]
        public int _0x4786f550(string _0xfa8ab798, int _0x0a064bc4, int _0x2f5147a3)
        {
            _0x3c98f17d _0xe6d23a99 = _0x39899e37;
            if (_0xe6d23a99 == null || _0xe6d23a99._0x0cf8139a || _0xe6d23a99.HandleRequest == null)
                return 1;
            try
            {
                bool _0xb1c0ea22 = _0xe6d23a99.HandleRequest(_0xfa8ab798 ?? string.Empty, _0x0a064bc4 != 0, _0x2f5147a3 != 0);
                return _0xb1c0ea22 ? 1 : 0;
            }
            catch (Exception exception)
            {
                Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[16] { 129, 179, 180, 128, 191, 179, 161, 246, 190, 183, 184, 178, 186, 179, 236, 246 }, 214) + exception.Message);
                return 1;
            }
        }

        [UnityEngine.Scripting.Preserve]
        public void _0xc05f3b32(string _0x5ed99d51)
        {
            _0x39899e37._0x046ac412(_0x5ed99d51);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0xad6f31ff(string _0xa14c9501)
        {
            _0x39899e37._0x8dda9557(_0xa14c9501);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0x6eddc4ac(int _0xc56f6997)
        {
            _0x39899e37._0x4814137e(_0xc56f6997);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0x6409ee95(int _0xa0fb003a, string _0x66c690f0)
        {
            _0x39899e37._0x283b8575(_0xa0fb003a, _0x66c690f0);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0xe4c74a2a(string _0x170fd1d3)
        {
            _0x39899e37._0x1ae5983a(_0x170fd1d3);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0xd3d5f956(string _0xc63a7c8f)
        {
            _0x39899e37._0xa54126bf(_0xc63a7c8f);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0xda53bbaa(string _0x7d33ae5f)
        {
            _0x39899e37._0x3d482b6a(_0x7d33ae5f);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0x232fc3bd(string _0x8f5712ef, string _0x9aad542c)
        {
            _0x39899e37._0x7399d865(_0x8f5712ef, _0x9aad542c);
        }

        [UnityEngine.Scripting.Preserve]
        public void _0x2d8afc68(string _0xbe1e42e5, string _0x9145fe2f)
        {
            _0x39899e37._0x94b218d9(_0xbe1e42e5, _0x9145fe2f);
        }

        [UnityEngine.Scripting.Preserve]
        public int _0xad39e037(string _0xbf675a96)
        {
            _0x3c98f17d _0x376ea951 = _0x39899e37;
            if (_0x376ea951 == null || _0x376ea951._0x0cf8139a || _0x376ea951.AllowCapture == null)
                return 1;
            try
            {
                return _0x376ea951.AllowCapture(_0xbf675a96 ?? string.Empty);
            }
            catch (Exception exception)
            {
                Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[17] { 108, 94, 89, 109, 82, 94, 76, 27, 88, 90, 75, 79, 78, 73, 94, 1, 27 }, 59) + exception.Message);
                return 0;
            }
        }
    }

    public void _0xd91f352f(string _0x685ed794)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[12] { 224, 246, 231, 198, 224, 246, 225, 210, 244, 246, 253, 231 }, 147), _0x685ed794 ?? string.Empty);
    }

    public bool _0x7ff00bb4
    {
        get
        {
            return _0xa2fc06fb(_0xa5e33a86._0x202635da(new byte[9] { 153, 155, 148, 189, 149, 184, 155, 153, 145 }, 250));
        }
    }

    public void _0x6ddda77f(string _0x53ab2644)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[4] { 182, 181, 187, 190 }, 218), _0x53ab2644 ?? string.Empty);
    }

    public event Action<string> PopupOpened;
    public void _0x34696317(string _0x66a2ae0e)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[10] { 41, 38, 37, 57, 47, 26, 37, 58, 63, 58 }, 74), _0x66a2ae0e ?? string.Empty);
    }

    public event Action<int, string> PageFinished;
    public bool _0xeb20615f()
    {
        return _0xa2fc06fb(_0xa5e33a86._0x202635da(new byte[13] { 208, 223, 220, 192, 214, 231, 220, 195, 227, 220, 195, 198, 195 }, 179));
    }

    public event Action<string> PopupClosed;
    private void _0x283b8575(int _0x99abc195, string _0x322a49ff)
    {
        _0x079ed5db(PageFinished, _0x99abc195, _0x322a49ff ?? string.Empty);
    }

    public event Action<string, string> PopupPageStarted;
    public void _0x43cd50a9()
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[4] { 146, 137, 142, 150 }, 225));
    }

    public void _0x8ec780f4()
    {
        if (_0x0cf8139a)
            return;
        _0x0cf8139a = true;
        if (_0x26a090b3 == null)
            return;
        try
        {
            _0x26a090b3.Call(_0xa5e33a86._0x202635da(new byte[7] { 165, 164, 178, 181, 179, 174, 184 }, 193));
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[17] { 171, 153, 158, 170, 149, 153, 139, 220, 152, 153, 143, 136, 142, 147, 133, 198, 220 }, 252) + exception.Message);
        }

        _0x26a090b3.Dispose();
        _0x26a090b3 = null;
    }

    public Func<string, int> AllowCapture;
    public string _0x2fdd67ec()
    {
        return _0x5058de3a(_0xa5e33a86._0x202635da(new byte[12] { 92, 94, 79, 110, 72, 94, 73, 122, 92, 94, 85, 79 }, 59));
    }

    public void _0x1d05276b(bool _0x77d8ae3d)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[13] { 203, 196, 195, 196, 222, 197, 238, 204, 221, 217, 216, 223, 200 }, 173), _0x77d8ae3d ? 1 : 0);
    }

    public bool _0x5d44bfb7(string _0x20f4bbab)
    {
        if (_0x26a090b3 == null || _0x0cf8139a || string.IsNullOrEmpty(_0x20f4bbab))
            return false;
        try
        {
            return _0x26a090b3.Call<bool>(_0xa5e33a86._0x202635da(new byte[10] { 216, 199, 216, 221, 216, 233, 196, 193, 222, 205 }, 168), _0x20f4bbab);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[20] { 217, 235, 236, 216, 231, 235, 249, 174, 254, 225, 254, 251, 254, 207, 226, 231, 248, 235, 180, 174 }, 142) + exception.Message);
            return false;
        }
    }

    private void _0x4814137e(int _0x7e56e9f2)
    {
        _0x079ed5db(ProgressChanged, Mathf.Clamp(_0x7e56e9f2, 0, 100) / 100f);
    }

    public void _0xa6fceb00()
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[9] { 226, 242, 227, 254, 253, 253, 197, 254, 225 }, 145));
    }

    public event Action<float> ProgressChanged;
    private void _0x3d6e7cb7()
    {
        _0x07c00477 = new _0x57711cb4(this);
        using (var _0xcb3f4ac4 = new AndroidJavaClass(_0xa5e33a86._0x202635da(new byte[30] { 159, 147, 145, 210, 137, 146, 149, 136, 133, 207, 152, 210, 140, 144, 157, 133, 153, 142, 210, 169, 146, 149, 136, 133, 172, 144, 157, 133, 153, 142 }, 252)))
        using (var _0x2693f2c4 = _0xcb3f4ac4.GetStatic<AndroidJavaObject>(_0xa5e33a86._0x202635da(new byte[15] { 81, 71, 64, 64, 87, 92, 70, 115, 81, 70, 91, 68, 91, 70, 75 }, 50)))
        {
            if (_0x2693f2c4 == null)
                throw new InvalidOperationException(_0xa5e33a86._0x202635da(new byte[23] { 149, 131, 132, 132, 147, 152, 130, 183, 149, 130, 159, 128, 159, 130, 143, 214, 159, 133, 214, 152, 131, 154, 154 }, 246));
            _0x26a090b3 = new AndroidJavaObject(_0xa5e33a86._0x202635da(new byte[14] { 115, 126, 125, 101, 98, 63, 89, 116, 125, 97, 65, 112, 127, 116 }, 17), _0x2693f2c4, _0x07c00477);
        }
    }

    private void _0x8dda9557(string _0x9b292ea3)
    {
        _0x079ed5db(PageCommitted, _0x9b292ea3 ?? string.Empty);
    }

    public _0x3c98f17d()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            _0x3d6e7cb7();
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[23] { 128, 178, 181, 129, 190, 178, 160, 247, 182, 163, 163, 182, 180, 191, 247, 177, 182, 190, 187, 178, 179, 237, 247 }, 215) + exception.Message);
        }
#endif
    }

    public void _0x21bc97d6()
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[6] { 111, 103, 74, 105, 107, 99 }, 8));
    }

    public void _0x380fe754(bool _0x682d8e94)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[9] { 6, 16, 1, 37, 20, 0, 6, 16, 17 }, 117), _0x682d8e94);
    }

    private void _0x079ed5db<T>(Action<T> _0xdaa36dea, T _0x22acb6cf)
    {
        if (_0x0cf8139a || _0xdaa36dea == null)
            return;
        try
        {
            _0xdaa36dea(_0x22acb6cf);
        }
        catch (Exception exception)
        {
            Debug.LogWarning(_0xa5e33a86._0x202635da(new byte[18] { 192, 242, 245, 193, 254, 242, 224, 183, 244, 246, 251, 251, 245, 246, 244, 252, 173, 183 }, 151) + exception.Message);
        }
    }

    public event Action<string, string> PopupPageFinished;
    public void _0x78187aac(string _0xa9bc2207, string _0xfeaac575)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[9] { 187, 168, 191, 178, 142, 177, 174, 171, 174 }, 222), _0xa9bc2207 ?? string.Empty, _0xfeaac575 ?? string.Empty);
    }

    private void _0x7399d865(string _0x97195831, string _0xc7aff6f9)
    {
        _0x079ed5db(PopupPageStarted, _0x97195831 ?? string.Empty, _0xc7aff6f9 ?? string.Empty);
    }

    public void _0x8bde8adf(bool _0x989701ce)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[20] { 84, 66, 83, 115, 79, 78, 85, 67, 119, 70, 85, 83, 94, 100, 72, 72, 76, 78, 66, 84 }, 39), _0x989701ce);
    }

    public void _0x0c40a331(int _0x5f85b67c, int _0x0152a8d3, int _0xb753cf09, int _0x6eff9ce8, int _0xcfdab106, int _0xf11d6201)
    {
        _0x84a6a5fe(_0xa5e33a86._0x202635da(new byte[10] { 199, 209, 192, 249, 213, 198, 211, 221, 218, 199 }, 180), _0x5f85b67c, _0x0152a8d3, _0xb753cf09, _0x6eff9ce8, _0xcfdab106, _0xf11d6201);
    }

    public event Action<string> PageCommitted;
}

internal static class _0xa5e33a86
{
    internal static string _0x202635da(byte[] data, byte key)
    {
        var buffer = new byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            buffer[i] = (byte)(data[i] ^ key);
        return System.Text.Encoding.UTF8.GetString(buffer);
    }
}