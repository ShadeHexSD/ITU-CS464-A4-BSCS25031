// MY GLUE. Put it on every menu panel: when the panel turns on, its first button is selected, so the
// arrow keys and Enter work at once. (The EventSystem's First Selected only covers a menu that is
// there when the scene starts.)
using UnityEngine;

public class SelectOnEnable : MonoBehaviour
{
    [SerializeField] GameObject firstButton;

    void OnEnable() => UiEventSystem.Select(firstButton);
}
