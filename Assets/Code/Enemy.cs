using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nma;
    public Transform position;
    public float jumpUpForce = 500;
    public float jumpForwardForce = 200;
    public EnemyType enemyType;
    Vector3 enemyPosition;
    Vector3 playerPosition;
    public float howclose;
    public float dist;
    public float leaptimer = 0;
    public float maxleaptimer = 7;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayerMask;
    private bool isGrounded;

    public float agroDistance;

    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nma.SetDestination(position.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, position.position);
        if (distanceToPlayer > agroDistance)
        {
            nma.speed = 0;

            return;      
        }
        else
        {
            nma.speed = 1;
        }


        leaptimer = leaptimer + Time.deltaTime;
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance, groundLayerMask);


        if (nma.enabled)
        {
            nma.SetDestination(position.position);
        }
        if (enemyType == EnemyType.Leaper)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPosition = player.transform.position;
            dist = Vector3.Distance(playerPosition, transform.position);

            if (dist <= howclose && leaptimer >= maxleaptimer)
            {
                leaptimer = 0;
                StartCoroutine(NMADisable(.5f));
            }


        }

        if (enemyType == EnemyType.Stumbler)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPosition = player.transform.position;

        }

        if (enemyType == EnemyType.Superstumbler)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPosition = player.transform.position;
        }



    }

    IEnumerator NMADisable(float time)
    {
        Debug.Log("IEnumerator Reached");
        Rigidbody rb = GetComponent<Rigidbody>();
        nma.enabled = false;

        yield return new WaitForSeconds(time);
        Vector3 jumpDirection = (playerPosition - transform.position).normalized;
        Vector3 jumpUpDirection = new Vector3(0, 1, 0);
        rb.AddForce(jumpDirection * jumpForwardForce);
        rb.AddForce(jumpUpDirection * jumpUpForce);

        while (!isGrounded)
        {
            yield return null;
            isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance, groundLayerMask);
            yield return new WaitForEndOfFrame();
        }
        nma.enabled = true;
        nma.SetDestination(playerPosition);
        Debug.Log("SetDestination Reached");

    }


}

public enum EnemyType
{
    Leaper =0,
    Stumbler =1,
    Superstumbler =2,
}