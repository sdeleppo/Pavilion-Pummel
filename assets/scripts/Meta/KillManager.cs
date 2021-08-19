using UnityEngine;
using System.Collections;

public class KillManager : MonoBehaviour {

	//Handling
	public int currentStreak;
	public float resetTime = 3;
	public AudioClip[] cheer;
	//Internal
	AudioSource a;
	float coolDown;

	void Start()
	{
		a = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
	{
		coolDown -= Time.deltaTime;

		if (coolDown <= 0)
		{
			currentStreak = 0;
		}

	}

	public void EnemyKilled()
	{
		currentStreak ++;
		coolDown = resetTime;

		switch (currentStreak)//decide if to play a sound
		{
		case (5):
			a.PlayOneShot (cheer[0]);
			break;
		case (10):
			a.PlayOneShot (cheer[1]);
			break;
		case (15):
			a.PlayOneShot (cheer[3]);
			break;
		}

	}

}
