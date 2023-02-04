using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float UpwardVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //find what is hitting us, find rigid body, apply upwards force
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * UpwardVelocity);
    }


}
