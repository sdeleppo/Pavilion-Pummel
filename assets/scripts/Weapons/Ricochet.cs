using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {

	//Handling
	public int hits = 5;
	public int damage = 10;
	public float accuracy;
	public int score = 20;
	//Components
	public ParticleSystem p;
	public Rigidbody r;
	public AudioSource a;
	public GameObject scoreManager;
	ScoreManager s;

	// Use this for initialization
	void Start () {
		Vector3 a = new Vector3 (Random.Range (-accuracy, accuracy), 0, Random.Range (-accuracy, accuracy));
		r.AddRelativeForce((Vector3.forward*400)+a);
		Destroy (gameObject, 5);
		scoreManager = GameObject.FindGameObjectWithTag("score");
		s = scoreManager.GetComponent<ScoreManager>();
	}

	void OnCollisionEnter(Collision col)
	{
		p.Play ();
		a.Play ();
		score += 10;
		if (col.transform.tag == "Enemy")
		{
			s.Score(score);
			col.gameObject.SendMessage("Damage", damage);
		}

		hits --;
		if (hits <= 0){
			GameObject pt = p.gameObject;
			p.transform.parent = null;
			Destroy(p, 2);
			Destroy (gameObject);
		}
	}

}
