using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    private bool isPlayer;

    [SerializeField]
    private Collider2D leftSideGlove;

    [SerializeField]
    private Collider2D rightSideGlove;

    [SerializeField]
    private Collider2D topSideGlove;

    [SerializeField]
    private Collider2D bottomSideGlove;

    [SerializeField]
    private float speed = 6f;

    private float horizontal;
    private float vertical;

    public float balloonNumber = 0;
    public int[] floatinessScale = {-80, -35, 5, 15, 23, 28, 32, 35};
    

    [SerializeField]
    private Rigidbody2D rb;

    // object used for checking if player is on ground
    [SerializeField]
    private Transform groundCheck;

    // layer type to identify which objects are ground
    [SerializeField]
    private LayerMask[] groundLayers;

    [SerializeField]
    private Collider2D childCollider;

    [SerializeField]
    private Transform balloon;

    [SerializeField]
    SpriteRenderer[] balloons;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    SpriteRenderer render;

    private void Start()
    {
        isPlayer = true;
    }


    private void FixedUpdate()
    {
        animator.SetBool("HasRobot", isPlayer);

        float balloonScale = balloonNumber / 7 * 37f - 20f;
        balloonScale = (float) Math.Pow(1.1, balloonScale) + 0.25f;

        balloon.localScale = new Vector2(balloonScale, balloonScale);
        balloon.localPosition = new Vector2(-0.15f, 2.25f + balloons[0].bounds.size.y / 2f);

        float smallTrans = 0;
        float largeTrans = 0;
        
        if (balloonScale < 0.5)
        {
            smallTrans = 1;
        }
        else if (balloonScale < 1.2)
        {
            smallTrans = 1 - (balloonScale - 0.5f) / 0.7f;
        }
        else if (balloonScale < 3)
        {
            largeTrans = (balloonScale - 1.2f) / 1.8f;
        }
        else
        {
            largeTrans = 1;
        }

        balloons[0].color = new Color(1f, 1f, 1f, smallTrans);
        balloons[1].color = new Color(1f, 1f, 1f, largeTrans);

        if (!isPlayer) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        bool leftTouching = false, rightTouching = false, topTouching = false, bottomTouching = false;

        foreach (LayerMask layer in groundLayers)
        {
            leftTouching |= leftSideGlove.IsTouchingLayers();
            rightTouching |= rightSideGlove.IsTouchingLayers();
            topTouching |= topSideGlove.IsTouchingLayers();
            bottomTouching |= bottomSideGlove.IsTouchingLayers();
        }

        bool isEnclosed = false;

        if ((leftTouching && rightTouching) || (bottomTouching && topTouching))
        {
            isEnclosed = true;
        }

        if (!isEnclosed || vertical != 1)
        {
            balloonNumber += vertical * 0.05f;
            balloonNumber = Mathf.Clamp(balloonNumber, 0, floatinessScale.Length - 1);
        }

        if (!IsGrounded())
        {
            rb.AddForce((horizontal * speed * Vector2.right) + (GetFloatiness() * 0.18f));
        }
        else
        {
            rb.AddForce(GetFloatiness() * 0.2f);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) transform.localScale = new Vector2(1f, 1f);
        if (Input.GetKey(KeyCode.RightArrow)) transform.localScale = new Vector2(-1f, 1f);
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

    private Vector2 GetFloatiness()
    {
        int lower = (int) balloonNumber;
        int upper = Mathf.Min((int) balloonNumber + 1, floatinessScale.Length - 1);
        float remainer = balloonNumber % 1;
        Vector3 slerped = Vector3.Slerp(new Vector3(0, floatinessScale[lower], 0),
                                        new Vector3(0, floatinessScale[upper], 0),
                                        remainer);
        return new Vector2(0, slerped.y);
    }

    public void SetAsPlayer()
    {
        isPlayer = true;
        childCollider.isTrigger = false;
        rb.isKinematic = false;
        render.sortingOrder = 1;
        tag = "Player";
    }

    public void UnsetAsPlayer()
    {
        isPlayer = false;
        childCollider.isTrigger = true;
        rb.isKinematic = true;
        render.sortingOrder = 0;
        tag = "Untagged";
    }
}