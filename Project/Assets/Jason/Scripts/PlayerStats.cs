using UnityEngine;

public class PlayerStats
{
    private int playerLevel;
    private int exp;
    private int expMultiplier;
    private int maxExp;

    private int hp;
    private int shields;
    private float fireRate;
    private float movementSpeed;
    private float critRate;
    private float critDmg;
    private float dmgMultiplier;

    private float invincibilityTimer;
    private float hitTimer;
    private bool activeInvincibility;

    // Constructor
    public PlayerStats()
    {
        playerLevel = 1;
        exp = 0;
        expMultiplier = 1000;
        maxExp = playerLevel * 1000;
        
        hp = 1;
        shields = 0;
        fireRate = 10f;
        movementSpeed = 10f;
        critRate = 0f;
        critDmg = 0.50f;
        dmgMultiplier = 1f;
        
        invincibilityTimer = 3f;
        hitTimer = invincibilityTimer;
        activeInvincibility = false;
    }

    public int GetPlayerLevel() => playerLevel;

    public int GetCurrentHp() => hp;
    
    public int GetPlayerExp() => exp;

    public void ReduceCurrentHp() => hp -= 1;

    private void IncrementCurrentHp() => hp += 1;

    private void IncrementCurrentShield() => shields += 1;
    
    
    // Temporary values are being used to increment other stats
    // -----------------------------------------------------------
    private void IncrementCurrentFireRate() => fireRate += 1;
    
    private void IncrementCurrentMovementSpd() => fireRate += 1;
    
    private void IncrementCurrentCrtRate() => fireRate += 1;
    
    private void IncrementCurrentCrtDmg() => fireRate += 1;
    
    private void IncrementCurrentDmgMultiplier() => fireRate += 1;
    // ------------------------------------------------------------

    // Increment player exp and adjust maxExp scale when reached
    public void IncrementExp(int xp)
    {
        exp += xp;
        if (exp >= maxExp)
        {
            playerLevel += 1;
            maxExp += playerLevel * playerLevel * 500;
        }
    }

    public bool Invincible() => activeInvincibility;

    public void ActivateInvincibility() => activeInvincibility = true;

    // Reduces invincibility time limit when iFrames are activated
    public void ReduceInvincibleTimer()
    {
        hitTimer -= Time.deltaTime;
        if (hitTimer <= 0f)
        {
            hitTimer = invincibilityTimer;
            activeInvincibility = false;
        }
    }

    // This function upgrades the player's stat based on the indicator's value
    // Currently the player's stats aren't linked with anything.
    public void UpgradeStat(int indicator)
    {
        Debug.Log("Stat Upgraded");
        
        // List of Upgrades
        // {0, "Hp"},
        // {1, "Shield"},
        // {2, "FireRate"},
        // {3, "MovementSpeed"},
        // {4, "CrtRate"},
        // {5, "CrtDamage"},
        // {6, "DamageMultiplier"}

        switch (indicator)
        {
            case(0):
                IncrementCurrentHp();
                break;
            case(1):
                IncrementCurrentShield();
                break;
            case(2):
                IncrementCurrentFireRate();
                break;
            case(3):
                IncrementCurrentMovementSpd();
                break;
            case(4):
                IncrementCurrentCrtRate();
                break;
            case(5):
                IncrementCurrentCrtDmg();
                break;
            case(6):
                IncrementCurrentDmgMultiplier();
                break;
        }
    }
}
