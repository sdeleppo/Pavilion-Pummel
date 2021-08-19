using UnityEngine;
using System.Collections;

public class MenuNavigation : MonoBehaviour {

	//Handling
	public Transform main;
	public Transform select;
	public Transform cam;
    public Transform about;
    public Transform survival;
	//Internal
	Transform goal;

	void Start()
	{
        Time.timeScale = 1.0f;
		goal = main;
	}

	public void GoToMain()
	{
		goal = main;
	}

    public void GoToAbout()
    {
        goal = about;
    }

	public void GoToSelectCampaign()
	{
		goal = select;
	}
    public void GoToSelectSurvival()
    {
        goal = survival;
    }

	public void PickCharacter(int code)
	{
		//0 is guy 1 is girl
		PlayerPrefs.SetInt ("PlayerID", code);
		LoadGameLevel();
	}
    public void PickCharacterSuvival(int code)
    {
        //0 is guy 1 is girl
        PlayerPrefs.SetInt("PlayerID", code);
        Application.LoadLevel(2);
    }
	public void LoadGameLevel()
	{
		Application.LoadLevel (1);
	}
    public void LoadRacingLevel()
    {
        Application.LoadLevel("RaceMode");
    }

	void Update()
	{
		cam.position = Vector3.Slerp (cam.position, goal.position, Time.deltaTime * 2.5f);
		cam.eulerAngles = Vector3.Lerp (cam.eulerAngles, goal.eulerAngles, Time.deltaTime * 5);
	}

}
