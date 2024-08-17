using System;
using System.Collections;
using System.Collections.Generic;
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

            else if (Vector2.Distance(robot.transform.position, child.transform.position) < 0.5 && !isChild)
            {
                balloonMove.SetAsPlayer();
                robotMove.UnsetAsPlayer();
                isChild = true;
            }
        }

        if (isChild)
        {
            robot.transform.position = child.transform.position + new Vector3(0.5f, 0.5f, 1f);
        }
    }
}
