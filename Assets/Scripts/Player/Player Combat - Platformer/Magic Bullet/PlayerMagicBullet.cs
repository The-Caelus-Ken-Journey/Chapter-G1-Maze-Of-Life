using UnityEngine;

public class PlayerMagicBullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f; // Time before the bullet is destroyed

    private void Start()
    {
        // Destroy the bullet after a certain time
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if (enemy != null)
        {
            // Assuming the enemies have a method to take damage
            enemy.TakeDamage(10f); // Adjust damage as needed
            Debug.Log("Dealt damage to " + enemy.name + " with magic bullet.");

            Destroy(gameObject);
        }

        // Destroy the bullet on hit
        Destroy(gameObject);
    }
}