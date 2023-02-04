using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nma;
    public Transform position;
    public float jumpForce = 500;
    public float jumpForwardForce = 200;
    public EnemyType enemyType;
    Vector3 enemyPosition;
    Vector3 playerPosition;
    public float howclose;
    public float dist;
    public float leaptimer = 0;
    public float maxleaptimer = 7;


    
    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leaptimer = leaptimer + Time.deltaTime;
        
        nma.SetDestination(position.position);
        if (enemyType == EnemyType.Leaper)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPosition = player.transform.position;
            dist = Vector3.Distance(playerPosition, transform.position);

            if(dist <= howclose && leaptimer > maxleaptimer)
            {
                Vector3 jumpDirection = (playerPosition - transform.position).normalized;
                GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce);
                nma.SetDestination(playerPosition);
                leaptimer = 0;
            }
        }


    }
}

public enum EnemyType
{
    Leaper =0,
    Flyer =1,
}