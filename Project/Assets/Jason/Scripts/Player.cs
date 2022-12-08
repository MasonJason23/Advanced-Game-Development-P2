using UnityEngine;

public class Player : MonoBehaviour
{
    // Instance of playerStats
    private PlayerStats playerStats;
    public UIscript ui;
    
    // Has to be called in awake since it is referenced by the GameManager
    void Awake()
    {
        
        playerStats = new PlayerStats();
        ui.setheartsSheilds(0,playerStats.GetCurrentHp());
    }

    // Update is called once per frame
    void Update()
    {
        // The Player should have some invincibility frames (iFrames) after taking a hit, assuming the player doesn't die
        if (playerStats.Invincible())
        {
            playerStats.ReduceInvincibleTimer();
        }
    }

    // Getter function that can be access from other scripts
    public PlayerStats GetPlayerStats() => playerStats;
    
    // Player takes damage via a collision with it's rigidbody
    private void OnCollisionEnter(Collision collision)
    {
        
        // Player still has some iFrames active
        if (playerStats.Invincible())
        {
            return;
        }
        
        // Checks to make sure the collision is from the enemy
        if (collision.gameObject.name.Equals("Enemy(Clone)"))
        {
            ui.hit();
            if (playerStats.GetCurrentHp() <= 0)
            {
                // Deactivating player game object (Temporary solution to player death)
                gameObject.SetActive(false);
                GameManager._isPlayerAlive = GameManager.GamePhase.Dead;
                ui.gameOver();
            }
            else
            {
                // Player takes damage, upon surviving, iFrames are activated
                playerStats.ReduceCurrentHp();
                playerStats.ActivateInvincibility();
              
            }
        }
    }

    // This is how the player will obtain xp
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Xp"))
        {
            Debug.Log("Exp gained");
            playerStats.IncrementExp(200);
            ui.IncrementExperience(200);
        }
    }
}
