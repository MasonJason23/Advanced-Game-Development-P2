using System;
using UnityEngine;
using UnityEngine.Serialization;

// This script allows each ability, with a collider, to interact with the enemies.
public class AbilityDamager2 : MonoBehaviour
{
    public int damage;
    public float cooldown;
    [FormerlySerializedAs("currentCD")] public float currentCd;

    void Update()
    {
        currentCd -= Time.deltaTime;
        if (currentCd <= 0.1f) currentCd = cooldown;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<enemySCR>().takeDamage(damage);
        }
    }
}
