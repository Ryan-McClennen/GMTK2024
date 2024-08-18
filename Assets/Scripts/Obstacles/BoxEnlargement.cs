using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class BoxEnlargement : MonoBehaviour
{
    public GameObject onButton;
    public Collider2D onButtonCollider;
    public Collider2D robotCollider;
    [SerializeField]
    private float boxSizeNumber;
    [SerializeField]
    public int boxSizeNumberCap;
    [SerializeField]
    public float logNum;
    [SerializeField]
    public float logMult;

    [SerializeField]
    private float shownBoxScale;
    // Start is called before the first frame update
    void Start()
    {
        onButton = GameObject.Find("On Button");
        onButtonCollider = onButton.GetComponent<Collider2D>();
        robotCollider = GameObject.Find("Robot").GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        float boxScale = boxSizeNumber;
        boxScale = (float)(Math.Log(boxScale, logNum) * logMult) + 1f;

        if (boxScale == 0)
            Debug.Log("Hit zero");
        shownBoxScale = boxScale;


        transform.localScale = new Vector2(boxScale, boxScale);

        if (robotCollider.IsTouching(onButtonCollider))
        {
            boxSizeNumber += (1) * 0.05f;
            boxSizeNumber = Mathf.Clamp(boxSizeNumber, 1f, boxSizeNumberCap);
        }
        else
        {
            boxSizeNumber += (-1) * 0.05f;
            boxSizeNumber = Mathf.Clamp(boxSizeNumber, 1f, boxSizeNumberCap);
        }
    }
}
