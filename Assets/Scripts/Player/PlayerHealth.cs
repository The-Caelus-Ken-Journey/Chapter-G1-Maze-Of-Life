using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Maximum health of the player
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to decrease the player's health
    public void TakeDamage(int damage)
    {
        maxHealth -= damage; // Decrease health by damage amount
        if (maxHealth <= 0)
        {
            Die(); // Call the Die function if health is zero or less
        }
    }
    // Function to handle player death
    private void Die()
    {
        // Add code to handle player death, such as disabling controls or triggering death animations
        Debug.Log("Player died!");
    }
}
