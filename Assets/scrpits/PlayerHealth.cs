using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image vida;
    public int maxHealth = 100;
    public int currentHealth;

    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageColorDuration = 0.2f;

    public float invulnerabilityTime = 1f; // Tiempo de invulnerabilidad despu�s de recibir da�o
    public float knockbackForce = 10f; // Fuerza de impulso hacia atr�s al recibir da�o

    private bool isCoroutineRunning = false;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isCoroutineRunning)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(ChangeSpriteColor());
                StartCoroutine(ApplyKnockback());
                StartCoroutine(InvulnerabilityTime());
            }
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private System.Collections.IEnumerator ChangeSpriteColor()
    {
        isCoroutineRunning = true;

        // Cambiar el color del sprite al color de da�o
        spriteRenderer.color = damageColor;

        // Esperar la duraci�n del cambio de color
        yield return new WaitForSeconds(damageColorDuration);

        // Restaurar el color original del sprite
        spriteRenderer.color = originalColor;

        isCoroutineRunning = false;
    }

    private System.Collections.IEnumerator ApplyKnockback()
    {
        // Obtener la direcci�n contraria hacia donde ve el jugador
        Vector2 knockbackDirection = -transform.right + new Vector3(0,1,0);

        // Aplicar el impulso hacia atr�s
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Esperar un frame para evitar que el jugador quede atascado en una colisi�n
        yield return null;
    }

    private System.Collections.IEnumerator InvulnerabilityTime()
    {
        // Hacer que el jugador sea invulnerable
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("enemy"), true);

        // Esperar el tiempo de invulnerabilidad
        yield return new WaitForSeconds(invulnerabilityTime);

        // Hacer que el jugador sea vulnerable nuevamente
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("enemy"), false);
    }

    private void Die()
    {
        // Aqu� puedes agregar la l�gica de lo que sucede cuando el jugador muere
        gameObject.SetActive(false);
    }

    void Update()
    {
        vida.fillAmount = (1.0f * currentHealth) / maxHealth;
    }
}
