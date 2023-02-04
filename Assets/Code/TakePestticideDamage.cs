using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePestticideDamage : MonoBehaviour
{
    public float health;

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name);
        health -= 1;
        print(health);
    }

    private void OnParticleCollision(GameObject other)
    {
        health -= 1;
        print(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
