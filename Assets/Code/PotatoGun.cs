using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PotatoGun : MonoBehaviour
{
    public Light flashlight;
    public Light flashlightTwo;
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
    bool isPlayingSpraySound;
    public AudioSource spraySound;
    public AudioSource ammoSwitchSound;
    public AudioSource potatoShootSound;
    public AudioSource flashlightSound;
    float initalZPosition;
    public Rigidbody fpsRigidBody;
    public Transform gunModel;

    public GameObject hintText;

    // Start is called before the first frame update
    void Start()
    {
        pesticideIndex = 0;
        isPlayingSpraySound = false;
        initalZPosition = transform.localPosition.z;
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
            hintText.SetActive(false);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            SwitchPesticideDown();
            hintText.SetActive(false);
        }
    }

    void ToggleFlashLight()
    {
        flashlight.enabled = !flashlight.enabled;
        flashlightTwo.enabled = !flashlightTwo.enabled;

        if (flashlight.enabled)
        {
            flashlightSound.pitch = 0.9f;
        }
        else
        {
            flashlightSound.pitch = 1.02f;
        }
        flashlightSound.Play();
    }
    void FirePesticide()
    {
        if (!isPlayingSpraySound)
        {
            spraySound.Play();
            isPlayingSpraySound = true;
        }
        pesticides[pesticideIndex].Play();
    }

    void StopPesticide()
    {
        pesticides[pesticideIndex].Stop();
        isPlayingSpraySound = false;
        spraySound.Stop();
    }

    void Firetato() //Fires potato from launcher
    {
        //spawn and then launch game object from point of origin
        int rand = Random.Range(0, tatoes.Count);
        GameObject newTato = Instantiate(tatoes[rand], tatoSpawnPoint.position, transform.rotation);
        print("fpsRigidBody.velocity" + fpsRigidBody.velocity);
        newTato.GetComponent<Rigidbody>().AddForce((tatoSpawnPoint.forward * launchSpeed) + (fpsRigidBody.velocity * 30));
        Destroy(newTato,5);
        potatoShootSound.pitch = Random.Range(0.92f, 1.08f);
        potatoShootSound.Play();
        Recoil();
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
        ammoSwitchSound.pitch = 0.95f;
        ammoSwitchSound.Play();

        GameplayUI.Instance.ShowCurrentPesticide(pesticideIndex);
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
        ammoSwitchSound.pitch = 1.08f;
        ammoSwitchSound.Play();

        GameplayUI.Instance.ShowCurrentPesticide(pesticideIndex);
    }

    void Recoil()
    {
        //StopCoroutine("RecoilCo");
        StartCoroutine(RecoilCo());
    }

    private IEnumerator RecoilCo()
    {
        float recoilDuration = .04f;
        gunModel.transform.DOLocalMoveZ(initalZPosition - .03f, recoilDuration, false);//.SetEase(Ease.InOutCirc);
        yield return new WaitForSeconds(recoilDuration);
        gunModel.transform.DOLocalMoveZ(initalZPosition, recoilDuration * 2, false);//.SetEase(Ease.InOutQuad);
    }
}
