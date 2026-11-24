// MY GLUE. The lives label on the HUD (the assignment wants the score AND the lives as TMP_Text).
using TMPro;
using UnityEngine;

public class LivesText : MonoBehaviour
{
    [SerializeField] TMP_Text label;

    public void Show(int lives) => label.SetText("Lives: {0}", lives);
}
