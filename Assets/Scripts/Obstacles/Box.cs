using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    private float massMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb.mass = transform.localScale.x * transform.localScale.y * massMultiplier;
    }
}
