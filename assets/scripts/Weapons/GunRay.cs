using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]
public class GunRay : MonoBehaviour {

	public Transform start;
	public float distance = 5;
	public LayerMask mask;
	//Components
	LineRenderer l;
	RaycastHit hit;

	void Start()
	{
		l = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {
	
		l.SetPosition (0, start.position);

		if (Physics.Raycast (start.position, transform.forward, out hit, distance, mask))
		{
			l.SetPosition (1, hit.point);
		}else
		{
			l.SetPosition (1, start.position + (transform.forward * distance));
		}

	}
}
