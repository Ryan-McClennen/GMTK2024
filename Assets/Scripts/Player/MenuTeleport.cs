using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuTeleport : MonoBehaviour
{
    [SerializeField]
    private GameObject top;
    [SerializeField]
    private GameObject bottom;
    [SerializeField]
    private GameObject left;
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Collider2D balloonCollider;

    [SerializeField]
    private Collider2D topColl;
    [SerializeField]
    private Collider2D botColl;
    [SerializeField]
    private Collider2D leftColl;
    [SerializeField]
    private Collider2D rightColl;
    private void Update()
    {

        if (balloonCollider.IsTouching(topColl))
        {
            Debug.Log("lmao2");
            player.position.Set(player.position.x, player.position.y - 80f, 0f);
        }
        else if (balloonCollider.IsTouching(botColl))
        {
            player.position.Set(player.position.x, player.position.y + 80f, 0f);
        }
        else if (balloonCollider.IsTouching(leftColl))
        {
            player.position.Set(right.transform.position.y - 5f, player.position.y, 0f);
        }
        else if (balloonCollider.IsTouching(rightColl))
        {
            player.position.Set(left.transform.position.y + 5f, player.position.y, 0f);
        }
    }
    
}
