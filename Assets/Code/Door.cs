using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{

    public GameObject hinge;
    public float openTime;
    bool isOpening = false;
    bool keypressed = false;
    public float remainOpenTime;
    public Vector3 openAngleEuler;
    

    void Update()
    {
        keypressed = Input.GetKey(KeyCode.E);
    }

    private IEnumerator OpenDoor()
    {

        isOpening = true;
        
        Quaternion beginRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(beginRot.eulerAngles.x + openAngleEuler.x, beginRot.eulerAngles.y + openAngleEuler.y, beginRot.eulerAngles.z + openAngleEuler.z);

        float timer = 0;

        while (timer <= openTime)
        {
            timer += Time.deltaTime;
            hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, endRot, timer / openTime);
            yield return new WaitForEndOfFrame();
            
        }

        timer = 0;
        yield return new WaitForSeconds(remainOpenTime);

        while (timer <= openTime)
        {
            timer += Time.deltaTime;
            hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, beginRot, timer / openTime);
            yield return new WaitForEndOfFrame();
            
        }
        
        isOpening = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (keypressed && !isOpening && hinge != null)
        {
            StartCoroutine(OpenDoor());
        }
    }
}

