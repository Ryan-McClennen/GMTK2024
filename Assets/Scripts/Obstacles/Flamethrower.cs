using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Flamethrower : Obstacle
{
    [SerializeField]
    private Collider2D flameArea;

    [SerializeField]
    public LayerMask childLayer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ParticleSystem flame;

    private void Start()
    {
        isActive = true;
        animator.SetBool("isActive", true);
        ParticleSystem.MainModule mainModule = flame.main;
        mainModule.startLifetime = flameArea.bounds.size.x / mainModule.startSpeed.constant;
    }

    private void Update()
    {
        if (isActive && flameArea.IsTouchingLayers(childLayer))
        {
            GameObject.Find("Player").GetComponent<PlayerContoller>().CommitDie();
        }
    }

    public override void Activate()
    {
        animator.SetBool("isActive", true);
        ParticleSystem.EmissionModule emission = flame.emission;
        emission.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("isActive", false);
        ParticleSystem.EmissionModule emission = flame.emission;
        emission.enabled = false;
    }
}
