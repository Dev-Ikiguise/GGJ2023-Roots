using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    Light daylight;
    public float daylength;
    public float nightlength;
    public float transitionlength;

    float timeInState = 0;
    bool isDay = true;
    bool isTransitioning = false;


    private void Awake()
    {
        daylight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timeInState += Time.deltaTime;

        if (!isTransitioning && (isDay && timeInState > daylength) || (!isDay && timeInState > nightlength))
        {
            isTransitioning = true;
            timeInState = 0;
        }

        if (isTransitioning)
        {
            if (isDay)
            {
                daylight.color = Color.Lerp(daylight.color, Color.black, timeInState / transitionlength);
                if (daylight.color == Color.black)
                {
                    isTransitioning = false;
                    isDay = false;
                    timeInState = 0;
                }
            }
            else
            {
                daylight.color = Color.Lerp(daylight.color, Color.white, timeInState / transitionlength);
                if (daylight.color == Color.white)
                {
                    isTransitioning = false;
                    isDay = true;
                    timeInState = 0;
                }
            }
        }

        //float colVal = 

        //light.color = new Color(colVal, colVal, colVal);
    }
}
