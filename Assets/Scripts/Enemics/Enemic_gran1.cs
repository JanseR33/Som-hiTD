using System.Linq;
using UnityEngine;

public class SOMI_enemy : MonoBehaviour, IDamageable
{
    public float speed = 3f;          // Speed of the enemy
    public float baseHealth = 50f;    // Base health of the enemy
    public float healthMultiplier = 1f; // Multiplier to scale health (e.g., set to 2x for a Big Enemy)
    private float health;             // Current health of the enemy
    private Transform[] waypoints;    // Array of waypoints the enemy will follow
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Set initial health based on the multiplier
        health = baseHealth * healthMultiplier;

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
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector2 direction = targetWaypoint.position - transform.position;
            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, step);

            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            ReachEnd();
        }
    }

    void ReachEnd()
    {
        Destroy(gameObject);
    }

        public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Enemy took {damage} damage, health remaining: {health}");
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
