using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject defaultEnemyPrefab; // Default enemy to spawn
    public float defaultSpawnInterval; // Default time between spawns
    public int defaultQuantity; // Default number of enemies to spawn

    private bool isSpawning = false;

    public IEnumerator SpawnEnemies(GameObject enemyPrefab, int quantity, float spawnInterval)
    {
        isSpawning = true;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next enemy
        }

        isSpawning = false; // Mark spawning as finished
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }
}
