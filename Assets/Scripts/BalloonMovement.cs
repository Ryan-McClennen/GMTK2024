using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    private float horizontal;
    [SerializeField]
    private float jumpingPow = 16f;

    private int balloonNumber = 0;


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
        horizontal = Input.GetAxisRaw("Horizontal");
        // jump
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPow);
        }

        if (Input.GetKeyDown(KeyCode.Q) && balloonNumber < 10)
        {
            Inflate();
        }

        if (Input.GetKeyDown(KeyCode.E) && balloonNumber > 0)
        {
            Deflate();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y + (balloonNumber * 0.2f));
    }

    private bool IsGrounded()
    {
        // spawns circle to see if it overlaps with ground objects under player.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Inflate()
    {
        balloonNumber++;
    }

    private void Deflate()
    {
        balloonNumber--;
    }
}
