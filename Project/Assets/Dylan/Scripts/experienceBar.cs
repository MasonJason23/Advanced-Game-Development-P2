using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class experienceBar: MonoBehaviour
{
    private Slider slider;
    public float fillSpeed = 0.2f;
    private float targetProgress = 0;
    public int level = 1;
    private int currentExperience = 0;
    
    // Start is called before the first frame update
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementExperience(200);
        }
       
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
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
}
