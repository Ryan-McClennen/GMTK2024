using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMove : MonoBehaviour
{
    [Header ("Player speed")]
    public int speed = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(0, 0, 0);
        moveVector += Input.GetAxisRaw("Horizontal") * Vector3.right;
        moveVector += Input.GetAxisRaw("Vertical") * Vector3.up;

        transform.position +=  moveVector.normalized * speed / 60;
    }
}
