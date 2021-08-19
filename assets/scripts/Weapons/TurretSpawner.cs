using UnityEngine;
using System.Collections;

public class TurretSpawner : MonoBehaviour {

	//Handling
	public float fireRate=1;
	float coolDown;
	public Vector3 spawnForce;
	//Components
	public Transform spawn;
	public GameObject turret;
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.root.tag == "Player")
		{
			
			coolDown -= Time.deltaTime;
			
			if (Input.GetButton ("Fire1") && coolDown<= 0)
			{
				GameObject t = Instantiate (turret, spawn.position, spawn.rotation) as GameObject;
				t.GetComponent<Rigidbody>().AddRelativeForce(spawnForce);
				coolDown = fireRate;
			}
		}
	}
}
