using System.Linq;
using UnityEngine;

public class SOMI_enemy : MonoBehaviour, IDamageable
{
    public float speed = 3f;
    public float baseHealth = 50f;
    public float healthMultiplier = 1f;
    private float health;
    private Transform[] waypoints;
    public int howmanyenemyspawn = 3;
    public GameObject enemiestospawn;
    public float spawnradius = 0f;
    private int currentWaypointIndex = 0;
    public int DamagePlayer = 20;
    public PlayerStats PlayerStats;

    void Start()
    {
        health = baseHealth * healthMultiplier;

        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("checkpoint");
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
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, step);

            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            BetaCuck();
        }
    }

    void BetaCuck()
    {
        Destroy(gameObject);
        PlayerStats.playerhit(DamagePlayer);
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
        Debug.Log($"SOMI_enemy at {transform.position} died. Spawning small enemies.");
        skibidi();
        Destroy(gameObject);
    }

    void skibidi()
    {
        for (int i = 0; i < howmanyenemyspawn; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnradius;
            GameObject spawnedEnemy = Instantiate(enemiestospawn, spawnPosition, Quaternion.identity);

            // Script that assigns each spawned enemie the waypoint it should follow
            IWaypointFollower waypointFollower = spawnedEnemy.GetComponent<IWaypointFollower>();
            if (waypointFollower != null)
            {
                waypointFollower.SetCurrentWaypointIndex(currentWaypointIndex);
            }
            else
            {
                Debug.LogWarning("No furula");
            }
        }
    }
}
