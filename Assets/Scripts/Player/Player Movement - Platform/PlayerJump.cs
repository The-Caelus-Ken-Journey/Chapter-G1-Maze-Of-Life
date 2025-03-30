using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool canTripleJump = false;
    [SerializeField] private float jumpForce = 5f;

    private int jumpCount = 0; // Track the number of jumps performed
    private Rigidbody playerRigidbody;
    private float groundCheckDistance = 1.1f;

    // Check if the player is grounded or not by raycasting down to the ground
    private bool isGrounded
    {
        get
        {
            return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        }
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            jumpCount = 0; // Reset jump count when grounded
        }

        HandleDoubleTripleJump();
    }

    private void HandleDoubleTripleJump()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string jumpType = "";

            if (canDoubleJump && jumpCount < 1)
            {
                jumpType = "Double Jump";
                PerformJump(jumpType);
            }
            else if (canTripleJump && jumpCount < 2)
            {
                jumpType = "Triple Jump";
                PerformJump(jumpType);
            }
            else if (isGrounded)
            {
                jumpType = "Normal Jump";
                PerformJump(jumpType);
            }
        }
    }

    private void PerformJump(string jumpType)
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount++;
    }
}