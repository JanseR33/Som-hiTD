using UnityEngine;
public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;
    private Transform target;
    private float proximityThreshold = 0.1f; // Distance to trigger hit detection

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            // Move towards the target
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            CheckProximity();
        }
        
        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    void CheckProximity()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) <= proximityThreshold)
        {
            // Check if the target implements IDamageable
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                // Deal damage using the interface
                damageable.TakeDamage(damage);
                Destroy(gameObject); // Destroy the projectile after hitting
            }
            else
            {
                Debug.LogWarning($"Target {target.name} does not implement IDamageable.");
            }
        }
    }
}
