using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteZoom : MonoBehaviour
{
    public KeyCode readNote;
    public bool isReading;
    public Sprite note;
    public Canvas noteCanvas;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        noteCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (Input.GetKeyDown(readNote))
        {
            if (!isReading)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);

                image.sprite = note;

                noteCanvas.enabled = true;
                isReading = true;
            }
            else if (isReading)
            {
                noteCanvas.enabled = false;
                isReading = false;
            }

        }
    }
}