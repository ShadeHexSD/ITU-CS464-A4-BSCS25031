// Lecture 09, slide 39. LISTENER 3: one burst of sparks on every hit.
// The ParticleSystem: Looping off, Play On Awake off, Emission Rate over Time 0 and one Burst of ~15.
using UnityEngine;

public class HitSparks : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] ParticleSystem sparks;

    void OnEnable() => health.Damaged += Burst;
    void OnDisable() => health.Damaged -= Burst;

    void Burst(int current, int max) => sparks.Play();
}
