// MY GLUE (the lecture shows the pattern on slides 39 and 50). LISTENER 1: the HUD bar answers Health's events.
// Put it on the HUD. Health is on the player, which is in the same level scene, so drag it in
// (or find it by tag in Start if the HUD is a prefab: see the guide).
using UnityEngine;

public class HealthBarListener : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] HealthBar bar;

    void OnEnable()
    {
        if (health == null) return;
        health.Damaged += OnChanged;
        health.Healed += OnChanged;
        bar.Set(health.Current, health.Max);
    }

    void OnDisable()
    {
        if (health == null) return;
        health.Damaged -= OnChanged;
        health.Healed -= OnChanged;
    }

    void OnChanged(int current, int max) => bar.Set(current, max);
}
