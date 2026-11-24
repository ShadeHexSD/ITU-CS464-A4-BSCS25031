using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;

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
