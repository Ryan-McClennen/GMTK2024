using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    private float horizontal;
    private float vertical;

    public int balloonNumber = -1;
    public int floatinessScale = 0;
    

    [SerializeField]
    private Rigidbody2D rb;

    // object used for checking if player is on ground
    [SerializeField]
    private Transform groundCheck;

    // layer type to identify which objects are ground
    [SerializeField]
    private LayerMask groundLayer;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && balloonNumber < 5)
        {
            Debug.Log("Up: Balloon Level = " + (balloonNumber + 1));
            Inflate();
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && balloonNumber > -1)
        {
            Debug.Log("Down: Balloon Level = " + (balloonNumber - 1));
            Deflate();
        }
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (!IsGrounded())
        {
            rb.AddForce((horizontal * speed * Vector2.right) + (floatinessScale * 0.18f * Vector2.up));
        }
        else
        {
            if(balloonNumber < 1)
            {
                rb.gravityScale = 3.2f;
            }
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            rb.AddForce(floatinessScale * 0.2f * Vector2.up);
        }
    }
    // helloo
    private bool IsGrounded()
    {
        // spawns circle to see if it overlaps with ground objects under player.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Inflate()
    {
        balloonNumber++;
        ChangeFloatiness();
    }

    private void Deflate()
    {
        balloonNumber--;
        ChangeFloatiness();
    }

    private void ChangeFloatiness()
    {
        if (balloonNumber == -1)
        {
            floatinessScale = 0;
        }
        else if (balloonNumber == 0)
        {
            floatinessScale = 55;
        }
        else if(balloonNumber == 1)
        {
            floatinessScale = 70;
            rb.gravityScale = 1.2f;
        }
        else if (balloonNumber == 2)
        {
            floatinessScale = 75;
        }
        else if (balloonNumber == 3)
        {
            floatinessScale = 85;
        }
        else if (balloonNumber == 4)
        {
            floatinessScale = 100;
        }
        else if (balloonNumber == 5)
        {
            floatinessScale = 110;
        }
    }
}
