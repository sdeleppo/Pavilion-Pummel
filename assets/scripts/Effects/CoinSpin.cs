using UnityEngine;
using System.Collections;

public class CoinSpin : MonoBehaviour {

	public float speed = 50;

	// Use this for initialization
	void Start () {
		speed += Random.Range (-1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * speed * Time.deltaTime);
	}
}
