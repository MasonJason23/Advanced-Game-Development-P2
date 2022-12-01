using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public class AbilityClass
    {
        /*
         * Each value represents a tier level of an ability and their cooldown timers.
         * Abilities in order:
         * 1. Explosion
         * 2. Orb
         * 3. Aura
         * 4. Lighting
         */
        private float[][] abilityStats = new float[][]
        {
            new float[] { 0, 5f },
            new float[] { 0, 1f },
            new float[] { 0, 1f },
            new float[] { 0, 3f }
        };

        // Timer variables for each ability
        private float cdTimer1;
        private float cdTimer2;
        private float cdTimer3;
        private float cdTimer4;

        private bool hasAbilities;

        private bool a1;
        private bool a2;
        private bool a3;
        private bool a4;

        public AbilityClass()
        {
            cdTimer1 = cdTimer2 = cdTimer3 = cdTimer4 = 0f;
            
            hasAbilities = false;

            a1 = a2 = a3 = a4 = false;
        }

        public bool hasAnyAbilities()
        {
            return this.hasAbilities;
        }

        public bool isAbilityActive(int i)
        {
            if (i == 1)
            {
                return this.a1;
            }
            else if (i == 2)
            {
                return this.a2;
            }
            else if (i == 3)
            {
                return this.a3;
            }
            else if (i == 4)
            {
                return this.a4;
            }
            else
            {
                Debug.Log("isAbilityActive: Wrong integer passed");
                return false;
            }
        }

        public float getAbilityTimer(int i)
        {
            if (i == 1)
            {
                return this.cdTimer1;
            }
            else if (i == 2)
            {
                return this.cdTimer2;
            }
            else if (i == 3)
            {
                return this.cdTimer3;
            }
            else if (i == 4)
            {
                return this.cdTimer4;
            }
            else
            {
                Debug.Log("abilityTimer: Wrong integer passed");
                return -1;
            }
        }
        
        public void setAbilityTimer(int i)
        {
            if (i == 1)
            {
                this.cdTimer1 = this.abilityStats[i][1];
            }
            else if (i == 2)
            {
                this.cdTimer2 = this.abilityStats[i][1];
            }
            else if (i == 3)
            {
                this.cdTimer3 = this.abilityStats[i][1];
            }
            else if (i == 4)
            {
                this.cdTimer4 = this.abilityStats[i][1];
            }
            else
            {
                Debug.Log("setAbilityTimer: Wrong integer passed");
            }
        }
        
        public void reduceAbilityTimer(int i)
        {
            if (i == 1)
            {
                this.cdTimer1 -= Time.deltaTime;
            }
            else if (i == 2)
            {
                this.cdTimer2 -= Time.deltaTime;
            }
            else if (i == 3)
            {
                this.cdTimer3 -= Time.deltaTime;
            }
            else if (i == 4)
            {
                this.cdTimer4 -= Time.deltaTime;
            }
            else
            {
                Debug.Log("reduceAbilityTimer: Wrong integer passed");
            }
        }
        
        public float getAbilityTierLevel(int i)
        {
            if (i == 1)
            {
                return this.abilityStats[i][0];
            }
            else if (i == 2)
            {
                return this.abilityStats[i][0];
            }
            else if (i == 3)
            {
                return this.abilityStats[i][0];
            }
            else if (i == 4)
            {
                return this.abilityStats[i][0];
            }
            else
            {
                Debug.Log("getA1Stats: Wrong integer passed");
                return -1;
            }
        }
    }
    
    // Game timer
    [SerializeField] private float gameTimer;
    
    // Reference to the player game object
    [SerializeField] private GameObject player;
    
    // Reference to player abilities game object
    [SerializeField] private PlayerAbilities PlayerAbilities;

    private AbilityClass _abilityClass;
    
    private void Start()
    {
        // Initializing game timer at start
        gameTimer = 0f;

        _abilityClass = new AbilityClass();
    }

    private void Update()
    {
        DebuggingAbility();
        
        // Updating game timer every frame
        gameTimer += Time.deltaTime;

        if (!_abilityClass.hasAnyAbilities())
        {
            return;
        }
        

        if (_abilityClass.isAbilityActive(1))
        {
            _abilityClass.reduceAbilityTimer(1);
            if (_abilityClass.getAbilityTimer(1) <= 0f)
            {
                PlayerAbilities.Explosion(player.transform, _abilityClass.getAbilityTierLevel(1));
            }
        }
        
        if (_abilityClass.isAbilityActive(2))
        {
            _abilityClass.reduceAbilityTimer(2);
            if (_abilityClass.getAbilityTimer(2) <= 0f)
            {
                PlayerAbilities.Orb(player.transform, _abilityClass.getAbilityTierLevel(2));
            }
        }
        
        if (_abilityClass.isAbilityActive(3))
        {
            _abilityClass.reduceAbilityTimer(3);
            if (_abilityClass.getAbilityTimer(3) <= 0f)
            {
                PlayerAbilities.Aura(player.transform, _abilityClass.getAbilityTierLevel(3));
            }
        }
        
        if (_abilityClass.isAbilityActive(4))
        {
            _abilityClass.reduceAbilityTimer(4);
            if (_abilityClass.getAbilityTimer(4) <= 0f)
            {
                PlayerAbilities.Lighting(player.transform, _abilityClass.getAbilityTierLevel(4));
            }
        }
    }

    private void DebuggingAbility()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // PlayerAbilities.Explosion(player.transform, 1);
            // PlayerAbilities.Explosion(player.transform, 2);
            // PlayerAbilities.Explosion(player.transform, 3);
            // PlayerAbilities.Orb(player.transform, 1);
        }
    }
}