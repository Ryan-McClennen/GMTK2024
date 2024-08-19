using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    private bool isPlayer;

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
    private LayerMask[] groundLayers;

    [SerializeField]
    private Collider2D robotCollider;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    SpriteRenderer render;

    private void Start()
    {
        UnsetAsPlayer();
    }

    private void Update()
    {
        animator.SetBool("isPlayer", isPlayer);
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isMoving", Input.GetAxisRaw("Horizontal") != 0);

        if (IsGrounded()) animator.ResetTrigger("Jump");
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Z) && IsGrounded())
            {
                animator.SetTrigger("Jump");
                rb.velocity += jumpingPow * Vector2.up;
            }
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
        foreach (LayerMask layer in groundLayers)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, layer)) return true;
        }
        return false;
    }

    public void SetAsPlayer()
    {
        isPlayer = true;
        robotCollider.isTrigger = false;
        rb.isKinematic = false;
        render.sortingOrder = 1;
        tag = "Player";

        animator.ResetTrigger("Deactivate");
    }

    public void UnsetAsPlayer()
    {
        isPlayer = false;
        robotCollider.isTrigger = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        render.sortingOrder = 0;

        transform.localPosition = new Vector3(0.85f, 0.4f, 1f);
        tag = "Untagged";

        animator.SetTrigger("Deactivate");
    }
}
