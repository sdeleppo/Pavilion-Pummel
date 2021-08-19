using UnityEngine;
using System.Collections;

public class AIVehicleInput : MonoBehaviour {

    //Handling
    public Vector3 directionToNextWaypoint;
    [Range(0, 1)]
    public float speed = 0.5f;
    public float destinationRange;
    //Components
    TrackManager manager;
    RaceCarManager raceManager;
    WayPoint lastWaypoint;
    public WayPoint goalWaypoint;
    SimpleCarController car;
    Rigidbody r;
    //Internal
    public Vector3 input;
    bool hasGoalPosition;
    public Vector3 goalPosition;
    bool reversing;
    float reverseTime;
    Vector3 relativePoint;
    public Color debugWaypointColor;

	// Use this for initialization
	void Start () {
        r = GetComponent<Rigidbody>();
        car = GetComponent<SimpleCarController>();
        raceManager = GetComponent<RaceCarManager>();
        manager = raceManager.manager;
        goalWaypoint = raceManager.currentWaypoint;
        input.z = speed;
	}

    public void NewGoalWaypoiny(WayPoint point)
    {
        if (point.locationInTrack == manager.wayPoint.Length - 1)
        {
            goalWaypoint = manager.wayPoint[0];
        }
        else
        {
            lastWaypoint = point;
            goalWaypoint = manager.wayPoint[lastWaypoint.locationInTrack+1];
        }
        hasGoalPosition = false;
    }

	// Update is called once per frame
	void Update () 
    {
        if (Time.timeScale == 1)
        {
            while (!hasGoalPosition)
            {
                /*print("trying to find a place to go");
                float x = goalWaypoint.transform.localScale.x/2;
                Vector3 randoPos = goalWaypoint.transform.position;
                randoPos += goalWaypoint.transform.TransformDirection(new Vector3(Random.Range(-x, x), 0, 0));

                if (goalWaypoint.GetComponent<Collider>().bounds.Contains(randoPos))
                {
                    goalPosition = randoPos;
                    hasGoalPosition = true;
                }*/

                goalPosition = goalWaypoint.transform.position + (goalWaypoint.transform.forward * destinationRange);
                hasGoalPosition = true;

            }

            //Get the direction we need to get to the next waypoint
            Vector3 heading = goalWaypoint.transform.position - transform.position;
            float distance = heading.magnitude;
            //directionToNextWaypoint = heading / distance;
            // transform.InverseTransformPoint(directionToNextWaypoint);

            //WORKING attempt
            relativePoint = transform.InverseTransformPoint(goalPosition);


            //Drive the vehicle
            input = new Vector3(0, 0, speed);


            if (relativePoint.x >= .5f) { input.x = 1; }
            if (relativePoint.x <= -.5f) { input.x = -1; }
            if (relativePoint.x > .5f && relativePoint.x < -.5f) { input.x = 0; }


            ReverseCheck();

            car.ControlVehicle(input);

        }
	}

    void ReverseCheck()
    {
        //Check for the need to reverse
        if (!reversing) { reverseTime += Time.deltaTime; }

        if (r.velocity.magnitude <= 2 && reverseTime >= 4)
        {
            if (!reversing)
            {
                reversing = true;
                reverseTime = 0;
            }
        }

        if (reversing)
        {
            reverseTime -= Time.deltaTime;
            input.z = -1;

            if (relativePoint.x >= .5f) { input.x = -1; }
            if (relativePoint.x <= -.5f) { input.x = 1; }

            if (reverseTime < -2.5f)
            {
                reversing = false;
                input.z = speed;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugWaypointColor;
        Gizmos.DrawWireSphere(goalPosition, 1);
    }
}
