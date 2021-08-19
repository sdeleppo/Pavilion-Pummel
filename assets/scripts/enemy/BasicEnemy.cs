using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {

	//Handling
	public Transform player;
	public float approachDistance;
	public float attackDistance;
	public float AIRate = 1;
	public float range = 2f;
	public float damage;
	//Components
	private NavMeshAgent agent;
	private Animator anim;
	private EnemyFreeze freeze;
	private EnemyBeamIn beam;
	private Rigidbody rigid;
	EnemyHealth h;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		freeze = GetComponent<EnemyFreeze>();
		h = GetComponent<EnemyHealth>();
		beam = GetComponent<EnemyBeamIn>();
		rigid = GetComponent<Rigidbody>();
	}

	void OnEnable()
	{
		StartCoroutine("RunAI");//start the AI decisions
	}

	void Update()
	{
		anim.SetFloat ("Speed", agent.velocity.magnitude);

		if (h.health <= 0)
		{
			//play a popping sound effect
			Destroy (gameObject);
		}

	}

	public void Attack()
	{
		if (Vector3.Distance (transform.position, player.position) < attackDistance)
		{
			player.gameObject.SendMessage("Damage", damage);
		}
	}

	IEnumerator RunAI()
	{
		while (true && player!= null && h.health > 0 && rigid.isKinematic == true)
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
				if (dist < attackDistance)//Attack the player
				{
					agent.ResetPath();//attack
					anim.SetTrigger ("Attack");
				}
				
				yield return new WaitForSeconds(AIRate);
		}
	}

}
