using UnityEngine;

// This is where the actual player ability will behave. Some abilities might need their own script to function.
public class PlayerAbilities : MonoBehaviour
{
    // Reference to the ability game objects
    public GameObject explosion;
    public GameObject orbs;
    public GameObject aura;
    
    // Reference to the orb ability when activated for the first time
    private GameObject activeOrbs;
    private GameObject activeAura;

    // The explosion game object (ability) is created here
    public void Explosion(Transform playerT, int tierLevel, int dmg)
    {
        // Setting the explosion at the position of the player
        Vector3 newP = new Vector3(playerT.position.x, playerT.position.y + 1, playerT.position.z);
        
        // Instantiating explosion game object
        GameObject e = Instantiate(explosion, newP, Quaternion.LookRotation(playerT.up), playerT);
        
        // Update explosion size based on tier level given
        if (tierLevel != 1)
        {
            var localScale = e.gameObject.transform.localScale;
            localScale = new Vector3(localScale.x + tierLevel,
                localScale.y + tierLevel, localScale.z + tierLevel);
            e.gameObject.transform.localScale = localScale;
        }

        // Passing current damage stat to the explosion
        e.GetComponent<AbilityDamager>().damage = dmg;
        
        // Explosion is active for 1.5 seconds
        Destroy(e, 1.5f);
    }

    // Creates orbs upon first activation
    // Updates each orb based on current level
    public void Orb(Transform playerT, int tierLevel, int dmg)
    {
        // Create rotating orbs around the player that damages enemies based on tier level
        if (tierLevel == 1)
        {
            // Initializing activeOrbs variable (used later on when an upgrade is called)
            activeOrbs = Instantiate(orbs, playerT.position, Quaternion.identity, playerT);
            activeOrbs.GetComponent<OrbsAbility>().ActivateOrbs(playerT);
        }
        // There currently is only be 4 orbs in the OrbAbility prefab
        else if (tierLevel < 5)
        {
            // "Activate" an existing orb
            activeOrbs.GetComponent<OrbsAbility>().ActivateOrb();
        }
        else
        {
            // Give each orb a stat and/or size upgrade
            Debug.Log("Create function to upgrade orb stats");
        }
        
        // Passes new damage state upon each call to the Orbs
        foreach (var orb in activeOrbs.GetComponentsInChildren<Orb>())
        {
            orb.GetComponent<AbilityDamager>().damage = dmg;
        }
    }

    public void Aura(Transform playerT, int tierLevel, int dmg, float cd)
    {
        // Create a dmg over time aura effect based on tier level
        if (tierLevel == 1)
        {
            activeAura = Instantiate(aura, playerT.position, Quaternion.LookRotation(playerT.up), playerT);
        }
        else
        {
            activeAura.GetComponent<AbilityDamager2>().damage = dmg;
            activeAura.GetComponent<AbilityDamager2>().cooldown = cd;
        }
    }

    // public void Lighting(Transform playerT, float tierLevel)
    // {
    //     // Create some sort of lighting ability based on tier level
    // }
}
