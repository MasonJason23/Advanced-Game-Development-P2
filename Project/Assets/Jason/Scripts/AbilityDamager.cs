using UnityEngine;

// This script allows each ability, with a collider, to interact with the enemies.
public class AbilityDamager : MonoBehaviour
{
    // Damage variable
    public int damage;

    // Calls a "damage enemy" function from the enemy when triggering an abilities collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<enemySCR>().takeDamage(damage);
        }
    }
}
