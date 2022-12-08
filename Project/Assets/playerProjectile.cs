using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour
{
    private Rigidbody rb;
    private float despawnTimer = 5f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 50f; //changed
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
