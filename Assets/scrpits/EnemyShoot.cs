using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float shootingInterval = 2f; // Intervalo de tiempo entre disparos
    public float shootingAnimationDuration = 1f; // Duración de la animación de disparo
    private bool canShoot = true;
    private PlayerController pc;
    private Animator animator;
    [SerializeField] private float minDistance;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (canShoot)
            {
                while (Vector2.Distance(pc.transform.position, this.transform.position) > minDistance)
                {
                    // Cambiar el parámetro "DetectingPlayer" a falso para volver a la animación idle
                    animator.SetBool("DetectingPlayer", false);
                    yield return null;
                }

                // Cambiar el parámetro "DetectingPlayer" a verdadero para activar la animación de disparo
                animator.SetBool("DetectingPlayer", true);

                // Reproducir la animación de disparo
                animator.SetTrigger("Shoot");

                Shoot();
                canShoot = false;
                yield return new WaitForSeconds(shootingInterval);
                canShoot = true;
            }

            yield return null;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bullet.transform.right * bulletSpeed;
    }
}
