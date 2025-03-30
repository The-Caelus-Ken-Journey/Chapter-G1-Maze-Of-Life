using UnityEngine;

public class PlayerMagicBlast : MonoBehaviour
{
    public float radius = 5f; // Radius of the AoE effect
    public float damage = 10f; // Damage dealt to enemies

    void Start()
    {
        // Find all colliders within the radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            // Assuming the enemies have a method to take damage
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.TakeDamage(damage);
                Debug.Log("Dealt " + damage + " damage to " + enemy.name);
            }
        }

        // Optionally, destroy the AoE blast object after a short duration
        Destroy(gameObject, 1f); // Destroy after 2 seconds
    }
}