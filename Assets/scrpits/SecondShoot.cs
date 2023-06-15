using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePointUp;
    public Transform firePointDown;
    public float bulletSpeed = 10f;
    public int maxShots = 3;
    public float cooldownTime = 1f;
    private int shotsFired = 0;
    private bool isCoolingDown = false;
    private bool isShootingUp = false;
    private bool isShootingDown = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isShootingUp && !isShootingDown)
            {
                if (shotsFired < maxShots && !isCoolingDown)
                {
                    Shoot(firePoint);
                    shotsFired++;

                    if (shotsFired == maxShots)
                    {
                        StartCooldown();
                    }
                }
            }
            else
            {
                if (isShootingUp)
                {
                    if (shotsFired < maxShots && !isCoolingDown)
                    {
                        Shoot(firePointUp);
                        shotsFired++;

                        if (shotsFired == maxShots)
                        {
                            StartCooldown();
                        }
                    }
                }
                else
                {
                    if (shotsFired < maxShots && !isCoolingDown)
                    {
                        Shoot(firePointDown);
                        shotsFired++;

                        if (shotsFired == maxShots)
                        {
                            StartCooldown();
                        }
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            isShootingUp = true;
            isShootingDown = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            isShootingUp = false;
            isShootingDown = true;
        }
        else
        {
            isShootingUp = false;
            isShootingDown = false;
        }
    }

    private void Shoot(Transform shootP)
    {
        float bulletSpacing = 0.5f; // Espaciado entre balas
        float initialAngle = -15f; // Ángulo inicial para la separación de balas

        for (int i = 0; i < maxShots; i++)
        {
            float angle = initialAngle + i * bulletSpacing; // Calcular ángulo para cada bala
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.right * bulletSpeed;
        }
        if (isShootingUp)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 90f));
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.up * bulletSpeed;
        }
        else if (isShootingDown)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, -90f));
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = -bullet.transform.up * bulletSpeed;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, shootP.position, shootP.rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.right * bulletSpeed;
        }
    }

    private void StartCooldown()
    {
        isCoolingDown = true;
        Invoke("ResetShotsFired", cooldownTime);
    }

    private void ResetShotsFired()
    {
        shotsFired = 0;
        isCoolingDown = false;
    }
}
