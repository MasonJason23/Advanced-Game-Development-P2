using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb; 
    public float movementSpeed = 6f; 
    public float jumpForce = 5f;
    private float horizontalInput;
    private float verticalInput;
    public Vector3 boxsize; // the (x,y,z) size of the the box cast
    public float maxDistance; // how far from the canter is the box placed
    public LayerMask LayerMask;
    
    
    //######## NOTE: please make sure every plane in you scene has a layer called Ground so that
    //## that the jump will work.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // private void OnDrawGizmos() // used to show the box in the scene view
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(transform.position- transform.up *maxDistance, boxsize);
    // }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && GroundCheck())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    

    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, boxsize,
                -transform.up, transform.rotation, maxDistance,
                LayerMask)) // make sure the ground has a layer mask of Ground so that jumping works
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    
}
