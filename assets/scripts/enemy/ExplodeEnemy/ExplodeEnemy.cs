using UnityEngine;
using System.Collections;

public class ExplodeEnemy : MonoBehaviour {
	
	//Handling
	public Transform player;
	public float approachDistance;
	public float attackDistance;
	public float AIRate = 1;
	public float range = 2f;
	public float damage;
	public ParticleSystem explosion;
	public bool dead;
	public bool attackedonce;
	public float radius = 5.0F;

	//Components
	private NavMeshAgent agent;
	private Animator anim;
	private EnemyFreeze freeze;
	private EnemyBeamIn beam;
	ExplodeEnemyHealth h;

	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		freeze = GetComponent<EnemyFreeze>();
		h = GetComponent<ExplodeEnemyHealth>();
		beam = GetComponent<EnemyBeamIn>();
		//StartCoroutine("RunAI");
	}
	
	void OnEnable()
	{
		StartCoroutine("RunAI");//start the AI decisions
	}
	
	void Update()
	{
		anim.SetFloat ("Speed", agent.velocity.magnitude);
		if (h.health <= 20 && h.health != 0) 
		{
			//print(h.health);
			explosion.Play();
			h.Dead();
			dead = true;
			if (Vector3.Distance (transform.position, player.position) < attackDistance)
			{
                Invoke("Explode", 0.5f);
			}

		}
		//if (dead)
			//h.Dead();

	}
	
	public void Attack()
	{
		/*if (h.health <= 20) 
		{
			player.gameObject.SendMessage("Damage", damage);
			explosion.Play();
			h.health = 0;
		}*/

		if (Vector3.Distance (transform.position, player.position) < attackDistance && dead != true && attackedonce != true)
		{
            Invoke("Explode", 0.5f);

		}
	}

    public void Explode()
    {
        attackedonce = true;
        dead = true;
        h.Dead();
        h.Damage(500);
        explosion.Play();
        explosion.GetComponent<AudioSource>().Play();
        explosion.transform.parent = null;

        //explode bitch
        //player.gameObject.SendMessage("Damage", damage);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            hit.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
	
	IEnumerator RunAI()
	{

		while (true && player!= null && !freeze.frozen && h.health > 0 && dead != true && attackedonce != true)
		{
			float dist = Vector3.Distance (transform.position, player.position);
			
			if (dist>approachDistance)//GO to a point NEAR the player
			{
				Vector3 random = new Vector3(Random.Range (-3f, 3f), 0, Random.Range (-3f, 3f));
				agent.SetDestination(player.position + random);
			}
			if (dist < approachDistance && dist > attackDistance)//go TO the player
			{
				agent.SetDestination(player.position);//move to the player
			}
			if (dist < attackDistance && attackedonce != true)//Attack the player
			{
				agent.ResetPath();//attack
                Invoke("Explode", 0.5f);
			}
			
			yield return new WaitForSeconds(AIRate);
		}
	}
	
}