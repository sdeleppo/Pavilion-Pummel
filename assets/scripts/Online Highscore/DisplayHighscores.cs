using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHighscores : MonoBehaviour {

	public Text[] highscoreText;
	Highscores highscoreManager;


	void Start () {
		for (int i = 0; i < highscoreText.Length; i++)
		{
			highscoreText[i].text = i+1 + ". Fetching...";
		}

		highscoreManager = GetComponent<Highscores>();

		StartCoroutine("RefreshHighscores");
	}

	public void OnHighscoresDownloaded(Highscore[] highscoreList)
	{
		for (int i = 0; i < highscoreText.Length; i++)
		{
			highscoreText[i].text = i+1 + ". ";
			if (highscoreList.Length > i)
			{
				highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
			}
			else
			{
				highscoreText[i].text = null;
			}
		}
	}

	IEnumerator RefreshHighscores()
	{
		while (true)
		{
			highscoreManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}


}
