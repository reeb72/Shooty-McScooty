using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(5.0f); // Deal 5 damage
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
