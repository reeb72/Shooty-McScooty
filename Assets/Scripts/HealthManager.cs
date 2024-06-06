using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBarImage;

    public void SetMaxHealth(int maxHealth)
    {
        healthBarImage.fillAmount = 1.0f; // Full health at start
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        healthBarImage.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
