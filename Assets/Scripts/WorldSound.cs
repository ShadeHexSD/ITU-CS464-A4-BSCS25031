// Lecture 10, slide 48. A sound that lives in the world. Put it on an object with an AudioSource
// (Loop on, Output = SFX group, a clip assigned). It sets Spatial Blend 1 and the 3D Sound Settings.
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WorldSound : MonoBehaviour
{
    [SerializeField] AudioRolloffMode rolloff = AudioRolloffMode.Logarithmic;
    [SerializeField] float minDistance = 1f;     // full volume inside this
    [SerializeField] float maxDistance = 25f;    // Logarithmic stops fading here; Linear is silent here
    [SerializeField, Range(0f, 5f)] float doppler = 1f;

    void Awake()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.spatialBlend = 1f;                // 0 is 2D, 1 is fully 3D
        source.rolloffMode = rolloff;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.dopplerLevel = doppler;
    }
}
