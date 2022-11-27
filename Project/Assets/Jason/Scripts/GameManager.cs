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

        public float[] getAbilityStats(int i)
        {
            if (i == 1)
            {
                return this.abilityStats[1];
            }
            else if (i == 2)
            {
                return this.abilityStats[2];
            }
            else if (i == 3)
            {
                return this.abilityStats[3];
            }
            else if (i == 4)
            {
                return this.abilityStats[4];
            }
            else
            {
                Debug.Log("getA1Stats: Wrong integer passed");
                return new float[] {0, 0};
            }
        }

        public float abilityTimer(int i)
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
        // Updating game timer every frame
        gameTimer += Time.deltaTime;

        if (!_abilityClass.hasAnyAbilities())
        {
            return;
        }
        

        if (_abilityClass.isAbilityActive(1))
        {
            _abilityClass.reduceAbilityTimer(1);
            if (_abilityClass.abilityTimer(1) <= 0f)
            {
                PlayerAbilities.Explosion(player.transform, _abilityClass.getAbilityStats(1));
            }
        }
        
        if (_abilityClass.isAbilityActive(2))
        {
            _abilityClass.reduceAbilityTimer(2);
            if (_abilityClass.abilityTimer(2) <= 0f)
            {
                PlayerAbilities.Orb(player.transform, _abilityClass.getAbilityStats(2));
            }
        }
        
        if (_abilityClass.isAbilityActive(3))
        {
            _abilityClass.reduceAbilityTimer(3);
            if (_abilityClass.abilityTimer(3) <= 0f)
            {
                PlayerAbilities.Aura(player.transform, _abilityClass.getAbilityStats(3));
            }
        }
        
        if (_abilityClass.isAbilityActive(4))
        {
            _abilityClass.reduceAbilityTimer(4);
            if (_abilityClass.abilityTimer(4) <= 0f)
            {
                PlayerAbilities.Lighting(player.transform, _abilityClass.getAbilityStats(4));
            }
        }
    }
}