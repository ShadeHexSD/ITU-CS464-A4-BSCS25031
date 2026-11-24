// MY HELPER (not from a lecture). Optional. Applies the saved volumes when a scene starts, so a level
// opened on its own (or after the menu) is not at the mixer's default. Put it on the Audio prefab root.
using UnityEngine;
using UnityEngine.Audio;

public class MixerApply : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string[] parameters = { "MusicVolume", "SfxVolume" };

    void Start()
    {
        foreach (string p in parameters)
        {
            float v = SaveData.Volume(p);
            mixer.SetFloat(p, Mathf.Log10(v) * 20f);
        }
    }
}
