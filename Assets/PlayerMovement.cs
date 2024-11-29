using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float wiggleAmount = 10f;
    public float wiggleSpeed = 10f;
    private Vector2 targetPosition;
    private bool isMoving;

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world position
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        // Move player towards the target position
        if (isMoving)
        {
            // Calculate direction and move the player
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            // Add wiggle effect
            float angle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if ((Vector2)transform.position == targetPosition)
            {
                isMoving = false;
                transform.rotation = Quaternion.identity; // Reset rotation
            }
        }
    }
}
