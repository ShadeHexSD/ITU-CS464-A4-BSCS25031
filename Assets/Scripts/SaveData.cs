// Lecture 10, slide 54. Everything the game remembers between sessions. The level keys are spelled HERE ONLY.
using UnityEngine;

public static class SaveData
{
    const string UnlockedKey = "unlocked";
    static string BestKey(int level) => "best" + level;

    // Level 1 is always open: the default for a key that was never set.
    public static int Unlocked => PlayerPrefs.GetInt(UnlockedKey, 1);

    public static void Unlock(int level)
    {
        if (level <= Unlocked) return;
        PlayerPrefs.SetInt(UnlockedKey, level);
        PlayerPrefs.Save();
    }

    public static bool HasBest(int level) => PlayerPrefs.HasKey(BestKey(level));
    public static float BestTime(int level) => PlayerPrefs.GetFloat(BestKey(level), 0f);

    // True if this time beat the saved one (or there wasn't one).
    public static bool Record(int level, float seconds)
    {
        if (HasBest(level) && seconds >= BestTime(level)) return false;
        PlayerPrefs.SetFloat(BestKey(level), seconds);
        PlayerPrefs.Save();
        return true;
    }
}
