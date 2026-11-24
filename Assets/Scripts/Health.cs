// Lecture 09, slide 66 (usings added). Health only announces what happened; it knows nothing about
// the bar, the sound or the sparks. Put it on the player.
using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int max = 3;
    [SerializeField] float iFrames = 1f;
    public bool shielded;
    public event Action<int, int> Damaged;
    public event Action<int, int> Healed;
    public event Action Died;
    public UnityEvent onDied;
    int current;
    float invulnerableUntil;

    public int Current => current;
    public int Max => max;

    void Awake() => current = max;

    public bool TakeDamage(int amount)
    {
        if (amount <= 0 || current <= 0 || Time.time < invulnerableUntil) return false;
        invulnerableUntil = Time.time + iFrames;
        if (shielded)
        {
            shielded = false;
            return false;
        }
        current = Mathf.Max(0, current - amount);
        Damaged?.Invoke(current, max);
        GameEvents.RaisePlayerDamaged(current, max);

        // Announce death once, whatever manager happens to be in the scene.
        if (current <= 0)
        {
            Died?.Invoke();
            onDied?.Invoke();
        }

        // Notify GameManager to deduct life & handle respawn
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateUI();
            if (current <= 0) GameManager.Instance.PlayerDied();
        }
        else if (GameManager_03.Instance != null)
        {
            GameManager_03.Instance.UpdateUI();
            if (current <= 0) GameManager_03.Instance.PlayerDied();
        }
        else if (current <= 0)
        {
            // If standalone without GameManager, onDied triggers Game Over
            EndScreens endScreens = FindAnyObjectByType<EndScreens>();
            if (endScreens != null) endScreens.ShowGameOver();
        }

        return true;
    }

    public void Restore(float protection)
    {
        current = max;
        shielded = false;
        invulnerableUntil = Time.time + protection;
        Healed?.Invoke(current, max);

        if (GameManager.Instance != null) GameManager.Instance.UpdateUI();
        else if (GameManager_03.Instance != null) GameManager_03.Instance.UpdateUI();
    }
}
