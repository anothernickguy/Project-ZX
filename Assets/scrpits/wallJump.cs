using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallJump : MonoBehaviour
{
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 5f;
    public float wallJumpTime = 0.5f;
    public float wallJumpDelay = 0.2f;

    private Rigidbody2D rb;
    private bool isWallSliding = false;
    private bool isWallJumping = false;
    private float wallJumpTimeCounter = 0f;
    private float wallJumpDelayCounter = 0f;
    private bool isJumping = false;

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
                isJumping = false;
            }
        }

        if (isWallSliding && !isWallJumping && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isWallSliding)
            {
                if (wallJumpDelayCounter <= 0f)
                {
                    WallJump();
                    wallJumpDelayCounter = wallJumpDelay;
                }
            }
            else if (!isWallJumping && !isJumping)
            {
                Jump();
            }
        }

        wallJumpDelayCounter -= Time.deltaTime;
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
            wallJumpDelayCounter = 0f;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, wallJumpForce);
        isJumping = true;
    }

    private void WallJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, wallJumpForce);
        isWallJumping = true;
        wallJumpTimeCounter = wallJumpTime;
    }
}
