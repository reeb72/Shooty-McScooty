using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, DashDistance, JumpHeight, FireRate, DoubleShot };
    public PowerUpType powerUpType;
    public float increment = 1f; // increment to increase

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                switch (powerUpType)
                {
                    case PowerUpType.Speed:
                        playerMovement.IncreaseSpeed(increment);
                        break;
                    case PowerUpType.DashDistance:
                        playerMovement.IncreaseDashDistance(increment);
                        break;
                    case PowerUpType.JumpHeight:
                        playerMovement.IncreaseJumpHeight(increment);
                        break;
                    case PowerUpType.FireRate:
                        playerMovement.DecreaseReloadTime(increment);
                        break;
                    case PowerUpType.DoubleShot:
                        playerMovement.EnableDoubleShot();
                        break;
                }
                Destroy(gameObject); // Destroy the power-up after applying its effect
            }
        }
    }
}
