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

        print(other.gameObject.name);

        if (other.gameObject.GetComponent<NavMeshAgent>())
        {
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * UpwardVelocity);
    }
}
