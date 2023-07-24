using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToggle : MonoBehaviour
{
    public PlayerShoot playerShootScript;
    public SecondShoot secondShootScript;
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Alternar la activación de los scripts
            playerShootScript.enabled = !playerShootScript.enabled;
            secondShootScript.enabled = !secondShootScript.enabled;

            // Activar y desactivar los Game Objects
            objectToActivate.SetActive(!objectToActivate.activeSelf);
            objectToDeactivate.SetActive(!objectToDeactivate.activeSelf);
        }
    }
}
