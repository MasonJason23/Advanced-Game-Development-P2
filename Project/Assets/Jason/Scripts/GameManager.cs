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
        public float[][] abilityStats = new float[][]
        {
            new float[] { 0, 100f },
            new float[] { 0, 1f },
            new float[] { 0, 1f },
            new float[] { 0, 3f }
        };

        // Timer variables for each ability
        public float cdTimer1;
        public float cdTimer2;
        public float cdTimer3;
        public float cdTimer4;

        public bool hasAbilities;

        public bool a1;
        public bool a2;
        public bool a3;
        public bool a4;

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

        _abilityClass.hasAbilities = true;
        PlayerAbilities.Orb(player.transform, 1);
    }

    private void Update()
    {
        // DebuggingAbility();
        
        // Updating game timer every frame
        gameTimer += Time.deltaTime;

        if (!_abilityClass.hasAnyAbilities())
        {
            return;
        }
        
        // if (_abilityClass.isAbilityActive(1))
        // {
        //     _abilityClass.reduceAbilityTimer(1);
        //     if (_abilityClass.getAbilityTimer(1) <= 0f)
        //     {
        //         PlayerAbilities.Explosion(player.transform, 3);
        //         _abilityClass.setAbilityTimer(1);
        //     }
        // }
        
        if (_abilityClass.a1)
        {
            _abilityClass.cdTimer1 -= Time.deltaTime;
            if (_abilityClass.cdTimer1 < 0f)
            {
                PlayerAbilities.Explosion(player.transform, 3);
                _abilityClass.cdTimer1 = 5f;
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