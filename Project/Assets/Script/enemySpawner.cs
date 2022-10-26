using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class enemySpawner : MonoBehaviour
{
    //enemy prefab, and the object for the map
    public GameObject enemyPrefab;
    public GameObject gameArea;
    public Transform target;
    
    //This is for testing enemy spawning
    public int enemyCount = 0;
    public int enemyLimit = 25;
    public int enemyPerFrame = 1;

    //The top one is for the spawning circle and the bottom is for testing, will maybe keep it(?)
    public float spawnCircleRadius = .5f;
    public float deathCircleRadius = 2.0f;
    
    //Speeds for testing 
    public float slowestSpeed = 10f;
    public float fastestSpeed = 1f;
    private void Start()
    {

    }

    private void Update()
    {
        MaintainPopulation();
    }

    void MaintainPopulation()
    {
        if (enemyCount < enemyLimit)
        {
            for (int i = 0; i < enemyPerFrame; i++)
            {
                Vector3 position = GetRandomPosition(true);
                enemySCR enemyScript = AddEnemy(position);
                
                enemyScript.transform.Rotate(Vector3.forward * Random.Range(-45.0f,45.0f));
            }
        }
    }

    Vector3 GetRandomPosition(bool withinCamera)
    {
        Vector3 position = Random.insideUnitCircle.normalized; //This will push enemies to the edge of the circle, later on may add some random number to move them either closer to further from the edge of the circle.

        position = new Vector3(position.x, 0, position.y);
        
        if(withinCamera == false)
        {
            position = position.normalized;
        }

        position *= spawnCircleRadius;
        position += gameArea.transform.position;
        
        return position;
    }

    enemySCR AddEnemy(Vector3 position)
    {
        enemyCount += 1;
        GameObject newEnemy = Instantiate(
            enemyPrefab,
            position,
            Quaternion.FromToRotation(Vector3.up, (gameArea.transform.position - position)),
            gameObject.transform);
        
        enemySCR enemyScript = newEnemy.GetComponent<enemySCR>();
        enemyScript.enemySpawner = this;
        enemyScript.gameArea = gameArea;
        enemyScript.target = target;
        enemyScript.speed = Random.Range(slowestSpeed, fastestSpeed);

        return enemyScript;
    }
}
