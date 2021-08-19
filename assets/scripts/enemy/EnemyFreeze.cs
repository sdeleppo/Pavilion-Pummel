using UnityEngine;
using System.Collections;

public class EnemyFreeze : MonoBehaviour {

	//Handling
	public float cold;
	public bool frozen;
	//Internal
	float frozenCoolDown;
	Color startColor;
	float startSpeed;
	//Components
	public Renderer r;
	public NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		startColor = r.material.color;
		startSpeed = agent.speed;
	}

	void OnParticleCollision(GameObject other) 
	{
		if (other.name == "Snow")
		{
			cold ++;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (cold > 15){cold=15;}

		if (cold > 10 && !frozen)
		{
			frozen = true;
			frozenCoolDown = 7.5f;
			r.material.color = Color.blue;
		}
		if (frozen)
		{
			frozenCoolDown -= Time.deltaTime;
			agent.speed = 0;
			if (frozenCoolDown <=0)
			{
				agent.speed = startSpeed;
				frozen = false;
				cold = 0;
				r.material.color = startColor;
			}
		}
	}

}
