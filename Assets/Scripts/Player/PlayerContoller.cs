using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [Header ("Child Data")]
    public GameObject child;
    public BalloonMovement balloonMove;

    [Header ("Robot Data")]
    public GameObject robot;
    public RobotMovement robotMove;

    [Header ("Camera Data")]
    [SerializeField]
    private Transform balloon;

    [SerializeField]
    private Transform balloonFollow;

    [SerializeField]
    private CinemachineVirtualCamera vCam;

    private bool isChild;

    private int MINSCREENSIZE = 10;

    public void Start()
    {
        GameObject start = GameObject.Find("PlayerStart");
        transform.position = start.transform.position;
        isChild = true;
        vCam = GameObject.Find ("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = balloonFollow;
        vCam.m_Lens.OrthographicSize = MINSCREENSIZE;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (balloonMove.IsGrounded() && isChild)
            {
                balloonMove.UnsetAsPlayer();
                robotMove.SetAsPlayer();
                vCam.Follow = robot.transform;
                isChild = false;
            }

            else if (Vector2.Distance(robot.transform.position, child.transform.position) < 1f && !isChild)
            {
                balloonMove.SetAsPlayer();
                robotMove.UnsetAsPlayer();
                vCam.Follow = balloonFollow;
                isChild = true;
            }
        }

        float scale = balloon.transform.localScale.x;
        balloonFollow.transform.localPosition = Vector2.down * (scale * 0.2f / 5.3f + 0.4f);
        if (scale < 1.15f) vCam.m_Lens.OrthographicSize = MINSCREENSIZE;
        else vCam.m_Lens.OrthographicSize = (scale - 1.15f) * 3f + MINSCREENSIZE;
    }

    public void CommitDie()
    {
        Time.timeScale = 0;
        print(isChild ? "Child is Kil" : "Robot is kil");
    }
}
