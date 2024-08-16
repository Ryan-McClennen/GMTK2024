using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    private float horizontal;
    private float vertical;

    public int balloonNumber = 0;


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
        
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical == 1 && balloonNumber < 100)
        {
            Inflate();
        }

        if (vertical == -1 && balloonNumber > 0)
        {
            Deflate();
        }
        rb.AddForce((horizontal * speed * Vector2.right) + (balloonNumber * 0.2f * Vector2.up));
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
