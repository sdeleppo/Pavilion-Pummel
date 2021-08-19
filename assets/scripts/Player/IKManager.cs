using UnityEngine;
using System.Collections;

public class IKManager : MonoBehaviour {

    public GameObject male;
    public GameObject female;

    public Transform leftHandGoal;
    public Transform rightHandGoal;
    Animator anim;

	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
	}


    void OnAnimatorIK()
    {

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1); 
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandGoal.position);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandGoal.position);

    }
}
