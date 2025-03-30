using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    #region Enemy Variables
    protected float enemyHealth;
    protected float enemyDamage;
    protected float enemyDefensePower;
    protected float enemyMovementSpeed;
    protected float enemyAttackRange; // Range of the enemy's attack
    protected float enemyAttackCooldown; // Time between attacks
    protected Rigidbody enemyRigidbody; // Reference to the enemy's Rigidbody component
    protected bool isFacingRight = true; // Track if the enemy is facing right
    #endregion

    #region Unity Methods
    protected virtual void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        // Initialize enemy stats here if needed
    }

    protected virtual void Update()
    {
        // Handle enemy behavior here (e.g., movement, attack, etc.)
        Move();
        Attack();
    }
    #endregion

    #region Enemy Methods
    public virtual void TakeDamage(float damage)
    {
        enemyHealth -= (damage - enemyDefensePower); // Calculate effective damage after defense
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Handle enemy death (e.g., play animation, drop loot, etc.)
        Destroy(gameObject); // Destroy the enemy object
    }

    public abstract void Attack();
    public abstract void Move();
    #endregion

}
