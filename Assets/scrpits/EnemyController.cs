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
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = rb.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float dist = rb.position.x - initialPosition.x;

        if (dist > 0 && isMovingRight && dist >= patrolDistance)
        {
            isMovingRight = !isMovingRight;
            FlipSprite();
        }
        if (dist < 0 && !isMovingRight && dist <= patrolDistance * -1)
        {
            isMovingRight = !isMovingRight;
            FlipSprite();
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

    private void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
