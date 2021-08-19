using UnityEngine;
using System.Collections;

public class ExplodeEnemyHealth : MonoBehaviour {
	
	//Handling
	public float health;
	public float knockbackAmount = 10f;
	
	//Components
	public ParticleSystem splatter;
	public Renderer r;
	private Rigidbody rigid;
	Animator anim;
	NavMeshAgent agent;
	Collider col;
	KillManager kill;
    ExplodeEnemy explode;
	
	//System
	private Color startColor;
	
	void Start()
	{
        explode = GetComponent<ExplodeEnemy>();
		startColor = r.material.color;
		rigid = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		col = GetComponent<Collider>();
		kill = GameObject.FindGameObjectWithTag("KillManager").GetComponent<KillManager>();
	}
	
	public void Damage(float amt)
	{
		splatter.Play ();
		health -= amt;
		rigid.AddForce (-transform.forward * knockbackAmount);//Do knockback
		StartCoroutine("HitFlash");
		anim.SetTrigger ("Hit");
		rigid.isKinematic = true;
		
		if (health <= 0)
		{
			Dead ();
		}
	}
	public void Dead ()
	{
		health = 0;
		kill.EnemyKilled ();
		col.enabled = false;
		agent.ResetPath ();
		agent.enabled = false;
		Destroy (gameObject, 2.5f);
		anim.SetBool ("Dead", true);
		rigid.isKinematic = true;
	}
	
	IEnumerator HitFlash()
	{
		r.material.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		r.material.color = startColor;
	}
}
