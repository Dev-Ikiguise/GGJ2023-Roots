using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverOverTarget : MonoBehaviour
{
    public Transform target;
    public float yOffset;

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z);
    }
}
