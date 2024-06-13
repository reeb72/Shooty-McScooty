using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float fireballDamage = 8.0f; // Damage inflicted by fireball
    public float laserDamage = 12.0f;  
    public float damage = 5.0f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);// Deal 5 damage
            }
        }
        
        else if (collision.gameObject.CompareTag("Boss")) // Check if collided with the boss
        {
            // Determine the type of attack based on the collided object's tag
            Health health = collision.GetComponent<Health>();
            if (collision.gameObject.CompareTag("Fireball"))
            {
                health.TakeDamage(fireballDamage); // Inflict 8 damage
            }
            else if (collision.gameObject.CompareTag("Laser"))
            {
                health.TakeDamage(laserDamage); // Inflict 10 damage
            }
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") 
        || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Bullet")
        || collision.gameObject.CompareTag("Cactus"))
        {
            Destroy(gameObject);
        }
    }
}
