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
