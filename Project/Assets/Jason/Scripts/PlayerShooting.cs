using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform playerTransform;
    
    [SerializeField] private Transform debugTransform;

    private Vector2 _screenCenterPoint;

    private void Start()
    {
        _screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void Update()
    {
        DebugMousePosition();
    }

    void Shoot()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(_screenCenterPoint);
        // Implement Invisible walls first inside PlaneGeneration script
    }

    void DebugMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            debugTransform.position = hit.point;
        }
    }
}
