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
        transform.position = openPos;
        goal = openPos;
        speed = Vector3.Distance(openPos, closedPos) / 50 / moveTime;
        isActive = false;
    }

    public override void Activate()
    {
        goal = closedPos;
    }

    public override void Deactivate()
    {
        goal = openPos;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, speed);
    }
}
