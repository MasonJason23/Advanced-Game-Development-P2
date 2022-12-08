using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public Transform target;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 180.0f;
    public Vector3 relativeDistance = Vector3.zero;

    private AudioSource _audioSource;
     
    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.enabled = true;

        if(target != null) 
        {
            relativeDistance = transform.position - target.position;
        }
    }
     
    void Orbit()
    {
        if(target != null)
        {
            // $$anonymous$$eep us at the last known relative position
            transform.position = target.position + relativeDistance;
            transform.RotateAround(target.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
            // Reset relative position after rotate
            relativeDistance = transform.position - target.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("Enemy")) gameObject.GetComponent<AudioSource>().Play();
    }

    void LateUpdate()
    {
        Orbit();
    }
}
