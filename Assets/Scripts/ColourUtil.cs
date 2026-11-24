// MY HELPER: the WCAG 2 contrast ratio from Lecture 08 slide 17, (Llight + 0.05) / (Ldark + 0.05),
// where L is the relative luminance from linear RGB. Use it to MEASURE every HUD text pair (>= 4.5).
using UnityEngine;

public static class ColourUtil
{
    static float Linear(float c) => c <= 0.04045f ? c / 12.92f : Mathf.Pow((c + 0.055f) / 1.055f, 2.4f);

    public static float Luminance(Color c) => 0.2126f * Linear(c.r) + 0.7152f * Linear(c.g) + 0.0722f * Linear(c.b);

    public static float Contrast(Color a, Color b)
    {
        float la = Luminance(a), lb = Luminance(b);
        float light = Mathf.Max(la, lb), dark = Mathf.Min(la, lb);
        return (light + 0.05f) / (dark + 0.05f);
    }
}
