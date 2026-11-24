// Lecture 09, slide 39. LISTENER 3: one burst of sparks on every hit.
// The ParticleSystem: Looping off, Play On Awake off, Emission Rate over Time 0 and one Burst of ~15.
using UnityEngine;

public class HitSparks : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] ParticleSystem sparks;

    void Awake()
    {
        if (sparks == null) sparks = GetComponent<ParticleSystem>();
        if (health == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) health = p.GetComponent<Health>();
            if (health == null) health = GetComponentInParent<Health>();
            if (health == null) health = FindAnyObjectByType<Health>();
        }
    }

    void OnEnable()
    {
        if (health == null) return;
        health.Damaged -= Burst;
        health.Damaged += Burst;
    }

    void OnDisable()
    {
        if (health == null) return;
        health.Damaged -= Burst;
    }

    void Burst(int current, int max)
    {
        if (sparks != null) sparks.Play();
    }
}
