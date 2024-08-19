using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishSign : MonoBehaviour
{
    public ChangeScene sceneChanger;

    [SerializeField]
    Collider2D hitbox;

    [SerializeField]
    LayerMask layer;

    private void Start()
    {
        sceneChanger = GameObject.Find("EventSystem").GetComponent<ChangeScene>();
    }

    private void Update()
    {
        if (hitbox.IsTouchingLayers(layer)) StartCoroutine(Wait(2f));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        sceneChanger.SceneChangeCurtain();
    }
}
