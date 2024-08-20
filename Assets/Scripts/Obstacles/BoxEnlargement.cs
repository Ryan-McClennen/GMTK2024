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

    [SerializeField]
    public SpriteRenderer[] boxes;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip inflating;

    [SerializeField]
    AudioClip deflating;

    private float massMultiplier =  0.8f;

    public bool isGrowing;
    public bool isShrinking;

    private void FixedUpdate()
    {
        float boxScale = MathF.Atan(boxSizeNumber - 4.5f) * 3.4f + 5.25f;

        transform.localScale = new Vector2(boxScale, boxScale);

        float goal = 4f;

        if (isGrowing && !isShrinking)
        {
            goal = 10f;
        }
        if (isShrinking  && !isGrowing)
        {
            goal = 1f;
        }

        if (boxSizeNumber + 0.05 < goal && (!source.isPlaying || source.clip == deflating))
        {
            source.clip = inflating;
            source.Play();
        }
        else if (boxSizeNumber - 0.05 > goal && (!source.isPlaying || source.clip == inflating))
        {
            source.clip = deflating;
            source.Play();
        }

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

        
        if ((isGrowing == isShrinking && Math.Abs(boxSizeNumber - 4f) < 0.05) ||
            (isGrowing && boxScale > 9.9f)  || 
            (isShrinking && boxScale <  1.1f))
            source.Stop();

        float smallTrans = 0;
        float medTrans = 0;
        float largeTrans = 0;
        
        if (boxScale < 1.5)
        {
            smallTrans = 1;
        }
        else if (boxScale < 2.5)
        {
            smallTrans = Mathf.Pow(2.5f - boxScale, 0.5f);
            medTrans = Mathf.Pow(boxScale - 1.5f, 0.5f);
        }
        else if (boxScale <  5.5)
        {
            medTrans = 1;
        }
        else if (boxScale < 6.5)
        {
            medTrans = Mathf.Pow(6.5f - boxScale, 0.5f);
            largeTrans = Mathf.Pow(boxScale - 5.5f, 0.5f);
        }
        else
        {
            largeTrans = 1;
        }

        boxes[0].color = new Color(1f, 1f, 1f, smallTrans);
        boxes[1].color = new Color(1f, 1f, 1f, medTrans);
        boxes[2].color = new Color(1f, 1f, 1f, largeTrans);

        rb.mass = transform.localScale.x * transform.localScale.y * massMultiplier;
    }
}
