using System.Collections;
using UnityEngine;
public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private GameObject defaultEnemyPrefab; // Default enemy to spawn
    private float defaultSpawnInterval; // Default time between spawns
    private int defaultQuantity; // Default number of enemies to spawn

    private bool isSpawning = false;

    public IEnumerator SpawnEnemies(GameObject enemyPrefab, int quantity, float spawnInterval)
    {
        isSpawning = true;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next enemy
        }

        isSpawning = false;
        {
            yield return null;
            Debug.Log($"Spawn of {gameObject.name} finished");
        }; // Mark spawning as finished
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }

    // Public read-only properties
    public int DefaultQuantity => defaultQuantity;
    public float DefaultSpawnInterval => defaultSpawnInterval;
}