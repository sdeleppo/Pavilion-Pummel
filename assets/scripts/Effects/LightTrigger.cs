using UnityEngine;
using System.Collections;

public class LightTrigger : MonoBehaviour {
    public bool lightOff;
    public GameObject showLight;
    public GameObject normLight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player")
        {
            if (!lightOff)
            {
                showLight.SetActive(true);
                normLight.SetActive(false);
            }
            else
            {
                showLight.SetActive(false);
                normLight.SetActive(true);
                
            }
        }
	}
}
