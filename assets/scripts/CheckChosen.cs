using UnityEngine;
using System.Collections;

public class CheckChosen : MonoBehaviour {
	 GameObject chosen;
	public GameObject dudebrobody;
	public GameObject chickoritabody;
	// Use this for initialization
	void Awake () {
		chosen = GameObject.FindGameObjectWithTag("CharacterChosen");

		if (chosen.GetComponent<CharacterChoose>().dudeBro == true)
			print ("meep");
		else 
		{
			dudebrobody.SetActive (false);
			chickoritabody.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
