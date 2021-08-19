using UnityEngine;
using System.Collections;

public class VortexGun : MonoBehaviour {

	public GameObject vortex;
	public Transform spawn;
	GameObject cam;

	//Handling
	public Vector3 shootForce;

	void Start()
	{
		cam = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.root.tag == "Player")
		{
			if (Input.GetButtonDown ("Fire1"))
			{
				GameObject vortexBall = Instantiate (vortex, spawn.position, spawn.rotation) as GameObject;
				vortexBall.GetComponent<Rigidbody>().AddRelativeForce(shootForce);
				cam.SendMessage ("Shake", 2);
			}
		}
	}	
}
