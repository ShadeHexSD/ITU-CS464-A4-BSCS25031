// Lecture 08, slide 77 (Quit only). Put it on the main menu; the Quit button calls Quit().
// All scene loading goes through LevelLoader, so there is no Play() here.
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
