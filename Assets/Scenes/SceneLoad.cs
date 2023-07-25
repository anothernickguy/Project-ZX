using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a cargar

    private void Update()
    {
        // Cargar la escena al presionar la tecla "Enter"
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        // Cargar la escena por su nombre
        SceneManager.LoadScene(sceneName);
    }
}
