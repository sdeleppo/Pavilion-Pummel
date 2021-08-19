using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int score = 0;
	public AudioClip getPoints;
	AudioSource a;

	void Start()
	{
		a = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void Score (int amt) {
		score+=amt;
		//a.Play();
	}
}
