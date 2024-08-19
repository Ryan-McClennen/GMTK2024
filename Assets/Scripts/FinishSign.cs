using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishSign : MonoBehaviour
{
    public ChangeScene sceneChanger;

    private void Start()
    {
        sceneChanger = GameObject.Find("EventSystem").GetComponent<ChangeScene>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("YOU WIN!");
            StartCoroutine(Wait(2f));
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        sceneChanger.SceneChangeCurtain();
    }
}
