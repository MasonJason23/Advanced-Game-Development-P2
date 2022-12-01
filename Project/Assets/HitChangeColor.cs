using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitChangeColor : MonoBehaviour
{
    private Renderer enemyRenderer;

    private Color redColor = Color.red;
    private Color whiteColor = Color.white;
    private float defaultColorChangeTime = 3f;
    private float colorChangeTime;
    private Color resultingColor;
    void Start()
    {
       enemyRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        colorChangeTime -= Time.deltaTime;
        if (colorChangeTime > 0)
        {
            restColor();
        }
    }

    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "playerBullet")
        {
            changeColor();
        }
    }



    void changeColor()
    {
        enemyRenderer.material.SetColor("_Color",redColor);
        colorChangeTime = defaultColorChangeTime;
    }

    void restColor()
    {
       resultingColor = Color.Lerp(enemyRenderer.material.GetColor("_Color"), whiteColor,0.12f);
       enemyRenderer.material.SetColor("_Color",resultingColor);
    }

}
