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
    private NavMeshAgent agent; //We need this for the navmesh 

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //allows the enemy to move
        // Move();
        //Makes the enemy target the player
        agent.SetDestination(target.position);
        
    }

    void Move()
    {
        transform.position += transform.up * (Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, gameArea.transform.position);
        //If the enemy is too far off the plane then the enemy is destroyed 
        if (distance > enemySpawner.deathCircleRadius)
        {
            RemoveEnemy();
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
        Debug.Log("We hit something");
        //we destroy the enemy if it only collides with the target 
        if (collision.gameObject.CompareTag("target"))
        {
            Destroy(gameObject);
            enemySpawner.enemyCount -= 1;
        }
    }
}
