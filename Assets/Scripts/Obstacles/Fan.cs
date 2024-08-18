using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Fan : Obstacle
{
    [SerializeField]
    private Collider2D blowArea;

    [SerializeField]
    public LayerMask childLayer;

    [SerializeField]
    public Rigidbody2D playerRB;
    
    [SerializeField]
    public float windForceMultiplier;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        playerRB = GameObject.Find("Child").GetComponent<Rigidbody2D>();
        isActive = true;
        animator.SetBool("isActive", true);
        animator.SetFloat("speed", windForceMultiplier / 2);
    }

    private void Update()
    {
        if (isActive && blowArea.IsTouchingLayers(childLayer))
        {
            playerRB.AddForce(transform.rotation * Vector3.right * windForceMultiplier / 3);
        }
    }

    public override void Activate()
    {
        animator.SetBool("isActive", true);
    }

    public override void Deactivate()
    {
        animator.SetBool("isActive", false);
    }
}
