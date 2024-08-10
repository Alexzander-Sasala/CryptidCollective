using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 1f;
    public float climbSpeed = 1f;
    public bool isJumping = false;
    public bool isClimbing = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Jumping code
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        // Movement code
        float moveX = Input.GetAxis("Horizontal");
        if (moveX != 0)
        {
            rb.AddForce(new Vector2(moveX * moveSpeed, 0), ForceMode2D.Impulse);
        }

        // Climbing code
        if (isClimbing)
        {
            float moveY = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, moveY * climbSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 1;
        }
    }
}
