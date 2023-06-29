using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secondshoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePointUp;
    public Transform firePointDown;
    public float bulletSpeed = 10f;
    public int maxShots = 3;
    public float cooldownTime = 1f;
    public float splitDelay = 0.5f; // Tiempo de retraso antes de que se dividan los disparos
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
                    StartCoroutine(SplitShoot(firePoint));
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
                        StartCoroutine(SplitShoot(firePointUp));
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
                        StartCoroutine(SplitShoot(firePointDown));
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
        else
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                isShootingUp = false;
                isShootingDown = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isShootingUp = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isShootingDown = false;
        }

    }

    private IEnumerator SplitShoot(Transform shootP)
    {
        // Esperar el tiempo de retraso antes de dividir el disparo
        yield return new WaitForSeconds(splitDelay);

        if (isShootingUp)
        {
            // Disparo hacia arriba
            GameObject bullet1 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 90f));
            Rigidbody2D bulletRB1 = bullet1.GetComponent<Rigidbody2D>();
            bulletRB1.velocity = bullet1.transform.right * bulletSpeed;

            GameObject bullet2 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 45f));
            Rigidbody2D bulletRB2 = bullet2.GetComponent<Rigidbody2D>();
            bulletRB2.velocity = bullet2.transform.right * bulletSpeed;

            GameObject bullet3 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 135f));
            Rigidbody2D bulletRB3 = bullet3.GetComponent<Rigidbody2D>();
            bulletRB3.velocity = bullet3.transform.right * bulletSpeed;
        }
        else if (isShootingDown)
        {

            GameObject bullet1 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 90f));
            Rigidbody2D bulletRB1 = bullet1.GetComponent<Rigidbody2D>();
            bulletRB1.velocity = bullet1.transform.right * -bulletSpeed;

            GameObject bullet2 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 45f));
            Rigidbody2D bulletRB2 = bullet2.GetComponent<Rigidbody2D>();
            bulletRB2.velocity = bullet2.transform.right * -bulletSpeed;

            GameObject bullet3 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 135f));
            Rigidbody2D bulletRB3 = bullet3.GetComponent<Rigidbody2D>();
            bulletRB3.velocity = bullet3.transform.right * -bulletSpeed;
            
        }
        else
        {
            // Disparo normal
            GameObject bullet1 = Instantiate(bulletPrefab, shootP.position, firePoint.rotation);
            Rigidbody2D bulletRB1 = bullet1.GetComponent<Rigidbody2D>();
            bulletRB1.velocity = bullet1.transform.right * bulletSpeed;

            Vector3 newAngle = firePoint.rotation.eulerAngles + new Vector3(0f, 0f, +15f);
            GameObject bullet2 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z));
            Rigidbody2D bulletRB2 = bullet2.GetComponent<Rigidbody2D>();
            bulletRB2.velocity = bullet2.transform.right * bulletSpeed;

            newAngle = firePoint.rotation.eulerAngles + new Vector3(0f, 0f, -15f);
            GameObject bullet3 = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z));// 
            Rigidbody2D bulletRB3 = bullet3.GetComponent<Rigidbody2D>();
            bulletRB3.velocity = bullet3.transform.right * bulletSpeed;
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
