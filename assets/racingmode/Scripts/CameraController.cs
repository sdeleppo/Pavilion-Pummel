using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float followSpeed = 8f;
    public float rotationSpeed = 80;
    public Transform target;
    public Vector3 offset;
    Transform cam;
    public bool autoCam;
    float autoRotateSpeed = 0;
    Vector3 relativePoint;

    void Start()
    {
        cam = transform.GetChild(0);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

        if (autoCam)
        {
            relativePoint = cam.InverseTransformPoint(target.position);
            

            if (relativePoint.x >=.2f) { autoRotateSpeed = Mathf.Lerp(autoRotateSpeed, 25, Time.deltaTime * 25); }//its to the right
            if (relativePoint.x <= -.2f) { autoRotateSpeed = Mathf.Lerp(autoRotateSpeed, -25, Time.deltaTime * 25);  }//its to the left
            if (relativePoint.x > -.2f && relativePoint.x < .2f) { autoRotateSpeed = Mathf.Lerp(autoRotateSpeed, 0, Time.deltaTime * 25); }

            Vector3 rSpeed = new Vector3(0, autoRotateSpeed, 0);

            transform.localEulerAngles += rSpeed * Time.deltaTime;


        }
        else
        {
            transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
        }
	}
}
