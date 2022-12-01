using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    // Reference to main camera
    [SerializeField] private Camera mainCamera;
    // Used to focus on a specific layer when raycasting
    [SerializeField] private LayerMask focusLayer;
    // Reference to the player's position
    // [SerializeField] private Transform playerTransform;
    // Reference to projectile game object
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private GameObject shootingPointGameObject;

    //=========== added ========================
    
    //if the timer finishes, the shooting
    //animation stops
    private float shootingAnimationIdelTime = 3f;
    private float shootingAnimIdelTimer;
    private float smoothVal;
    private int shootLayerId;
    private int RegularLayerId;
    private Animator animator;
    enum ShootingAnimMode 
    {
        //helps to move the arm for the
        //capsule model.
        ON,
        OFF
    };
    private ShootingAnimMode shootingAnimMode;
    

    // Used to move the "debug sphere" game object
    [SerializeField] private Transform debugTransform;
    // Use when implementing shooting
    private Vector2 _screenCenterPoint;

    private void Start()
    {
        // Initializing the center point of the screen
        _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        animator = GetComponent<Animator>(); //added
        
        shootLayerId = animator.GetLayerIndex("Shooting Mode");
        RegularLayerId = animator.GetLayerIndex("Base Layer");
        shootingAnimMode = ShootingAnimMode.OFF;
    }

    void Update()
    {
        DebugMousePosition();
        if (Input.GetButtonDown("Fire1"))
        {
            shootingAnimIdelTimer = shootingAnimationIdelTime;
            Shoot();
        }
        
        HandelShootingAnimationModes();

    }

    void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(_screenCenterPoint);
        
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetTrigger("attack");
        }
        
        // if (Physics.Raycast(ray, out RaycastHit hit,
        // float.MaxValue, focusLayer)) //ORIGINAL
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) //CHANGE HERE
        {
            Vector3 aimDir = (hit.point - projectileSpawnPosition.position).normalized;
            Instantiate(projectile, projectileSpawnPosition.position, 
                Quaternion.LookRotation(aimDir, Vector3.up));
        }
        else 
        {   //added
             Vector3 tempTransformFoward = mainCamera.transform.forward;
             Instantiate(projectile, projectileSpawnPosition.position, Quaternion.LookRotation(tempTransformFoward,
                 mainCamera.transform.up));
        }
    }

    // This function places a debug game object wherever the mouse position is located
    void DebugMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, focusLayer))
        {
            debugTransform.position = hit.point;
        }
    }

    private void HandelShootingAnimationModes()
    {

        if (shootingAnimIdelTimer > 0)
        {
            if (shootingAnimMode == ShootingAnimMode.OFF)
            {
                shootingAnimMode = ShootingAnimMode.ON;
                
                StartCoroutine(SmoothlyChangeShootingModeAnimation(
                    0f, 0.7f, 0.3f, shootLayerId));
            }
            shootingAnimIdelTimer -= Time.deltaTime;
        }
        else
        {
            if (shootingAnimMode == ShootingAnimMode.ON)
            {
                shootingAnimMode = ShootingAnimMode.OFF;
                StartCoroutine(SmoothlyChangeShootingModeAnimation(
                    0.7f, 0f, 0.3f, shootLayerId));
            } 
        }
    }

    //smoothly changes the animation Layer
    public IEnumerator SmoothlyChangeShootingModeAnimation(float oldValue, float newValue, float duration, int animlayerId) {
        for (float t = 0f; t < duration; t += Time.deltaTime) {
            smoothVal = Mathf.Lerp(oldValue, newValue, t / duration);
            animator.SetLayerWeight(animlayerId, smoothVal);

            yield return null;
        }
        smoothVal = newValue;
    }
}
