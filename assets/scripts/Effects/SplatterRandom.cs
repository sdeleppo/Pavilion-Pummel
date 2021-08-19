using UnityEngine;
using System.Collections;

public class SplatterRandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.eulerAngles += new Vector3(0, Random.Range(-360, 360), 0); //randomize rotation

        transform.position += new Vector3(0, Random.Range(-1f, 1f), 0); //randomize height(therefore size)
	}
	

}
