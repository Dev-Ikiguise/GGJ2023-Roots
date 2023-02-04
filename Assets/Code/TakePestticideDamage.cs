using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePestticideDamage : MonoBehaviour
{
    public float health;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Potato>())
        {
            health -= collision.gameObject.GetComponent<Potato>().damageToGive;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
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
