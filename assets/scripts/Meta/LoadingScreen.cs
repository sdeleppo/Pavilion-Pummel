using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    public GameObject p;
	
	// Update is called once per frame
	void Update () 
    {
        if (Application.isLoadingLevel)
        {
            p.SetActive(true);
        }
        else
        {
            p.SetActive(false);
        }
	}
}
