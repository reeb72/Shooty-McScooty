using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
     currHealth = maxHealth;   
    }

    public void TakeDamage(int damage){
        currHealth -= damage;
        if(currHealth <= 0){
            Die();
        }
    }

    private void Die(){
        if (gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentHealth()
    {
        return currHealth;
    }
}
