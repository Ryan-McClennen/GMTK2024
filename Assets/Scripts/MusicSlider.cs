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
        float volume;
        mixer.GetFloat(mixerGroup, out volume);
        slider.value = volume;
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat(mixerGroup, volume);
    }
}
