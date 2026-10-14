using UnityEngine;

public class _0x0075d85d : MonoBehaviour
{
    public void _0xb545c6cc(GameObject _0x43be674e, GameObject _0x5c05d5c2)
    {
        _0x43be674e.SetActive(true);
        _0x5c05d5c2.SetActive(false);
    }

    public GameObject panel_menu;
    public void _0x5e05ff7d(GameObject _0x0d01ca6d)
    {
        _0x0d01ca6d.SetActive(true);
    }

    public void _0x0edc3233(GameObject _0x63ab2990)
    {
        _0x63ab2990.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    public GameObject panel_loader;
    public static _0x0075d85d instance;
}