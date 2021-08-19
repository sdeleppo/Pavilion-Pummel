using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour {

	const string privateCode = "m_of0r-nGU6ezc3P5AY54w_AXQLjpgokOlqaZQNrayXA";
	const string publicCode = "552acebb6e51b605a0dea1ae";
	const string webURL = "http://dreamlo.com/lb/";

	public Highscore[] highscoresList;
	static Highscores instance;
	DisplayHighscores highscoresDisplay;

	void Awake()
	{
		highscoresDisplay = GetComponent<DisplayHighscores>();
		instance = this;
	}

	public static void AddNewHighScore(string username, int score)
	{
		instance.StartCoroutine (instance.UploadNewHighscore(username, score));
	}

	IEnumerator UploadNewHighscore(string username, int score)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			print ("Upload Successful");
			DownloadHighscores();
		}
		else{
			print ("Error uploading: "+ www.error);
		}
	}

	public void DownloadHighscores()
	{
		StartCoroutine ("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;
		
		if (string.IsNullOrEmpty(www.error))
		{
			FormatHighscores (www.text);
			highscoresDisplay.OnHighscoresDownloaded(highscoresList);
		}else{
			print ("Error downloading: "+ www.error);
		}
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split (new char[]{'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];
		for (int i = 0; i < entries.Length; i ++)
		{
			string[] entryInfo = entries[i].Split (new char[]{'|'});
			string username = entryInfo[0];
			int score = int.Parse (entryInfo[1]);
			highscoresList[i] = new Highscore(username, score);
			print (highscoresList[i].username + ": " + highscoresList[i].score);
		}
	}
	

}

public struct Highscore{
	public string username;
	public int score;

	public Highscore(string _username, int _score)
	{
		username = _username;
		score = _score;
	}

}
//
//
//
//
//
//
//
