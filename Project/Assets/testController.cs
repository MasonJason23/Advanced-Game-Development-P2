using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testController : MonoBehaviour
{
    private float currentPlayerRotation;

    private Vector2 playerInputXY;

    private CharacterController cc;

    public float PlayerSpeed = 5;

    public float RotationSpeed = 2;
    
    public float jumpForce = 5f;

    private bool jumpButtonPressed;
    Rigidbody rb; 
    
    public Vector3 boxsize; // the (x,y,z) size of the the box cast
    public float maxDistance; // how far from the canter is the box placed
    public LayerMask LayerMask; // used to specify the ground
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        jump();


    }

    void OnMove(InputValue inputValue)
    {
        playerInputXY = inputValue.Get<Vector2>();
        
    }

    void OnLook(InputValue inputValue)
    {
        currentPlayerRotation = inputValue.Get<Vector2>().x;
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(playerInputXY.x,
            0.0f, playerInputXY.y);

        movement = transform.forward * movement.z + transform.right * movement.x;
        movement *= PlayerSpeed;

        cc.Move(movement  * Time.deltaTime );
        cc.Move(Physics.gravity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        
        transform.Rotate(Vector3.up * currentPlayerRotation *RotationSpeed * Time.deltaTime);
    }

    void OnJump( InputValue inputValue)
    {
        jumpButtonPressed = true;
    }
    
    // private void OnDrawGizmos() // used to show the box in the scene view
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(transform.position- transform.up *maxDistance, boxsize);
    // }
    void jump()
    {
        if (jumpButtonPressed && GroundCheck() )
        {
            jumpButtonPressed = false;
            rb.velocity = new Vector3(playerInputXY.x * PlayerSpeed, rb.velocity.y, playerInputXY.y * PlayerSpeed);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

            cc.Move(rb.velocity) ;
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
