using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public float attackDuration = 1f; // Duraci�n de la animaci�n de ataque
    private bool isAttacking = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttacking && collision.CompareTag("Player"))
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        // Congelar el enemigo
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        // Activar la animaci�n de ataque
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);

        // Desactivar la animaci�n de ataque
        animator.ResetTrigger("Attack");

        // Descongelar el enemigo
        isAttacking = false;
    }
}
