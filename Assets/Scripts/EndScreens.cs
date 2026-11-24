// MY GLUE. The win screen and the game-over screen. Put it on the HUD.
// Win: Next level (loader.LoadNext) and Main menu. Game over: Restart (loader.Reload) and Main menu.
// Wire Health.onDied -> EndScreens.ShowGameOver in the Inspector. Both panels start inactive.
using TMPro;
using UnityEngine;

public class EndScreens : MonoBehaviour
{
    public static bool Over { get; private set; }   // the player's Update checks this

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject winFirstButton;     // Next level
    [SerializeField] GameObject overFirstButton;    // Restart
    [SerializeField] TMP_Text winTimeText;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip loseClip;
    [SerializeField] LevelLoader loader;

    void Awake()
    {
        Over = false;                               // a static survives a load
        UiEventSystem.Ensure();                     // levels 1/2 had no EventSystem
    }

    public void ShowWin(bool newBest, float seconds)
    {
        if (Over) return;
        Over = true;
        Time.timeScale = 0f;
        if (winTimeText != null)
            winTimeText.SetText(newBest ? "New best: {0:1} s" : "{0:1} s", seconds);
        if (winPanel != null) winPanel.SetActive(true);
        UiEventSystem.Select(winFirstButton);
        if (SfxPlayer.Instance != null) SfxPlayer.Instance.Play(winClip);
    }

    public void ShowGameOver()
    {
        if (Over) return;
        Over = true;
        Time.timeScale = 0f;
        if (overPanel != null) overPanel.SetActive(true);
        UiEventSystem.Select(overFirstButton);
        if (SfxPlayer.Instance != null) SfxPlayer.Instance.Play(loseClip);
    }

    // Buttons: Next level, Restart and Main menu call these (or the loader directly).
    public void NextLevel() { if (loader != null) loader.LoadNext(); }
    public void Restart() { if (loader != null) loader.Reload(); }
    public void MainMenu() { if (loader != null) loader.LoadMenu(); }
}
