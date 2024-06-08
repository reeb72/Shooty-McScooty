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
    private bool facingRight = false;
    public float detectionRange = 10f;
    public float stoppingDistance = 7.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            Debug.Log("Distance to Player: " + distanceToPlayer);

            if (distanceToPlayer <= detectionRange && distanceToPlayer > stoppingDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StopMoving();
            }

            if (distanceToPlayer <= detectionRange && Time.time >= lastShotEnemy)
            {
                Shoot();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

    private void Shoot()
    {
        Vector2 direction = (player.position - enemyFirePoint.position).normalized;
        direction.y = 0; // Ensure the bullet only travels in the x direction

        GameObject bullet = Instantiate(bulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        lastShotEnemy = Time.time + fireRate;
        Destroy(bullet, 2f); // Destroy the bullet after 2 seconds

    }

    private void StopMoving()
    {
        Debug.Log("Stopped moving");
        rb.velocity = Vector2.zero; // Stop the enemy's movement
    }
}
