// Lecture 09, slide 63. One player for every sound effect.
// Singleton that persists across scenes with DontDestroyOnLoad.
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    private static SfxPlayer instance;
    public static SfxPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SfxPlayer>();
                if (instance == null)
                {
                    // Auto-load from Resources or create dynamically if not placed in scene
                    GameObject prefab = Resources.Load<GameObject>("SfxPlayer");
                    if (prefab != null)
                    {
                        GameObject go = Instantiate(prefab);
                        instance = go.GetComponent<SfxPlayer>();
                    }
                    else
                    {
                        GameObject go = new GameObject("SfxPlayer");
                        instance = go.AddComponent<SfxPlayer>();
                        instance.InitSources();
                    }
                }
            }
            return instance;
        }
    }

    [SerializeField] AudioSource[] sources;      // four AudioSources
    [SerializeField] float pitchRange = 2f;      // +/- semitones
    [SerializeField] float minVolume = 0.85f;
    int next;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        InitSources();
    }

    void InitSources()
    {
        if (sources == null || sources.Length == 0)
        {
            sources = new AudioSource[4];
            for (int i = 0; i < 4; i++)
            {
                GameObject child = new GameObject($"Voice_{i + 1}");
                child.transform.SetParent(transform, false);
                AudioSource src = child.AddComponent<AudioSource>();
                src.playOnAwake = false;
                src.spatialBlend = 0f; // 2D sound
                sources[i] = src;
            }
        }
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        InitSources();
        if (sources == null || sources.Length == 0) return;

        AudioSource source = sources[next];
        next = (next + 1) % sources.Length;
        if (source != null)
        {
            source.pitch = Mathf.Pow(2f, Random.Range(-pitchRange, pitchRange) / 12f);
            source.PlayOneShot(clip, volume * Random.Range(minVolume, 1f));
        }
    }
}
