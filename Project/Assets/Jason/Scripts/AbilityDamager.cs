using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDamager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<enemySCR>().takeDamage(100);
        }
    }
}
