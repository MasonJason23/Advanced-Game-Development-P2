using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text pointsNumber;
    private float timer = 0.0f;
    public void Setup(float time,int score)
    {
        gameObject.SetActive(true);
        timer = time;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        pointsNumber.text = score.ToString();
    }

    public void restartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void exitButton()
    {
        SceneManager.LoadScene(1);
    }
}
