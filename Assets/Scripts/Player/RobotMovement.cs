using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip[] clips;

    [SerializeField]
    ParticleSystem deathExplosion;

    private bool canControl;

    private bool beep;

    private void Start()
    {
        UnsetAsPlayer();
        beep = true;
        canControl = true;
    }

    private void Update()
    {
        if  (!canControl) return;

        if (transform.position.y - 1f < -17f)
            GameObject.Find("Player").GetComponent<PlayerContoller>().CommitDie();

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

        AnimatorStateInfo currState = animator.GetCurrentAnimatorStateInfo(0);
        if (currState.IsName("Land")  && source.clip != clips[3])
        {
            source.loop = false;
            source.clip = clips[3];
            source.Play();
        }
        else if (currState.IsName("Start") && source.clip != clips[6])
        {
            source.loop = false;
            source.clip = clips[6];
            source.Play();
        }
        else if (currState.IsName("Move") && source.clip != clips[5])
        {
            source.loop = true;
            source.clip = clips[5];
            source.Play();
        }
        else if (currState.IsName("Jump") && source.clip != clips[2])
        {
            source.loop = true;
            source.clip = clips[2];
            source.Play();
        }
        else if (currState.IsName("Deactivate") && source.clip != clips[4])
        {
            source.loop = false;
            source.clip = clips[4];
            source.Play();
        }
        else if (currState.IsName("Idle"))
        {
            source.loop = false;
            source.clip = clips[0];
            if (beep)
            {
                print("Here");
                source.Play();
                beep = false;
                StartCoroutine(ResetBeep());
            }
        }
        else if (!IsGrounded() && source.clip == clips[5])
            source.Stop();
    }
        

    private void FixedUpdate()
    {
        if  (!canControl) return;

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

        source.loop = false;
        source.clip = clips[1];
        source.Play();
    }

    public void UnsetAsPlayer()
    {
        isPlayer = false;
        robotCollider.isTrigger = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        render.sortingOrder = 0;

        float xPos;
        if (transform.localScale.x == transform.parent.localScale.x) xPos = 0.875f;
        else xPos = 0.75f;
        transform.localPosition = new Vector3(xPos, 0.315f, 1f);

        tag = "Untagged";

        animator.SetTrigger("Deactivate");
    }

    IEnumerator ResetBeep()
    {
        float time = Random.Range(3, 5);
        yield return new WaitForSeconds(time);
        beep = true;
    }

    public void RemoveControl()
    {
        canControl = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    public void Die()
    {
        render.color = new Color(0, 0, 0);
        deathExplosion.Play();
    }
    
    public void ChildDie()
    {
        transform.localPosition = new Vector2(-1f * transform.parent.localScale.x, -2.5f);
        transform.localScale = new Vector2(transform.localScale.x,  -1);
    }
}
