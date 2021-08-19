using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : MonoBehaviour {

	//Components
	public Text weaponName;
	public Text score;
	public Text timer;
	public Slider health;
	private PlayerHealth h;
	private WeaponSwitch w;
	//Internal
	float time;
	ScoreManager s;
	int prevScore;
	Color scoreDefCol;

	// Use this for initialization
	void Start () {
		h = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		w = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSwitch>();
		s = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		time += Time.deltaTime;

		health.value = h.health;
		weaponName.text = w.currentWeapon.name;
		timer.text = Mathf.RoundToInt(time)+" Seconds";
		score.text = s.score+" Points";

		if (prevScore < s.score)
		{
			StartCoroutine("ScoreFlash");//increase size for a sec
			prevScore = s.score;
		}

	}

	IEnumerator ScoreFlash()
	{
		score.fontSize = 52;
		yield return new WaitForSeconds(0.1f);
		score.fontSize = 49;
	}

}
//
//
//
//
