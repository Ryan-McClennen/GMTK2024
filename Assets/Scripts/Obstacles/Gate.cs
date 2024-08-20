using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class Gate : Obstacle
{
    [Header ("Start and End Positions")]
    public Vector3 openPos;
    public Vector3 closedPos;

    [SerializeField]
    AudioSource source;

    private Vector3 start;
    private Vector3 goal;
    private int moveTime = 2;
    private float speed;


    private void Start()
    {
        start = transform.position;
        transform.position = start + closedPos;
        goal = start + closedPos;
        speed = Vector3.Distance(closedPos, openPos) / 50 / moveTime;
        isActive = true;
    }

    public override void Activate()
    {
        goal = start + closedPos;
        source.Play();
    }

    public override void Deactivate()
    {
        goal = start + openPos;
        source.Play();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, speed);

        if (Mathf.Approximately((transform.position - goal).magnitude, 0))
            source.Stop();
    }
}
