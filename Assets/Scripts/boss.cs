using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject LaserPrefab;
    public GameObject FireballPrefab;
    public Transform enemyFirePoint;
    public float bulletSpeed = 13f;
    public float moveSpeed = 0.75f;
    public float fireRate = 3f;
    private float lastShotEnemy;
    private Transform player;
    private Rigidbody2D rb;
    private bool facingRight = false;
    public float detectionRange = 13f;
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

        // Determine whether to instantiate a fireball or a laser
        float randomValue = Random.value; // Generates a random float
        if (randomValue <= 0.7f) // 70% chance for fireball
        {
            GameObject shot = Instantiate(FireballPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
            shot.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            lastShotEnemy = Time.time + fireRate;
            Destroy(shot, 2.5f);

        }
        else // 30% chance for laser
        {
            GameObject shot = Instantiate(LaserPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
            shot.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            lastShotEnemy = Time.time + fireRate;
            Destroy(shot, 2.5f);

        }
    }

    private void StopMoving()
    {
        rb.velocity = new Vector2(0,rb.velocity.y); // Stop the enemy's movement
    }
}
