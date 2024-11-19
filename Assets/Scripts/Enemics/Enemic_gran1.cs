using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class big_enemy : MonoBehaviour
{
    public float speed = 3f;          // Speed of the enemy
    public float health = 50f;       // Health of the enemy
    private Transform[] waypoints;    // Array of waypoints the enemy will follow
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Find all objects with the tag "Checkpoint"
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("checkpoint");

        // Log the number of checkpoint objects found
        Debug.Log("Checkpoint objects found: " + checkpointObjects.Length);

        // If no checkpoints are found, log an error and return
        if (checkpointObjects.Length == 0)
        {
            Debug.LogError("No checkpoints found! Please ensure they have the correct tag.");
            return;
        }

        // Order the checkpoints by name and assign to waypoints
        waypoints = checkpointObjects
            .OrderBy(checkpoint => checkpoint.name)
            .Select(checkpoint => checkpoint.transform)
            .ToArray();

        // Log waypoints for debugging
        Debug.Log("Waypoints found: " + waypoints.Length);
        for (int i = 0; i < waypoints.Length; i++)
        {
            Debug.Log("Waypoint " + i + ": " + waypoints[i].name);
        }
    }

    void Update()
    {
        // Call the method to move along the path
        MoveAlongPath();
    }

    // Method to move the enemy along the waypoints
    void MoveAlongPath()
    {
        // Check if waypoints are valid and the index is within range
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is null or empty!");
            return;
        }

        // If the current waypoint index is valid
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector2 direction = (targetWaypoint.position - transform.position).normalized;
            float step = speed * Time.deltaTime;

            // Move towards the target waypoint
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, step);

            // Smoothly rotate the enemy towards the direction of movement
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }

            // Check if the enemy is close enough to the waypoint
            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                Debug.Log("Reached waypoint: " + targetWaypoint.name);
                currentWaypointIndex++;
            }
        }
        else
        {
            // Called when the enemy reaches the end of the path
            ReachEnd();
        }
    }

    // Called when the enemy reaches the end of the path
    void ReachEnd()
    {
        // In development still
        Destroy(gameObject);
    }

    // Method to take damage
    public void takeDamageMethod(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            death();
        }
    }

    // Destroy the enemy when health reaches 0
    void death() => Destroy(gameObject);
}

