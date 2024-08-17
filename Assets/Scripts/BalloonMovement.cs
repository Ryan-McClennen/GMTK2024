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
    public int[] floatinessScale = {0, 25, 55, 70, 75, 85, 100, 110};
    

    [SerializeField]
    private Rigidbody2D rb;

    // object used for checking if player is on ground
    [SerializeField]
    private Transform groundCheck;

    // layer type to identify which objects are ground
    [SerializeField]
    private LayerMask groundLayer;

    [Header ("Player Switch Data")]
    public GameObject backRobot;
    public GameObject robot;
    public Collider2D childCollider;

    private void Start()
    {
        isPlayer = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (IsGrounded() && isPlayer)
            {
                robot.transform.position = transform.position;
                rb.velocity = Vector3.zero;
                backRobot.SetActive(false);
                robot.SetActive(true);
                childCollider.isTrigger = true;
                isPlayer = false;
                rb.gravityScale = 0;
            }

            else if (Vector3.Distance(robot.transform.position, transform.position) < 0.5 && !isPlayer)
            {
                backRobot.SetActive(true);
                robot.SetActive(false);
                childCollider.isTrigger = false;
                isPlayer = true;
                rb.gravityScale = 1.2f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isPlayer) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        balloonNumber += Input.GetAxisRaw("Vertical") * 0.05f;
        balloonNumber = Mathf.Clamp(balloonNumber, 0, floatinessScale.Length - 1);

        if(balloonNumber <= 0.25f) rb.gravityScale = 3.2f;
        else rb.gravityScale = 1.2f;

        if (!IsGrounded())
        {
            rb.AddForce((horizontal * speed * Vector2.right) + (GetFloatiness() * 0.18f));
        }
        else
        {
            rb.AddForce(GetFloatiness() * 0.2f);
        }
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
}