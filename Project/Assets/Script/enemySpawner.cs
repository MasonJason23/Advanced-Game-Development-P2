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
        //we check every frame to make sure we have enough enemies this will have to be changed if we want enemies spawn rate to scale with time.
        MaintainPopulation();
    }

    void MaintainPopulation()
    {
        if (enemyCount < enemyLimit)//we check to make sure we don't go over our limit, this will probably not be needed later on
        {
            for (int i = 0; i < enemyPerFrame; i++)
            {
                Vector3 position = GetRandomPosition(true); //we send a bool that shows if our enemy spawn positions would be in view 
                enemySCR enemyScript = AddEnemy(position);
                
                enemyScript.transform.Rotate(Vector3.forward * Random.Range(-45.0f,45.0f)); //this moves there spawning point to be closer but may not be needed 
            }
        }
    }

    Vector3 GetRandomPosition(bool withinCamera)
    {
        Vector3 position = Random.insideUnitCircle.normalized; //This will push enemies to the edge of the circle, later on may add some random number to move them either closer to further from the edge of the circle.
        
        //This makes there spawning be on y axis of 0 but there z position is equal to the random value we normalized 
        position = new Vector3(position.x, 0, position.y);
        
        //this will be used later so enemies just spawn in the camera view, so even in fog we can spawn enimies that aren't too far away! 
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
        enemyCount += 1; //we need to add on, later on this will be changed to append for the array
        //We instantiatew a new newEnemy, this is fairly staright forward, although we could change vector3.up to something else for grounded enemies
        GameObject newEnemy = Instantiate( 
            enemyPrefab,
            position,
            Quaternion.FromToRotation(Vector3.up, (gameArea.transform.position - position)),
            gameObject.transform);
        
        enemySCR enemyScript = newEnemy.GetComponent<enemySCR>(); //We give the enemySCR its elements 
        enemyScript.enemySpawner = this; //This just gives it this script, giving them access to one another 
        enemyScript.gameArea = gameArea; //Placing the gameArea(plane) to the enemyPrefab script
        enemyScript.target = target; //This is also needed for the nav mesh, the target is the player prefab 
        enemyScript.speed = Random.Range(slowestSpeed, fastestSpeed); //This is the speed range although its probably too fast rn 

        return enemyScript;
    }
}
