using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {

	//Handling variables
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;
	public float acceleration = 5;
	public bool canMove = true;
    public bool useControllerInput;

	// System variables
	private Quaternion targetRotation;
	private Vector3 currentVelocityModifier;
    public Vector3 controllerLookInput;

	// Components
	private CharacterController controller;
	private Camera cam;
    private Transform camParent;
	private Animator anim;
	Vector3 input;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		cam = Camera.main;
        camParent = cam.transform.parent;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
        if (useControllerInput)
        {
            ControlController();
        }
        else
        {
            ControlMouse();
        }

        if (Input.GetKeyDown(KeyCode.Semicolon)) { useControllerInput = !useControllerInput; }

		anim.SetFloat ("Velocity", controller.velocity.magnitude);
		Vector3 relVel = transform.InverseTransformDirection (controller.velocity);
		anim.SetFloat ("X", relVel.x);
		anim.SetFloat ("Z", relVel.z);
	}

    void ControlController()
    {
        controllerLookInput = new Vector3(Input.GetAxis("JoyRightHor"), 0, Input.GetAxis("JoyRightVert"));
        Vector3 lookPos = transform.position + controllerLookInput;

        targetRotation = Quaternion.LookRotation(lookPos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        //transform.LookAt(new Vector3(lookPos.x, transform.position.y, lookPos.z));

        input = new Vector3(Input.GetAxisRaw("JoyLeftHor"), 0, Input.GetAxisRaw("JoyLeftVert"));
        currentVelocityModifier = Vector3.MoveTowards(currentVelocityModifier, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityModifier;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        if (canMove)
        {
            controller.Move(motion * Time.deltaTime);
        }
    }

	void ControlMouse()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
		targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

		input = new Vector3(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		currentVelocityModifier = Vector3.MoveTowards(currentVelocityModifier, input, acceleration * Time.deltaTime);
		Vector3 motion = currentVelocityModifier;
		motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.z) == 1)?.7f:1;
		motion *= (Input.GetButton ("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;
		
		if (canMove)
		{
			controller.Move (motion * Time.deltaTime);
		}
		//anim.SetFloat ("Speed", Mathf.Sqrt(motion.x * motion.x + motion.z * motion.z));

	}

	void ControlWASD()
	{
		Vector3 input = new Vector3(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		
		if (input != Vector3.zero)
		{
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		}

		currentVelocityModifier = Vector3.MoveTowards(currentVelocityModifier, input, acceleration * Time.deltaTime);
		Vector3 motion = currentVelocityModifier;
		motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.z) == 1)?.7f:1;
		motion *= (Input.GetButton ("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;
		
		
		controller.Move (motion * Time.deltaTime);

	}

}
