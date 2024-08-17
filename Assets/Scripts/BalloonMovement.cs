using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    private bool isPlayer;

    [SerializeField]
    private float speed = 6f;

    private float horizontal;
    private float vertical;

    public float balloonNumber = 0;
    public int[] floatinessScale = {-100, -75, -50, -25, 10, 22, 30, 35};
    

    [SerializeField]
    private Rigidbody2D rb;

    // object used for checking if player is on ground
    [SerializeField]
    private Transform groundCheck;

    // layer type to identify which objects are ground
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Collider2D childCollider;

    [SerializeField]
    private Transform balloon;

    [SerializeField]
    SpriteRenderer balloonRenderer;

    [SerializeField]
    Sprite[] balloons;


    [SerializeField]
    private Animator animator;

    private void Start()
    {
        isPlayer = true;
    }


    private void FixedUpdate()
    {
        animator.SetBool("HasRobot", isPlayer);

        float balloonScale = (balloonNumber * 15 / 7 + 1) / 4;
        balloon.localScale = new Vector2(balloonScale, balloonScale);
        balloon.localPosition = new Vector2(-0.15f, 2.25f + balloonRenderer.bounds.size.y / 2f);
        if (balloonScale <= 0.71f) balloonRenderer.sprite = balloons[0];
        else if (balloonScale <= 1.4f) balloonRenderer.sprite = balloons[1]; 
        else balloonRenderer.sprite = balloons[2]; 

        if (!isPlayer) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        balloonNumber += Input.GetAxisRaw("Vertical") * 0.05f;
        balloonNumber = Mathf.Clamp(balloonNumber, 0, floatinessScale.Length - 1);

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
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
        transform.position += Vector3.back;
    }

    public void UnsetAsPlayer()
    {
        isPlayer = false;
        childCollider.isTrigger = true;
        rb.isKinematic = true;
        transform.position += Vector3.forward;
    }
}