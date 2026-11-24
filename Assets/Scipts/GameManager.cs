using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Lecture 07's single manager, expanded for Assignment 03 & Assignment 04's HUD & EndScreens.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Per-level settings")]
    [SerializeField] int targetScore = 10;
    [SerializeField] int lives = 3;
    [SerializeField] float respawnDelay = 0.6f;
    [SerializeField] float spawnProtection = 1.5f;

    [SerializeField] Health player;
    [SerializeField] Transform respawnPoint;

    [Header("TMP and panels")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text livesText;
    [SerializeField] TMP_Text healthText; // Optional: otherwise hearts appear in livesText.
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    LivesText hudLivesText;
    EndScreens endScreens;

    int score;
    bool gameEnded;
    bool respawning;

    public bool CanCollect => !gameEnded && !respawning;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Use exactly one GameManager in each scene.", this);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Time.timeScale = 1f;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Start()
    {
        if (player == null) player = FindAnyObjectByType<Health>();
        hudLivesText = FindAnyObjectByType<LivesText>();
        endScreens = FindAnyObjectByType<EndScreens>();

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        UpdateScoreUI();
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (gameEnded || respawning) return;
        score += amount;
        UpdateScoreUI();
        if (score >= targetScore) Win();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score} / {targetScore}";
    }

    // Called by Health when hearts change and by this manager when lives change.
    public void UpdateUI()
    {
        if (hudLivesText == null) hudLivesText = FindAnyObjectByType<LivesText>();
        if (hudLivesText != null)
        {
            hudLivesText.Show(Mathf.Max(lives, 0));
        }

        if (livesText != null)
        {
            livesText.text = $"Lives: {Mathf.Max(lives, 0)}";
            if (healthText == null && player != null)
                livesText.text += $"   Hearts: {Mathf.Max(0, player.Current)} / {player.Max}";
        }
        if (healthText != null && player != null)
            healthText.text = $"Hearts: {Mathf.Max(0, player.Current)} / {player.Max}";
    }

    public void PlayerDied() => LoseLife();
    public void PlayerFell() => LoseLife();

    void LoseLife()
    {
        if (gameEnded || respawning) return;
        respawning = true;
        lives--;
        UpdateUI();

        PlayerController controller = player != null ? player.GetComponent<PlayerController>() : null;
        if (controller != null) controller.StopMovement(respawnDelay);

        if (lives <= 0)
        {
            Lose();
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        if (player != null && respawnPoint != null)
        {
            Rigidbody body = player.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.position = respawnPoint.position;
                body.rotation = respawnPoint.rotation;
                body.linearVelocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
            }
            else player.transform.position = respawnPoint.position;

            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller != null) controller.StopMovement();
            player.Restore(spawnProtection);
        }
        respawning = false;
    }

    public void SetCheckpoint(Transform point)
    {
        if (point != null) respawnPoint = point;
    }

    void Win()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (endScreens == null) endScreens = FindAnyObjectByType<EndScreens>();
        if (endScreens != null)
        {
            int level = gameObject.scene.buildIndex;
            float seconds = Time.timeSinceLevelLoad;
            bool best = SaveData.Record(level, seconds);
            SaveData.Unlock(level + 1);
            endScreens.ShowWin(best, seconds);
        }
        else if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Lose()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (endScreens == null) endScreens = FindAnyObjectByType<EndScreens>();
        if (endScreens != null)
        {
            endScreens.ShowGameOver();
        }
        else if (losePanel != null)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart() => RestartLevel();
}
