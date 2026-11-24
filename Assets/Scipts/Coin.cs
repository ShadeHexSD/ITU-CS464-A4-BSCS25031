using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] GameObject sparklePrefab;

    bool collected;

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        collected = true;

        // Lecture event for ScoreListener & HUD
        GameEvents.RaiseCoinCollected(value);

        // Lecture 09 requirement: play pickup through SfxPlayer (not on destroyed object)
        if (SfxPlayer.Instance != null && pickupSound != null)
        {
            SfxPlayer.Instance.Play(pickupSound);
        }

        // Particle effect (Commit 7)
        if (sparklePrefab != null)
        {
            Instantiate(sparklePrefab, transform.position, Quaternion.identity);
        }

        // Keep backwards compatibility with GameManager if present
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(value);
        }
        else if (GameManager_03.Instance != null)
        {
            GameManager_03.Instance.AddScore(value);
        }

        Destroy(gameObject);
    }
}
