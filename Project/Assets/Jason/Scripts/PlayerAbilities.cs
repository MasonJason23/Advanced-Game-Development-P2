using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject explosion;
    public GameObject orbs;
    
    private GameObject activeOrbs;

    public void Explosion(Transform playerT, float tierLevel)
    {
        // Create explosion based on tier level given
        float limiter = 0.50f;
        Vector3 newP = new Vector3(playerT.position.x, playerT.position.y + 1, playerT.position.z);
        GameObject e = Instantiate(explosion, newP, Quaternion.LookRotation(playerT.up), playerT);
        Vector3 scale =  new Vector3(e.transform.localScale.x * tierLevel * limiter, e.transform.localScale.y * tierLevel * limiter, e.transform.localScale.z);
        e.transform.localScale = scale;
        Destroy(e, 2f);
    }

    public void Orb(Transform playerT, float tierLevel)
    {
        // Create rotating orbs around the player that damages enemies based on tier level
        if ((int)tierLevel == 1)
        {
            activeOrbs = Instantiate(orbs, playerT.position, Quaternion.identity, playerT);
            activeOrbs.GetComponent<OrbsAbility>().ActivateOrbs(playerT);
        }
        else if (tierLevel < 4)
        {
            activeOrbs.GetComponent<OrbsAbility>().ActivateOrb();
        }
        else
        {
            Debug.Log("Create function to upgrade orb stats");
        }
    }

    public void Aura(Transform playerT, float tierLevel)
    {
        // Create a dmg over time aura effect based on tier level
    }

    public void Lighting(Transform playerT, float tierLevel)
    {
        // Create some sort of lighting ability based on tier level
    }
}
