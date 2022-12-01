using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsAbility : MonoBehaviour
{
    public int active;
    public GameObject[] orbs;

    private int currentTierLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTierLevel = 0;
        active = 1;
    }

    public void ActivateOrbs(Transform target)
    {
        for (int i = 0; i < orbs.Length; i++)
        {
            orbs[i].GetComponent<Orb>().target = target;
        }
        ActivateOrb();
    }
    
    public void ActivateOrb()
    {
        if (currentTierLevel < 4)
        {
            orbs[currentTierLevel].GetComponent<MeshRenderer>().enabled = true;
            orbs[currentTierLevel].GetComponent<SphereCollider>().enabled = true;
        }
        
        currentTierLevel += 1;
    }
}
