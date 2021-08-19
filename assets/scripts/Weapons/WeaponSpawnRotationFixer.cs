using UnityEngine;
using System.Collections;

public class WeaponSpawnRotationFixer : MonoBehaviour {
   
    public Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = cam.transform.TransformDirection (Input.mousePosition);

		//transform.LookAt(mousePos);
	}
}
