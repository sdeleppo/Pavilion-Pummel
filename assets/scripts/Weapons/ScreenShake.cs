using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	//Handling
	public float speed = 5;
	public float shakeAmount =.5f;
	//Internal
	Vector3 startPos;
	Vector3 shakePos;

	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
		shakePos = transform.localPosition;
	}

	void Update()
	{
		transform.localPosition = Vector3.Lerp (transform.localPosition, shakePos, speed *Time.deltaTime);
	}

	void Shake(float mult)
	{
		StartCoroutine("ShakeTime", mult);
	}

	IEnumerator ShakeTime(float multiplier)
	{
		shakePos = startPos + new Vector3 (Random.Range(-shakeAmount * multiplier, shakeAmount * multiplier), 0, Random.Range(-shakeAmount * multiplier, shakeAmount * multiplier));
		yield return new WaitForSeconds(0.1f);
		shakePos = startPos;
	}

}
