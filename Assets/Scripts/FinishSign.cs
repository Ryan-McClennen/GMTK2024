using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FinishSign : MonoBehaviour
{
    public ChangeScene sceneChanger;

    [SerializeField]
    Collider2D hitbox;

    [SerializeField]
    LayerMask layer;

    [SerializeField]
    AudioSource source;

    public bool levelDone = false;

    private void Start()
    {
        sceneChanger = GameObject.Find("EventSystem").GetComponent<ChangeScene>();
    }

    private void Update()
    {
        if (hitbox.IsTouchingLayers(layer) && !levelDone)
        {
            int oldMax = PlayerPrefs.GetInt("MaxLevel");
            string sceneName = SceneManager.GetActiveScene().name;
            string sceneNum = new string(sceneName.Where(c => char.IsDigit(c)).ToArray());
            int newMax = int.Parse(sceneNum) + 1;
            PlayerPrefs.SetInt("MaxLevel", Math.Max(oldMax, newMax));
            PlayerPrefs.Save();
            levelDone = true;
            if (!source.isPlaying) source.Play();
            GameObject.Find("Background").GetComponent<AudioSource>().Stop();
            GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Follow = null;
            StartCoroutine(Wait(3f));
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        sceneChanger.SceneChangeCurtain();
    }

}
