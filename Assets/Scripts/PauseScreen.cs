using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    private FinishSign finish;
    private ChangeScene sceneChanger;
    private Button pauseButton;
    [SerializeField]
    private Button backToMenuButton;
    [SerializeField]
    private RectTransform backToMenuTransform;
    [SerializeField]
    private Sprite backButtonSprite;
    [SerializeField]
    private Sprite pauseButtonSprite;
    [SerializeField]
    private Button restartButton;
    private bool timeStopped = false;
    private bool restartClicked = false;
    private float buttonScale = Screen.width * Screen.height / 400000f;

    [SerializeField]
    private RectTransform pauseTransform;
    [SerializeField]
    private RectTransform restartTransform;
    //make sure restart button doesnt glitch

    // Start is called before the first frame update
    void Start()
    {
        finish = GameObject.Find("LevelFinish").GetComponent<FinishSign>();
        sceneChanger = GameObject.Find("EventSystem").GetComponent<ChangeScene>();
        pauseButton = GameObject.Find("Pause Button").GetComponent<Button>();
        //restartTransform.localPosition = Vector3.zero;
        //restartTransform.localScale = new Vector2(buttonScale, buttonScale);
        //pauseTransform.localScale = new Vector2(buttonScale / 2f, buttonScale / 2f);
        //pauseTransform.localPosition = new Vector3((-Screen.width / 2) + pauseTransform.localScale.x * 30, (Screen.height / 2) - pauseTransform.localScale.y * 30, 0);
        backToMenuButton.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //restartTransform.localScale = new Vector2(buttonScale, buttonScale);
        //pauseTransform.localScale = new Vector2(buttonScale / 2f, buttonScale / 2f);
        //pauseTransform.localPosition = new Vector3((-Screen.width / 2) + pauseTransform.localScale.x * 30, (Screen.height / 2) - pauseTransform.localScale.y * 30, 0);

        //backToMenuTransform.localScale = new Vector2(buttonScale / 1.4f, buttonScale / 1.4f);
        //backToMenuTransform.localPosition = new Vector3(0, (-Screen.height / 2) + pauseTransform.localScale.y * 55, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!finish.levelDone && !sceneChanger.changingScenes && !sceneChanger.pause && !sceneChanger.changingOut)
            {
                sceneChanger.curtain.localPosition = sceneChanger.startPoint;
                sceneChanger.pause = true;
                sceneChanger.pauseMenuOpen = true;
                pauseButton.image.sprite = backButtonSprite;
            }
            else if (sceneChanger.pause)
            {
                Time.timeScale = 1f;
                sceneChanger.pause = false;
                pauseButton.image.sprite = pauseButtonSprite;
                timeStopped = false;
            }
        }

        if(Vector3.Distance(sceneChanger.curtain.localPosition, Vector3.zero) < 0.001f && sceneChanger.pause && !timeStopped)
        {
            stopTime();
        }

        if (finish.levelDone == true || sceneChanger.changingScenes || restartClicked)
        {
            restartButton.enabled = false;
        }
    }

    private void stopTime()
    {
        timeStopped = true;
        Time.timeScale = 0f;
    }

    public void PauseClicked()
    {
        if (!finish.levelDone && !sceneChanger.changingScenes && !sceneChanger.pause && !sceneChanger.changingOut)
        {
            sceneChanger.curtain.localPosition = sceneChanger.startPoint;
            sceneChanger.pause = true;
            sceneChanger.pauseMenuOpen = true;
            pauseButton.image.sprite = backButtonSprite;
        }
        else if (sceneChanger.pause)
        {
            Time.timeScale = 1f;
            sceneChanger.pause = false;
            pauseButton.image.sprite = pauseButtonSprite;
            timeStopped = false;
        }
    }

    public void MenuClicked()
    {
        backToMenuButton.enabled = false;
        restartButton.enabled = false;
        StartCoroutine(MenuLoad());

    }

    public void LevelRestart()
    {
        restartButton.enabled = false;
        backToMenuButton.enabled = false;
        restartClicked = true;
        StartCoroutine(sceneRestart());
    }
    public IEnumerator sceneRestart()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        sceneChanger.pause = false;
        sceneChanger.pauseMenuOpen = false;
        pauseButton.image.sprite = pauseButtonSprite;
        //StartCoroutine(Wait(4f));

    }

    IEnumerator MenuLoad()
    {
        String currentSceneName = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu Screen");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //sceneChanger.pause = false;
        //sceneChanger.pauseMenuOpen = false;
        //pauseButton.image.sprite = pauseButtonSprite;
        //StartCoroutine(Wait(4f));

    }

    public IEnumerator Wait(float timeAmount)
    {
        yield return new WaitForSeconds(timeAmount);
    }
}
