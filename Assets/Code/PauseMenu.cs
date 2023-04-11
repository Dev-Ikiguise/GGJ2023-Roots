using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject hint;
    public GameObject container;

    void Start()
    {
        if (PlayerPrefs.GetInt("hasSeenPauseMenu") != 1)
        {
            hint.SetActive(true);
            container.SetActive(true);
        }
    }
}
