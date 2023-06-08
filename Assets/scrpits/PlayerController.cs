using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;  // Número máximo de saltos
    private int jumpsRemaining;  // Saltos restantes
    private bool isJumping = false;
    private bool isFacingRight = true;  // Indica si el jugador está mirando a la derecha
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Movimiento horizontal
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Control de volteo del sprite
        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }

        // Control de salto
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            jumpsRemaining--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Restablecer los saltos cuando el jugador toca el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpsRemaining = maxJumps;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

