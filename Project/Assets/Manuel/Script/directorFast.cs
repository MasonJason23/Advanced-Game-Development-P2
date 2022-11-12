using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directorFast : MonoBehaviour
{
    public float credits = 0; //Credits which this director has to spend to buy enemies

    public enemySpawner EnemySpawner;
    
    public float creditsPerSecond; //should be about .935 per every 4 seconds right now

    public float creditMulitplayer = 1.0f; //This right now is just for testing
    
    public float spawnTimer = 0f;

    public float fastChecker = 0f;

    public float elapsed = 0f;

    
    //TODO 1: Make sure each function at the time it needs to i.e Spawning for fast director happens every 4+ its last failed check
    //TODO 2: Make either the slowDirector & make more enemies 
    //TODO 3: Add enemy tiers
    
    /*
     * My own comments (Manuel):
     *
     * I think I should work more so on enemies and if possible get the slow director up and running
     * Tiers would be cool but this is giving me more trouble then thought which is kinda bad
     * Granted if I figure out why this is happening then I can finish all of this in less time and just work on more
     * cool enemies 
     */
    public delegate void spawnAction();

    public static event spawnAction spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= fastChecker)
        {
            buyEnemy();
        }
        elapsed += Time.deltaTime;
        //Debug.Log(gameTime);

        //credits += creditsPerSecond; this was causing major issues 


        if (elapsed >= 1f) //Need to rethink this causes the add credits function to keep happening
        {
            elapsed = elapsed % 1f;
            addCredits();
        }
    }

    void addCredits()
    {
        creditsPerSecond = creditMulitplayer * (1 + -2.6f * .025f) * (1); //.035f credit a second 
        credits += creditsPerSecond; 
        //When credits just plus equals .35 it adds it well, 
    }

    void buyEnemy()
    {
        if (credits < EnemySpawner.enemyCost)
        {
            fastChecker += 4.01f;
            //Debug.Log(EnemySpawner.enemyCost);
        }
        else{
            spawn();
            credits -= EnemySpawner.enemyCost;
        }
    }   
    
}
    