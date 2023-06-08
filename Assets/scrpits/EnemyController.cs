using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float patrolDistance = 5f;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private bool isMovingRight = true;
    private Vector2 initialPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = rb.position;
    }

    private void Update()
    {
        float dist = rb.position.x - initialPosition.x;
        // Debug.Log(dist);

        
        if(dist > 0 && isMovingRight && dist >= patrolDistance)
        {
            isMovingRight = !isMovingRight;
        }
        if (dist < 0 && !isMovingRight && dist <= patrolDistance*-1)
        {
            isMovingRight = !isMovingRight;
        }

        if (isMovingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }
}

