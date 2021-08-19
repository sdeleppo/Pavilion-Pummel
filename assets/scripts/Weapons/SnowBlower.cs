using UnityEngine;
using System.Collections;

public class SnowBlower : MonoBehaviour {

	//Handling

	//Components
	public ParticleSystem p;
	GameObject cam;

	void Start()
	{
		cam = Camera.main.gameObject;
	}

	// Update is called once per frame
	void Update () 
	{
		if (transform.root.tag == "Player")
		{
			if (Input.GetButton ("Fire1"))
			{
                print("snooooooooooow");
				p.Play ();
				//cam.SendMessage ("Shake", .5f);
			}
			else
			{
				p.Stop ();
			}
		}
	}

}
