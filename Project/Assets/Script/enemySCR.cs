using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SubsystemsImplementation;

public class enemySCR : MonoBehaviour
{
    public enemySpawner enemySpawner;
    public GameObject gameArea;
    public Transform target;
    private NavMeshAgent agent;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        agent.SetDestination(target.position);
        
    }

    void Move()
    {
        transform.position += transform.up * (Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, gameArea.transform.position);
        if (distance > enemySpawner.deathCircleRadius)
        {
            RemoveEnemy();
        }
    }

    void RemoveEnemy()
    {
        Destroy(gameObject);
        enemySpawner.enemyCount -= 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We hit something");
        if (collision.gameObject.CompareTag("target"))
        {
            Destroy(gameObject);
            enemySpawner.enemyCount -= 1;
        }
    }
}
