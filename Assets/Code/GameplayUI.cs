using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;

    public List<GameObject> pesticideCanImages;
    public Image trapImage;

    void Awake()
    {
        Instance = this;
    }

    public void ToggleTrapIcon()
    {
        if (trapImage.color == Color.white)
        {
            trapImage.color = Color.gray;
        }
        else
        {
            trapImage.color = Color.white;
        }
    }

    public void ShowCurrentPesticide(int index)
    {
        foreach (GameObject image in pesticideCanImages)
        {
            image.SetActive(false);
        }
        pesticideCanImages[index].SetActive(true);
    }
}
