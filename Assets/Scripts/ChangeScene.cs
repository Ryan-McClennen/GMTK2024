using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    public RectTransform curtain;
    public bool changingScenes = false;
    public bool changingOut = false;
    public bool pause = false;
    [SerializeField]
    private bool loadingIn;
    private bool curtainLoadIn;
    public bool pauseMenuOpen = false;
    public RectTransform canvasTransform;
    public Vector3 startPoint;
    private Vector3 endPoint;
    [SerializeField]
    public String nextScene;
    private FinishSign finishLine;

    //private Collider2D finishLine;

    private void Start()
    {
        curtain.localPosition = Vector3.zero;
        Time.timeScale = 1f;
        //curtain = GameObject.Find("Curtain").GetComponent<RectTransform>();
        canvasTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        finishLine = GameObject.Find("LevelFinish").GetComponent<FinishSign>();
        curtain.sizeDelta = canvasTransform.sizeDelta;

        startPoint = new Vector3(canvasTransform.rect.width * (-1), 0f, 0f);
        endPoint = new Vector3(canvasTransform.rect.width, 0f, 0f);

        if (loadingIn)
        {
            curtainLoadIn = true;
            changingOut = true;
            changingScenes = false;
        }
        //finishLine = GameObject.Find("LevelFinish").GetComponent<Collider2D>();
    }
    public void SceneChangeCurtain()
    {
        curtain.localPosition = startPoint;
        changingScenes = true;
        changingOut = false;
    }

    private void Update()
    {

        curtain.sizeDelta = canvasTransform.sizeDelta;

        startPoint = new Vector3(canvasTransform.rect.width * (-1), 0f, 0f);
        endPoint = new Vector3(canvasTransform.rect.width, 0f, 0f);

        if (changingScenes)
        {
            Vector3 moveDir = (curtain.localPosition) * (-1);
            float moveSpeed = 10f;
            curtain.localPosition += moveDir * moveSpeed * Time.unscaledDeltaTime;
        }

        if (pauseMenuOpen && !changingScenes)
        {
            if (pause)
            {
                Vector3 moveDir = (curtain.localPosition) * (-1);
                float moveSpeed = 24f;
                curtain.localPosition += moveDir * moveSpeed * Time.unscaledDeltaTime;
            }
            else
            {
                Vector3 moveDir = startPoint - curtain.localPosition;
                float moveSpeed = 24f;
                curtain.localPosition += moveDir * moveSpeed * Time.unscaledDeltaTime;
            }
        }

        if (Vector3.Distance(curtain.localPosition, endPoint) < 0.001f)
        {
            pauseMenuOpen = false;
            changingOut = false;
        }

        if (Vector3.Distance(curtain.localPosition, Vector3.zero) < 0.001f && changingScenes && !pauseMenuOpen)
        {
                changingScenesOut();
        }

        if (changingOut)
        {
            if (loadingIn && curtainLoadIn)
            {
                StartCoroutine(LoadInWait());
            }
            else if (!loadingIn && !finishLine.levelDone)
            {
                Vector3 moveDir = endPoint - curtain.localPosition;
                float moveSpeed = 10f;
                curtain.localPosition += moveDir * moveSpeed * Time.unscaledDeltaTime;
            }
        }
    }

    private void changingScenesOut()
    {
        StartCoroutine(sceneSwitch(nextScene));
        StartCoroutine(Wait(2.5f));
        changingScenes = false;
    }

    IEnumerator sceneSwitch(String sceneToLoad)
    {
        curtain.localPosition = Vector3.zero;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        changingOut = true;
    }

    IEnumerator Wait(float time)
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(time);
    }
    IEnumerator LoadInWait()
    {
        curtainLoadIn = false;
        Debug.Log("Waiting");
        yield return new WaitForSeconds(0.34f);
        loadingIn = false;
    }

}
