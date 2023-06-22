using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int pointsToAdd = 10; // Puntos a sumar al recolectar el objeto
    public Score scoreScript; // Referencia al script de Score

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Incrementar el puntaje al recolectar el objeto
            scoreScript.IncreaseScore();

            // Desactivar el objeto recolectable
            gameObject.SetActive(false);
        }
    }
}
