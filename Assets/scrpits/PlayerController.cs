using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;  // Número máximo de saltos
    public Transform groundCheck; // Punto para verificar si el jugador está en el suelo
    public LayerMask groundLayer; // Capa del suelo
    private int jumpsRemaining;  // Saltos restantes
    private bool isJumping = false;
    private bool isFacingRight = true;  // Indica si el jugador está mirando a la derecha
    private bool isTouchingGround; // Indica si el jugador está tocando el suelo
    private Rigidbody2D rb;
    private Animator animator; // Referencia al controlador de animaciones

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Verificar si el jugador está tocando el suelo en el eje x
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

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

                // Reproducir la animación de salto (si existe)
                animator.SetTrigger("Jump");
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            jumpsRemaining--;
        }

        // Control de animaciones
        // La variable "Speed" en el controlador de animaciones controlará la reproducción de la animación de caminar
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        // La variable "IsJumping" en el controlador de animaciones controlará la reproducción de la animación de salto
        animator.SetBool("IsJumping", isJumping);

        // La variable "Wall" en el controlador de animaciones controlará la reproducción de la animación de pared
        animator.SetBool("Wall", isTouchingGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Restablecer los saltos cuando el jugador toca el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpsRemaining = maxJumps;

            // Reproducir la animación de aterrizaje (si existe)
            animator.SetTrigger("Land");
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
