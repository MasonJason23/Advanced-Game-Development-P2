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
    //time the player can jump after leaving the ground
    private float cayoteTime = 0.2f;
    private float coyoteTimeCounter;
    //makes sure that if the jump button is press 0.2sec before hitting the ground, the player
    //jumpes when it hits the ground 
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    public float jumpBoostForce = 6f;
    // the window of time a player can jump boost
    private float jumpBoostWindow; 
    private Vector3 moveDir;
    // keeps track of if player is in the air
    private bool onair; 




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       onair=  !GroundCheck();
    }

    private void FixedUpdate()
    {
        // Locks the cursor
        Cursor.lockState = CursorLockMode.Locked;
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
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce( (transform.forward.normalized +moveDir * speed * Time.deltaTime ) , ForceMode.Force);
        }

        if (Input.GetButtonDown("Jump"))
        {
            // set JumBuffer Window time
            jumpBufferCounter = jumpBufferTime; 
            
            if (onair)
            {
                //set jumpBoost window time
                jumpBoostWindow = 0.2f; // same time as the jumpBufferTime
            }
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
            jumpBoostWindow -= Time.deltaTime;
        }
        

        //CoyoteTime is always > 0 when player is on the ground
        //jumpBufferCounter > 0 when jump button is pressed
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            //For vertical jump
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            
            //  For the jump Boost
            if ((horizontalInputVal != 0 || verticalInputVal != 0) && jumpBoostWindow > 0)
            {
                Debug.Log("jumpBoost!");
                rb.AddForce( (new Vector3(moveDir.normalized.x  * jumpBoostForce, 
                    0, moveDir.normalized.z * jumpBoostForce) ), ForceMode.VelocityChange );
                
                // so it doesn't jump boost forever
                jumpBoostWindow = 0f; 
            }
            //so it doesn't jump forever
            jumpBufferCounter = 0f; 
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
            // makes the player drop 
            // faster if the player leaves the jump button
            coyoteTimeCounter = 0f;

        } 
    }

    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, boxsize,
                -transform.up, transform.rotation, maxDistance,
                LayerMask)) // make sure the ground has a layer mask of Ground so that jumping works
        {
            coyoteTimeCounter = cayoteTime;
            return true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            return false;
        }
        
    }
    
    // private void OnDrawGizmos() // used to show the box in the scene view
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(transform.position- transform.up *maxDistance, boxsize);
    // }
}
