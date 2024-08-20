using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip select;

    [SerializeField]
    AudioClip enter;

    public void OnHover()
    {
        source.clip = select;
        source.Play();
    }

    public void OnPress()
    {
        source.clip = enter;
        source.Play();
    }
}
