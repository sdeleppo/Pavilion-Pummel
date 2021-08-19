using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int addAmount = 100;
	public ParticleSystem effect;
	ScoreManager score;

	void Start()
	{
		score = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreManager>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerHealth>().health += 5;
			score.Score (addAmount);
			effect.gameObject.SetActive(true);
			effect.transform.parent = null;
			Destroy (effect.gameObject, 2);
			Destroy (gameObject);
		}
	}
}
