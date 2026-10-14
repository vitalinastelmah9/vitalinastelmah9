using UnityEngine;

// The one palette for both scenes. Every text colour here is LIGHT on purpose: all TMP
// labels share a single material whose outline is BaseDeep, so a dark face would merge
// into its own outline and read as a blot whatever sits behind it (rule C.14).
// Never pass Base / BaseDeep / Panel / PanelLift / Locked into anything that becomes a
// text colour.
public static class _0xd7ad5941
{
    private const int TintCount = 3;
    public static readonly Color PanelLift = new Color(0.141176f, 0.109804f, 0.239216f, 1f);
    public static readonly Color Accent = new Color(0.949020f, 0.788235f, 0.298039f, 1f);
    public static readonly Color Locked = new Color(0.290196f, 0.254902f, 0.376471f, 1f);
    /// <summary>High contrast mode lifts every panel fill to full opacity.</summary>
    public static Color PanelFill(bool _0x7f316b60)
    {
        return _0x7f316b60 ? Panel : WithAlpha(Panel, 0.88f);
    }

    public static readonly Color Secondary = new Color(0.180392f, 0.768627f, 0.713725f, 1f);
    public static readonly Color Base = new Color(0.090196f, 0.070588f, 0.145098f, 1f);
    public static readonly Color AccentDeep = new Color(0.788235f, 0.592157f, 0.184314f, 1f);
    /// <summary>One of the three accent hues, by slot.</summary>
     // A switch, not a static Color[] initializer: Plana shuffles declarations, and a
    // field initializer that READS sibling statics would silently hold three
    // transparent blacks if it were moved above them. A method body runs after the
    // cctor, so declaration order cannot affect it.
    public static Color Tint(int _0xa3acaa9a)
    {
        switch (((_0xa3acaa9a % TintCount) + TintCount) % TintCount)
        {
            case 0:
                return Accent;
            case 1:
                return Secondary;
            default:
                return Primary;
        }
    }

    // Text colours. These four, and nothing else, may become a label colour.
    public static readonly Color Text = new Color(0.972549f, 0.960784f, 1.000000f, 1f);
    public static readonly Color Primary = new Color(0.482353f, 0.380392f, 1.000000f, 1f);
    public static readonly Color PanelGlow = new Color(0.227451f, 0.176471f, 0.388235f, 1f);
    public static readonly Color Panel = new Color(0.117647f, 0.090196f, 0.200000f, 1f);
    public static readonly Color BaseDeep = new Color(0.054902f, 0.039216f, 0.094118f, 1f);
    public static Color PanelEdge(bool _0x7a5eef13)
    {
        return _0x7a5eef13 ? Accent : WithAlpha(Primary, 0.55f);
    }

    public static readonly Color TextSoft = new Color(0.788235f, 0.749020f, 0.894118f, 1f);
    public static Color WithAlpha(Color _0xabaa6f8e, float _0xa40aaa21)
    {
        return new Color(_0xabaa6f8e.r, _0xabaa6f8e.g, _0xabaa6f8e.b, _0xa40aaa21);
    }
}