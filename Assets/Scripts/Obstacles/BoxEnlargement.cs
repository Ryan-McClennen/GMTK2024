using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class BoxEnlargement : MonoBehaviour
{
    [SerializeField]
    private float boxSizeNumber;
    [SerializeField]
    public int boxSizeNumberCap;
    [SerializeField]
    public float logNum;
    [SerializeField]
    public float logMult;

    public bool isGrowing;
    public bool isShrinking;

    private void FixedUpdate()
    {
        float boxScale = boxSizeNumber;
        if (boxScale > 4)
            boxScale = (float)(Math.Log(boxScale - 2.4f, logNum) * logMult) + 2.4f;
        else
            boxScale = (float)Math.Pow(1.7, boxScale - 1.7f) + 1f;

        transform.localScale = new Vector2(boxScale, boxScale);

        float goal = 4f;

        if (isGrowing && !isShrinking) goal = 10f;
        if (isShrinking  && !isGrowing) goal = 1f;

        if (!Mathf.Approximately(boxSizeNumber, goal))
        {
            if (boxSizeNumber < goal)
            {
                boxSizeNumber += 0.05f;
                boxSizeNumber = Mathf.Clamp(boxSizeNumber, 1f, boxSizeNumberCap);
            }
            if (boxSizeNumber > goal)
            {
                boxSizeNumber += -0.05f;
                boxSizeNumber = Mathf.Clamp(boxSizeNumber, 1f, boxSizeNumberCap);
            }
        }
    }
}
