using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directorFast : MonoBehaviour
{
    private float credits = 0; //Credits which this director has to spend to buy enemies

    public enemySpawner EnemySpawner;
    
    public float creditsPerSecond;

    public float creditMulitplayer = 1.0f; //This rn is just for testing
    
    public int testing = 2; //cost of enemies

    public float spawnTimer = 0;

    public float elapsed = 0f;
    
    private float gameTime;

    public delegate void spawnAction();

    public static event spawnAction spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        elapsed += Time.deltaTime;
        //Debug.Log(gameTime);

        credits += creditsPerSecond;
        if (spawnTimer == 0f)
        {
            buyEnemy();
        }
        else
        {
            Invoke("buyEnemy",spawnTimer);
        }

        if (elapsed >= 4f)
        {
            elapsed = elapsed % 4f;
            addCredits();
        }
        Debug.Log(credits);
    }

    void addCredits()
    {
        creditsPerSecond = creditMulitplayer * (1 + -2.6f * .025f) * (1); //.035f credit a second 
        credits += creditsPerSecond;
    }

    void buyEnemy()
    {
        if (credits < EnemySpawner.enemyCost)
        {
            spawnTimer += 4.01f;
        }
        else{
            spawn();
        }
    }   
    
}
    