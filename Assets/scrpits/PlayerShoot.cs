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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
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

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bullet.transform.right * bulletSpeed;
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
