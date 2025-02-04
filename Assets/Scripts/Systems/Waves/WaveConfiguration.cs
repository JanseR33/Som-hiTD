using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;

// This class defines the configuration for spawning enemies at a spawn point
[System.Serializable]
public class SpawnPointConfig
{
    public string spawnpointID;     // ID of the scene spawn point
    public GameObject enemyPrefab; // Enemy prefab to spawn
    public int quantity;           // Number of enemies to spawn
    public float spawnInterval;    // Time between each spawn
}



// This attribute allows me to create WaveData assets from the Unity editor's asset creation menu
[CreateAssetMenu(fileName = "WaveConfiguration", menuName = "Wave")]
public class WaveConfiguration : ScriptableObject
{
    public List<SpawnPointConfig> spawnPointConfigs = new List<SpawnPointConfig>();
}