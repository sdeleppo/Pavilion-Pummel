using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RacingGameManager : MonoBehaviour {

    public RaceCarManager playerRaceCar;
    Rigidbody playerRigid;
    public SimpleCarController playerCarController;
    public bool gameEnded;
    public GameObject endCam;
    public GameObject racefinished;
    GameObject playerCam;
    AudioSource a;
    [Header("Text Components")]
    public Text speedo;
    public Text lapDisplay;
    public Text timeSpentRacing;

    void Start()
    {
        a = GetComponent<AudioSource>();
        Time.timeScale = .9f;
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Vehicle");
        foreach (GameObject car in vehicles) { if (car.GetComponent<PlayerCarInput>() != null) { playerRaceCar = car.GetComponent<RaceCarManager>(); } }
        playerCarController = playerRaceCar.gameObject.GetComponent<SimpleCarController>();
        playerCarController.controlled = false;
        playerCam = Camera.main.gameObject;
        playerRigid = playerRaceCar.gameObject.GetComponent<Rigidbody>();
    }

    public void PlayBoop(AudioClip boop)
    {
        a.PlayOneShot(boop);
    }

    void Update()
    {
        speedo.text = Mathf.RoundToInt(playerRigid.velocity.magnitude) + "MPH";
        lapDisplay.text = playerRaceCar.currentLap + "/" + 2;
        timeSpentRacing.text = playerRaceCar.timeSpentRacing.ToString("F2") + "'";
        if (playerRaceCar.finished)
        {
            //do stuff to make the game finish
            gameEnded = true;//set the game to finished
            playerCarController.controlled = false;//player stops controlling the car
            playerCam.SetActive(false);//turn off player cam
            endCam.SetActive(true);//turn off game end cam
            racefinished.SetActive(true);//turn on the finished message
        }
    }

    public void StartRace()
    {
        Time.timeScale = 1;
        playerCarController.controlled = true;
    }
}
