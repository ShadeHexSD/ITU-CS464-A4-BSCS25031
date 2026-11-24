// Lecture 10 flow: the last scene in the play order returns to the menu.
// Put it on the Credits canvas. The button can call ReturnToMenu directly, or be
// assigned to menuButton so this script wires it at runtime. autoAdvanceSeconds = 0
// means "wait for the button".
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] LevelLoader loader;
    [SerializeField] Button menuButton;
    [SerializeField] float autoAdvanceSeconds = 0f;

    bool returning;

    void Start()
    {
        if (loader == null) loader = FindAnyObjectByType<LevelLoader>();
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(ReturnToMenu);
            UiEventSystem.Select(menuButton.gameObject);
        }
    }

    void Update()
    {
        if (!returning && autoAdvanceSeconds > 0f && Time.unscaledTime >= autoAdvanceSeconds)
            ReturnToMenu();
    }

    public void ReturnToMenu()
    {
        if (returning) return;
        returning = true;
        if (loader != null) loader.LoadMenu();
        else Debug.LogError("[A04] No LevelLoader found in Credits; cannot return to menu.", this);
    }
}
