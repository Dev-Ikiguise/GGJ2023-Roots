using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trap : MonoBehaviour
{
    public float UpwardVelocity;

    bool canLaunch;
    public GameObject closedTrap;
    public GameObject openedTrap;

    public AudioSource springSound;
    public AudioSource unspringSound;

    private void Start()
    {
        canLaunch = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && canLaunch)
        {
            Launch(collision.gameObject.GetComponent<Rigidbody>());
            //find what is hitting us, find rigid body, apply upwards force
            if (collision.gameObject.GetComponent<NavMeshAgent>())
            {
                collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Rigidbody>() && canLaunch)
        {
            Launch(other.gameObject.GetComponent<Rigidbody>());

            //find what is hitting us, find rigid body, apply upwards force
            if (other.gameObject.GetComponent<NavMeshAgent>())
            {
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;

            }
        }
    }

    void Launch(Rigidbody rigidbody)
    {
        rigidbody.isKinematic = false;

        float randX = Random.Range(-20, 20);
        float randZ = Random.Range(-20, 20);
        Vector3 randomVector3 = new Vector3(randX, randZ, randZ);

        rigidbody.AddForce((Vector3.up * UpwardVelocity) + randomVector3);
        rigidbody.AddTorque(randomVector3);

        StartCoroutine(AnimateCo());

        springSound.Play();
    }

    private IEnumerator AnimateCo()
    {
        canLaunch = false;
        closedTrap.SetActive(false);
        openedTrap.SetActive(true);
        yield return new WaitForSeconds(3);
        closedTrap.SetActive(true);
        openedTrap.SetActive(false);
        canLaunch = true;
        unspringSound.Play();
    }
}
