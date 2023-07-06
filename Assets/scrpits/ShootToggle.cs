using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToggle : MonoBehaviour
{
    public PlayerShoot playerShootScript;
    public SecondShoot secondShootScript;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Alternar la activación de los scripts
            playerShootScript.enabled = !playerShootScript.enabled;
            secondShootScript.enabled = !secondShootScript.enabled;
        }
    }
}
