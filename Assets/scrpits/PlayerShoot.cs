using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
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
                    Shoot();
                    shotsFired++;

                    if (shotsFired == maxShots)
                    {
                        StartCooldown();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isShootingUp = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isShootingUp = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isShootingDown = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isShootingDown = false;
        }
    }

    private void Shoot()
    {
        if (isShootingUp)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 90f));
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.up * bulletSpeed;
        }
        else if (isShootingDown)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, -90f));
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = -bullet.transform.up * bulletSpeed;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
