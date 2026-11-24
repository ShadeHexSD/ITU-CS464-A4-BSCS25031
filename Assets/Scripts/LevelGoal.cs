// Lecture 10, slide 55: LevelGoal records time, unlocks next level, and triggers EndScreens.ShowWin
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] EndScreens endScreens;
    bool done;

    void Start()
    {
        if (endScreens == null) endScreens = FindAnyObjectByType<EndScreens>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (done || !other.CompareTag("Player")) return;
        done = true;
        int level = gameObject.scene.buildIndex;
        float seconds = Time.timeSinceLevelLoad;
        bool best = SaveData.Record(level, seconds);
        SaveData.Unlock(level + 1);

        if (endScreens == null) endScreens = FindAnyObjectByType<EndScreens>();
        if (endScreens != null)
        {
            endScreens.ShowWin(best, seconds);
        }
    }
}
