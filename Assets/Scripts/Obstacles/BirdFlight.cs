using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BirdFlight : MonoBehaviour
{
    [SerializeField]
    private int speed;
    [SerializeField]
    Vector3 destination;
    [SerializeField]
    private float waitTime = 2f;

    [SerializeField]
    private Collider2D hitbox;

    [SerializeField]
    private LayerMask layer;

    private bool ableToFly = true;

    private Vector3 pos1;
    private Vector3 pos2;
    private void Start()
    {
        StartCoroutine(RandomWait(Random.Range(0f, 3f)));
        pos1 = transform.position;
        pos2 = destination;
    }
    // Update is called once per frame
    void Update()
    {
        if (ableToFly)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pos2, step);
            if (Vector3.Distance(transform.position, pos2) < 0.001f)
            {
                ableToFly = false;
                StartCoroutine (Wait(waitTime));
            }
            
        }

        if (hitbox.IsTouchingLayers(layer))
            GameObject.Find("Player").GetComponent<PlayerContoller>().CommitDie();
    }
    IEnumerator Wait(float time)
    {
        transform.localScale = new Vector3(-transform.localScale.x, 0.75f, 1f);
        yield return new WaitForSeconds(time);
        Vector2 temp = pos2;
        pos2 = pos1;
        pos1 = temp;
        ableToFly = true;
    }
    IEnumerator RandomWait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
