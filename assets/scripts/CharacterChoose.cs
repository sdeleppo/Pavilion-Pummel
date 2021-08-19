using UnityEngine;
using System.Collections;

public class CharacterChoose : MonoBehaviour {
	public bool dudeBro;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	public void DudeBroSelect () {
		dudeBro = true;
		print ("hi");
		Application.LoadLevel(2);
	}
	public void ChickoritaSelect () {
		dudeBro = false;
		Application.LoadLevel(2);
	}
}
