using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // Game timer
    [SerializeField] private float gameTimer;
    
    private void Start()
    {
        // Initializing game timer at start
        gameTimer = 0f;
    }

    private void Update()
    {
        // Updating game timer every frame
        gameTimer += Time.deltaTime;
    }
}