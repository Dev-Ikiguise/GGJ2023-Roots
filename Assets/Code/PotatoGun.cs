using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoGun : MonoBehaviour
{
    public Light flashlight;
    public KeyCode flashLightToggleKey;
    public ParticleSystem pesticide;
    public KeyCode pesticideToggleKey;
    bool isFiringPesticide;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(flashLightToggleKey))
        {
            ToggleFlashLight();
        }
        if (Input.GetMouseButtonDown(0))
        {
            FirePesticide();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopPesticide();
        }
    }

    void ToggleFlashLight()
    {
        flashlight.enabled = !flashlight.enabled;
        //TODO Play Brian's Flashlight sound;
    }
    void FirePesticide()
    {
        pesticide.Play();
    }

    void StopPesticide()
    {
        pesticide.Stop();
    }
}
