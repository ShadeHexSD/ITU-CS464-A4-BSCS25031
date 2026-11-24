// Lecture 08 & 09: LISTENER 1: the HUD bar answers Health's events.
// Put it on the HUD. The HUD prefab cannot store a scene player reference, so find it in Awake;
// subscription then happens in OnEnable and is removed in OnDisable, like every other listener.
using UnityEngine;

public class HealthBarListener : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] HealthBar bar;

    void Awake()
    {
        if (bar == null) bar = GetComponent<HealthBar>();

        if (health == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) health = player.GetComponent<Health>();
            if (health == null) health = FindAnyObjectByType<Health>();
        }

        if (bar != null && health != null) bar.Set(health.Current, health.Max);
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
