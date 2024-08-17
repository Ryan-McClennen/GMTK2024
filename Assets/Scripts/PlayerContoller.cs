using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [Header ("Child Data")]
    public GameObject child;
    public BalloonMovement balloonMove;

    [Header ("Robot Data")]
    public GameObject robot;
    public RobotMovement robotMove;

    private bool isChild;

    public void Start()
    {
        isChild = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (balloonMove.IsGrounded() && isChild)
            {
                balloonMove.UnsetAsPlayer();
                robotMove.SetAsPlayer();
                isChild = false;
            }

            else if (Vector2.Distance(robot.transform.position, child.transform.position) < 1f && !isChild)
            {
                balloonMove.SetAsPlayer();
                robotMove.UnsetAsPlayer();
                isChild = true;
            }
        }
    }
}
