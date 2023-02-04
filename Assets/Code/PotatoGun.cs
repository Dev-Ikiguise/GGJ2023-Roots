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
    public List<GameObject> tatoes;
    public Transform tatoSpawnPoint;
    public float launchSpeed;


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
        if (Input.GetMouseButtonDown(1))
        {
            Firetato();
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

    void Firetato() //Fires potato from launcher
    {
        //if key is pressed, spawn and then launch game object from point of origin
        GameObject newTato = Instantiate(tatoes[0], tatoSpawnPoint.position, transform.rotation);
        newTato.GetComponent<Rigidbody>().AddForce(tatoSpawnPoint.forward * launchSpeed);
        Destroy(newTato,5);
    }
}
