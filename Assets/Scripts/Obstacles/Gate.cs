using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gate : Obstacle
{
    [Header ("Start and End Positions")]
    public Vector3 openPos;
    public Vector3 closedPos;

    private Vector3 goal;
    private int moveTime = 2;
    private float speed;


    private void Start()
    {
        transform.position = closedPos;
        goal = closedPos;
        speed = Vector3.Distance(closedPos, openPos) / 50 / moveTime;
        isActive = false;
    }

    public override void Activate()
    {
        goal = openPos;
    }

    public override void Deactivate()
    {
        goal = closedPos;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, speed);
    }
}
