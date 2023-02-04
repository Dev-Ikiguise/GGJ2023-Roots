using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PotatoGun : MonoBehaviour
{
    public Light flashlight;
    public KeyCode flashLightToggleKey;
    public List <ParticleSystem> pesticides;
    public KeyCode pesticideToggleKey;
    bool isFiringPesticide;
    public List<GameObject> tatoes;
    public Transform tatoSpawnPoint;
    public Transform pesticideChamber;
    public float rotationAmount;
    public float launchSpeed;
    public int pesticideIndex;
    public AudioSource spraySound;
    public AudioSource ammoSwitchSound;
    public AudioSource potatoShootSound;


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
        if (Input.GetMouseButton(0))
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

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            SwitchPesticideUp();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            SwitchPesticideDown();
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
        spraySound.Play();
    }

    void StopPesticide()
    {
        pesticides[pesticideIndex].Stop();
        spraySound.Stop();
    }

    void Firetato() //Fires potato from launcher
    {
        //spawn and then launch game object from point of origin
        GameObject newTato = Instantiate(tatoes[0], tatoSpawnPoint.position, transform.rotation);
        newTato.GetComponent<Rigidbody>().AddForce(tatoSpawnPoint.forward * launchSpeed);
        Destroy(newTato,5);
        potatoShootSound.Play();
    }
    void SwitchPesticideUp()
    {
        //Stop previous pesticide
        pesticides[pesticideIndex].Stop();
        //switches to new pesticide
        pesticideIndex++;
        if(pesticideIndex >= pesticides.Count)
        {
            pesticideIndex = 0;
        }

        rotationAmount += 90;
        pesticideChamber.DOLocalRotate(new Vector3(rotationAmount, 0, 0), .2f, RotateMode.Fast);
        ammoSwitchSound.Play();
    }
    void SwitchPesticideDown()
    {
        //same as SwitchPesticideUp but counting down
        pesticides[pesticideIndex].Stop();
        pesticideIndex--;
        if (pesticideIndex < 0)
        {
            pesticideIndex = pesticides.Count - 1;
        }
        rotationAmount -= 90;
        pesticideChamber.DOLocalRotate(new Vector3(rotationAmount, 0, 0), .2f, RotateMode.Fast);
        ammoSwitchSound.Play();
    }
}
