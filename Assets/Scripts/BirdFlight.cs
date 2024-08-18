using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlight : MonoBehaviour
{
    [SerializeField]
    private int speed;
    [SerializeField]
    Transform destination;
    [SerializeField]
    private float waitTime = 2f;
    private bool ableToFly = true;

    private Vector3 pos1;
    private Vector3 pos2;
    private void Start()
    {
        StartCoroutine(RandomWait(Random.Range(0f, 3f)));
        pos1 = transform.position;
        pos2 = destination.position;
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
    }
    IEnumerator Wait(float time)
    {
        Debug.Log("Waiting");
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
