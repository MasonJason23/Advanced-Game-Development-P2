using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class experienceBar: MonoBehaviour
{
    private Slider slider;
    private ParticleSystem particleSystem;
    public float fillSpeed = 0.5f;
    private float targetProgress = 0;
    
    // Start is called before the first frame update
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        particleSystem = GameObject.Find("xpBarParticles").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        IncrementProgress(0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            particleSystem.Stop();
        }
    }

    public void IncrementProgress(float newProgress)
    {
      targetProgress = slider.value+newProgress;
    }
}
