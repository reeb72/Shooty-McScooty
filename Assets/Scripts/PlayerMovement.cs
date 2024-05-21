using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal; 
    private float speed = 10f;
    private float jump = 16f;
    private booll facingRight = true; 

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(isGrounded() && Input.GetButtonDown("Jump")){
            rb.velocity = new Vector(rb.velocity.x, jump);
        }

        if(input.GetButtonDown("Jump") && rb.velocity.y > 0f){
            rb.velocity = new Vector(rb.velocity.x, rb.velocity.y * 0.7f);
        }
        
        Flip();
    }

    private void FixedUpdate(){
        rb.velocity = new Vector(horizontal * speed, rb.velocity);
    }

    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip(){
        if(facingRight && horizontal < 0f || !facingRight && horizontal > 0f){
            facingRight = !facingRight;
            Vector3 scale = transform.localScale; 
            scale.x *+ -1f;
            transform.localScale = scale;
        }
    }
}
