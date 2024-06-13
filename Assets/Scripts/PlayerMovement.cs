using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 5.5f;
    private float jump = 8f;
    private bool canDoubleJump;
    private bool facingRight = true;
    private float lastShot;
    private float reloadTime = 0.6f;
    private bool doubleShot = false;
    private int extraBullet = 0;

    private bool isDashing;
    private float dashDistance = 13.5f;
    private float dashDuration = 0.25f;
    private float dashCooldown = 0.55f;
    private float lastDash;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform firePoint; // Point from where the bullets are shot
    [SerializeField] private GameObject bulletPrefab;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            canDoubleJump = true; // To reset the double jump when grounded
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                canDoubleJump = false;
            }
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= lastShot + reloadTime) // Check if enough time has passed since the last shot
        {
            if (doubleShot)
            {
                StartCoroutine(SecondShot());
            }
            else
            {
                Shoot();
            }
            lastShot = Time.time; // Update the last shot time!!!
        }

        if (Input.GetButtonDown("Dash") && Time.time >= lastDash + dashCooldown && !isDashing) // Check if dash input is pressed and dash is not on cooldown
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

    private bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.25f, groundLayer);
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
        // Instantiate the bullet at the firePoint (Positioned in unity)
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        float bulletSpeed = 10.5f;

        bulletRb.velocity = new Vector2(facingRight ? bulletSpeed : -1f * bulletSpeed, 0); // TERTIARY OPERATOR USAGE

        // Remove the bullet once it heads offScreen 
        Destroy(bullet, 1.5f);
    }

    private IEnumerator SecondShot()
    {
        Shoot();
        
        // Shoot additional bullets based on the number of stacks
        for (int i = 0; i < extraBullet; i++)
        {
            yield return new WaitForSeconds(0.1f); // Delay between shots
            Shoot();
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // Disable gravity during dash, mainly for parkour or other obstacles
        rb.velocity = new Vector2(facingRight ? dashDistance : -1f * dashDistance, 0f); // this is my FIRST time using a ternary operator
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
        lastDash = Time.time; // Update the last dash time
    }

    // Collision Handling 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health health = GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10f);
            }
        }
        else if (collision.gameObject.CompareTag("Cactus"))
        {
            Health health = GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(2.5f);
            }
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.NextLevel();
        }
    }

    // Methods to handle power-ups
    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    public void IncreaseDashDistance(float amount)
    {
        dashDistance += amount;
    }

    public void IncreaseJumpHeight(float amount)
    {
        jump += amount;
    }

    public void DecreaseReloadTime(float amount)
    {
        reloadTime -= amount;
    }

    public void EnableDoubleShot()
    {
        doubleShot = true;
        extraBullet++;
    }
}
