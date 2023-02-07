using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour
{
    public float damageToGive;
    public AudioSource impactSound;

    public void PlayImpactSound()
    {
        float rand = Random.Range(.9f, 1.1f);
        impactSound.Play();
    }
}
