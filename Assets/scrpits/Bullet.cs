using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int damageAmount = 10;

    private void Update()
    {
        // Mover la bala hacia adelante
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Restar vida al enemigo
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

            // Desactivar el objeto de la bala
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            // Desactivar el objeto de la bala al colisionar con "Ground"
            Destroy(gameObject);
        }
    }
}
