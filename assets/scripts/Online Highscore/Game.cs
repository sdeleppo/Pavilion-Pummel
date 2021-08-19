using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			int score = Random.Range (0, 2000);
			string username = "";
			string alphabet = "abcdefghijklmnopqrstuvwxyz";

			for (int i = 0; i < Random.Range (5, 10); i ++)
			{
				username += alphabet[Random.Range (0, alphabet.Length)];
			}

			Highscores.AddNewHighScore(username, score);

		}
	}
}
