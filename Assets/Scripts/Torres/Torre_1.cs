using UnityEngine;

public class Tower : MonoBehaviour
{
    // Summary: Controls tower behavior, shooting at the closest enemy within range at a specified fire rate.

    public float range = 10f;            // The range of the tower
    public float fireRate = 1f;          // Time between shots (seconds)
    public GameObject projectilePrefab;   // The projectile prefab
    private Transform towerTransform;     // Cached tower transform
    private float fireCooldown = 0f;      // Time until the next shot

    
    void Start()
    {
        towerTransform = transform;
    }

    void Update()
    {
        // Decrease fireCooldown based on time passed
        fireCooldown -= Time.deltaTime;

        // Check for enemies in range
        if (fireCooldown <= 0)
        {
            GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemyGO in enemyGameObjects)
            {
                float distance = Vector2.Distance(towerTransform.position, enemyGO.transform.position);
                if (distance < closestDistance && distance <= range)
                {
                    closestDistance = distance;
                    closestEnemy = enemyGO;
                }
            }
        }

      //    // If an enemy is found within range, shoot at it
      //    if (closestEnemy!= null)
      //    {
      //        Shoot(closestEnemy.transform);
      //        fireCooldown = fireRate; // Reset cooldown based on fire rate
      //    }
      //}
    }

    // Method to shoot a projectile at the target
  //void Shoot(Transform target)
  //{
  //    GameObject projectile = Instantiate(projectilePrefab, towerTransform.position, Quaternion.identity);
  //    Projectile proj = projectile.GetComponent<Projectile>();
  //    
  //    if (proj == null)
  //    {
  //        Debug.LogError("Projectile prefab is missing Projectile component.", projectile);
  //        return;
  //    }
  //    
  //    proj.SetTarget(target);
  //}
}