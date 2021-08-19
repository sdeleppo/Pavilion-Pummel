using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {

    public int locationInTrack;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle")
        {
            other.GetComponent<RaceCarManager>().UpdateLocation(this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
