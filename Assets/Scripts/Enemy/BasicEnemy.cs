using UnityEngine;

public class BasicEnemy : Enemy
{
    public enum AttackType { Melee, Ranged }
    [Header("Basic Enemy Settings")]
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyCustomHealth = 100;
    [SerializeField] private int enemyCustomDamage = 10; // Adjusted to a reasonable value
    [SerializeField] private int enemyCustomDefensePower = 5;
    [SerializeField] private float enemyCustomMovementSpeed = 2f; // Set a default movement speed
    [SerializeField] private float attackRange = 1.5f; // Attack range for melee
    [SerializeField] private float rangedAttackRange = 5f; // Attack range for ranged
    [SerializeField] private AttackType attackType = AttackType.Melee; // Default attack type
    [SerializeField] private GameObject rangedAttackPrefab; // Prefab for ranged attack
    [SerializeField] private Transform rangedAttackSpawnPoint; // Where the ranged attack will spawn

    // Patrol variables
    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 5f; // Radius for patrol
    private float patrolStartX; // Starting x position for patrol
    private float patrolEndX; // Ending x position for patrol
    private bool movingRight = true; // Direction of patrol

    protected override void Start()
    {
        base.Start();
        enemyHealth = enemyCustomHealth;
        enemyDamage = enemyCustomDamage;
        enemyDefensePower = enemyCustomDefensePower;
        enemyMovementSpeed = enemyCustomMovementSpeed;

        // Set patrol boundaries based on the current position
        patrolStartX = transform.position.x - patrolRadius / 2;
        patrolEndX = transform.position.x + patrolRadius / 2;
    }

    public override void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (attackType == AttackType.Melee && distanceToPlayer <= attackRange)
            {
                // Deal damage to the player
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage((int)enemyDamage);
                    Debug.Log($"{enemyName} Attacked Player with melee damage: {enemyDamage}");
                }
            }
            else if (attackType == AttackType.Ranged && distanceToPlayer <= rangedAttackRange)
            {
                // Instantiate a ranged attack
                if (rangedAttackPrefab != null && rangedAttackSpawnPoint != null)
                {
                    Instantiate(rangedAttackPrefab, rangedAttackSpawnPoint.position, Quaternion.identity);
                    Debug.Log($"{enemyName} Attacked Player with ranged damage: {enemyDamage}");
                }
            }
            else
            {
                Debug.Log("Player is out of range for attack.");
            }
        }
    }

    public override void Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player is within the attack range
            if (distanceToPlayer <= rangedAttackRange + 1f) // Adjust the range as needed
            {
                // Move towards the player
                Vector3 direction = (player.transform.position - transform.position).normalized;
                transform.position += direction * enemyMovementSpeed * Time.deltaTime;
            }
            else
            {
                // Patrol behavior
                Patrol();
            }
        }
        else
        {
            // Patrol behavior if no player is found
            Patrol();
        }

        // Log the movement speed for debugging
        Debug.Log($"{enemyName} Moving with speed: {enemyMovementSpeed}");
    }

    private void Patrol()
    {
        // Move back and forth within the defined patrol range
        if (movingRight)
        {
            transform.position += Vector3.right * enemyMovementSpeed * Time.deltaTime;

            // Check if we've reached the end of the patrol range
            if (transform.position.x >= patrolEndX)
            {
                movingRight = false; // Change direction
            }
        }
        else
        {
            transform.position += Vector3.left * enemyMovementSpeed * Time.deltaTime;

            // Check if we've reached the start of the patrol range
            if (transform.position.x <= patrolStartX)
            {
                movingRight = true; // Change direction
            }
        }

        Debug.Log($"{enemyName} is patrolling between {patrolStartX} and {patrolEndX}");
    }

    protected override void Die()
    {
        Debug.Log($"{enemyName} has died");
        base.Die();
    }
}