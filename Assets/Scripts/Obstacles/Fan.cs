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

    [SerializeField]
    private ParticleSystem air;

    private void Start()
    {
        playerRB = GameObject.Find("Child").GetComponent<Rigidbody2D>();
        isActive = true;
        animator.SetBool("isActive", true);
        animator.SetFloat("speed", windForceMultiplier / 2);
        ParticleSystem.MainModule mainModule = air.main;
        mainModule.startSpeed = windForceMultiplier * 4;
        mainModule.startLifetime = blowArea.bounds.size.x / mainModule.startSpeed.constant;
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
        ParticleSystem.EmissionModule emission = air.emission;
        emission.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("isActive", false);
        ParticleSystem.EmissionModule emission = air.emission;
        emission.enabled = false;
    }
}
