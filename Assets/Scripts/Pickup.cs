// MY GLUE, from Lecture 09 slide 15. A pickup: a sound through SfxPlayer (so it is not cut off with the object),
// a particle effect that destroys itself (Stop Action = Destroy on the prefab's ParticleSystem), then
// the pickup goes. Tag the player "Player". Is Trigger on.
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] AudioClip chime;
    [SerializeField] GameObject sparklePrefab;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameEvents.RaiseCoinCollected(value);
        if (SfxPlayer.Instance != null) SfxPlayer.Instance.Play(chime);
        if (sparklePrefab != null) Instantiate(sparklePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
