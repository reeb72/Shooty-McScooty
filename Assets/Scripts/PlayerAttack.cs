using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject); // Destroy this bullet on collision with any object
    }
}
