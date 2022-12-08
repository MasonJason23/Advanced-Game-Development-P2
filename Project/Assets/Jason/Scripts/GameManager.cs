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
    [SerializeField] private GameObject levelUpScreen;
    public static bool isGamePaused = false;

    private void Start()
    {
        // Initializing game timer at start
        gameTimer = 0f;
    }

    private void Update()
    {
        // Updating game timer every frame
        gameTimer += Time.deltaTime;
        if(Input.GetKey(KeyCode.U)) 
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        levelUpScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isGamePaused = true;
    }
    
    public void playGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        levelUpScreen.SetActive(false);
        isGamePaused = false;
    }
}