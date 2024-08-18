using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private int offSet;

    private Camera mainCam;

    private float startX;

    private int WIDTH = 48;
    private int TELEPORTCHECK = 72;

    void Start()
    {
        mainCam = Camera.main;
        GameObject start = GameObject.Find("PlayerStart");
        startX = start.transform.position.x + offSet * WIDTH;
    }

    void Update()
    {
        float cameraX = mainCam.transform.position.x;
        Vector3 newPos = new Vector3(startX + cameraX * speed, 64, 0);
        transform.position = newPos;

        if (cameraX - newPos.x > TELEPORTCHECK) startX += WIDTH * 3;
        if (cameraX - newPos.x < -TELEPORTCHECK) startX -= WIDTH * 3;
    }
}
