using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
    Animator anim;
    bool isSet;
    public string animState = "";
    
    void Start()
    {
        anim = GetComponent<Animator>();
        if (animState != "")
        {
            anim.SetBool(animState, true);
        }
    }

    void Update()
    {
        if (!isSet)
        {
            isSet = true;
            StartCoroutine(SetAnim());
        }
    }

    IEnumerator SetAnim()
    {
        anim.speed = Random.Range(.15f, 1f);
        yield return new WaitForSeconds(Random.Range(1f, 20f));
        isSet = false;
    }
}
