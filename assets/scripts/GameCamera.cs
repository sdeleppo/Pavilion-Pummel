﻿using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Vector3 cameraTarget;

	private Transform target;


	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	

	void Update () {
		cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
		transform.position = Vector3.Lerp (transform.position, cameraTarget, Time.deltaTime * 8);

		if (!target)
		{
			if (GameObject.FindGameObjectWithTag("Player").transform != null)
			{
				target = GameObject.FindGameObjectWithTag("Player").transform;
			}
		}

	}
}
