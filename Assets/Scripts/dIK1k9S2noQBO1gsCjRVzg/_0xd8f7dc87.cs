using UnityEngine;

// The lobby's hall badge rocks a few degrees and back. It must ROCK, never spin: a
// continuous rotation on a screen the capture is waiting to settle on reads as "the
// screen never stopped changing" and the walkthrough comes back with one frame.
// REDUCED MOTION switches it off entirely.
public sealed class _0xd8f7dc87 : MonoBehaviour
{
    private void Update()
    {
        if (!this._0x2e31b407 || this._target == null)
        {
            return;
        }

        this._0xb81f422d += Time.deltaTime;
        if (this._0xb81f422d > Period)
        {
            this._0xb81f422d -= Period;
        }

        float _0x87513415 = Mathf.Sin((this._0xb81f422d / Period) * Mathf.PI * 2f) * Amplitude;
        this._target.localRotation = Quaternion.Euler(0f, 0f, _0x87513415);
    }

    private const float Amplitude = 3f;
    private float _0xb81f422d;
    [SerializeField]
    private RectTransform _target;
    private bool _0x2e31b407 = true;
    private void OnDisable()
    {
        if (this._target != null)
        {
            this._target.localRotation = Quaternion.identity;
        }
    }

    private const float Period = 2.6f;
    public void _0x7b4ea113(bool _0x1a693cfe)
    {
        this._0x2e31b407 = _0x1a693cfe;
        if (!_0x1a693cfe && this._target != null)
        {
            this._target.localRotation = Quaternion.identity;
        }
    }
}