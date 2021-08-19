using UnityEngine;
using System.Collections;

public class EnemyBeamIn : MonoBehaviour {

	public bool done;
	public Transform beam;

	float timer;

	void Start()
	{
		beam.gameObject.SetActive(true);
		timer = 1;
	}

	// Update is called once per frame
	void Update () 
	{
		beam.localScale = Vector3.MoveTowards (beam.localScale, new Vector3 (0, 20, 0), Time.deltaTime *2);
		if (timer > -1){
			timer -= Time.deltaTime;
		}

		if (timer <=0)
		{
			done = true;
			beam.gameObject.SetActive(false);
		}

	}
}
