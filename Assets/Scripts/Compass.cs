using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField]
    GameObject needle;

    private Camera cam;
    private Vector3 goal;
    private Quaternion initialTransform;

    void Start()
    {
        cam = Camera.main;
        goal = GameObject.Find("LevelFinish").transform.position;
        initialTransform =  transform.rotation;
    }

    void Update()
    {
        needle.transform.rotation = initialTransform;
        needle.transform.Rotate(0, 0, Vector3.Angle(cam.transform.position, goal));
    }
}
