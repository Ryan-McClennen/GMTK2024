using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    private bool isPlayer;

    private bool wasInAir = false;

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
    private Collider2D robotCollider;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        UnsetAsPlayer();
    }

    private void Update()
    {

        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Z) && IsGrounded())
            {
                animator.ResetTrigger("Land");
                animator.SetTrigger("Jump");
                rb.velocity += jumpingPow * Vector2.up;
            }

            else if (wasInAir && IsGrounded())
            {
                animator.ResetTrigger("Jump");
                animator.ResetTrigger("Start");
                animator.ResetTrigger("Stop");
                animator.SetTrigger("Land");
            }

            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.ResetTrigger("Stop");
                animator.SetTrigger("Start");
            }

            else if (rb.velocity.magnitude == 0)
            {
                animator.ResetTrigger("Start");
                animator.ResetTrigger("Jump");
                animator.SetTrigger("Stop");
            }

            wasInAir = !IsGrounded();
        }
    }
        

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            if (rb.velocity.x > 0) transform.localScale = new Vector2(-transform.parent.localScale.x, 1f);
            if (rb.velocity.x < 0) transform.localScale = new Vector2(transform.parent.localScale.x, 1f);
        }
    }

    public bool IsGrounded()
    {
        // spawns circle to see if it overlaps with ground objects under player.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void SetAsPlayer()
    {
        animator.ResetTrigger("Deactivate");
        animator.SetTrigger("Activate");

        isPlayer = true;
        robotCollider.isTrigger = false;
        rb.isKinematic = false;
        transform.position += Vector3.back;
    }

    public void UnsetAsPlayer()
    {
        animator.ResetTrigger("Activate");
        animator.ResetTrigger("Stop");
        animator.ResetTrigger("Land");
        animator.ResetTrigger("Start");
        animator.SetTrigger("Deactivate");

        isPlayer = false;
        robotCollider.isTrigger = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position += Vector3.forward;

        transform.localPosition = new Vector3(0.85f, 0.4f, 1f);
    }
}
