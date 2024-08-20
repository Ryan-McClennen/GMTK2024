using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using  UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    string mixerGroup;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(mixerGroup);
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat(mixerGroup, volume);
        PlayerPrefs.SetFloat(mixerGroup, volume);
    }
}
