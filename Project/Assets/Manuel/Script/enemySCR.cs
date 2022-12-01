using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SubsystemsImplementation;

public class enemySCR : MonoBehaviour
{
    //all of these are elements that are given to the enemySCR through the enemySpawner
    public enemySpawner enemySpawner;
    public GameObject gameArea;
    public Transform target;
    public float enemyCost = 1.0f;
    public int hp = 100; 

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //allows the enemy to move
        Move(); 
        //Makes the enemy target the player
    }

    void Move()
    {
        /*
         * "Ideally the enemy should face towards the player and then move towards that player depending on its unique speed"
         * Pseudo Code
         * (Facing towards the player)
         * Can either use Transform.LookAt -> https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
         * or Quaternion look rotation -> https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
         * (Moving towards the player)
         * transform.position = transform.forwards * delta time * speed
         */
        
        //Attempting to look at player, happens every frame since its in update currently 
        
        //TODO 1: Change look at to quaternion, if it still allows eneimies to float look for other solutions
        
        //TODO 2: Didn't like the quaternion doc on unity, will change it to something else 
        //Vector3 relativePos = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.LookAt(new Vector3(target.position.x, 0.154f,target.position.z)); //this works but enemies are stuck in the ground for rn 

        transform.position += transform.forward * (Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, gameArea.transform.position);
        //If the enemy is too far off the plane then the enemy is destroyed 
        if (distance > enemySpawner.deathCircleRadius)
        {
            //RemoveEnemy();
        }
    }

    //We destroy the enemy since its like way too far
    void RemoveEnemy()
    {
        Destroy(gameObject);
        enemySpawner.enemyCount -= 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("We hit something");
        //we destroy the enemy if it only collides with the target 
        if (collision.gameObject.CompareTag("target"))
        {
            Destroy(gameObject);
            enemySpawner.enemyCount -= 1;
        }
    }

    public void takeDamage(int value)
    {
        
    }
}
