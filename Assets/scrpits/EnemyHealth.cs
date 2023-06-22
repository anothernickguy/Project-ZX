using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Score scoreScript; // Referencia al script de Score


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
            EnemyDie();
        }
        else
        {
            if (!isCoroutineRunning)
            {
                StartCoroutine(ChangeSpriteColor());
            }
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

    private void EnemyDie()
    {
        // Desactivar el game object
        gameObject.SetActive(false);

        scoreScript.IncreaseScore();
    }
}
