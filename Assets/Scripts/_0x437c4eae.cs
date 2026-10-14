using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class _0x437c4eae : MonoBehaviour
{
    private void _0x1d520ec5()
    {
        _0x7b380ad0();
    }

    [SerializeField]
    private _0x0075d85d _panelsFlow;
    //ui
    [SerializeField]
    private TextMeshProUGUI text_ui_main_score;
    private void Update()
    {
        _0x1d520ec5();
    }

    private void _0xafda9077()
    {
        button_ui_main_privacy_policy.onClick.AddListener(() => _controllerPrivacyPolicy._0x3caf9aa1(privacyPolicy));
    }

    private void _0x7b380ad0()
    {
        text_ui_main_score.text = game_score.ToString();
    }

    private void Awake()
    {
        _panelsFlow = _0x0075d85d.instance;
    }

    [SerializeField]
    private Button button_ui_main_privacy_policy;
    private void Start()
    {
        _0xafda9077();
    }

    [SerializeField]
    private _0x787a00d9 _controllerPrivacyPolicy;
    //gameplay
    [SerializeField]
    private int game_score = 0;
    [SerializeField]
    private string privacyPolicy = "";
    [SerializeField]
    private Button button_ui_main_play;
}