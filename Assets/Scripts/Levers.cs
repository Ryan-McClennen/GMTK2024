using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Levers : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer lever;
    [SerializeField]
    public GameObject gate;
    [SerializeField]
    public Collider2D leverCollider;
    [SerializeField]
    public Collider2D playerCollider;
    private float originalGateYPos;
    private float objectHeight;
    private Vector2 targetPos;

    private bool opened = false;

    [SerializeField]
    public float openingSpeed;

    public void Start()
    {
        originalGateYPos = gate.transform.position.y;
        objectHeight = gate.transform.localScale.y;
        targetPos = new Vector2(gate.transform.position.x, originalGateYPos - objectHeight);
    }
    public void Update()
    {
        if (leverCollider.IsTouching(playerCollider))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lever.color = Color.green;
                opened = true;
                
            }
        }
        if (opened)
        {
            var step = openingSpeed * Time.deltaTime;
            gate.transform.position = Vector2.MoveTowards(gate.transform.position, targetPos, step);
        }
    }
}
