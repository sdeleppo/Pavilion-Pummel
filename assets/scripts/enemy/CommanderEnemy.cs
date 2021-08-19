using UnityEngine;
using System.Collections;

public class CommanderEnemy : MonoBehaviour {

	//Handling
	public Transform player;
	public float approachDistance = 6;
	public float AIRate = 1;
	public float range = 2f;
	public float damage;
	public float fireRate;
	//Components
	public GameObject projectile;
	private NavMeshAgent agent;
	private Animator anim;
	private EnemyFreeze freeze;
	private EnemyBeamIn beam;
	private Rigidbody rigid;
	EnemyHealth h;
	public Transform followTrans;
	public Transform followTransParent;
	//Internal
	float coolDown;
	float rotateSpeed;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		freeze = GetComponent<EnemyFreeze>();
		h = GetComponent<EnemyHealth>();
		beam = GetComponent<EnemyBeamIn>();
		rigid = GetComponent<Rigidbody>();
		rotateSpeed = Random.Range (30f, 40f);
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
			if (dist < approachDistance)//Walk AROUND the player
			{
				if (followTransParent.parent != null){followTransParent.parent = null;}
				followTransParent.position = player.position;
				followTrans.RotateAround (player.position, Vector3.up, rotateSpeed);//rotate the transform around the player at a random speed
				agent.SetDestination(followTrans.position);//set your destination to the transform

				//and shoot at them
			}

			
			yield return new WaitForSeconds(AIRate);
		}
	}
}
