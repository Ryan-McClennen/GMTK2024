using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public RectTransform curtain;
    public bool changingScenes = false;
    public bool changingOut = false;
    public bool pause = false;
    [SerializeField]
    private bool loadingIn;
    public bool pauseMenuOpen = false;
    private RectTransform canvasTransform;
    public Vector3 startPoint;
    private Vector3 endPoint;
    [SerializeField]
    public String nextScene;

    //private Collider2D finishLine;

    private void Start()
    {
        curtain = GameObject.Find("Curtain").GetComponent<RectTransform>();
        canvasTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        curtain.sizeDelta = canvasTransform.sizeDelta;

        startPoint = new Vector3(canvasTransform.rect.width * (-1), 0f, 0f);
        endPoint = new Vector3(canvasTransform.rect.width, 0f, 0f);

        if (loadingIn)
        {
            changingOut = true;
            changingScenes = false;
            StartCoroutine(Wait(2.5f));
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
        if (changingScenes)
        {
            Vector3 moveDir = (curtain.localPosition) * (-1);
            float moveSpeed = 10f;
            curtain.localPosition += moveDir * moveSpeed * Time.deltaTime;
        }

        if (pauseMenuOpen)
        {
            if (pause)
            {
                Vector3 moveDir = (curtain.localPosition) * (-1);
                float moveSpeed = 10f;
                curtain.localPosition += moveDir * moveSpeed * Time.deltaTime;
            }
            else
            {
                Vector3 moveDir = startPoint - curtain.localPosition;
                float moveSpeed = 7f;
                curtain.localPosition += moveDir * moveSpeed * Time.deltaTime;
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
            Vector3 moveDir = endPoint - curtain.localPosition;
            float moveSpeed = 7f;
            curtain.localPosition += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    private void changingScenesOut()
    {
        StartCoroutine(sceneSwitch(nextScene));
        //StartCoroutine(Wait(2.5f));
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

}
