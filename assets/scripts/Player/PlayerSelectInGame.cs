using UnityEngine;
using System.Collections;

public class PlayerSelectInGame : MonoBehaviour {

	public GameObject male;
	public GameObject female;
	public Avatar mAvatar;
	public Avatar fAvatar;
	public Animator anim;

    public Transform fBody;
    public Transform mBody;
    public Transform weapons;

	// Use this for initialization
	void Start () 
	{
        if (PlayerPrefs.GetInt("PlayerID") == 1)
        {
            male.SetActive(false);
            female.SetActive(true);
            anim.avatar = fAvatar;
            //weapons.parent = fBody;
        }
        else
        {
           // weapons.parent = mBody;
        }
	}

}
