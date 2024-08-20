using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CreatePrefs : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;

    void Start()
    {
        if (!PlayerPrefs.HasKey("MaxLevel"))
            PlayerPrefs.SetInt("MaxLevel", 1);
        if (!PlayerPrefs.HasKey("Master"))
            PlayerPrefs.SetFloat("Master", 0f);
        if (!PlayerPrefs.HasKey("BGM"))
            PlayerPrefs.SetFloat("BGM", -20f);
        if (!PlayerPrefs.HasKey("SFX"))
            PlayerPrefs.SetFloat("SFX", -20f);

        PlayerPrefs.Save();

        mixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));
        mixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
        mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));
    }
}
