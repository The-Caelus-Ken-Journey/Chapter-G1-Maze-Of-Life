using UnityEngine;

public class PlayerBasicCombat : MonoBehaviour
{
    [SerializeField] private GameObject magicBulletPrefab;
    [SerializeField] private GameObject aoeBlastPrefab;
    [SerializeField] private Transform rightShootingPoint;
    [SerializeField] private Transform aoeBlastPoint;
    [SerializeField] private float bulletSpeed = 20f; // Adjusted speed for bullets
    [SerializeField] private float chargeTime = 3f;
    private float chargeStartTime;
    private bool isCharging = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) // Left mouse button
        {
            StartCharging();
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            ReleaseCharge();
        }
    }

    void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
    }

    void ReleaseCharge()
    {
        if (isCharging)
        {
            float chargeDuration = Time.time - chargeStartTime; // Calculate how long the button was held
            isCharging = false; // Reset charging state

            if (chargeDuration >= chargeTime)
            {
                // Fully charged attack
                FireAoEBlast();
            }
            else
            {
                // Normal attack
                FireMagicBullet();
            }
        }
    }

    void FireMagicBullet()
    {
        // Determine the shooting point based on the player's facing direction
        Transform shootingPoint = rightShootingPoint;
        GameObject bullet = Instantiate(magicBulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Set the bullet's velocity based on the facing direction
        float directionMultiplier = playerMovement.isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector3(directionMultiplier * bulletSpeed, 0, 0); // Use rb.velocity instead of rb.linearVelocity
        Debug.Log("Fired normal magic bullet");
    }

    void FireAoEBlast()
    {
        Transform blastingPoint = aoeBlastPoint;
        GameObject aoeBlast = Instantiate(aoeBlastPrefab, aoeBlastPoint.position, Quaternion.identity);
        Rigidbody rb = aoeBlast.GetComponent<Rigidbody>();

        Debug.Log("Fired charged attack");
    }
}