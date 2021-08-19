using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Color of the gizmo
    public Color gizmoColor = Color.red;


    // The different Enemy levels
    public enum EnemyLevels
    {
        Easy,
        Medium,
        Hard,
        Boss
    }

    // Enemy level to be spawnedEnemy
    public EnemyLevels enemyLevel = EnemyLevels.Easy;
    public EnemyLevels enemyLevel2 = EnemyLevels.Medium;
    //----------------------------------
    // Enemy Prefabs
    //----------------------------------
    public GameObject EasyEnemy;
    public GameObject MediumEnemy;
    public GameObject HardEnemy;
    public GameObject BossEnemy;
    public GameObject[] gos;
    public int numofEachEnemy = 0;
    public Text timerText;
    public float time = 5;
    int wave = 1;
    //public int numMedium = 0;
    private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(4);
    void Start()
    {
        //set up timer
        time = 5;
        Enemies.Add(EnemyLevels.Easy, EasyEnemy);
        Enemies.Add(EnemyLevels.Boss, BossEnemy);
        Enemies.Add(EnemyLevels.Medium, MediumEnemy);
        Enemies.Add(EnemyLevels.Hard, HardEnemy);
    }
    void OnDrawGizmos()
    {
        // Sets the color to red
        Gizmos.color = gizmoColor;
        //draws a small cube at the location of the gam object that the script is attached to
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
    //checks if any enemies are alive, if not start a countdown to add enemies again in the next wave
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            time -= Time.deltaTime;
            timerText.text = "Wave " + wave + " begins in..." + (int)time;

            if (time <= 0)
            {
                timerText.text = " ";
                spawnEnemy();
            }
        }


    }
    // spawns an enemy based on the enemy level that you selected
    //currently spawns 2 different types of enemies and creates a certain number of both types
    private void spawnEnemy()
    {
        time = 5;
        wave += 1;
        //reset timer
        gos = new GameObject[numofEachEnemy];
        for (int i = 0; i < gos.Length; i++)
        {
            GameObject clone = (GameObject)Instantiate(Enemies[enemyLevel], gameObject.transform.position, Quaternion.identity);
            GameObject Enemy2 = (GameObject)Instantiate(Enemies[enemyLevel2], gameObject.transform.position, Quaternion.identity);
            gos[i] = clone;
            gos[i] = Enemy2;
        }

    }

}