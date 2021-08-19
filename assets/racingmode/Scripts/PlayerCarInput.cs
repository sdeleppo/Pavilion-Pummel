using UnityEngine;
using System.Collections;

public class PlayerCarInput : MonoBehaviour {

    SimpleCarController car;

	// Use this for initialization
	void Start () {
        car = GetComponent<SimpleCarController>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        car.ControlVehicle(input);
        if (Input.GetButton("Jump")) { car.braking = true; } else { car.braking = false; }
	}
}
