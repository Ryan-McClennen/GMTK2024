using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private string sceneName;
    public int maxLevelUnlocked;
    [SerializeField]
    public Button[] levels;


    private void Start()
    {
        foreach(var button in levels)
        {
            button.enabled = false;
        }

        for (int i = 0; i < LevelTracker.unlockedLevelMax; i++)
        {
            levels[i].enabled = true;
        }
    }

    public void clicked1()
    {
        sceneName = "Level 1";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked2()
    {
        sceneName = "Level 2";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked3()
    {
        sceneName = "Level 3";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked4()
    {
        sceneName = "Level 4";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked5()
    {
        sceneName = "Level 5";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked6()
    {
        sceneName = "Level 6";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked7()
    {
        sceneName = "Level 7";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked8()
    {
        sceneName = "Level 8";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked9()
    {
        sceneName = "Level 9";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked10()
    {
        sceneName = "Level 10";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked11()
    {
        sceneName = "Level 11";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clicked12()
    {
        sceneName = "Level 12";
        StartCoroutine(LoadLevel(sceneName));
    }

    public void clickedMain()
    {
        sceneName = "Main Menu Screen";
        StartCoroutine(LoadLevel(sceneName));
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
