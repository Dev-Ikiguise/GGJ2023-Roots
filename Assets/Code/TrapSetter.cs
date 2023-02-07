using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSetter : MonoBehaviour
{
    public GameObject trapPrefab;
    bool trapIsSet;
    public KeyCode trapKey;
    GameObject currentTrap;

    public AudioSource trapSetSound;
    public AudioSource trapRetrievedSound;

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
                trapRetrievedSound.Play();
                GameplayUI.Instance.ToggleTrapIcon();
            }
            else
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);

                if (Vector3.Distance(hit.point, transform.position) > 40) return;

                currentTrap = Instantiate(trapPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                trapIsSet = true;
                trapSetSound.Play();
                GameplayUI.Instance.ToggleTrapIcon();
            }
        }
    }
}
