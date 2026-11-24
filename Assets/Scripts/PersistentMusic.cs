// Lecture 10, slide 52. Put it on the root object that holds the looping music AudioSource
// (Loop on, Output = Music group). Place that object in EVERY scene: the first one stays,
// every later copy destroys itself, so there is never a second music player.
using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    static PersistentMusic instance;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
