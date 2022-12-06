using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{
    public int life = 3;
    public int shield = 3;
    public GameObject[] hearts;
    public GameObject[] shields;
    private Slider slider;
    public float fillSpeed = 0.2f;
    private float targetProgress = 0;
    public int level = 1;
    public int currentExperience = 0;
    public TMP_Text levelNumber;
    public TMP_Text timerText;
    public gameOverScreen gameOverScreen;
    private float timer = 0.0f;
    
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject xpbar = GameObject.Find("experienceBar");
        slider = xpbar.GetComponent<Slider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hit();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
          gainHits(1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            gainHits(2);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            gainHits(3);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementExperience(200);
        }
        //for xpbar
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }

        levelNumber.text = level.ToString();
        //for Timer
        timer += Time.deltaTime;
        displayTime();
    }

    public void hit()
    {
        if (shield > 0)
        {
            takeShieldDamage();
        }
        else
        {
            takeDamage();
        }
    }

    public void gainHits(int type)
    {
        if (type == 1)
        {
            healDamage();
        }

        if (type == 2)
        {
            gainShields();
        }

        if (type == 3)
        {
            healDamage();
            gainShields();
        }
    }

    public void takeDamage()
    {
        if (life>0)
        {
            life--;
        }
        if (life < 1)
        {
            hearts[0].gameObject.SetActive(false);
            hearts[1].gameObject.SetActive(false);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life < 2)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(false);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life < 3)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(true);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life ==3)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(true);
            hearts[2].gameObject.SetActive(true);
        }

        if (life == 0)
        {
            gameOver();
        }
        
    }

    private void gameOver()
    {
        gameObject.SetActive(false);
        gameOverScreen.Setup(timer,currentExperience);
    }

    public void takeShieldDamage()
    {
        if (shield>0)
        {
            shield--;
        }
        if (shield < 1)
        {
            shields[0].gameObject.SetActive(false);
            shields[1].gameObject.SetActive(false);
            shields[2].gameObject.SetActive(false);
        }
        else if (shield < 2)
        {
            shields[0].gameObject.SetActive(true);
            shields[1].gameObject.SetActive(false);
            shields[2].gameObject.SetActive(false);
        }
        else if (shield < 3)
        {
            shields[0].gameObject.SetActive(true);
            shields[1].gameObject.SetActive(true);
            shields[2].gameObject.SetActive(false);
        }
        else if (shield ==3)
        {
            shields[0].gameObject.SetActive(true);
            shields[1].gameObject.SetActive(true);
            shields[2].gameObject.SetActive(true);
        }
        
    }
    public void healDamage()
    {
        if (life < 3)
        {
            life++;
        }
        if (life < 1)
        {
            hearts[0].gameObject.SetActive(false);
            hearts[1].gameObject.SetActive(false);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life < 2)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(false);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life < 3)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(true);
            hearts[2].gameObject.SetActive(false);
        }
        else if (life ==3)
        {
            hearts[0].gameObject.SetActive(true);
            hearts[1].gameObject.SetActive(true);
            hearts[2].gameObject.SetActive(true);
        }
    }

    public void gainShields()
    {
        if (shield < 3)
        {
            shield++;
        }

        if (shield < 1)
        {
            shields[0].gameObject.SetActive(false);
            shields[1].gameObject.SetActive(false);
            shields[2].gameObject.SetActive(false);
        }
        else if (shield < 2)
        {
            shields[0].gameObject.SetActive(true);
            shields[1].gameObject.SetActive(false);
            shields[2].gameObject.SetActive(false);
        }
        else if (shield < 3)
        {
            shields[0].gameObject.SetActive(true);
            shields[1].gameObject.SetActive(true);
            shields[2].gameObject.SetActive(false);
        }
        else if (shield == 3)
        {
            shields[0].gameObject.SetActive(true);
            shields[1].gameObject.SetActive(true);
            shields[2].gameObject.SetActive(true);
        }
    }
    public void IncrementExperience(int experience)
    { 
        float targetExperience = level * 1000;
        currentExperience += experience;
        if (slider.value >= 1.0f)
        {
            level++;
          
            while (slider.value > 0f)
            {
                slider.value -= fillSpeed * Time.deltaTime;
            }
        }
        

        float newProgress = experience / targetExperience;
        
        targetProgress = slider.value+newProgress;
        
    }
    void displayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
