using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player stats class
    public class PlayerStats
    {
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

        public int getCurrentHP()
        {
            return this.hp;
        }

        public void reduceCurrentHP()
        {
            this.hp -= 1;
        }
        
        public bool invincible()
        {
            return this.activeInvincibility;
        }
        
        public void activateInvincibility()
        {
            activeInvincibility = true;
        }

        public void reduceInvincibleTimer()
        {
            this.hitTimer -= Time.deltaTime;
            if (this.hitTimer <= 0f)
            {
                hitTimer = invincibilityTimer;
                this.activeInvincibility = false;
            }
        }
    }

    // Instance of playerStats
    private PlayerStats _playerStats;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerStats = new PlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStats.invincible())
        {
            _playerStats.reduceInvincibleTimer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_playerStats.invincible())
        {
            return;
        }
        
        if (collision.gameObject.name.Equals("Enemy"))
        {
            if (_playerStats.getCurrentHP() <= 0)
            {
                // Deactivating player game object (Temporary solution)
                gameObject.SetActive(false);
            }
            else
            {
                _playerStats.reduceCurrentHP();
                _playerStats.activateInvincibility();
            }
        }
    }
}
