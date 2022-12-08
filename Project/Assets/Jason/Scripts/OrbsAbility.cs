using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds each orb and has them rotate around a target
public class OrbsAbility : MonoBehaviour
{
    // Contains each individual Orb in an array
    public GameObject[] orbs;

    // Keeps track of which Orb to "Activate" 
    private int currentTierLevel;
    
    // currentTierLevel variable resets to 0 when incremented the first time when using Start()
    // Temporary Solution is to initialize these variables on Awake()
    void Awake()
    {
        currentTierLevel = 0;
    }

    // Initial state of the Orbs ability (Only 1 orb is active)
    // Makes sure each ability is targeting the player in order to rotate around it
    public void ActivateOrbs(Transform target)
    {
        foreach (var orb in orbs)
        {
            orb.GetComponent<Orb>().target = target;
        }
        ActivateOrb();
    }
    
    // "Activates" one individual orb by enabling its MeshRenderer and Collider (They are disabled upon instantiation)
    public void ActivateOrb()
    {
        // Makes sure not to access outside of the Orbs array range
        if (currentTierLevel < 4)
        {
            orbs[currentTierLevel].GetComponent<MeshRenderer>().enabled = true;
            orbs[currentTierLevel].GetComponent<SphereCollider>().enabled = true;
        }
        
        // Increment orb tier level
        currentTierLevel += 1;
    }
}
