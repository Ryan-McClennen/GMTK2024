using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadMenuAndLevelSelect : MonoBehaviour
{
    private string sceneName;

    public void OnClick(string scene)
    {
        sceneName = scene;
        StartCoroutine(LoadLevel(sceneName));
    }

    public void PlayFurthestLevel()
    {
        int furthest = Math.Min(PlayerPrefs.GetInt("MaxLevel"), 12);
        StartCoroutine(LoadLevel("Level " + furthest));
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
