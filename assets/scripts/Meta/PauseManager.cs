using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

	//Handing
	public string menuName;
	public GameObject highscore;
	public GameObject hud;
	//Internal
	bool paused;
	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetButtonDown ("Pause"))
		{
			paused = !paused;

			if (paused){anim.SetBool ("Show", true);}
			if (!paused){anim.SetBool ("Show", false);}
		}
	}

	public void ShowHighScore()
	{
		highscore.SetActive(true);
        highscore.GetComponent<AudioSource>().enabled = false;
		hud.SetActive(false);
		this.gameObject.SetActive(false);
	}

	public void SetTime(float scale)
	{
		if (paused){Time.timeScale = 0;}
		if (!paused){Time.timeScale = 1;}
	}

	public void ResumeGame()
	{
		paused = false;
		anim.SetBool ("Show", false);
	}

	public void QuitToMainMenu()
	{
		Application.LoadLevel(menuName);
		Time.timeScale = 1;
	}

}
