using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float shootingInterval = 2f; // Intervalo de tiempo entre disparos
    private bool canShoot = true;
    private PlayerController pc;
    [SerializeField] private float minDistance;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        StartCoroutine(ShootCoroutine());
        
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (canShoot)
            {
                while(Vector2.Distance(pc.transform.position, this.transform.position) > minDistance)
                {
                    yield return null;
                }
                
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
