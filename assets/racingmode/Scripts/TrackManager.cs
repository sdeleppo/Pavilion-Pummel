using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour {

    public WayPoint[] wayPoint;
    public int lapsRequiredToFinish = 3;
    public List<RacerData> racers;

    void Start()
    {
        //Get all of the racers in a list
        racers = new List<RacerData>();
        GameObject[] rArray = GameObject.FindGameObjectsWithTag("Vehicle");
        foreach(GameObject r in rArray){racers.Add(new RacerData(r.GetComponent<RaceCarManager>(), 1, 0, 0));}
        //Give the racers a different lane to race in
        for (int r = 0; r < racers.Count; r++)
        {
            if (racers[r].car.isAI) 
            {
                float startRange = racers[r].car.GetComponent<AIVehicleInput>().destinationRange * -1;
                float percent = racers[r].car.GetComponent<AIVehicleInput>().destinationRange*2 / racers.Count;

                racers[r].car.GetComponent<AIVehicleInput>().destinationRange = startRange + (percent* (r + 1)); 
            }
        }

        //Give each waypoint its position in relation to the track
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPoint[i].locationInTrack = i;
        }   

        InvokeRepeating("CheckRacersPlace", 1, 0.25f);

    }

    [System.Serializable]
    public class RacerData
    {
        public RaceCarManager car;
        public int currentLap;
        public float distanceFromFinish;
        public float timeSpentRacing;

        public RacerData(RaceCarManager _car, int _currentLap, float _distanceFromFinish, float _timeSpentRacing)
        {
            car = _car;
            currentLap = _currentLap;
            distanceFromFinish = _distanceFromFinish;
            timeSpentRacing = _timeSpentRacing;
        }
    }

    void CheckRacersPlace()
    {
        for (int i = 0; i < racers.Count; i++)
        {
            float distanceFromFinishLine =0;

            racers[i].timeSpentRacing = racers[i].car.timeSpentRacing;
            racers[i].currentLap = racers[i].car.currentLap;

            /*for (int w = 0; w < wayPoint.Length; w++)
            {
                WayPoint currWay = racers[i].car.currentWaypoint;

                if (currWay.locationInTrack >= w)
                {
                    if (currWay.locationInTrack != wayPoint.Length-1)
                        distanceFromFinishLine += Vector3.Distance(currWay.transform.position, wayPoint[currWay.locationInTrack + 1].transform.position);

                    if (currWay.locationInTrack == wayPoint.Length - 1)
                        distanceFromFinishLine += Vector3.Distance(currWay.transform.position, wayPoint[0].transform.position);
                }
            }*/
            racers[i].distanceFromFinish = distanceFromFinishLine;
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < wayPoint.Length; i++)
        {
            if (i < wayPoint.Length - 1)
            {
                Gizmos.DrawLine(wayPoint[i].transform.position, wayPoint[i + 1].transform.position);
            }
            else
            {
                Gizmos.DrawLine(wayPoint[i].transform.position, wayPoint[0].transform.position);
            }
        }
    }
}
