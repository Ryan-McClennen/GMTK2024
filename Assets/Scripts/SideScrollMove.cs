using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    private float horizontal;
    [SerializeField]
    private float jumpingPow = 16f;

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
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        // spawns circle to see if it overlaps with ground objects under player.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
