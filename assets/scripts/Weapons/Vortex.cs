using UnityEngine;
using System.Collections;

public class Vortex : MonoBehaviour {

	public float radius = 5.0F;
	public float power = 10.0F;
	public float damage = 50;
	public int score = 100;
	public ParticleSystem explode;
	public GameObject scoreManager;
	ScoreManager s;

	void Start()
	{
		InvokeRepeating ("Suck", 0, 0.1f);
		Invoke ("Explode", 2.95f);
		Destroy (gameObject, 3);
		scoreManager = GameObject.FindGameObjectWithTag("score");
		s = scoreManager.GetComponent<ScoreManager>();
	}

	void Suck() {
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			if (hit && hit.GetComponent<Rigidbody>() && hit.tag != "Weapon")
				hit.GetComponent<Rigidbody>().AddExplosionForce(-power, explosionPos, radius, 0);
			
		}
	}

	void Explode() {
		ParticleSystem e = explode;
		e.Play ();
		e.transform.parent = null;
		Destroy (e.gameObject, 2);
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			s.Score(score/10);

			if (hit && hit.GetComponent<Rigidbody>() && hit.tag != "Weapon" && hit.tag != "Player")
				hit.GetComponent<Rigidbody>().AddExplosionForce(power*2, explosionPos, radius, 0);
			hit.gameObject.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}

}
