// MY GLUE. LISTENER 2: plays the hit sound when Health is damaged.
// Put on player or HUD. Auto-finds player Health if unassigned.
using UnityEngine;

public class HurtSound : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] AudioClip hurt;

    void Awake()
    {
        if (health == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) health = p.GetComponent<Health>();
            if (health == null) health = GetComponent<Health>();
            if (health == null) health = FindAnyObjectByType<Health>();
        }
    }

    void OnEnable()
    {
        if (health == null) return;
        health.Damaged -= OnHit;
        health.Damaged += OnHit;
    }

    void OnDisable()
    {
        if (health == null) return;
        health.Damaged -= OnHit;
    }

    void OnHit(int current, int max)
    {
        if (SfxPlayer.Instance != null && hurt != null)
        {
            SfxPlayer.Instance.Play(hurt);
        }
    }
}
