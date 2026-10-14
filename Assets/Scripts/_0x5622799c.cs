using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class _0x5622799c : MonoBehaviour
{
    private void _0x3817b648()
    {
        _0xe6d5b681();
        _0x1a713d39();
    }

    private void _0x88364ac8()
    {
        _panelsFlow._0xb545c6cc(_panelsFlow.panel_menu, _panelsFlow.panel_loader);
    }

    private bool _0x0931e8d4 = false;
    [SerializeField]
    private Slider slider_loading;
    private void _0x1a713d39()
    {
        if (!_0x0931e8d4)
        {
            float _0xc4fa4db9 = Time.time - _0x6dcf243c;
            if (_0xc4fa4db9 >= slider_wait_time)
            {
                _0x88364ac8();
                _0x0931e8d4 = true;
            }
        }
    }

    private void Start()
    {
        _0x66f5788f();
        _0x6dcf243c = Time.time;
    }

    private void _0x66f5788f()
    {
    // any UI setup can go here if needed
    }

    private void _0xe6d5b681()
    {
        float _0xbbe8c5d7 = Time.time - _0x6dcf243c;
        float _0x98b17969 = Mathf.Clamp01(_0xbbe8c5d7 / slider_wait_time);
        slider_loading.value = _0x98b17969 * 100;
    }

    private float _0x6dcf243c;
    private void Awake()
    {
        _panelsFlow = _0x0075d85d.instance;
    }

    [SerializeField]
    private int slider_wait_time = 30;
    private void Update()
    {
        _0x3817b648();
    }

    [SerializeField]
    private _0x0075d85d _panelsFlow;
}