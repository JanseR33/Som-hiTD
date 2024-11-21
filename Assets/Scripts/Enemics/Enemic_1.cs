using System.Linq;
using UnityEngine;

public class enemy : MonoBehaviour, IDamageable
{
    public float speed = 3f;          // Speed of the enemy
    public float health = 50f;        // Health of the enemy
    private Transform[] waypoints;    // Array of waypoints the enemy will follow
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Find all objects with the tag "Checkpoint"
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("checkpoint");

        // Order the checkpoints by name and assign to waypoints
        waypoints = checkpointObjects
            .OrderBy(checkpoint => checkpoint.name)
            .Select(checkpoint => checkpoint.transform)
            .ToArray();
    }

    void Update()


    
    {
        // Call the method to move along the path
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        // If the current waypoint index is valid
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector2 direction = targetWaypoint.position - transform.position;
            float step = speed * Time.deltaTime;

            // Move towards the target waypoint
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, step);

            // Check if the enemy is close enough to the waypoint
            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            // Called when the enemy reaches the end of the path
            ReachEnd();
        }
    }

    void ReachEnd()
    {
        // Behavior when reaching the end of the path (in development)
        Destroy(gameObject);
    }

    // Method to set the current waypoint index
    public void SetCurrentWaypointIndex(int index)
    {
        currentWaypointIndex = index;
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}