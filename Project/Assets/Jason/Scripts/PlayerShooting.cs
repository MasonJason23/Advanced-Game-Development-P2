using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    // Reference to main camera
    [SerializeField] private Camera mainCamera;
    // Used to focus on a specific layer when raycasting
    [SerializeField] private LayerMask focusLayer;
    // Reference to the player's position
    [SerializeField] private Transform playerTransform;
    // Reference to projectile game object
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPosition;
    
    // Used to move the "debug sphere" game object
    [SerializeField] private Transform debugTransform;

    // Use when implementing shooting
    private Vector2 _screenCenterPoint;

    private void Start()
    {
        // Initializing the center point of the screen
        _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void Update()
    {
        DebugMousePosition();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Ray ray = mainCamera.ScreenPointToRay(_screenCenterPoint);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, focusLayer))
        {
            Vector3 aimDir = (hit.point - projectileSpawnPosition.position).normalized;
            Instantiate(projectile, projectileSpawnPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
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
}
