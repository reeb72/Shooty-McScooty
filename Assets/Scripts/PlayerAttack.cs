using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject); // Destroy this bullet on collision with any object
    }
}
