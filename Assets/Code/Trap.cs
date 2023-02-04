using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trap : MonoBehaviour
{
    public float UpwardVelocity;

    private void OnCollisionEnter(Collision collision)
    {
        //find what is hitting us, find rigid body, apply upwards force

        print(collision.gameObject.name);

        if (collision.gameObject.GetComponent<NavMeshAgent>())
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * UpwardVelocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        //find what is hitting us, find rigid body, apply upwards force

        Vector3 initialVelocity;

        print(other.gameObject.name);

        if (other.gameObject.GetComponent<NavMeshAgent>())
        {
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            
        }
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            float randX = Random.Range(-20, 20);
            float randZ = Random.Range(-20, 20);
            Vector3 randomVector3 = new Vector3(randX, randZ, randZ);

            other.gameObject.GetComponent<Rigidbody>().AddForce((Vector3.up * UpwardVelocity) + randomVector3);
            other.gameObject.GetComponent<Rigidbody>().AddTorque(randomVector3);
        }
        //if (other.gameObject.GetComponent<EnemyHealth>())
        //{
        //    other.gameObject.GetComponent<EnemyHealth>().HandleDeath();
        //}
    }
}
