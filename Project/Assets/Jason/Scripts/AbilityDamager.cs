using System;
using UnityEngine;

// This script allows each ability, with a collider, to interact with the enemies.
public class AbilityDamager : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitEffect;

    // Damage variable
    public int damage;

    // Calls a "damage enemy" function from the enemy when triggering an abilities collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("Enemy"))
        {
            if (hitEffect)
            {
                var o = other.gameObject;
                ParticleSystem efx = Instantiate(hitEffect, o.transform.position, Quaternion.identity, o.transform);
                efx.Play();
                Destroy(efx, 5f);
            }
            other.gameObject.GetComponent<enemySCR>().takeDamage(damage);
        }
    }
}
