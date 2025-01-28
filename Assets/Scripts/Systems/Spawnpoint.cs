using System.Collections;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private string spawnpointID;
    [SerializeField] private GameObject defaultEnemyPrefab;
    private float defaultSpawnInterval; 
    private int defaultQuantity; 

    private bool isSpawning = false;
    public string SpawnpointID => spawnpointID; // Public accessor

    public IEnumerator SpawnEnemies(GameObject enemyPrefab, int quantity, float spawnInterval)
    {
        isSpawning = true;

        for (int i = 0; i < quantity; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next enemy
        }

        isSpawning = false;
        Debug.Log($"Spawn of {gameObject.name} finished");
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }
}