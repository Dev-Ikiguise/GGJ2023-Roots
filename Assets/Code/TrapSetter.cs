using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSetter : MonoBehaviour
{
    public GameObject trapPrefab;
    bool trapIsSet;
    public KeyCode trapKey;
    public GameObject currentTrap;

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (Input.GetKeyDown(trapKey))
        {
            if (trapIsSet)
            {
                Destroy(currentTrap);
                trapIsSet = false;
            }
            else
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
                currentTrap = Instantiate(trapPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);

                //if (Physics.Raycast(ray.origin, ray.direction, out hitInfo) == true)
                //{
                //    currentTrap = Instantiate(trapPrefab, new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z), Quaternion.identity);
                //}

                trapIsSet = true;
            }

        }
    }
}