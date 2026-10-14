using UnityEngine;

// A card that fades and scales in when it is raised. Written by hand in Update rather
// than with a tween library because this template ships none - and a hand-driven
// animation cannot outlive its own object, which is the whole point of the tween rule.
public sealed class _0x46d7eff9 : MonoBehaviour
{
    private const float Duration = 0.26f;
    [SerializeField]
    private RectTransform _target;
    private void OnEnable()
    {
        this._0xc97aad27 = 0f;
        this._0xa2c34854 = true;
        this._0xf45dfff7(0f);
    }

    private const float FromScale = 0.9f;
    private void Update()
    {
        if (!this._0xa2c34854)
        {
            return;
        }

        this._0xc97aad27 += Time.unscaledDeltaTime;
        float _0x9538e53d = this._0xc97aad27 / Duration;
        if (_0x9538e53d >= 1f)
        {
            _0x9538e53d = 1f;
            this._0xa2c34854 = false;
        }

        this._0xf45dfff7(_0x9538e53d);
    }

    private void _0xf45dfff7(float _0xa1a0d537)
    {
        // Ease out cubic.
        float _0xe127ccac = 1f - _0xa1a0d537;
        float _0xeb31e668 = 1f - (_0xe127ccac * _0xe127ccac * _0xe127ccac);
        if (this._group != null)
        {
            this._group.alpha = _0xeb31e668;
        }

        if (this._scaleIn && this._target != null)
        {
            float _0xa208556f = FromScale + ((1f - FromScale) * _0xeb31e668);
            this._target.localScale = new Vector3(_0xa208556f, _0xa208556f, 1f);
        }
    }

    private float _0xc97aad27;
    private bool _0xa2c34854;
    [SerializeField]
    private CanvasGroup _group;
    [SerializeField]
    private bool _scaleIn = true;
    private void OnDisable()
    {
        this._0xa2c34854 = false;
    }
}