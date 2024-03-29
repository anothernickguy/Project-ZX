using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePointUp;
    public Transform firePointDown;
    public float bulletSpeed = 10f;
    public int maxShots = 3;
    public float cooldownTime = 1f;
    public AudioClip shootSound;
    public AudioClip cooldownSound;

    private int shotsFired = 0;
    private bool isCoolingDown = false;
    private bool isShootingUp = false;
    private bool isShootingDown = false;
    private AudioSource shootAudioSource;
    private AudioSource cooldownAudioSource;

    private void Start()
    {
        shootAudioSource = gameObject.AddComponent<AudioSource>();
        cooldownAudioSource = gameObject.AddComponent<AudioSource>();
    }

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

    private void Shoot(Transform shootP)
    {
        if (isShootingUp)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, 90f));
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.right * bulletSpeed;
        }
        else if (isShootingDown)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootP.position, Quaternion.Euler(0f, 0f, -90f));
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = -bullet.transform.right * -bulletSpeed;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, shootP.position, firePoint.rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.right * bulletSpeed;
        }

        // Reproducir el sonido de disparo
        shootAudioSource.PlayOneShot(shootSound);
    }

    private void StartCooldown()
    {
        isCoolingDown = true;
        Invoke("ResetShotsFired", cooldownTime);

        // Reproducir el sonido de cooldown
        cooldownAudioSource.PlayOneShot(cooldownSound);
    }

    private void ResetShotsFired()
    {
        shotsFired = 0;
        isCoolingDown = false;
    }
}
