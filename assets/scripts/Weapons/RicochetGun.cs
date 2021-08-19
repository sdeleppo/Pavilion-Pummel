using UnityEngine;
using System.Collections;

public class RicochetGun : MonoBehaviour {

	//Components
	public Transform spawn;
	public GameObject ric;
	public GameObject flash;
	public PlayerHealth h;
	GameObject cam;
	AudioSource a;
	//Handling
	public AudioClip fireSound;
	public float rpm;
	//Internal
	private float coolDown;

	void Start()
	{
		cam = Camera.main.gameObject;
		a = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (transform.root.tag == "Player")
		{
			coolDown -= Time.deltaTime;
			
			if (Input.GetButton ("Fire1") && coolDown <=0)
			{
				Instantiate(ric, spawn.position, spawn.rotation);
				coolDown = 60/rpm;
				cam.SendMessage ("Shake", 1);
				a.PlayOneShot(fireSound);
				StartCoroutine ("MuzzleFlash");
				h.StartCoroutine(h.KnockBack (0.01f, -25));
			}
		}
	}

	IEnumerator MuzzleFlash()
	{
		flash.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		flash.SetActive(false);
	}

}
