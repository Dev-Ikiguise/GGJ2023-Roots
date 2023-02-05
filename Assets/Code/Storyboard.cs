using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyboard : MonoBehaviour
{
    public UnityEngine.UI.Image targetImage;

    public List<Sprite> sprites;
    public List<float> timeIntervals;
    public float defaultInterval = 2.5f;

    public AudioSource audioSource;
    public AudioClip PageFlip1;
    public AudioClip PageFlip2;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartStory());   
    }

    IEnumerator StartStory()
    {
        for (int i = 0; i < sprites.Count; i++)
        {

            audioSource.PlayOneShot(i % 2 == 0 ? PageFlip1 : PageFlip2);

            targetImage.sprite = sprites[i];
            float secondsToWait = (i >= timeIntervals.Count ? defaultInterval : timeIntervals[i]);
            yield return new WaitForSeconds(secondsToWait);
        }
        FadeManager.Instance.SetTransition();
    }


}
