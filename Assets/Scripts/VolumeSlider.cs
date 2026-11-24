// Lecture 09, slide 65 (usings added). A volume slider for one mixer group.
// The slider runs 0.0001 to 1 (set Min Value and Max Value on it); the mixer wants decibels.
// Put this on the CANVAS (always active), not on a hidden panel: scripts on an inactive object don't start.
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string parameter = "MusicVolume";   // the group's Volume, exposed and renamed
    [SerializeField] Slider slider;                       // Min Value 0.0001, Max Value 1

    void Start()
    {
        slider.value = SaveData.Volume(parameter);
        slider.onValueChanged.AddListener(Set);
        Set(slider.value);
    }

    void Set(float value)
    {
        mixer.SetFloat(parameter, Mathf.Log10(value) * 20f);   // 1 -> 0 dB, 0.5 -> -6 dB, 0.0001 -> -80 dB
        SaveData.SetVolume(parameter, value);
    }
}
