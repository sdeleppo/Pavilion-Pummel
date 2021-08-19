using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	//Handling
	public float fireRate;
	public float damage;
	public float life = 5;
	public int score = 2;
	//Internal
	public bool activated;
	float coolDown;
	//Components
	public GameObject scoreManager;
	ScoreManager s;
	AudioSource a;
	public AudioClip shotSound;
	public Transform turretHead;
	Rigidbody r;
	LineRenderer l;

	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody>();
		l = GetComponent<LineRenderer>();
		a = GetComponent<AudioSource>();
		scoreManager = GameObject.FindGameObjectWithTag("score");
		s = scoreManager.GetComponent<ScoreManager>();
	}

	void Update()
	{
		if (activated)
		{
			coolDown-=Time.deltaTime;
			if (coolDown <=0)
			{
				Attack ();
				coolDown = fireRate;
			}

			Destroy (gameObject, life);
		}
	}

	void Attack()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
		bool shot = false;

		for (int i = 0; i < hitColliders.Length;i++) {
			if (hitColliders[i].transform.tag == "Enemy" && !shot)
			{
				a.clip = shotSound;
				a.Play ();
				turretHead.LookAt (hitColliders[i].transform.position);
				hitColliders[i].SendMessage ("Damage", damage);//send damage
				StartCoroutine("RayEffect", hitColliders[i].transform.position);//play the effect
				shot = true;//dont shoot again

				s.Score(score);
			}
		}
	}

	IEnumerator RayEffect(Vector3 end)
	{
		l.enabled = true;
		l.SetPosition (0, transform.position);
		l.SetPosition (1, end);
		yield return new WaitForSeconds(0.1f);
		l.enabled = false;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.tag == "Ground")
		{
			activated = true;
			r.isKinematic = true;
		}
	}
}
