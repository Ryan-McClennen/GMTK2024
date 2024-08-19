using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    private RectTransform curtain;
    private bool changingScenes = false;
    private bool changingOut = false;
    private RectTransform canvasTransform;
    private Vector3 startPoint;
    private Vector3 endPoint;

    [SerializeField]
    private float distanceStart;
    [SerializeField]
    private float distanceMiddle;
    private void Start()
    {
        curtain = GameObject.Find("Curtain").GetComponent<RectTransform>();
        canvasTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        startPoint = new Vector3(-1113.5f, 0f, 0f);
        endPoint = new Vector3(1113.5f, 0f, 0f);
    }
    public void SceneChangeCurtain()
    {
        changingScenes = true;
    }

    private void Update()
    {
        if (changingScenes)
        {
            Vector3 moveDir = (curtain.localPosition) * (-1);
            float moveSpeed = 10f;
            curtain.localPosition += moveDir * moveSpeed * Time.deltaTime;
        }

        if (Vector3.Distance(curtain.localPosition, Vector3.zero) < 0.001f)
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
        StartCoroutine(Wait(2.5f));
        curtain.localPosition = Vector3.zero;
        changingOut = true;
        changingScenes = false;
    }

    IEnumerator Wait(float time)
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(time);
    }

}
