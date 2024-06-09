using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBarImage;

    public void SetMaxHealth(float maxHealth)
    {
        healthBarImage.fillAmount = 1.0f; // Full health at start
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthBarImage.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
