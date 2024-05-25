using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform enemyFirePoint;
    public float bulletSpeed = 10f;
    public float moveSpeed = 1.5f;
    public float fireRate = 3f;
    private float lastShotEnemy;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastShotEnemy = Time.time + fireRate;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (Time.time >= lastShotEnemy)
        {
            Shoot();
            lastShotEnemy = Time.time + fireRate;
        }

        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Check if the player is in front of the enemy before shooting
            if (Time.time >= lastShotEnemy && Vector2.Dot(direction, transform.right) > 0)
            {
                Shoot();
                lastShotEnemy = Time.time + fireRate;
            }
        }
    }

    private void Shoot()
    {
        if (player == null) return;

        Vector2 direction = (player.position - enemyFirePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
        Destroy(bullet, 2f); // Destroy the bullet after 2 seconds
    }
}
