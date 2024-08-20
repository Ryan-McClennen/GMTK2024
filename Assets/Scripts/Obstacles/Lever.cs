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
    private Animator animator;

    [SerializeField]
    AudioSource source;

    private bool canFlip;

    private void Start()
    {
        canFlip = true;
        animator.SetBool("isActive", true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player" &&  canFlip)
                {
                    if (!obstacle.isActive)
                    {
                        obstacle.Activate();
                        animator.SetBool("isActive", true);
                    }
                    else
                    {
                        obstacle.Deactivate();
                        animator.SetBool("isActive", false);
                    }
                    source.Play();

                    obstacle.isActive = !obstacle.isActive;
                    canFlip = false;
                    StartCoroutine(DelayLever());
                }
            }
        }
    }

    IEnumerator DelayLever()
    {
        yield return new WaitForSeconds(0.5f);
        canFlip = true;
    }
}
