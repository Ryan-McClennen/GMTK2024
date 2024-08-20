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
    float startDelay = 0f;

    [SerializeField]
    private float waitTime = 2f;

    [SerializeField]
    private Collider2D hitbox;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    AudioSource source;

    private bool ableToFly;
    Camera cam;

    private Vector3 pos1;
    private Vector3 pos2;
    private void Start()
    {
        cam = Camera.main;
        pos1 = transform.position;
        pos2 = destination;
        ableToFly = false;
        StartCoroutine(DelayStart());
        Invoke("StartSound", Random.value);
    }
    
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

        float distance = Vector3.Distance(transform.position, cam.transform.position);
        if (distance < 30)
            source.volume = 0.4f * (900 - Mathf.Pow(distance, 2)) / 900f;
        else
            source.volume = 0;

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

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(startDelay);
        ableToFly = true;
    }

    void StartSound()
    {
        source.Play();
    }
}
