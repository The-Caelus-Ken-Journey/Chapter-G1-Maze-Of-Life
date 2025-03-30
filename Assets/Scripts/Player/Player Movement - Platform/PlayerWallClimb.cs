using UnityEngine;

public class PlayerWallClimb : MonoBehaviour
{
    [Header("Wall Climb Settings")]
    [SerializeField] private float wallClimbSpeed = 3f;
    [SerializeField] private float maxClimbTime = 2f;
    [SerializeField] private float wallStickTime = 0.2f;

    [Header("Charged Climb Settings")]
    [SerializeField] private float maxChargeTime = 1f;
    [SerializeField] private float chargedClimbBoost = 6f;

    [Header("Wall Jump Settings")]
    [SerializeField] private float normalJumpForce = 5f;
    [SerializeField] private float chargedJumpForce = 10f;
    [SerializeField] private float wallJumpCooldown = 0.5f;

    [Header("Detection")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 0.5f;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody rb;
    private bool isTouchingWall;
    private bool isWallClimbing;
    private bool isChargingClimb;
    private float climbTimer;
    private float chargeTimer;
    private bool canWallJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        climbTimer = maxClimbTime; // Initialize climb timer
    }

    private void Update()
    {
        CheckWallCollision();
        HandleWallClimb();
        HandleWallJump();
    }

    private void CheckWallCollision()
    {
        isTouchingWall = Physics.CheckSphere(wallCheck.position, wallCheckRadius, wallLayer);

        if (!isTouchingWall)
        {
            isWallClimbing = false;
            isChargingClimb = false;
            climbTimer = maxClimbTime; // Reset climb timer when not touching wall
        }
    }

    private void HandleWallClimb()
    {
        if (isTouchingWall && Input.GetKey(KeyCode.W))
        {
            if (climbTimer > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, wallClimbSpeed, rb.linearVelocity.z);
                climbTimer -= Time.deltaTime;
                isWallClimbing = true;
            }
        }
        else if (Input.GetKey(KeyCode.LeftControl) && isTouchingWall)
        {
            isChargingClimb = true;
            chargeTimer += Time.deltaTime;
            chargeTimer = Mathf.Clamp(chargeTimer, 0, maxChargeTime);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && isChargingClimb)
        {
            float climbBoost = Mathf.Lerp(0, chargedClimbBoost, chargeTimer / maxChargeTime);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, wallClimbSpeed + climbBoost, rb.linearVelocity.z);
            isChargingClimb = false;
            chargeTimer = 0;
        }
    }

    private void HandleWallJump()
    {
        if (isTouchingWall && canWallJump && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpForce = normalJumpForce;

            if (isChargingClimb)
            {
                jumpForce = chargedJumpForce;
                isChargingClimb = false;
                chargeTimer = 0;
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            canWallJump = false;
            Invoke(nameof(ResetWallJump), wallJumpCooldown);
        }
    }

    private void ResetWallJump()
    {
        canWallJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
}
