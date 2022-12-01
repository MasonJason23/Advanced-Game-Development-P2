using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartsAndSheilds : MonoBehaviour
{
    public int life = 3;
    public int shield = 3;
    public GameObject[] hearts;
    public GameObject[] shields;
    
    // Start is called before the first frame update
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
}
