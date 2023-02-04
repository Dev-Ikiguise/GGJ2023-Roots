using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public GameEventManager Instance { get; private set; }

    bool isPaused = false;

    bool isQuitting = false;
    public GameObject confirmationUI;
    public AudioSource pauseAudioSource;

    public bool useTimescalePause = true;
    public bool useFindObjectPause = true;
    public List<string> findObjectPauseTags; 


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            if (pauseAudioSource != null)
            {
                if (isPaused)
                {
                    SoundManager.Instance.bgmMusicManager.Pause();
                    pauseAudioSource.Play();
                }
                else
                {
                    pauseAudioSource.Stop();
                    SoundManager.Instance.bgmMusicManager.UnPause();
                }
            }

            Time.timeScale = (isPaused ? 0 : 1);
        }

        if (Input.GetButtonDown("Quit"))
        {
            isQuitting = !isQuitting;

            if (isQuitting && confirmationUI == null)
            {
                Application.Quit();
            }
            else
            {
                confirmationUI.SetActive(isQuitting);
            }
        }

        if (isQuitting)
        {
            // Do menu navigation for buttons
        }
        

    }

}
