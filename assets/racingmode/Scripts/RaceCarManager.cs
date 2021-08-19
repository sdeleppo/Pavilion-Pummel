using UnityEngine;
using System.Collections;

public class RaceCarManager : MonoBehaviour {

    public TrackManager manager;
    public WayPoint currentWaypoint;
    AIVehicleInput ai;
    public int currentLap = 1;
    public bool finished;
    public float timeSpentRacing;
    public bool isAI;
    public AudioClip lapComplete;
    AudioSource a;
    Rigidbody rigid;

	// Use this for initialization
	void Awake () {
        rigid = GetComponent<Rigidbody>();
        a = GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("TrackManager").GetComponent<TrackManager>();
        currentWaypoint = manager.wayPoint[0].GetComponent<WayPoint>();

        if (GetComponent<AIVehicleInput>() != null) { ai = GetComponent<AIVehicleInput>(); }
	}

    void Update()
    {
        //DetectFlip();

        if (Input.GetButtonDown("VehicleReset") && !isAI)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            transform.position = new Vector3(transform.position.x, 2.6f, transform.position.z);
        }

        if (!finished && Time.timeScale == 1)
        {
            timeSpentRacing += Time.deltaTime;
        }
    }


    void DetectFlip()
    {
        if (transform.localEulerAngles.z < 91 && transform.localEulerAngles.z > 10)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            transform.position = new Vector3(transform.position.x, 2.6f, transform.position.z);
        }
    }

    public void UpdateLocation(WayPoint waypoint)
    {
        if (ai != null) { ai.NewGoalWaypoiny(waypoint); }


        //Check if we finished a lap
        if (waypoint.locationInTrack == 0 && currentWaypoint.locationInTrack == manager.wayPoint.Length - 1)
        {

            if (currentLap == manager.lapsRequiredToFinish)
            {
                finished = true;
            }
            else
            {
                if (!isAI) { a.PlayOneShot(lapComplete); }
                currentLap++;
            }

        }

        currentWaypoint = waypoint;
    }
}
