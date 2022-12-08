using UnityEngine;

public class AbilityClass
{
    /*
     * Each value represents a tier level of the ability, cooldown, damage and max level.
     * Abilities in order:
     * 1. Explosion
     * 2. Orb
     * 3. Aura
     * 4. Lighting
     */
    public float[][] abilityStats = {
        new[] { 0, 10f, 25f, 6 },
        new[] { 0, 0f, 10f, 8 },
        new[] { 0, 1f, 5f, 5 },
        // new[] { 0, 3f, 15f, 6 }
    };
    private readonly float _upgradeMultiplier;
    
    // Timer variables for each ability
    // Not needed for Orbs and Aura ability
    public float cdTimer1;
    public float cdTimer4;
    
    public bool a1;
    public bool a2;
    public bool a3;
    public bool a4;

    public AbilityClass()
    {
        // Initializing variables
        _upgradeMultiplier = 0.2f;
        cdTimer1 = cdTimer4 = 0f;

        a1 = a2 = a3 = a4 = false;
    }

    // This function is where an upgrade to an ability happens.
    public void UpgradeAbility(int indicator)
    {
        // The indicator allows us to distinguish what ability is chosen for an upgrade.
        // Refer to GameManager (Line 110) for the listing
        int newIndicator = indicator - 6;

        // Determining what ability to upgrade
        // Each ability has their own functions to upgrade their ability (a bit of code duplication though so it will be refactored soon)
        switch (newIndicator)
        {
            case (1):
                // Checks if the ability has reached max limit (Disabled for the time being)
                // if ((int)abilityStats[0][0] == (int)abilityStats[0][3])
                // {
                //     Debug.Log("Explosion ability at max level!");
                //     break;
                // }
                UpgradeExplosion();
                break;
            case (2):
                UpgradeOrb();
                break;
            case (3):
                UpgradeAura();
                break;
            // case (4):
            //     UpgradeLighting();
            //     break;
        }
    }

    private void UpgradeExplosion()
    {
        // Incrementing Tier Level
        abilityStats[0][0] += 1;
        
        a1 = true;
        // Activating ability if first time getting ability
        if ((int)abilityStats[0][0] == 1)
        {
            return;
        }
        
        // Decreasing cooldown and upgrading damage
        abilityStats[0][1] -= abilityStats[0][1] * _upgradeMultiplier;
        abilityStats[0][2] += abilityStats[0][2] * _upgradeMultiplier;
    }
    
    private void UpgradeOrb()
    {
        // Incrementing Tier Level
        abilityStats[1][0] += 1;
        
        a2 = true;
        // Activating ability if first time getting ability
        if ((int)abilityStats[1][0] == 1)
        {
            return;
        }

        // Upgrading damage
        abilityStats[1][2] += abilityStats[1][2] * _upgradeMultiplier;
    }
    
    private void UpgradeAura()
    {
        // Incrementing Tier Level
        abilityStats[2][0] += 1;
        
        a3 = true;
        // Activating ability if first time getting ability
        if ((int)abilityStats[2][0] == 1)
        {
            return;
        }
        
        // Decreasing cooldown and upgrading damage
        abilityStats[2][1] *= 0.8f;
        abilityStats[2][2] += abilityStats[2][2] * _upgradeMultiplier;
    }
    
    // private void UpgradeLighting()
    // {
    //     Debug.Log("Implement lighting ability in order to upgrade it!");
    // }
}
