using UnityEngine;
using System.Collections;

public class BeatGame : MonoBehaviour {

	//Handling
	public bool beatenGame;
	public GameObject highscores;
	public GameObject gameBeatenScreen;
	public GameObject oldGameMusic;
	//Internal
	bool reachedEnd;
	bool beatenBoss;

	void Start()
	{
		beatenBoss = true;
	}

	void Update()
	{
		if (reachedEnd && beatenBoss)
		{
			beatenGame = true;
            Time.timeScale = 0.0f;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			reachedEnd = true;
			oldGameMusic.SetActive(false);
			gameBeatenScreen.SetActive(true);
		}
	}

	public void ShowHighscores()
	{
		highscores.SetActive(true);
	}

}
