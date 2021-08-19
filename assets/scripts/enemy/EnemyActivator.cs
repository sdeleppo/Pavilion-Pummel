using UnityEngine;
using System.Collections;

public class EnemyActivator : MonoBehaviour {
	public GameObject enemies;

	// Use this for initialization
	void Start () 
	{
		enemies.SetActive(false);
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider player) 
	{
		if (player.tag == "Player")
		{
			enemies.SetActive(true);
			this.gameObject.SetActive(false);
		}
	}
}
