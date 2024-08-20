using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField]
    AudioSource source;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        playerRB = GameObject.Find("Child").GetComponent<Rigidbody2D>();
        isActive = true;
        animator.SetBool("isActive", true);
        animator.SetFloat("speed", windForceMultiplier / 2);

        ParticleSystem.MainModule mainModule = air.main;
        mainModule.startSpeed = windForceMultiplier * 4;
        float fanLength = transform.rotation.eulerAngles.z % 180 == 0 ? blowArea.bounds.size.x : blowArea.bounds.size.y;
        mainModule.startLifetime = fanLength / mainModule.startSpeed.constant;
        mainModule.startRotationZ = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        
        ParticleSystem.ShapeModule shape = air.shape;
        shape.scale = new Vector3(transform.localScale.y * 2, 0, 1);
    }

    private void Update()
    {
        if (isActive && blowArea.IsTouchingLayers(childLayer))
        {
            playerRB.AddForce(transform.rotation * Vector3.right * windForceMultiplier / 3);
        }

        float distance = Vector3.Distance(transform.position, cam.transform.position);
        if (isActive && distance < 30)
            source.volume = (900 - Mathf.Pow(distance, 2)) / 900f;
        else
            source.volume = 0;
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
