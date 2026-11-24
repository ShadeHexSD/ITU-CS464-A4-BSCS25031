// Lecture 10, slide 55, changed the way the assignment's page 4 says: record the time, unlock the next
// level, then show the WIN SCREEN (the Next level button calls loader.LoadNext()).
// Level numbers match build indices: MainMenu is 0, Level 1 is 1. Put it on the goal trigger (Is Trigger on).
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] EndScreens endScreens;
    bool done;

    void OnTriggerEnter(Collider other)
    {
        if (done || !other.CompareTag("Player")) return;
        done = true;
        int level = gameObject.scene.buildIndex;
        float seconds = Time.timeSinceLevelLoad;
        bool best = SaveData.Record(level, seconds);
        SaveData.Unlock(level + 1);
        endScreens.ShowWin(best, seconds);
    }
}
