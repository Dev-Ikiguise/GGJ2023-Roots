using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform _camera;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        Vector3 lookPosition = new Vector3(_camera.position.x, transform.position.y, _camera.position.z);
        transform.LookAt(lookPosition);
    }
}