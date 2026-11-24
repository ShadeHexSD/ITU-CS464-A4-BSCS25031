// Lecture 10, slides 31-35 and 51 (usings and null checks added). The ONLY script that loads a scene.
// One per scene (put it in the Loader prefab with a loading panel and a Slider, Min 0, Max 1).
// Scene List: MainMenu 0, Level1..Level5 = 1..5, Credits 6.
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider progressBar;
    [SerializeField] int lastInPlayOrder = 6;    // the credits
    [SerializeField] float minDisplayTime = 0.35f; // ensure loading bar reaches 100% and is visibly animated

    public void LoadNext()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next > lastInPlayOrder) next = 0;    // after Credits, back to the menu
        Load(next);
    }

    public void Reload() => Load(SceneManager.GetActiveScene().buildIndex);
    public void LoadMenu() => Load(0);

    public void Load(int buildIndex)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadAsync(buildIndex));
    }

    IEnumerator LoadAsync(int buildIndex)
    {
        if (loadingPanel != null) loadingPanel.SetActive(true);
        if (progressBar != null) progressBar.value = 0f;

        AsyncOperation op = SceneManager.LoadSceneAsync(buildIndex);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (op.progress < 0.9f || timer < minDisplayTime)
        {
            timer += Time.unscaledDeltaTime;
            // The assignment's formula: op.progress / 0.9f
            float targetProgress = Mathf.Clamp01(op.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, targetProgress, 3f * Time.unscaledDeltaTime);
            }
            yield return null;
        }

        // Fill bar to 100% as required by assignment ("a bar that reaches 100%")
        if (progressBar != null)
        {
            while (progressBar.value < 1f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, 4f * Time.unscaledDeltaTime);
                yield return null;
            }
        }

        yield return new WaitForSecondsRealtime(0.08f);
        op.allowSceneActivation = true;
    }
}
