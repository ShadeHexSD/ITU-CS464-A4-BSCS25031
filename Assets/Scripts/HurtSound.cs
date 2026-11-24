// MY GLUE. LISTENER 2: plays the hit sound when Health is damaged.
using UnityEngine;

public class HurtSound : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] AudioClip hurt;

    void OnEnable() => health.Damaged += OnHit;
    void OnDisable() => health.Damaged -= OnHit;

    void OnHit(int current, int max) => SfxPlayer.Instance.Play(hurt);
}
