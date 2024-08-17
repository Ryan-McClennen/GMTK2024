using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    private float horizontal;
    [SerializeField]
    private float jumpingPow = 25f;

    [SerializeField]
    private Rigidbody2D rb;

    // object used for checking if player is on ground
    [SerializeField]
    private Transform groundCheck;

    // layer type to identify which objects are ground
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Collider2D circleCollider;

    private bool isPlayer;

    private void Start()
    {
        UnsetAsPlayer();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && IsGrounded() && isPlayer)
        {
            rb.velocity += jumpingPow * Vector2.up;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    public bool IsGrounded()
    {
        // spawns circle to see if it overlaps with ground objects under player.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void SetAsPlayer()
    {
        isPlayer = true;
        circleCollider.isTrigger = false;
        rb.isKinematic = false;
        transform.position += Vector3.back;
    }

    public void UnsetAsPlayer()
    {
        isPlayer = false;
        circleCollider.isTrigger = true;
        rb.isKinematic = true;
        transform.position += Vector3.forward;
    }
}
