using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubmitHighscore : MonoBehaviour {

	public int score;//GET A REFERENCE OF THE PLAYER'S CURRENT SCORE HERE
	public InputField playerName;//THIS IS WHERE THEY ENTER THEIR NAME
	ScoreManager s;

	void Start()
	{
		s = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreManager>();
	}

	public void SubmitScore()
	{
		score = s.score;

		if (playerName.text.Length > 0)
		{
			Highscores.AddNewHighScore(playerName.text, score);
			this.gameObject.SetActive(false);
		}
	}
}
