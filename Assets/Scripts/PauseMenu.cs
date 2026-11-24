// Lecture 08, slide 75, with the assignment's loader (page 2). Pause on Esc; Resume, Restart, Quit to menu.
// Resume() comes before EVERY load: Time.timeScale, AudioListener.pause and the static IsPaused all survive a load.
// Put it on the HUD. The player's Update must start with:  if (PauseMenu.IsPaused || EndScreens.Over) return;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject firstButton;     // the Resume button
    [SerializeField] LevelLoader loader;
    public static bool IsPaused { get; private set; }

    void Awake()
    {
        IsPaused = false;                        // a static survives a load: start every scene unpaused
        UiEventSystem.Ensure();                  // levels 1/2 had no EventSystem
    }

    void Update()
    {
        if (EndScreens.Over) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        UiEventSystem.Select(firstButton);
    }

    public void Resume()
    {
        IsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void Restart()
    {
        Resume();
        if (loader != null) loader.Reload();
    }

    public void QuitToMenu()
    {
        Resume();
        if (loader != null) loader.LoadMenu();
    }
}
