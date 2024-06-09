using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float currHealth;
    public HealthManager healthBar; // Reference to HealthBar script

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Initialize the health bar
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;

        if (currHealth < 0)
        {
            currHealth = 0; // Ensure health doesn't go below zero
        }

        if (gameObject.CompareTag("Player"))
        {
            healthBar.SetHealth(currHealth, maxHealth); // Update the health bar
        }

        if (currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return currHealth;
    }
}