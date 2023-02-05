using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 direction;

    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Random.Range(0, 100) < 1)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        direction = new Vector3(Random.Range(-1.0f, 1.0f),
                                Random.Range(-1.0f, 1.0f),
                                Random.Range(-1.0f, 1.0f));
    }
}

