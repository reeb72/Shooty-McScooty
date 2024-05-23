using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jump = 16f;
    private bool facingRight = true;
    private float lastShotTime;
    private float reloadTime = 0.5f;

    private bool isDashing;
    private float dashDistance = 10f;
    private float dashDuration = 0.2f;
    private float dashCooldown = 1f;
    private float lastDashTime;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform firePoint; // Point from where the bullets are shot
    [SerializeField] private GameObject bulletPrefab; 

    void Start()
    {
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f);
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= lastShotTime + reloadTime) // Check if enough time has passed since the last shot
        {
            Shoot();
            lastShotTime = Time.time; // Update the last shot time
        }

        if (Input.GetButtonDown("Dash") && Time.time >= lastDashTime + dashCooldown && !isDashing) // Check if dash input is pressed and dash is not on cooldown
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool isGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
        //Debug.Log("Is Grounded: " + grounded);
        return grounded;
    }

    private void Flip()
    {
        if (facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    private void Shoot()
    {
        // Instantiate the bullet at the firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        float bulletSpeed = 10.5f; 

        if (facingRight)
        {
            bulletRb.velocity = new Vector2(bulletSpeed, 0);
        }
        else
        {
            bulletRb.velocity = new Vector2(-bulletSpeed, 0);
        }

        // Remove the bullet once it heads offScreen
        Destroy(bullet, 2f);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // Disable gravity during dash, mainly for parkour or other obstacles
        rb.velocity = new Vector2(facingRight ? dashDistance : -dashDistance, 0f);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
        lastDashTime = Time.time; // Update the last dash time
    }
}
