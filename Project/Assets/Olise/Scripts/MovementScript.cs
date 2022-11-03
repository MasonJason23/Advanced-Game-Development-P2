using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce = 5f;
    public float speed = 6f;
    private float horizontalInputVal;
    private float verticalInputVal;
    public Transform cam;

    //This gives the angle to rotate our player by.
    private float targetAngle;
    // stores the combination of the vertical and horizontal input
    private Vector3 direction; 
    // the (x,y,z) size of the the box cast
    public Vector3 boxsize; 
    // how far from the canter is the box placed
    public float maxDistance;
    // Adds smoothing when the player looks to a different direction
    [Tooltip("How quickly the player looks left or right")]
    public float turnSmoothningTime = 0.1f;
    //used to hold current smooth velocity
    private float currentSmoothVelocity;
    private float angle;
    public LayerMask LayerMask;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        //transform.LookAt();

    }

    private void FixedUpdate()
    {
        Cursor.visible = false;
        // Range [-1 - 1]
        horizontalInputVal = Input.GetAxisRaw("Horizontal"); 
        verticalInputVal = Input.GetAxisRaw("Vertical");
        
        // we normalize so that if two movement keys
        // are pressed at the same time, the player don't go faster.
        direction = new Vector3(horizontalInputVal, 0f, verticalInputVal).normalized;
        
        // if the length of our direction vector >=0.1
        //(means we should move)
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            // we added the camera y angle so ur target angle is based of the camera angle 
            targetAngle   = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
             
            // This code just helps the player to smoothly turn to the target angle
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, 
                ref currentSmoothVelocity, turnSmoothningTime);
            
            // rotate player to specified angle
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            //moveDir takes into account the smooth rotation val and the rotation of our camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce( (transform.forward.normalized +moveDir * speed * Time.deltaTime ) , ForceMode.Force);
        }
        
        if (Input.GetButton("Jump") && GroundCheck())
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
    
    // private void OnDrawGizmos() // used to show the box in the scene view
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(transform.position- transform.up *maxDistance, boxsize);
    // }
}
