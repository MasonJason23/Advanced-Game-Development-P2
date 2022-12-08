using System;
using UnityEngine;
using UnityEngine.Serialization;

// This script allows each ability, with a collider, to interact with the enemies.
public class AbilityDamager2 : MonoBehaviour
{
    public int damage;
    public float cooldown;
    [FormerlySerializedAs("currentCD")] public float currentCd;
    [SerializeField] private ParticleSystem hitEffect;

    void Update()
    {
        Debug.Log(damage);
        currentCd -= Time.deltaTime;
        if (currentCd <= 0.1f) currentCd = cooldown;
    }

    private void OnTriggerStay(Collider other)
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
