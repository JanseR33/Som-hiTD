using System.Collections.Generic;
using UnityEngine;

// This class defines the configuration for spawning enemies at a spawn point
[System.Serializable]
public class SpawnPointConfig
{

    public Spawnpoint spawnPoint;  // Ensure this matches your Spawnpoint class
    public GameObject enemyPrefab;
    public int quantity;
    public float spawnInterval;
}




// This attribute allows you to create WaveData assets from the Unity editor's asset creation menu
[CreateAssetMenu(fileName = "WaveConfiguration", menuName = "TowerDefense/Wave")]
public class WaveConfiguration : ScriptableObject
{
    public List<SpawnPointConfig> spawnPointConfigs = new List<SpawnPointConfig>();
}
