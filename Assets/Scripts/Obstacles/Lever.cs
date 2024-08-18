using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private Obstacle obstacle;
    
    [SerializeField]
    private SpriteRenderer render;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    if (!obstacle.isActive)
                    {
                        obstacle.Activate();
                        render.color = Color.green;
                    }
                    else
                    {
                        obstacle.Deactivate();
                        render.color = Color.red;
                    }

                    obstacle.isActive = !obstacle.isActive;

                    print("Interacted with: " + collider.name);
                }
            }
        }
    }
}
