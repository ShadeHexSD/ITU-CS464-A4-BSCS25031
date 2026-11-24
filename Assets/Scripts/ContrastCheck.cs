// MY HELPER. Add to any object, set the two colours, then use the component's context menu (the three dots) > Log contrast.
using UnityEngine;

public class ContrastCheck : MonoBehaviour
{
    [SerializeField] Color text = Color.white;
    [SerializeField] Color background = Color.black;

    [ContextMenu("Log contrast")]
    void Log() => Debug.Log($"Contrast {ColourUtil.Contrast(text, background):0.00} : 1  (text 4.5, large text and icons 3)");
}
