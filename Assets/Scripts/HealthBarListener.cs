// Lecture 08 & 09: LISTENER 1: the HUD bar answers Health's events.
// Put it on the HUD. If Health isn't assigned in Inspector, it automatically finds the player in the scene.
using UnityEngine;

public class HealthBarListener : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] HealthBar bar;

    void Awake()
    {
        if (bar == null) bar = GetComponent<HealthBar>();
    }

    void Start()
    {
        // Auto-find player in the scene if not explicitly assigned
        if (health == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) health = player.GetComponent<Health>();
            if (health == null) health = FindAnyObjectByType<Health>();

            // Subscribe once found and initialize bar
            if (health != null)
            {
                health.Damaged -= OnChanged;
                health.Healed -= OnChanged;
                health.Damaged += OnChanged;
                health.Healed += OnChanged;
                if (bar != null) bar.Set(health.Current, health.Max);
            }
        }
    }

    void OnEnable()
    {
        if (health == null) return;
        health.Damaged -= OnChanged;
        health.Healed -= OnChanged;
        health.Damaged += OnChanged;
        health.Healed += OnChanged;
        if (bar != null) bar.Set(health.Current, health.Max);
    }

    void OnDisable()
    {
        if (health == null) return;
        health.Damaged -= OnChanged;
        health.Healed -= OnChanged;
    }

    void OnChanged(int current, int max)
    {
        if (bar != null) bar.Set(current, max);
    }
}
