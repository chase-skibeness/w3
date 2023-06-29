using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float moveSpeed;
    private float currentHealth;

    public float rotationSpeed = 180f;

    private Vector2 currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Generate a random initial direction for the enemy
        currentDirection = Random.insideUnitCircle.normalized;
        moveSpeed = Random.Range(1f, 4f);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Move()
    {
        // Move the enemy in its current direction
        transform.Translate(currentDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Rotate()
    {
        // Calculate the target rotation angle
        float targetAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90f;

        // Smoothly rotate the enemy towards the target angle
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy collides with a boundary or obstacle, change its direction
        if (collision.CompareTag("Boundary") || collision.CompareTag("Obstacle"))
        {
            currentDirection = Random.insideUnitCircle.normalized;
        }
    }
}
