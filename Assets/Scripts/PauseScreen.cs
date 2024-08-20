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
    private Sprite backButtonSprite;
    [SerializeField]
    private Sprite pauseButtonSprite;
    // Start is called before the first frame update
    void Start()
    {
        finish = GameObject.Find("LevelFinish").GetComponent<FinishSign>();
        sceneChanger = GameObject.Find("EventSystem").GetComponent<ChangeScene>();
        pauseButton = GameObject.Find("Pause Button").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!finish.levelDone && !sceneChanger.changingScenes && !sceneChanger.pause)
            {
                sceneChanger.curtain.localPosition = sceneChanger.startPoint;
                sceneChanger.pause = true;
                sceneChanger.pauseMenuOpen = true;
                pauseButton.image.sprite = backButtonSprite;
            }
            else if (sceneChanger.pause)
            {
                sceneChanger.pause = false;
                pauseButton.image.sprite = pauseButtonSprite;
            }
        }
    }

    public void PauseClicked()
    {
        if (!finish.levelDone && !sceneChanger.changingScenes && !sceneChanger.pause)
        {
            sceneChanger.curtain.localPosition = sceneChanger.startPoint;
            sceneChanger.pause = true;
            sceneChanger.pauseMenuOpen = true;
            pauseButton.image.sprite = backButtonSprite;
        }
        else if (sceneChanger.pause)
        {
            sceneChanger.pause = false;
            pauseButton.image.sprite = pauseButtonSprite;
        }
    }

    public void LevelRestart()
    {
        StartCoroutine(sceneRestart());
    }
    IEnumerator sceneRestart()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        sceneChanger.pause = false;
        pauseButton.image.sprite = pauseButtonSprite;
    }
}
