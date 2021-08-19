using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	//Handling
	public float health;
	public float knockbackAmount = 10f;
	public bool leaveSplatter;

	//Components
	public GameObject bloodSplatter;
	public ParticleSystem splatter;
	public Renderer r;
	private Rigidbody rigid;
	Animator anim;
	NavMeshAgent agent;
	Collider col;
	KillManager kill;

	//System
	private Color startColor;
	bool knockingBack;

	void Start()
	{
		startColor = r.material.color;
		rigid = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		col = GetComponent<Collider>();
		kill = GameObject.FindGameObjectWithTag("KillManager").GetComponent<KillManager>();
	}

	void Damage(float amt)
	{
		splatter.Play ();
		health -= amt;
		StartCoroutine ("KnockBack");//Do knockback
		StartCoroutine("HitFlash");
		anim.SetTrigger ("Hit");
		rigid.isKinematic = true;
		
		if (health <= 0)
		{
			kill.EnemyKilled();
			col.enabled = false;
			agent.ResetPath();
			agent.enabled = false;
			Destroy(gameObject, 2.5f);
			anim.SetBool ("Dead", true);
			Vector3 randomY = new Vector3(0, Random.Range (-360, 360), 0);
			if (leaveSplatter){Instantiate (bloodSplatter, transform.position + bloodSplatter.transform.position, bloodSplatter.transform.rotation);}
		}
	}

	IEnumerator KnockBack()
	{
		knockingBack = true;//mark knockback
		agent.ResetPath();
		rigid.AddForce (-transform.forward * knockbackAmount);//knockback
		yield return new WaitForSeconds(0.5f);//wait a half second
		knockingBack = false;//mark knockback
	}

	IEnumerator HitFlash()
	{
		r.material.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		r.material.color = startColor;
	}
}
