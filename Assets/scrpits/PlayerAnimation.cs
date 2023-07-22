using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Animación de disparo
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("shoot", true);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("shoot", false);
        }

        // Animación de subir
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("up", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("up", false);
        }

        // Animación de bajar
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("down", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("down", false);
        }
    }
}
