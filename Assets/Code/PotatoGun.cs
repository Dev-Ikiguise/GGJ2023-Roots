using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoGun : MonoBehaviour
{
    public Light flashlight;
    public KeyCode flashLightToggleKey;
    public List <ParticleSystem> pesticides;
    public KeyCode pesticideToggleKey;
    bool isFiringPesticide;
    public List<GameObject> tatoes;
    public Transform tatoSpawnPoint;
    public float launchSpeed;
    public int pesticideIndex;



    // Start is called before the first frame update
    void Start()
    {
        pesticideIndex = 0;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchPesticideDown();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchPesticideUp();
        }

    }

    void ToggleFlashLight()
    {
        flashlight.enabled = !flashlight.enabled;
        //TODO Play Brian's Flashlight sound;
    }
    void FirePesticide()
    {
        pesticides[pesticideIndex].Play();
    }

    void StopPesticide()
    {
        pesticides[pesticideIndex].Stop();
    }

    void Firetato() //Fires potato from launcher
    {
        //spawn and then launch game object from point of origin
        GameObject newTato = Instantiate(tatoes[0], tatoSpawnPoint.position, transform.rotation);
        newTato.GetComponent<Rigidbody>().AddForce(tatoSpawnPoint.forward * launchSpeed);
        Destroy(newTato,5);
    }
    void SwitchPesticideUp()
    {
        //Stop previous pesticide
        pesticides[pesticideIndex].gameObject.SetActive(false);
        //switches to new pesticide
        pesticideIndex++;
        if(pesticideIndex >= pesticides.Count)
        {
            pesticideIndex = 0;
        }
        //starts new pesticide
        pesticides[pesticideIndex].gameObject.SetActive(true);
    }
    void SwitchPesticideDown()
    {
        //same as SwitchPesticideUp but counting down
        pesticides[pesticideIndex].gameObject.SetActive(false);
        pesticideIndex--;
        if (pesticideIndex < 0)
        {
            pesticideIndex = pesticides.Count - 1;
        }
        pesticides[pesticideIndex].gameObject.SetActive(true);
    }
}
