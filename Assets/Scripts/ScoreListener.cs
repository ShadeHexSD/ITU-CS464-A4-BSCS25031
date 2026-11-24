// MY GLUE. An extra listener: keeps the score and shows it. Subscribes to a STATIC event, so it
// MUST unsubscribe in OnDisable. If your game already has a score system, call ScoreText.Show from it instead.
using UnityEngine;

public class ScoreListener : MonoBehaviour
{
    [SerializeField] ScoreText scoreText;
    [SerializeField] int target = 10;
    int score;

    void OnEnable()
    {
        // The HUD prefab is shared by every level, so its serialized target of 10 is wrong
        // for levels with 6/7 coins. Read the real total for this scene before showing it.
        int detected = LevelScoreGoal.TotalInActiveScene();
        if (detected > 0) target = detected;

        GameEvents.CoinCollected += OnCoin;
        scoreText.Show(score, target);
    }

    void OnDisable() => GameEvents.CoinCollected -= OnCoin;

    void OnCoin(int value)
    {
        score += value;
        scoreText.Show(score, target);
    }
}
