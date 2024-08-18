using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBlowing : MonoBehaviour
{
    [SerializeField]
    private Collider2D blowArea;
    [SerializeField]
    public Collider2D playerCollider;
    [SerializeField]
    public Rigidbody2D playerRB;
    [Range(-1, 1)]
    public int horizontalWindDirection;
    [Range(-1, 1)]
    public int verticalWindDirection;
    [SerializeField]
    public float windForceMultiplier;

    private void Update()
    {
        if (blowArea.IsTouching(playerCollider))
        {
            playerRB.AddForce(new Vector2(horizontalWindDirection * windForceMultiplier, verticalWindDirection * windForceMultiplier));
        }
    }
}
