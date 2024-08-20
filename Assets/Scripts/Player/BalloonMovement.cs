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
    private Collider2D bottomSideGloveTwo;

    [SerializeField]
    private float speed = 6f;

    private float horizontal;
    private float vertical;

    public float balloonNumber = 0;
    public int[] floatinessScale;
    

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

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip[] clips;

    [SerializeField]
    Animator balloonDeath;

    [SerializeField]
    SpriteRenderer balloonDeathRenderer;

    [SerializeField]
    SpriteRenderer balloonString;

    [SerializeField]
    Transform balloonPos;

    [SerializeField]
    float topOfWorld = 148f;

    private bool canControl;

    private void Start()
    {
        canControl = true;
        isPlayer = true;
        balloonDeath.SetInteger("deathNum", -1);
    }

    private void Update()
    {
        if (balloonDeath.GetCurrentAnimatorStateInfo(0).IsName("Done")) balloonDeathRenderer.sprite  = null;

        if (!canControl) return;

        if (balloonPos.position.y + 1.7f * balloonPos.localScale.x > topOfWorld || transform.position.y - 1.25f < -17f)
            GameObject.Find("Player").GetComponent<PlayerContoller>().CommitDie();

        if (!isPlayer) return;

        if (Input.GetKey(KeyCode.LeftArrow)) transform.localScale = new Vector2(1f, 1f);
        if (Input.GetKey(KeyCode.RightArrow)) transform.localScale = new Vector2(-1f, 1f);

        if (balloonNumber == 0 || balloonNumber == 8) source.Stop();

        bool up = Input.GetKeyDown(KeyCode.UpArrow);
        bool continuousUp = Input.GetKey(KeyCode.UpArrow);
        bool upReleased = Input.GetKeyUp(KeyCode.UpArrow);
        bool down = Input.GetKeyDown(KeyCode.DownArrow);
        bool continuousDown = Input.GetKey(KeyCode.DownArrow);
        bool downReleased = Input.GetKeyUp(KeyCode.DownArrow);

        if (continuousUp == continuousDown)
        {
            source.Stop();
        }
        else if ((up && balloonNumber != 8) || (downReleased && continuousUp))
        {
            source.clip = clips[1];
            source.Play();
        }
        else if ((down && balloonNumber != 0) || (upReleased && continuousDown))
        {
            source.clip = clips[0];
            source.Play();
        }
    }

    private void FixedUpdate()
    {
        if (!canControl) return;

        animator.SetBool("HasRobot", isPlayer);

        float balloonScale = balloonNumber / 7 * 22f - 8f;
        balloonScale = (float) Math.Pow(1.1, balloonScale) - 0.1f;

        balloon.localScale = new Vector2(balloonScale, balloonScale);
        balloon.localPosition = new Vector2(-0.15f, 2.25f + balloons[0].bounds.size.y / 2f);

        float smallTrans = 0;
        float medTrans = 0;
        float largeTrans = 0;
        
        if (balloonScale < 0.36)
        {
            smallTrans = 1;
        }
        else if (balloonScale < 1)
        {
            if (balloonScale > 0.5)
                smallTrans = Mathf.Pow((1f - balloonScale) * 2, 0.5f);
            else
                smallTrans = 1;

            medTrans = Mathf.Pow(balloonScale, 0.5f);
        }
        else if (balloonScale <  2.5)
        {
            medTrans = 1;
        }
        else if (balloonScale < 3.5)
        {
            medTrans = 1;
            largeTrans = Mathf.Pow(balloonScale - 2.5f, 0.5f);
        }
        else
        {
            largeTrans = 1;
        }

        balloons[0].color = new Color(1f, 1f, 1f, smallTrans);
        balloons[1].color = new Color(1f, 1f, 1f, medTrans);
        balloons[2].color = new Color(1f, 1f, 1f, largeTrans);

        if (!isPlayer) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        bool leftTouching = false, rightTouching = false, topTouching = false, bottomTouching = false, bottomTouchingTwo = false;

        foreach (LayerMask layer in groundLayers)
        {
            leftTouching |= leftSideGlove.IsTouchingLayers(layer);
            rightTouching |= rightSideGlove.IsTouchingLayers(layer);
            topTouching |= topSideGlove.IsTouchingLayers(layer);
            bottomTouching |= bottomSideGlove.IsTouchingLayers(layer);
            bottomTouchingTwo |= bottomSideGloveTwo.IsTouchingLayers(layer);
        }

        bool isEnclosed = ((leftTouching && rightTouching) || (bottomTouching && topTouching) || (bottomTouchingTwo && balloonNumber > 4.2)) && balloonNumber > 3;

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
        Vector3 slerped = Vector3.Lerp(new Vector3(0, floatinessScale[lower], 0),
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
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        render.sortingOrder = 0;
        tag = "Untagged";
    }

    public void RemoveControl()
    {
        canControl = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 2f;
    }

    public void Die()
    {
        animator.SetTrigger("isDead");
        if (balloonNumber < 0.75f)
            balloonDeath.SetInteger("deathNum", 0);
        else if (balloonNumber < 3f)
            balloonDeath.SetInteger("deathNum", 1);
        else
            balloonDeath.SetInteger("deathNum", 2);

        foreach (SpriteRenderer b in balloons)
            b.color = new Color(0, 0, 0, 0);

        balloonString.color = new Color(0, 0, 0, 0);
    }
}