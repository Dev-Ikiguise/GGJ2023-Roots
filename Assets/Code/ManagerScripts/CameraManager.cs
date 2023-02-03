using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager Instance { get; private set; }

	public GameObject player;
	public Vector3 offset = Vector3.zero;
	public float groundHeight;
	float initXRotation;
	public bool reverseXOffset = false;
	public float heightRotationMultiplier = 1;
	public float cameraSpeed = 1;

	public float shakeIntensity = 0;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
    {
		groundHeight = player.transform.position.y;
		initXRotation = transform.rotation.eulerAngles.x;
    }

    void LateUpdate()
	{
		Vector3 shake = Vector3.zero;
		if (shakeIntensity > 0)
		{
			shake = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity));
		}
		transform.position = Vector3.Lerp(transform.position, player.transform.position + (reverseXOffset ? new Vector3(offset.x * -1, offset.y, offset.z) : offset) + shake, Time.deltaTime * cameraSpeed);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(initXRotation + (player.transform.position.y - groundHeight) * heightRotationMultiplier, 0, 0), Time.deltaTime * cameraSpeed);
	}

	public void Shake(float intensity, float fadeTime = 1, float rampTime = 0)
    {
		StartCoroutine(ShakeInternal(intensity, fadeTime, rampTime));
	}

	private IEnumerator ShakeInternal (float intensity, float fadeTime = 1, float rampTime = 0 )
    {
		float timeStart = Time.time;
		while (shakeIntensity < intensity && rampTime != 0)
        {
			shakeIntensity = intensity * ((Time.time - timeStart) / rampTime);
			yield return new WaitForEndOfFrame();
        }
		shakeIntensity = intensity;

		timeStart = Time.time;
		while (shakeIntensity > 0 && fadeTime != 0)
		{
			shakeIntensity = intensity - (intensity * ((Time.time - timeStart) / fadeTime));
			yield return new WaitForEndOfFrame();
		}

		shakeIntensity = 0;
	}
}