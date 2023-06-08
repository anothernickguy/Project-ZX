using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCollision : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageColorDuration = 0.2f;
    public bool disableColliderOnCollision = true;
    public bool disableRigidbodyOnCollision = true;

    private bool isColliding = false;
    private bool isCoroutineRunning = false;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isColliding)
        {
            isColliding = true;

            if (disableColliderOnCollision)
            {
                boxCollider.enabled = false;
            }

            if (disableRigidbodyOnCollision)
            {
                rb.simulated = false;
            }

            if (!isCoroutineRunning)
            {
                StartCoroutine(ChangeSpriteColor());
            }
        }
    }

    private System.Collections.IEnumerator ChangeSpriteColor()
    {
        isCoroutineRunning = true;

        // Guardar el color original del sprite
        Color originalColor = spriteRenderer.color;

        // Oscurecer el sprite con el color de daño
        spriteRenderer.color = damageColor;

        // Esperar la duración del oscurecimiento
        yield return new WaitForSeconds(damageColorDuration);

        // Restaurar el color original del sprite
        spriteRenderer.color = originalColor;

        isCoroutineRunning = false;
    }
}
