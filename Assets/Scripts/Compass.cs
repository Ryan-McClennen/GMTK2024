using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField]
    GameObject needle;

    private GameObject reference;
    private Vector3 goal;

    void Start()
    {
        reference = GameObject.Find("Reference");
        goal = GameObject.Find("LevelFinish").transform.position;
        goal.z = 0;
    }

    void Update()
    {
        reference.transform.right = goal - reference.transform.position;
        needle.transform.rotation = reference.transform.rotation;
    }
}
