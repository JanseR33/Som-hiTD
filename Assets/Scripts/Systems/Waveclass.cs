[System.Serializable]
public class Wave
{
    public string waveName; // Optional: Name for debugging
    public EnemyType[] enemyTypes; // Array of enemy types in this wave
    public int[] enemyCounts; // How many of each type
    public float spawnInterval; // Time between spawns
}
