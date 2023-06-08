using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageColorDuration = 0.2f;

    private bool isCoroutineRunning = false;
    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (!isCoroutineRunning)
            {
                StartCoroutine(ChangeSpriteColor());
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

        // Cambiar el color del sprite al color de daño
        spriteRenderer.color = damageColor;

        // Esperar la duración del cambio de color
        yield return new WaitForSeconds(damageColorDuration);

        // Restaurar el color original del sprite
        spriteRenderer.color = originalColor;

        isCoroutineRunning = false;
    }

    private void Die()
    {
        // Aquí puedes agregar la lógica de lo que sucede cuando el jugador muere
        gameObject.SetActive(false);
    }
}
