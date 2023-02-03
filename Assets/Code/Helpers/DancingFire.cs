using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingFire : MonoBehaviour
{

    private Vector3 position;
    public float offset = .75f;
    private Vector3 target;
    private bool ismoving = false;
    private float time = 0;
    private float timeToReachTarget;
    public float targetTimeMin = .05f;
    public float targetTimeMax = .4f;
    private Vector3 startPosition;

    public float spotAngleOffset = 15;
    private float angle;
    private float targetAngle;
    private Light firelight;
    private float startAngle;

    public float colorROffset;
    public float colorGOffset;
    public float colorBOffset;
    private Color color;
    private Color targetColor;
    private Color startColor;


    void Start()
    {
        position = transform.position;
        startPosition = position;
        firelight = GetComponent<Light>();
        angle = firelight.spotAngle;
        color = firelight.color;
    }

    // Update is called once per frame
    void Update()
    {
        ismoving = !(Mathf.Approximately(transform.position.x, target.x) && Mathf.Approximately(transform.position.z, target.z) && Mathf.Approximately(transform.position.z, target.z)) && time < timeToReachTarget;

        if (ismoving)
        {
            time += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPosition, target, time);
            firelight.spotAngle = Mathf.Lerp(startAngle, targetAngle, time);
            firelight.color = Color.Lerp(startColor, targetColor, time);
        }
        else
        {
            time = 0;
            timeToReachTarget = Random.Range(targetTimeMin, targetTimeMax);
            startPosition = transform.position;
            target = new Vector3(Random.Range(position.x - offset, position.x + offset), Random.Range(position.y - offset, position.y + offset), Random.Range(position.z - offset, position.z + offset));
            startAngle = firelight.spotAngle;
            targetAngle = Random.Range(angle - spotAngleOffset, angle + spotAngleOffset);
            startColor = firelight.color;
            targetColor = new Color(Random.Range(color.r - colorROffset, color.r + colorROffset), Random.Range(color.g - colorGOffset, color.g + colorGOffset), Random.Range(color.b - colorBOffset, color.b + colorBOffset));
        }
    }
}
