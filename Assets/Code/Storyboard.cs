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
            switch (i)
            {
                case 0:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 3:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 4:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 5:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 8:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 11:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 12:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 13:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 16:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                case 17:
                    audioSource.PlayOneShot(PageFlip1);
                    break;
                default:
                    break;
            }

            targetImage.sprite = sprites[i];
            float secondsToWait = (i >= timeIntervals.Count ? defaultInterval : timeIntervals[i]);
            yield return new WaitForSeconds(secondsToWait);
        }
        FadeManager.Instance.SetTransition();
    }


}
