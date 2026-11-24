// MY GLUE. Plays a click through SfxPlayer for EVERY Button under this object (including inactive ones).
// Put it on the root of the HUD prefab and on each menu Canvas, so you don't wire every button.
using UnityEngine;
using UnityEngine.UI;

public class UiClickSounds : MonoBehaviour
{
    [SerializeField] AudioClip click;

    void Start()
    {
        foreach (Button b in GetComponentsInChildren<Button>(true))
            b.onClick.AddListener(Click);
    }

    void Click()
    {
        if (SfxPlayer.Instance != null) SfxPlayer.Instance.Play(click);
    }
}
