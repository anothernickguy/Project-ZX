using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 5f;
    public float wallJumpTime = 0.5f;
    public float wallJumpDelay = 0.2f;
    private bool isWallSliding = false;
    private bool isWallJumping = false;
    private float wallJumpTimeCounter = 0f;
    private float wallJumpDelayCounter = 0f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isWallJumping)
        {
            wallJumpTimeCounter -= Time.deltaTime;
            if (wallJumpTimeCounter <= 0f)
            {
                isWallJumping = false;
            }
        }

        if (isWallSliding && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }

        UpdateWallJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWallSliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWallSliding = false;
        }
    }

    private void UpdateWallJump()
    {
        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            isWallSliding = false;
            isWallJumping = true;
            wallJumpTimeCounter = wallJumpTime;
            rb.velocity = new Vector2(rb.velocity.x, wallJumpForce);
        }

        if (isWallJumping && Input.GetButtonUp("Jump"))
        {
            wallJumpTimeCounter = 0f;
        }
    }
}
