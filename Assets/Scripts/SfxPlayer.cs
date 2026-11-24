// Lecture 09, slide 63. One player for every sound effect.
// Put it on the root of the Audio prefab. Give "Sources" four AudioSources (children are fine)
// whose Output is the mixer's SFX group. Call it from anywhere: SfxPlayer.Instance.Play(clip);
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer Instance { get; private set; }

    [SerializeField] AudioSource[] sources;      // four or so, all with Output set to the mixer's SFX group
    [SerializeField] float pitchRange = 2f;      // +/- semitones
    [SerializeField] float minVolume = 0.85f;
    int next;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);           // a reload doesn't cut off the sound that caused it
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        AudioSource source = sources[next];
        next = (next + 1) % sources.Length;
        source.pitch = Mathf.Pow(2f, Random.Range(-pitchRange, pitchRange) / 12f);
        source.PlayOneShot(clip, volume * Random.Range(minVolume, 1f));
    }
}
